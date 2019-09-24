//index.js
//获取应用实例
var app = getApp()
Page({
  data: {
    motto: 'Senparc.Weixin SDK Demo v2019.8.19',
    userInfo: {},
    hasUserInfo: false,
    canIUse: wx.canIUse('button.open-type.getUserInfo')
  },
  //事件处理函数
  bindViewTap: function() {
    wx.navigateTo({
      url: '../logs/logs'
    })
  },

  bindWebsocketTap: function(){
    wx.navigateTo({
      url: '../websocket_signalr/websocket_signalr'// 此页面对应 .net core demo，如果为 .net framework，请使用'../websocket_signalr/websocket'
    })
  },

  //处理wx.request请求
  doRequest:function(){
    var that = this;
    wx.request({
      url: wx.getStorageSync('domainName') + '/WxOpen/RequestData',
      data: { nickName : that.data.userInfo.nickName},
      method: 'POST', // OPTIONS, GET, HEAD, POST, PUT, DELETE, TRACE, CONNECT
      header: { 'content-type':'application/x-www-form-urlencoded'}, 
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
       var submitData = {
          sessionId:wx.getStorageSync("sessionId"),
          formId:e.detail.formId
        };

        wx.request({
          url: wx.getStorageSync('domainName') + '/WxOpen/TemplateTest',
          data: submitData,
          method: 'POST', 
          header: { 'content-type': 'application/x-www-form-urlencoded' }, 
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
      header: { 'content-type': 'application/x-www-form-urlencoded' }, 
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
  wxPay: function(){
    wx.request({
      url: wx.getStorageSync('domainName') + '/WxOpen/GetPrepayid',//注意：必须使用https
      data: {
        sessionId: wx.getStorageSync('sessionId')
      },
      method: 'POST',
      header: { 'content-type': 'application/x-www-form-urlencoded' }, 
       success: function (res) {
        // success
        var json = res.data;
        console.log(res.data);

        if (json.success) {
          wx.showModal({
            title: '得到预支付id',
            content: 'package：' + json.package,
            showCancel: false
          });

          //开始发起微信支付
          wx.requestPayment(
            {
              'timeStamp': json.timeStamp,
              'nonceStr': json.nonceStr,
              'package': json.package,
              'signType': 'MD5',
              'paySign': json.paySign,
              'success': function (res) {
                wx.showModal({
                  title: '支付成功！',
                  content: '请在服务器后台的回调地址中进行支付成功确认，不能完全相信UI！',
                  showCancel: false
                });

                wx.request({
                url: wx.getStorageSync('domainName') + '/WxOpen/TemplateTest',
                data: {
                  sessionId: wx.getStorageSync('sessionId'),
                  formId: json.package
                },
                method: 'POST',
                  header: { 'content-type': 'application/x-www-form-urlencoded' }, 
                  success: function (templateMsgRes) {
                  if (templateMsgRes.data.success){
                    wx.showModal({
                      title: '模板消息发送成功！',
                      content: templateMsgRes.data.msg,
                      showCancel: false
                    });
                  }else{
                    wx.showModal({
                      title: '模板消息发送失败！',
                      content: templateMsgRes.data.msg,
                      showCancel: false
                    });
                  }
                }
              });

               },
              'fail': function (res) {
                console.log(res);
                wx.showModal({
                  title: '支付失败！',
                  content: '请检查日志！',
                  showCancel: false
                });
               },
              'complete': function (res) { 
                wx.showModal({
                  title: '支付流程结束！',
                  content: '执行 complete()，成功或失败都会执行！',
                  showCancel: false
                });
              }
            })
         
        }else{
          wx.showModal({
            title: '微信支付发生异常',
            content: json.msg,
            showCancel: false
          });
        }
      }
      });
  },
  getRunData:function(){
    wx.getWeRunData({
      success(res) {
        const encryptedData = res.encryptedData;

        wx.request({
          url: wx.getStorageSync('domainName') + '/WxOpen/DecryptRunData',
          data: {
            sessionId: wx.getStorageSync('sessionId'),
            encryptedData: encryptedData,
            iv: res.iv, 
          },
          method: 'POST',
          header: { 'content-type': 'application/x-www-form-urlencoded' },
          success: function (runDataRes) {
            if (runDataRes.data.success) {
              wx.showModal({
                title: '成功获得步数信息！',
                content: JSON.stringify(runDataRes.data.runData),
                showCancel: false
              });
            } else {
              wx.showModal({
                title: '获取步数信息失败！',
                content: runDataRes.data.msg,
                showCancel: false
              });
            }
          }
        });

      }
    })
  },
  getRunData:function(){
    wx.getWeRunData({
      success(res) {
        const encryptedData = res.encryptedData;

        wx.request({
          url: wx.getStorageSync('domainName') + '/WxOpen/DecryptRunData',
          data: {
            sessionId: wx.getStorageSync('sessionId'),
            encryptedData: encryptedData,
            iv: res.iv, 
          },
          method: 'POST',
          header: { 'content-type': 'application/x-www-form-urlencoded' },
          success: function (runDataRes) {
            if (runDataRes.data.success) {
              wx.showModal({
                title: '成功获得步数信息！',
                content: JSON.stringify(runDataRes.data.runData),
                showCancel: false
              });
            } else {
              wx.showModal({
                title: '获取步数信息失败！',
                content: runDataRes.data.msg,
                showCancel: false
              });
            }
          }
        });

      }
    })
  },
  openLivePusher:function(){
    wx.navigateTo({
      url: '../LivePusher/LivePusher'
    })
  },
  openQrCodePage:function(e){
    var codeType = e.target.dataset.codetype;
    wx.navigateTo({
      url: '../QrCode/QrCode?codeType=' + codeType
    })
  },
  onLoad: function () {
    console.log('onLoad')
    var that = this

    //判断是否登录
    if (app.globalData.userInfo) {
      this.setData({
        userInfo: app.globalData.userInfo,
        hasUserInfo: true
      })
    } else if (this.data.canIUse) {
      // 由于 getUserInfo 是网络请求，可能会在 Page.onLoad 之后才返回
      // 所以此处加入 callback 以防止这种情况
      app.userInfoReadyCallback = res => {
        this.setData({
          userInfo: res.userInfo,
          hasUserInfo: true
        })
      }
    } else {
      // 在没有 open-type=getUserInfo 版本的兼容处理
      wx.getUserInfo({
        success: res => {
          app.globalData.userInfo = res.userInfo
          this.setData({
            userInfo: res.userInfo,
            hasUserInfo: true
          })
        }
      })
    }

    // //调用应用实例的方法获取全局数据
    // app.getUserInfo(function(userInfo){
    //   //更新数据
    //   that.setData({
    //     userInfo:userInfo
    //   })
    //   //console.log(userInfo);
    // })

    var interval = setInterval(function() {
        that.setData({time:new Date().toLocaleTimeString()});
    },1000);
  },
  getUserInfo: function (e) {
    console.log(e)
    app.getUserInfo(e);
    app.globalData.userInfo = e.detail.userInfo
    this.setData({
      userInfo: e.detail.userInfo,
      hasUserInfo: true
    })
  }
})
