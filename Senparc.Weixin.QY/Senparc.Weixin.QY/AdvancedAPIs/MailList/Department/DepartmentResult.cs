using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.QY.AdvancedAPIs
{
    /// <summary>
    /// 创建部门返回结果
    /// </summary>
    public class CreateDepartmentResult : WxJsonResult
    {
        public int id { get; set; }//创建的部门id
    }

    public class GetDepartmentListResult : WxJsonResult
    {
        public List<DepartmentList> department { get; set; }
    }

    public class DepartmentList
    {
        public int id { get; set; }
        public string name { get; set; }
        public int parentid { get; set; }
    }
}
