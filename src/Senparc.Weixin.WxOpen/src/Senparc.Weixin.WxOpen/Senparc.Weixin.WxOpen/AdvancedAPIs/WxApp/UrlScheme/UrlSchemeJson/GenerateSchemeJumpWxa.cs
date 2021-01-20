using Senparc.Weixin.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.WxOpen.AdvancedAPIs.UrlScheme
{
    /// <summary>
    /// GenerateScheme() 接口中的 jumpWxa 参数
    /// <para>2021年1月6日更新公告：https://developers.weixin.qq.com/community/develop/doc/00060aa283430885398b44a7c5d001?blockType=1</para>
    /// </summary>
    public class GenerateSchemeJumpWxa
    {
        /// <summary>
        /// （必填）通过scheme码进入的小程序页面路径，必须是已经发布的小程序存在的页面，不可携带query。path为空时会跳转小程序主页。
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// （必填）通过scheme码进入小程序时的query，最大128个字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~
        /// </summary>
        public string query { get; set; }

        /// <summary>
        /// GenerateScheme() 接口中的 jumpWxa 参数
        /// </summary>
        /// <param name="path"></param>
        /// <param name="query"></param>
        public GenerateSchemeJumpWxa(string path, string query)
        {
            this.path = path ?? "";
            this.query = query ?? "";
        }
    }
}
