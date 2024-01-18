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
    
    文件名：GroupChat_UpdateJoinWayRequest.cs
    文件功能描述：“更新客户群进群方式配置”接口 请求参数
    
    
    创建标识：Senparc - 20220918

----------------------------------------------------------------*/

namespace Senparc.Weixin.Work.AdvancedAPIs.External.ExternalJson
{
#nullable enable

    /// <summary>
    /// “更新客户群进群方式配置”接口 请求参数
    /// </summary>
    public class GroupChat_UpdateJoinWayRequest
    {
        /// <summary>
        /// 企业联系方式的配置id
        /// </summary>
        public string config_id { get; set; }
        /// <summary>
        /// 场景。1 - 群的小程序插件 2 - 群的二维码插件
        /// </summary>
        public int scene { get; set; }
        /// <summary>
        /// 联系方式的备注信息，用于助记，超过30个字符将被截断
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 当群满了后，是否自动新建群。0-否；1-是。 默认为1
        /// </summary>
        public int auto_create_room { get; set; }
        /// <summary>
        /// 自动建群的群名前缀，当auto_create_room为1时有效。最长40个utf8字符
        /// </summary>
        public string room_base_name { get; set; }
        /// <summary>
        /// 自动建群的群起始序号，当auto_create_room为1时有效
        /// </summary>
        public int room_base_id { get; set; }
        /// <summary>
        /// 使用该配置的客户群ID列表。见<see href="https://developer.work.weixin.qq.com/document/path/92229#19330">客户群ID获取方法</see>
        /// </summary>
        public string[] chat_id_list { get; set; }
        /// <summary>
        /// 业自定义的state参数，用于区分不同的入群渠道。不超过30个UTF-8字符。如果有设置此参数，在调用获取客户群详情接口时会返回每个群成员对应的该参数值，详见文末<see href="https://developer.work.weixin.qq.com/document/path/92229#%E9%99%84%E5%BD%952%EF%BC%9A%E8%8E%B7%E5%8F%96%E5%AE%A2%E6%88%B7%E7%BE%A4%E8%AF%A6%E6%83%85%EF%BC%8C%E8%BF%94%E5%9B%9Estate%E5%8F%82%E6%95%B0">附录2</see>
        /// </summary>
        public string state { get; set; }

    }
#nullable disable

}
