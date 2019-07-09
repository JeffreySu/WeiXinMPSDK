var signalR = require("../../utils/signalr.1.0.js")

var connection;
var app = getApp()
var socketOpen = false;//WebSocket 打开状态
Page({
  data: {
    messageTip: '',
    messageTextArr:[],
    messageContent:'TEST',
    userinfo:{}
  },
  //sendMessage
  formSubmit: function(e) {
    var that = this;
    console.log('formSubmit',e);
    var msg =  e.detail.value.messageContent;//获得输入文字
    console.log('send message:' +msg );
     if (socketOpen) {

       //如果使用Senparc.WebSocket，必须严格按照以下data数据字段发送（只能多不能少）
       var submitData = JSON.stringify({
          Message:msg,//必填
          SessionId:wx.getStorageSync("sessionId"),//选填，不需要可输入''
          FormId:e.detail.formId//选填formId用于发送模板消息，不需要可输入''
        });

      wx.sendSocketMessage({
        data:submitData
      });
      that.setData({
        messageContent:''
      })
    } else {
      that.setData({
        messageTip:'WebSocket 链接失败，请重新连接！'
      })
    }
  },
  onLoad: function () {
    console.log('onLoad')
    var that = this

    //连接 Websocket
    wx.connectSocket({
      // url: wx.getStorageSync('wssDomainName') + '/SenparcWebSocket',
      url: wx.getStorageSync('wssDomainName') + '/SenparcHub',
      header:{ 
        'content-type': 'application/json'
      },
      method:"GET"
    });


    //WebSocket 连接成功
    wx.onSocketOpen(function(res) {
      console.log('WebSocket 连接成功！')
      socketOpen = true;
      that.setData({
        messageTip:'WebSocket 连接成功！'
      })
        })
    //收到 WebSocket 推送消息
    wx.onSocketMessage(function(res) {
      console.log('收到服务器内容：' + res.data)
      var jsonResult = JSON.parse(res.data);
      var currentIndex= that.data.messageTextArr.length+1;
      var newArr = that.data.messageTextArr;
      newArr.unshift(
        {
          index:currentIndex,
          content:jsonResult.content,
          time:jsonResult.time
        });
        console.log(that);
      that.setData({
        messageTextArr:newArr
      });
    })
    //WebSocket 已关闭
    wx.onSocketClose(function(res) {
      console.log('WebSocket 已关闭！')
      socketOpen = false;
    })
    //WebSocket 打开失败
    wx.onSocketError(function(res){
      console.log('WebSocket连接打开失败，请检查！')
    })
  }
})
