# 添加新的RequestMsgType步骤

MP项目为例：

1. Enums.cs，在 RequestMsgType 枚举（目前已统一到 NeuChar 中，Event 的具体类型仍然保留在项目中）中添加对应类型（如File）

2. Entities/Request 目录下添加对应类，如：RequestMessageFile.cs

3. RequestMessageFactory.cs，GetRequestEntity() 方法中添加对应 RequestMsgType 的 case 判断，如：

``` C#
    case RequestMsgType.File:
        requestMessage = new RequestMessageFile();
        break;
```

4. MessageHandler.cs 或 MessageHandler.Event.cs 或 MessageHandler.Message.cs 中，添加对应 OnXX()事件，如：

``` C#
        /// <summary>
        /// 文件请求
        /// </summary>
        public virtual IResponseMessageBase OnFileRequest(RequestMessageFile requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
```

5. MessageHandler.cs 中的 Execute() 方法，添加对应 RequestMessage.MsgType 判断，如：

``` C#
        case RequestMsgType.File:
            ResponseMessage = OnFileRequest(RequestMessage as RequestMessageFile);
            break;

```

6.  在异步 MessageHandler：MessageHandler.Async.Message.cs 中添加对应 OnXXAsync() 异步事件，如：

``` C#
        /// <summary>
        /// 【异步方法】文件类型请求
        /// </summary>
        public virtual async Task<IResponseMessageBase> OnFileRequestAsync(RequestMessageFile requestMessage)
        {
            return await DefaultAsyncMethod(requestMessage, () => OnFileRequest(requestMessage));
        }
```


7. 在异步 MessageHandler：MessageHandlerAsync.cs 中添加对应 RequestMessage.MsgType 的判断，如：

``` C#
        case RequestMsgType.File:
            ResponseMessage = await OnFileRequestAsync(RequestMessage as RequestMessageFile);
            break;

```
