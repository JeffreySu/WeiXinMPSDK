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
    Copyright(C) 2023 Senparc
    
    文件名：UpdateTemplateCardRequest.cs
    文件功能描述：“更新模版卡片消息”接口请求信息
    
    
    创建标识：Senparc - 20230612

----------------------------------------------------------------*/


namespace Senparc.Weixin.Work.AdvancedAPIs.Mass.UpdateTemplateCard
{
    public class UpdateTemplateCardRequest
    {
        public string[] userids { get; set; }
        public int[] partyids { get; set; }

        public int[] tagids { get; set; }

        public int atall { get;set; }

        public int agentid { get; set; }

        public string response_code { get; set; }

        public int enable_id_trans { get; set; } = 0;

        public SendTemplateCard.Template_CardBase template_card { get; private set; }

        public Button button { get; set; }

        UpdateTemplateCardRequest() { 
        
        }

        public UpdateTemplateCardRequest(SendTemplateCard.Template_CardBase templateCard): this()
        {
            template_card = templateCard;
        }

        public class Button
        {
            public string replace_name { get; set; }
        }
    }
}
