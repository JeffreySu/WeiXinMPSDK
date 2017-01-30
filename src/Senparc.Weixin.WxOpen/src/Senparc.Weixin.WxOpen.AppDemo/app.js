//app.js
App({
  onLaunch: function () {
    //调用API从本地缓存中获取数据
    var logs = wx.getStorageSync('logs') || []
    logs.unshift(Date.now())
    wx.setStorageSync('logs', logs)
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
      url: 'https://sdk.weixin.senparc.com/WxOpen/OnLogin',
      method: 'POST',
      data: {
        code: res.code
      },
      success:function(json){
        var result = json.data;
        if(result.success)
        {
          wx.setStorageSync('sessionId', result.sessionId);
          console.log('sessionId:',wx.getStorageSync('sessionId'));
        }else{
          console.log('储存session失败！',json);
        }
      }
    })

    wx.getUserInfo({
      success: function (res) {
        console.log('get userinfo',res);
        that.globalData.userInfo = res.userInfo
        typeof cb == "function" && cb(that.globalData.userInfo)

        //校验
        wx.request({
          url: 'https://sdk.weixin.senparc.com/WxOpen/CheckWxOpenSignature',
          method: 'POST',
          data: {
            sessionId: wx.getStorageSync('sessionId'),
            rawData:res.rawData,
            signature:res.signature
          },
          success:function(json){
            console.log(json.data);
          }
        });
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