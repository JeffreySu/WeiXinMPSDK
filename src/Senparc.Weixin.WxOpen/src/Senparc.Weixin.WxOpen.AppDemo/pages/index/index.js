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
      url: wx.getStorageSync('domainName') + '/WxOpen/RequestData',
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
          url: wx.getStorageSync('domainName') + '/WxOpen/TemplateTest',
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
  getPhoneNumber: function (e) {
    console.log(e.detail.errMsg)
    console.log(e.detail.iv)
    console.log(e.detail.encryptedData);

    //传输到后台解密
    wx.request({
      url: wx.getStorageSync('domainName') + '/WxOpen/DecryptPhoneNumber',
      data: { 
        sessionId: wx.getStorageSync('sessionId'), 
        iv: e.detail.iv, 
        encryptedData:e.detail.encryptedData 
        },
      method: 'POST',
      success: function (res) {
        // success
        var json = res.data;
        console.log(res.data);

        if(!json.success){

          wx.showModal({
            title: '解密过程发生异常',
            content: json.msg,
            showCancel: false
          });          
          return;
        }

        //模组对话框
        var phoneNumberData = json.phoneNumber;
        var msg = '手机号：' + phoneNumberData.phoneNumber+
          '\r\n手机号（不带区号）：' + phoneNumberData.purePhoneNumber+
          '\r\n区号（国别号）' + phoneNumberData.countryCode+
          '\r\n水印信息：' + JSON.stringify(phoneNumberData.watermark);

        wx.showModal({
          title: '收到解密后的手机号信息',
          content: msg,
          showCancel: false
        });
      }
    })

  } ,

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
