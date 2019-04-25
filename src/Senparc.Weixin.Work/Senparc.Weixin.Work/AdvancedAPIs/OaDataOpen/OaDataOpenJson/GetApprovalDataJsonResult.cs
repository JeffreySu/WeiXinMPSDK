/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：GetApprovalDataJsonResult.cs
    文件功能描述：获取审批数据接口返回结果
    
    
    创建标识：Senparc - 20170617

----------------------------------------------------------------*/

using Senparc.Weixin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.OaDataOpen
{
    public class GetApprovalDataJsonResult : WorkJsonResult
    {
        /// <summary>
        /// 拉取的审批单个数，最大值为10000
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 时间段内的总审批单个数
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 拉取列表的最后一个审批单号
        /// 附：时间段内审批单数量超过10000时：
        /// 当时间段内审批单超过10000时，可通过填写next_spnum的值，从而多次拉取列表的方式来满足需求。
        /// 具体而言，就是在调用接口时，将上一次调用得到的返回中的next_spnum值，作为下一次调用中的next_spnum值。
        /// </summary>
        public long next_spnum { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public GetApprovalDataJsonResult_Data[] data { get; set; }
    }

    public class GetApprovalDataJsonResult_Data
    {
        /// <summary>
        /// 审批名称(请假，报销，自定义审批名称)
        /// </summary>
        public string spname { get; set; }
        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string apply_name { get; set; }
        /// <summary>
        /// 申请人部门
        /// </summary>
        public string apply_org { get; set; }
        /// <summary>
        /// 审批人姓名
        /// </summary>
        public string[] approval_name { get; set; }
        /// <summary>
        /// 抄送人姓名
        /// </summary>
        public string[] notify_name { get; set; }
        /// <summary>
        /// 审批状态：1审批中；2 已通过；3已驳回；4已取消
        /// </summary>
        public int sp_status { get; set; }
        /// <summary>
        /// 审批单号
        /// </summary>
        public object sp_num { get; set; }
        /// <summary>
        /// 报销类型
        /// </summary>
        public GetApprovalDataJsonResult_Data_Expense expense { get; set; }
        /// <summary>
        /// leave
        /// </summary>
        public GetApprovalDataJsonResult_Data_Leave leave { get; set; }
        /// <summary>
        /// 自定义审批模版
        /// </summary>
        public GetApprovalDataJsonResult_Data_Comm comm { get; set; }
    }

    public class GetApprovalDataJsonResult_Data_Expense
    {
        /// <summary>
        /// 报销类型：1差旅费；2交通费；3招待费；4其他报销
        /// </summary>
        public int expense_type { get; set; }
        /// <summary>
        /// 报销事由
        /// </summary>
        public string reason { get; set; }
        /// <summary>
        /// 报销明细
        /// </summary>
        public GetApprovalDataJsonResult_Data_Expense_Item[] item { get; set; }
    }

    public class GetApprovalDataJsonResult_Data_Expense_Item
    {
        /// <summary>
        /// 费用类型：1飞机票；2火车票；3的士费；4住宿费；5餐饮费；6礼品费；7活动费；8通讯费；9补助；10其他
        /// </summary>
        public int expenseitem_type { get; set; }
        /// <summary>
        /// 发生时间，unix时间 //PS：官方文档：发生事件，unix时间
        /// </summary>
        public long time { get; set; }
        /// <summary>
        /// 费用金额，单位元
        /// </summary>
        public decimal sums { get; set; }
        /// <summary>
        /// 明细事由
        /// </summary>
        public string reason { get; set; }
    }

    public class GetApprovalDataJsonResult_Data_Leave
    {
        /// <summary>
        /// 请假时间单位：0半天；1小时
        /// </summary>
        public int timeunit { get; set; }
        /// <summary>
        /// 请假类型：1年假；2事假；3病假；4调休假；5婚假；6产假；7陪产假；8其他
        /// </summary>
        public int leave_type { get; set; }
        /// <summary>
        /// 请假开始时间，unix时间
        /// </summary>
        public long start_time { get; set; }
        /// <summary>
        /// 请假结束时间，unix时间
        /// </summary>
        public long end_time { get; set; }
        /// <summary>
        /// 请假时长，单位小时
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// 	 
        /// </summary>
        public string reason { get; set; }
    }

    public class GetApprovalDataJsonResult_Data_Comm
    {
        /*
          【注解1】apply_data：
            {
                "item-1490450365815": {
                    "title": "加班理由",        // 类目名
                    "type": "textarea",         // 类目类型【 text: "文本", textarea: "多行文本", number: "数字", date: "日期", datehour: "日期+时间",  select: "选择框" 】
                    "value": "项目需要"     // 填写的内容，只有Type是图片时，value是一个数组，数据示例如下方所示；
                },
                "item-1490450379069": {
                    "title": "加班开始时间",
                    "type": "date",
                    "value": "1490371200000"    //日期格式为时间缀
                },
                "item-1490450399494": {
                    "title": "加班证明",
                    "type": "image",
                    "value": [
                                "https://p.qpic.cn/pic_wework/4116602740/a2a481aa4e87639055774e51bc6cabde5595cd4458b57343/0",
                                "https://p.qpic.cn/pic_wework/4116602740/a2a481aa4e87639055774e51bc6cabde5595cd4458b57343/0"
                            ]
                }
            }
        */

        /// <summary>
        /// 自定义审批申请的单据数据，请参考官方文档【注解1】：http://work.weixin.qq.com/api/doc#11228。
        /// </summary>
        public string apply_data { get; set; }


    }


}
