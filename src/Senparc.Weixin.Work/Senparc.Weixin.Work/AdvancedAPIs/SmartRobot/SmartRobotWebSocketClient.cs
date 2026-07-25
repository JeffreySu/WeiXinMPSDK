/*----------------------------------------------------------------
    Copyright (C) 2026 Senparc

    文件名：SmartRobotWebSocketClient.cs
    文件功能描述：SmartRobotWebSocketClient 相关功能


    创建标识：Senparc - 20260723

    修改标识：Senparc - 20260724
    修改描述：v3.32.1 补齐企业微信通讯录、安全、智能机器人、微信客服和获客助手接口

----------------------------------------------------------------*/

using Senparc.CO2NET.Extensions;
using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.SmartRobot
{
    /// <summary>
    /// 企业微信智能机器人长连接客户端。负责订阅、心跳、分片接收、断线重连和命令发送；
    /// 业务方可在 <see cref="MessageReceived"/> 中按 cmd 解析回调。
    /// </summary>
    public sealed class SmartRobotWebSocketClient : IDisposable, IAsyncDisposable
    {
        /// <summary>企业微信智能机器人长连接默认地址。</summary>
        public const string DefaultEndpoint = "wss://openws.work.weixin.qq.com";

        private readonly Uri _endpoint;
        private readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);
        private ClientWebSocket _socket;
        private string _botId;
        private string _secret;
        private bool _disposed;

        /// <summary>收到完整文本消息时触发；参数为服务端原始 JSON。</summary>
        public event Func<string, Task> MessageReceived;

        /// <summary>服务端关闭连接或连接异常断开时触发。</summary>
        public event Action<WebSocketCloseStatus?, string> ConnectionClosed;

        /// <summary>当前 WebSocket 连接状态。</summary>
        public WebSocketState State => _socket?.State ?? WebSocketState.None;

        /// <summary>
        /// 创建企业微信智能机器人长连接客户端。
        /// </summary>
        /// <param name="endpoint">WebSocket 服务地址，默认使用企业微信官方地址。</param>
        public SmartRobotWebSocketClient(string endpoint = DefaultEndpoint)
        {
            _endpoint = new Uri(endpoint);
            _socket = CreateSocket();
        }

        /// <summary>
        /// 异步连接智能机器人 WebSocket 并订阅事件。
        /// </summary>
        /// <param name="botId">智能机器人 ID。</param>
        /// <param name="secret">智能机器人 Secret。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>表示异步操作的任务。</returns>
        public async Task ConnectAndSubscribeAsync(string botId, string secret,
            CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            _botId = string.IsNullOrWhiteSpace(botId) ? throw new ArgumentException("BotID 不能为空。", nameof(botId)) : botId;
            _secret = string.IsNullOrWhiteSpace(secret) ? throw new ArgumentException("Secret 不能为空。", nameof(secret)) : secret;

            if (_socket.State != WebSocketState.None)
            {
                _socket.Dispose();
                _socket = CreateSocket();
            }

            await _socket.ConnectAsync(_endpoint, cancellationToken).ConfigureAwait(false);
            await SendCommandAsync("aibot_subscribe", CreateRequestId(), new
            {
                bot_id = _botId,
                secret = _secret
            }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>持续接收消息，直到取消、服务端关闭或网络断开。</summary>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>表示异步操作的任务。</returns>
        public async Task ReceiveLoopAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            var buffer = new byte[16 * 1024];
            using var message = new MemoryStream();

            while (!cancellationToken.IsCancellationRequested && _socket.State == WebSocketState.Open)
            {
                message.SetLength(0);
                WebSocketReceiveResult result;
                do
                {
                    result = await _socket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken)
                        .ConfigureAwait(false);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        ConnectionClosed?.Invoke(result.CloseStatus, result.CloseStatusDescription);
                        return;
                    }

                    message.Write(buffer, 0, result.Count);
                } while (!result.EndOfMessage);

                if (result.MessageType == WebSocketMessageType.Text && MessageReceived != null)
                {
                    var json = Encoding.UTF8.GetString(message.ToArray());
                    await MessageReceived.Invoke(json).ConfigureAwait(false);
                }
            }
        }

        /// <summary>持续连接并在异常断开后重新订阅。</summary>
        /// <param name="botId">智能机器人 ID。</param>
        /// <param name="secret">智能机器人 Secret。</param>
        /// <param name="reconnectDelay">连接断开后的重连间隔。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>表示异步操作的任务。</returns>
        public async Task RunWithReconnectAsync(string botId, string secret,
            TimeSpan? reconnectDelay = null, CancellationToken cancellationToken = default)
        {
            var delay = reconnectDelay ?? TimeSpan.FromSeconds(5);
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await ConnectAndSubscribeAsync(botId, secret, cancellationToken).ConfigureAwait(false);
                    await ReceiveLoopAsync(cancellationToken).ConfigureAwait(false);
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
                catch (WebSocketException)
                {
                    ConnectionClosed?.Invoke(_socket.CloseStatus, _socket.CloseStatusDescription);
                }

                await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 异步回复智能机器人欢迎语事件。
        /// </summary>
        /// <param name="requestId">智能机器人 WebSocket 请求 ID。</param>
        /// <param name="reply">智能机器人回复内容。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>表示异步操作的任务。</returns>
        public Task RespondWelcomeAsync(string requestId, SmartRobotReply reply,
            CancellationToken cancellationToken = default)
            => SendCommandAsync("aibot_respond_welcome_msg", requestId, reply, cancellationToken);

        /// <summary>
        /// 异步回复智能机器人 WebSocket 消息。
        /// </summary>
        /// <param name="requestId">智能机器人 WebSocket 请求 ID。</param>
        /// <param name="reply">智能机器人回复内容。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>表示异步操作的任务。</returns>
        public Task RespondAsync(string requestId, SmartRobotReply reply,
            CancellationToken cancellationToken = default)
            => SendCommandAsync("aibot_respond_msg", requestId, reply, cancellationToken);

        /// <summary>
        /// 异步更新智能机器人 WebSocket 回复。
        /// </summary>
        /// <param name="requestId">智能机器人 WebSocket 请求 ID。</param>
        /// <param name="reply">智能机器人回复内容。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>表示异步操作的任务。</returns>
        public Task UpdateResponseAsync(string requestId, SmartRobotReply reply,
            CancellationToken cancellationToken = default)
            => SendCommandAsync("aibot_respond_update_msg", requestId, reply, cancellationToken);

        /// <summary>
        /// 异步发送智能机器人主动消息。
        /// </summary>
        /// <param name="chatId">智能机器人会话 ID。</param>
        /// <param name="chatType">会话类型。</param>
        /// <param name="reply">智能机器人回复内容。</param>
        /// <param name="requestId">智能机器人 WebSocket 请求 ID。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>表示异步操作的任务。</returns>
        public Task SendMessageAsync(string chatId, int chatType, SmartRobotReply reply,
            string requestId = null, CancellationToken cancellationToken = default)
            => SendCommandAsync("aibot_send_msg", requestId ?? CreateRequestId(), new
            {
                chatid = chatId,
                chat_type = chatType,
                reply.msgtype,
                reply.text,
                reply.markdown,
                reply.stream,
                reply.template_card
            }, cancellationToken);

        /// <summary>
        /// 异步发送智能机器人 WebSocket 心跳。
        /// </summary>
        /// <param name="requestId">智能机器人 WebSocket 请求 ID。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>表示异步操作的任务。</returns>
        public Task SendPingAsync(string requestId = null, CancellationToken cancellationToken = default)
            => SendCommandAsync("ping", requestId ?? CreateRequestId(), null, cancellationToken);

        /// <summary>发送官方协议中的任意命令，可用于媒体分片上传等扩展命令。</summary>
        /// <param name="command">智能机器人 WebSocket 命令。</param>
        /// <param name="requestId">智能机器人 WebSocket 请求 ID。</param>
        /// <param name="body">命令正文；无正文的命令可传 <see langword="null"/>。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>表示异步操作的任务。</returns>
        public async Task SendCommandAsync(string command, string requestId, object body,
            CancellationToken cancellationToken = default)
        {
            if (_socket.State != WebSocketState.Open)
            {
                throw new InvalidOperationException("智能机器人长连接尚未建立。 ");
            }

            var envelope = new SmartRobotSocketEnvelope
            {
                cmd = command,
                headers = new SmartRobotSocketHeaders { req_id = requestId },
                body = body
            };
            var bytes = Encoding.UTF8.GetBytes(envelope.ToJson());

            await _sendLock.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                await _socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true,
                    cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                _sendLock.Release();
            }
        }

        /// <summary>
        /// 创建智能机器人 WebSocket 请求 ID。
        /// </summary>
        /// <returns>32 位无连字符 GUID 请求 ID。</returns>
        public static string CreateRequestId() => Guid.NewGuid().ToString("N");

        private static ClientWebSocket CreateSocket()
        {
            var socket = new ClientWebSocket();
            socket.Options.KeepAliveInterval = TimeSpan.FromSeconds(20);
            return socket;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(SmartRobotWebSocketClient));
            }
        }

        /// <summary>
        /// 释放 WebSocket 连接和发送锁。
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _socket.Dispose();
            _sendLock.Dispose();
        }

        /// <summary>
        /// 异步释放 WebSocket 连接和发送锁。
        /// </summary>
        /// <returns>表示释放操作的任务。</returns>
        public ValueTask DisposeAsync()
        {
            Dispose();
            return default;
        }
    }
}
