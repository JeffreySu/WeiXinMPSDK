using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Open.WxaAPIs
{
    /// <summary>
    /// 用户生成内容场景（UGC）信息安全声明
    /// </summary>
    public class SubmitAuditUgcDeclareInfo
    {
        /// <summary>
        /// UGC场景 0,不涉及用户生成内容, 1.用户资料,2.图片,3.视频,4.文本,5其他, 可多选,当scene填0时无需填写下列字段
        /// </summary>
        public int[] scene { get; set; }

        /// <summary>
        /// 当scene选其他时的说明,不超时256字
        /// </summary>
        public string other_scene_desc { get; set; }

        /// <summary>
        /// 内容安全机制 1.使用平台建议的内容安全API,2.使用其他的内容审核产品,3.通过人工审核把关,4.未做内容审核把关
        /// </summary>
        public int[] method { get; set; }

        /// <summary>
        /// 是否有审核团队, 0.无,1.有,默认0
        /// </summary>
        public int has_audit_team { get; set; }

        /// <summary>
        /// 说明当前对UGC内容的审核机制,不超过256字
        /// </summary>
        public string audit_desc { get; set; }
    }
}
