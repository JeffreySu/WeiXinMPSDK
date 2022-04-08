#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2021 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2021 Senparc
  
    文件名：GroupChatListResult.cs
    文件功能描述：获取客户群列表 返回结果
    
    
    创建标识：lishewen - 20200318


    修改标识：WangDrama - 20210630
    修改描述：v3.9.600 添加：外部联系人 - 客户群统计+联系客户+群直播+客户群事件 相关功能

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.External
{
    /// <summary>
    /// 返回结果
    /// </summary>
    public class TransferCustomerResult : WorkJsonResult
    {
        /// <summary>
        /// 客户分配结果
        /// </summary>
        public Customer[] customer { get; set; }
    }
    public class Customer
    {
        /// <summary>
        /// 客户的external_userid
        /// </summary>
        public string external_userid { get; set; }

        /// <summary>
        /// 对此客户进行分配的结果, 具体可参考全局错误码, 0表示成功发起接替,待24小时后自动接替,并不代表最终接替成功
        /// </summary>
        public ReturnCode_Work errcode { get; set; }
    }

    public class TransferCustomerParam
    {
        /// <summary>
        /// 是	原跟进成员的userid (离职分配必须是已离职用户。)
        /// </summary>
        public string handover_userid { get; set; }


        /// <summary>
        /// 是	接替成员的userid
        /// </summary>
        public string takeover_userid { get; set; }

        /// <summary>
        /// 客户的external_userid列表，最多一次转移100个客户 external_userid必须是handover_userid的客户（即配置了客户联系功能的成员所添加的联系人）。
        /// </summary>
        public string[] external_userid { get; set; }

        /// <summary>
        /// 在职继承使用：否	转移成功后发给客户的消息，最多200个字符，不填则使用默认文案
        /// </summary>
        public string transfer_success_msg { get; set; }
    }


    public class TransferStatusParam
    {
        /// <summary>
        /// 是	原跟进成员的userid (离职分配必须是已离职用户。)
        /// </summary>
        public string handover_userid { get; set; }


        /// <summary>
        /// 是	接替成员的userid
        /// </summary>
        public string takeover_userid { get; set; }

        /// <summary>
        /// 否	分页查询的cursor，每个分页返回的数据不会超过1000条；不填或为空表示获取第一个分页；
        /// </summary>
        public string cursor { get; set; }
    }


    public class TransferResult : WorkJsonResult
    {
        /// <summary>
        /// 客户分配结果
        /// </summary>
        public TransferCustomer[] customer { get; set; }

        /// <summary>
        /// 下个分页的起始cursor
        /// </summary>
        public string next_cursor { get; set; }
    }

    public class TransferCustomer
    {
        /// <summary>
        /// 客户的external_userid
        /// </summary>
        public string external_userid { get; set; }

        /// <summary>
        /// 在职接替状态， 1-接替完毕 2-等待接替 3-客户拒绝 4-接替成员客户达到上限 5-无接替记录
        /// 离职接替状态， 1-接替完毕 2-等待接替 3-客户拒绝 4-接替成员客户达到上限
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 接替客户的时间，如果是等待接替状态，则为未来的自动接替时间
        /// </summary>
        public long takeover_time { get; set; }
    }

    public class TransferGroupchatParam
    {
        /// <summary>
        /// 需要转群主的客户群ID列表。取值范围： 1 ~ 100
        /// </summary>
        public string[] chat_id_list { get; set; }

        /// <summary>
        /// 新群主ID
        /// </summary>
        public string new_owner { get; set; }
    }

    public class TransferGroupchatResult : WorkJsonResult
    {
        public FailedChat[] failed_chat_list { get; set; }
    }
    public class FailedChat: WorkJsonResult
    {
        /// <summary>
        /// 没能成功继承的群ID
        /// </summary>
        public string chat_id { get; set; }
    }

    public class UnassignedParam
    {
        /// <summary>
        /// 否	分页查询，要查询页号，从0开始
        /// </summary>
        public int page_id { get; set; }
        /// <summary>
        /// 否	每次返回的最大记录数，默认为1000，最大值为1000
        /// </summary>
        public int page_size { get; set; }

        /// <summary>
        /// 否	分页查询游标，字符串类型，适用于数据量较大的情况，如果使用该参数则无需填写page_id，该参数由上一次调用返回
        /// </summary>
        public string cursor { get; set; }
    }
    public class UnassignedResult: WorkJsonResult
    {
        public UnassignedInfo[] info { get; set; }
        /// <summary>
        /// 是否是最后一条记录
        /// </summary>
        public bool is_last { get; set; }

        /// <summary>
        /// 分页查询游标, 已经查完则返回空(“”)，使用page_id作为查询参数时不返回
        /// </summary>
        public string next_cursor { get; set; }
    }
    public class UnassignedInfo
    {
        /// <summary>
        /// 离职成员的userid
        /// </summary>
        public string handover_userid { get; set; }
        /// <summary>
        /// 外部联系人userid
        /// </summary>
        public string external_userid { get; set; }
        /// <summary>
        /// 成员离职时间
        /// </summary>
        public long dimission_time { get; set; }
    }

}
