/* senparc.websocket.js - 20190716 - v1.0 */

var senparcWebSocketConnection;

function buildConnectionAndStart(hubUrl, signalR, onStart){
  senparcWebSocketConnection = new signalR.HubConnectionBuilder().withUrl(hubUrl).build();
  senparcWebSocketConnection.start(onStart()).then().catch(function (err) {
    return console.error(err.toString());
  });
  return senparcWebSocketConnection;
}

function sendMessage(text,sessionId,formId){
  //如果使用 Senparc.WebSocket，必须严格按照以下 submitData 数据字段发送（参数只能多不能少）
  var submitData = JSON.stringify({
    Message: text,//必填
    SessionId: sessionId,//选填，不需要可输入''
    FormId: formId//选填formId用于发送模板消息，不需要可输入''
  });

  //ReceiveMessage 为特殊约定的方法入口，请勿修改，如果使用其他名称，则会对应到 SenparcHub 下的其他自定义方法
  senparcWebSocketConnection.invoke("ReceiveMessage", submitData).catch(function (err) {
    return console.error(err.toString());
  });
}

function onReceiveMessage(receive){
  senparcWebSocketConnection.on("ReceiveMessage", function (res) {
    receive(res);
  });
}

module.exports = {
  buildConnectionAndStart: buildConnectionAndStart,
  sendMessage: sendMessage,
  onReceiveMessage: onReceiveMessage 
}