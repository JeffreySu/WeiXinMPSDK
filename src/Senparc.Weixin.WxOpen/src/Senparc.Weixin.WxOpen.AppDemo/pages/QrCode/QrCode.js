// pages/QrCode/QrCode.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    qrCodeImg: '',
    codeType: '1',
    sceneCode: null
  },

  /* 预览图片 */
  viewQrCode: function(e) {
    console.log('注意：base64编码的图片无法使用预览，必须使用文件地址');
    var src = e.currentTarget.dataset.src; //获取data-src  循环单个图片链接
    var imgList = [];
    imgList.push(src);
    wx.previewImage({
      current: src, // 当前显示图片的http链接
      urls: imgList // 需要预览的图片http链接列表
    })
  },
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function(query) {
    var that = this;

    if (query.codeType) {
      that.setData({
        codeType: query.codeType
      });
    }

    // scene 需要使用 decodeURIComponent 才能获取到生成二维码时传入的 scene
    if (query.scene) {
      const scene = decodeURIComponent(query.scene);
      var sceneData = scene.split('#');
      var sceneCode = sceneData[0];
      var sceneCodeType = sceneData[1];
      that.setData({
        sceneCode: sceneCode
      });
      that.setData({
        codeType: sceneCodeType
      });
    }

    console.log('codeType', query.codeType);

    //使用图片文件流直接加载图片
    that.setData({
      qrCodeImg: wx.getStorageSync('domainName') +
        '/WxOpen/GetQrCode?sessionId=' + wx.getStorageSync('sessionId') +
        '&codeType=' + that.data.codeType
    });

    //使用 base64 方式加载图片
    // wx.request({
    //   url: wx.getStorageSync('domainName') + '/WxOpen/GetQrCode?useBase64=1',
    //   data: {
    //     sessionId:,
    //   },
    //   method: 'POST',
    //   header: { 'content-type': 'application/x-www-form-urlencoded' },
    //   success: function (res) {
    //     if (res.data.success===false) {
    //       wx.showModal({
    //         title: '获取二维码失败',
    //         content: res.data.msg,
    //         showCancel: false
    //       });

    //     } else {
    //       console.log('获取二维码成功：' + res.data.msg);
    //       that.setData({
    //         qrCodeImg: 'data:image/jpeg;base64,' + res.data.msg,  // data 为接口返回的base64字符串
    //       });
    //       console.log(that.data.qrCodeImg);
    //     }
    //   }
    // });
  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function() {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function() {

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function() {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function() {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function() {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function() {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function() {

  }
})