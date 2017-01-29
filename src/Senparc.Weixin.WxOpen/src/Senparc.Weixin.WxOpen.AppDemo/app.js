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
      }
    })

    wx.getUserInfo({
      success: function (res) {
        console.log('get userinfo',res);
        that.globalData.userInfo = res.userInfo
        typeof cb == "function" && cb(that.globalData.userInfo)
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