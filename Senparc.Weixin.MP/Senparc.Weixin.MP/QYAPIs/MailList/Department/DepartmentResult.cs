using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.QYPIs
{
    public class CreateDepartmentResult : WxJsonResult
    {
        public int id { get; set; }
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
