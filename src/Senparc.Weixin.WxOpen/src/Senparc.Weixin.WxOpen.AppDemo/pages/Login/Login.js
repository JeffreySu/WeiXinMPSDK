// pages/Login/Login.js
//获取应用实例
var app = getApp()

Page({

  /**
   * 页面的初始数据
   */
  data: {
    userInfo:{},
    hasUserInfo: false,
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
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
      console.log('不支持 open-type=getUserInfo');
    }
  },

  //获取用户信息
  getUserInfo: function (e) {
    console.log('getUserInfo（已更新至 2021.4.13 新版本）', e)
    //app.getUserInfo(e);//2021.4.13 后发布不支持
    var that = this;
    app.getUserInfo(e, function(userInfo){
      app.globalData.userInfo = userInfo
      that.setData({
        userInfo: userInfo,
        hasUserInfo: true
      });
      wx.navigateTo({
        url: '../index/index',
      })
    });
   
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  }
})