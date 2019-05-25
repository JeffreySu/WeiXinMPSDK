/*----------------------------------------------------------------
    Copyright (C) 2019 Senparc
    
    文件名：DepartmentResult.cs
    文件功能描述：部门接口返回结果
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
    
    修改标识：Senparc - 20150408
    修改描述：添加order字段

    修改标识：Senparc - 20171017
    修改描述：v1.2.0 部门id改为long类型    

    修改标识：Senparc - 20171127
    修改描述：v1.2.3 GetDepartmentListResult.order改为long类型
----------------------------------------------------------------*/

using System.Collections.Generic;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Work.AdvancedAPIs.MailList
{
    /// <summary>
    /// 创建部门返回结果
    /// </summary>
    public class CreateDepartmentResult : WorkJsonResult
    {
        /// <summary>
        /// 创建的部门id
        /// </summary>
        public long id { get; set; }
    }

    public class GetDepartmentListResult : WorkJsonResult
    {
        public List<DepartmentList> department { get; set; }
    }

    public class DepartmentList
    {
        /// <summary>
        /// 部门id
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 上级部门id
        /// </summary>
        public long parentid { get; set; }
        /// <summary>
        /// 在父部门中的次序值。order值小的排序靠前。
        /// </summary>
        public long order { get; set; }
    }
}
