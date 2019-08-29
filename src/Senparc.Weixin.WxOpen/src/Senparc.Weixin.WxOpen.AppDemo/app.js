//app.js
App({
  onLaunch: function () {
    //调用API从本地缓存中获取数据
    var logs = wx.getStorageSync('logs') || []
    logs.unshift(Date.now())
    wx.setStorageSync('logs', logs)

    var isDebug = false;//调试状态使用本地服务器，非调试状态使用远程服务器
    if(!isDebug){
    //远程域名
      wx.setStorageSync('domainName', "https://sdk.weixin.senparc.com")
      wx.setStorageSync('wssDomainName', "wss://sdk.weixin.senparc.com")
    }
    else 
    {
    //本地测试域名
      // wx.setStorageSync('domainName', "http://localhost:58936")
      // wx.setStorageSync('wssDomainName', "ws://localhost:58936")

      wx.setStorageSync('domainName', "http://localhost:58936/VirtualPath")
      wx.setStorageSync('wssDomainName', "ws://localhost:58936/VirtualPath")
    }

    // 打开调试
    // wx.setEnableDebug({
    //   enableDebug: true
    // })
  },
  getUserInfo:function(cb){
    var that = this
    if(this.globalData.userInfo){
      typeof cb == "function" && cb(this.globalData.userInfo)
    }else{
      //调用登录接口
      wx.login({
        success: function (res) {
          //换取openid & session_key
          wx.request({
            url: wx.getStorageSync('domainName') + '/WxOpen/OnLogin',
            method: 'POST',
            header: { 'content-type': 'application/x-www-form-urlencoded' },
            data: {
              code: res.code
            },
            success:function(json){
              var result = json.data;
              if(result.success)
              {
                wx.setStorageSync('sessionId', result.sessionId);

                //获取userInfo并校验
                wx.getUserInfo({
                  success: function (userInfoRes) {
                    console.log('get userinfo',userInfoRes);
                    that.globalData.userInfo = userInfoRes.userInfo
                    typeof cb == "function" && cb(that.globalData.userInfo)

                    //校验
                    wx.request({
                      url: wx.getStorageSync('domainName') + '/WxOpen/CheckWxOpenSignature',
                      method: 'POST',
                      header: { 'content-type': 'application/x-www-form-urlencoded' },
                      data: {
                        sessionId: wx.getStorageSync('sessionId'),
                        rawData:userInfoRes.rawData,
                        signature:userInfoRes.signature
                      },
                      success:function(json){
                        console.log(json.data);
                      }
                    });

                    //解密数据（建议放到校验success回调函数中，此处仅为演示）
                    wx.request({
                      url: wx.getStorageSync('domainName') + '/WxOpen/DecodeEncryptedData',
                      method: 'POST',
                      header: { 'content-type': 'application/x-www-form-urlencoded' },
                      data: {
                        'type':"userInfo",
                        sessionId: wx.getStorageSync('sessionId'),
                        encryptedData:userInfoRes.encryptedData,
                        iv:userInfoRes.iv
                      },
                      success:function(json){
                        console.log(json.data);
                      }
                    });
                  }
                })
        }else{
          console.log('储存session失败！',json);
        }
      }
    })
  }
})
    }
  },
  globalData:{
    userInfo:null
  }
})