using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Entities
{
    /// <summary>
    /// 只返回"success"等指定字符串的响应信息基类
    /// </summary>
    public abstract class SuccessResponseMessageBase : ResponseMessageBase
    {
        /// <summary>
        /// 返回字符串内容，默认为"success"
        /// </summary>
        public string ReturnText { get; set; }

        /// <summary>
        /// SuccessResponseMessage构造函数
        /// </summary>
        protected SuccessResponseMessageBase()
        {
            ReturnText = "success";
        }
    }
}
