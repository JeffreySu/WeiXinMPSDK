var signalR = require("../../utils/signalr.1.0.js")
var senparcWebsocket = require("../../utils/senparc.websocket.js")

var connection;// Signalr 连接
var app = getApp()
var socketOpen = false;//WebSocket 打开状态
Page({
  data: {
    messageTip: '正在连接中，请等待...',
    messageTextArr: [],
    messageContent: 'TEST',
    userinfo: {}
  },
  //sendMessage
  formSubmit: function (e) {
    var that = this;
    console.log('formSubmit', e);
    if (socketOpen) {
      var text = e.detail.value.messageContent;//必填，获得输入文字
      var sessionId = wx.getStorageSync("sessionId");//选填，不需要可输入''
      var formId = e.detail.formId//选填formId用于发送模板消息，不需要可输入''
      senparcWebsocket.sendMessage(text,sessionId,formId);//发送 websocket 请求

      that.setData({
        messageContent: ''//清空文本框内容
      })
    } else {
      that.setData({
        messageTip: 'WebSocket 链接失败，请重新连接！'
      })
    }
  },
  onLoad: function () {
    console.log('onLoad')
  },
  onShow:function(){
    console.log('onShow');

    var that = this;
    var hubUrl = wx.getStorageSync('wssDomainName') + "/SenparcHub";//Hub Url
    var onStart = function () {
      console.log('ws started');
      socketOpen = true;
      that.setData({
        messageTip: 'WebSocket 连接成功！'
      })
    };
    connection = senparcWebsocket.buildConnectionAndStart(hubUrl, signalR, onStart);

    //定义收到消息后触发的事件
    var onReceive = function (res) {
      console.log('收到服务器内容：' + res)
      var jsonResult = JSON.parse(res);
      var currentIndex = that.data.messageTextArr.length + 1;
      var newArr = that.data.messageTextArr;
      newArr.unshift(
        {
          index: currentIndex,
          content: jsonResult.content,
          time: jsonResult.time
        });
      console.log(that);
      that.setData({
        messageTextArr: newArr
      });
    };
    senparcWebsocket.onReceiveMessage(onReceive);

    //WebSocket 连接成功
    wx.onSocketOpen(function (res) {
      console.log('WebSocket 连接成功！')
      
    })
    //WebSocket 已关闭
    wx.onSocketClose(function (res) {
      console.log('WebSocket 已关闭！')
      socketOpen = false;
    })
    //WebSocket 打开失败
    wx.onSocketError(function (res) {
      console.log('WebSocket连接打开失败，请检查！')
    })
  }
})
