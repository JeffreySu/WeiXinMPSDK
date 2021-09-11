"use strict";

document.getElementById("sendButton").disabled = true;

var senparcWebsocket = require(["js/senparc.websocket.2.0"], function (senparcWebsocket) {
    var hubPath = '/SenparcHub';

    var onStart = function () {
        var li = document.createElement("li");
        li.textContent = 'WebSocket 连接成功！';
        document.getElementById("messagesList").appendChild(li);
        document.getElementById("sendButton").disabled = false;
    };

    var connection = senparcWebsocket.buildConnectionAndStart(hubPath, signalR, onStart);
    console.log(signalR);

    var onReceiveMessage = function (jsonStr) {
        var jsonResult = JSON.parse(jsonStr);
        var message = jsonResult.content;
        var time = jsonResult.time;

        var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        var encodedMsg = /*user + " says " +*/ msg;
        var li = document.createElement("li");
        li.innerHTML = "<span class='chat-time'>" + time + "</span><br>" + encodedMsg + "<br><br>";
        var listNode = document.getElementById("messagesList");
        listNode.insertBefore(li, listNode.firstChild);
    };
    senparcWebsocket.onReceiveMessage(onReceiveMessage);

    document.getElementById("sendButton").addEventListener("click", function (event) {
        var user = document.getElementById("userInput").value;
        var message = document.getElementById("messageInput").value;
        senparcWebsocket.sendMessage(message, user, null);//发送 websocket 请求
        event.preventDefault();
    });


});