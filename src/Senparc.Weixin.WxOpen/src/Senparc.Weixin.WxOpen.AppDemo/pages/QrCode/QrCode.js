// pages/QrCode/QrCode.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    qrCodeImgBase64Data:''
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    var that = this;
    wx.request({
      url: wx.getStorageSync('domainName') + '/WxOpen/GetQrCode',
      data: {
        sessionId: wx.getStorageSync('sessionId'),
      },
      method: 'POST',
      header: { 'content-type': 'application/x-www-form-urlencoded' },
      success: function (res) {
        if (res.data.success) {
          console.log('获取二维码成功：' + res.data.msg);
          that.setData({
            qrCodeImgBase64Data: 'data:image/jpeg;base64,' + res.data.msg,  // data 为接口返回的base64字符串  
          });
          console.log(that.data.qrCodeImgBase64Data);
        } else {
          wx.showModal({
            title: '获取二维码失败',
            content: res.data.msg,
            showCancel: false
          });
        }
      }
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