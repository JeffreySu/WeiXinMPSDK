namespace Senparc.Weixin.MP.Helpers
{
    public static class UserInfoHelper
    {
        /// <summary>
        /// 性别: 未知, 0
        /// </summary>
        public const int SexUnknown = 0;
        /// <summary>
        /// 性别: 男, 1
        /// </summary>
        public const int SexMale = 1;
        /// <summary>
        /// 性别: 女, 2
        /// </summary>
        public const int SexFemale = 2;

        /// <summary>
        /// 头像尺寸: 640*640, 0
        /// </summary>
        public const int HeadImageSize0 = 0;
        /// <summary>
        /// 头像尺寸: 46*46, 46
        /// </summary>
        public const int HeadImageSize46 = 46;
        /// <summary>
        /// 头像尺寸: 64*64, 64
        /// </summary>
        public const int HeadImageSize64 = 64;
        /// <summary>
        /// 头像尺寸: 96*96, 96
        /// </summary>
        public const int HeadImageSize96 = 96;
        /// <summary>
        /// 头像尺寸: 132*132, 132
        /// </summary>
        public const int HeadImageSize132 = 132;

        /// <summary>
        /// 获取指定大小的用户头像网址
        /// </summary>
        /// <param name="headimgurl">原始头像地址</param>
        /// <param name="size">代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像）</param>
        /// <returns></returns>
        public static string GetHeadImageUrlWithSize(string headimgurl, int size = 0)
        {
            var url = headimgurl;
            if (url == null)
                return null;

            //客服头像的网址后面有查询参数, 如: "?wx_fmt=jpeg"
            string query = null;
            var qmIndex = headimgurl.LastIndexOf('?');
            if (qmIndex > 0)
            {
                query = headimgurl.Substring(qmIndex);
                url = headimgurl.Substring(0, qmIndex);
            }

            var tail = "/" + size.ToString("d");
            if (url.EndsWith(tail))
                return url + query;

            var slashIndex = url.LastIndexOf('/');
            if (slashIndex < 0)
                return url + query;

            return url.Substring(0, slashIndex) + tail + query;
        }
    }
}
