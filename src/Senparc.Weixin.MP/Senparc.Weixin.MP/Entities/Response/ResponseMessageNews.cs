#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
    
    文件名：ResponseMessageNews.cs
    文件功能描述：响应回复图文消息
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.NeuChar.Entities;
using System.Collections.Generic;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 图文消息
    /// </summary>
    public class ResponseMessageNews : ResponseMessageBase, IResponseMessageNews
    {
        public override ResponseMsgType MsgType
        {
            get { return ResponseMsgType.News; }
        }

        public int ArticleCount
        {
            get
            {
                return Articles == null ? 0 : Articles.Count;
            }
            set
            {
                //这里开放set只为了逆向从Response的Xml转成实体的操作一致性，没有实际意义。
            }
        }

        /// <summary>
        /// 文章列表，微信客户端只能输出前10条（可能未来数字会有变化，出于视觉效果考虑，建议控制在8条以内）
        /// </summary>
        public List<Article> Articles { get; set; }

        public ResponseMessageNews()
        {
            Articles = new List<Article>();
        }
    }
}
