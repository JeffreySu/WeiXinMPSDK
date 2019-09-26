"use strict";
var hubPath = '/senparcHub';
if (window.location.host.indexOf('localhost') === 0) {
    hubPath = '/VirtualPath' + hubPath;
}
var connection = new signalR.HubConnectionBuilder().withUrl(hubPath).build();
console.log(signalR);

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveCustomMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendCustomMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});