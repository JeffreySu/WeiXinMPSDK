using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.AdvancedAPIs.WorkBench.WorkBenchJson
{
    /// <summary>
    /// 应用在用户工作台展示的数据参数
    /// </summary>
    public class SetWorkBenchDataModel:SetWorkBenchTemplateModel
    {
        /// <summary>
        /// 需要设置的用户的userid
        /// </summary>
        public string userid { get; set; }
    }
}
