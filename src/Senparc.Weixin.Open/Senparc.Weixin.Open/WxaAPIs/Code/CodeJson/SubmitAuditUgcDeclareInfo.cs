#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2024 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

/*----------------------------------------------------------------
    Copyright (C) 2024 Senparc
    
    文件名：SubmitAuditUgcDeclareInfo.cs
    文件功能描述：用户生成内容场景（UGC）信息安全声明 
    
    
    创建标识：Senparc - 20210413

----------------------------------------------------------------*/

namespace Senparc.Weixin.Open.WxaAPIs.Code
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
