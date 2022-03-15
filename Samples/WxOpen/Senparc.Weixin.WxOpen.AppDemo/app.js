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

      //使用.NET Core 2.2 Sample（Senparc.Weixin.MP.Sample.vs2017.sln）配置：
      // wx.setStorageSync('domainName', "http://localhost:58936/VirtualPath")
      // wx.setStorageSync('wssDomainName', "ws://localhost:58936/VirtualPath")

      //使用 .NET Core 3.0 Samole（Senparc.Weixin.Sample.NetCore3.vs2019.sln）配置：
      // wx.setStorageSync('domainName', "https://localhost:44381")
      // wx.setStorageSync('wssDomainName', "wss://localhost:44381")

      //使用 .NET 6.0 Samole（Senparc.Weixin.Sample.Net6.sln）配置：
      wx.setStorageSync('domainName', "https://localhost:44382")
      wx.setStorageSync('wssDomainName', "wss://localhost:44382")
    }

    // 打开调试
    // wx.setEnableDebug({
    //   enableDebug: true
    // })
  },
  getUserInfo:function(cb,callback){
    var that = this
    if(this.globalData.userInfo){
      typeof cb == "function" && cb(this.globalData.userInfo)
    }else{
    //获取userInfo并校验
    console.log('准备调用 wx.getUserProfile');
    wx.getUserProfile({
      desc: '用于完善会员资料', // 声明获取用户个人信息后的用途，后续会展示在弹窗中，请谨慎填写
      success: function (userInfoRes) {
        console.log('get getUserProfile', userInfoRes);
        that.globalData.userInfo = userInfoRes.userInfo
        typeof cb == "function" && cb(that.globalData.userInfo)
        typeof callback == "function" && callback(userInfoRes.userInfo)
        
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
              console.log('wx.login - request-/WxOpen/OnLogin Result:', json);
              var result = json.data;
              if(result.success)
              {
                wx.setStorageSync('sessionId', result.sessionId);
                //校验
                wx.request({
                  url: wx.getStorageSync('domainName') + '/WxOpen/CheckWxOpenSignature',
                  method: 'POST',
                  header: { 'content-type': 'application/x-www-form-urlencoded' },
                  data: {
                    sessionId: result.sessionId,//wx.getStorageSync('sessionId'),
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
                    sessionId: result.sessionId,//wx.getStorageSync('sessionId'),
                    encryptedData: userInfoRes.encryptedData,
                    iv: userInfoRes.iv
                  },
                  success:function(json){
                    console.log('数据解密：', json.data);
                  }
                });
                
              }else{
                console.log('储存session失败！',json);
              }
            }
          })
        }
      })
      
        }
      });
    }

  },
  globalData:{
    userInfo:null
  }
})