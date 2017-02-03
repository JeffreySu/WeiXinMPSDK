//index.js
//获取应用实例
var app = getApp()
Page({
  data: {
    motto: 'Senparc.Weixin SDK Demo',
    userInfo: {}
  },
  //事件处理函数
  bindViewTap: function() {
    wx.navigateTo({
      url: '../logs/logs'
    })
  },

  bindWebsocketTap: function(){
    wx.navigateTo({
      url: '../websocket/websocket'
    })
  },

  //处理wx.request请求
  doRequest:function(){
    var that = this;
    wx.request({
      url: 'https://sdk.weixin.senparc.com/WxOpen/RequestData',
      data: { nickName : that.data.userInfo.nickName},
      method: 'POST', // OPTIONS, GET, HEAD, POST, PUT, DELETE, TRACE, CONNECT
      // header: {}, // 设置请求的 header
      success: function(res){
        // success
        var json = res.data;
        //模组对话框
        wx.showModal({
          title: '收到消息',
          content: json.msg,
          showCancel:false,
          success: function(res) {
            if (res.confirm) {
              console.log('用户点击确定')
            }
          }
        });
      },
      fail: function() {
        // fail
      },
      complete: function() {
        // complete
      }
    })
  },
  //测试模板消息提交form
  formTemplateMessageSubmit:function(e)
  {
       var submitData = JSON.stringify({
          sessionId:wx.getStorageSync("sessionId"),
          formId:e.detail.formId
        });

        wx.request({
          url: 'https://sdk.weixin.senparc.com/WxOpen/TemplateTest',
          data: submitData,
          method: 'POST', 
          success: function(res){
            // success
            var json = res.data;
            console.log(res.data);
            //模组对话框
            wx.showModal({
              title: '已尝试发送模板消息',
              content: json.msg,
              showCancel:false
            });
          }
        })
  },

  onLoad: function () {
    console.log('onLoad')
    var that = this
    //调用应用实例的方法获取全局数据
    app.getUserInfo(function(userInfo){
      //更新数据
      that.setData({
        userInfo:userInfo
      })
      //console.log(userInfo);
    })

    var interval = setInterval(function() {
        that.setData({time:new Date().toLocaleTimeString()});
    },1000);
  }
})
