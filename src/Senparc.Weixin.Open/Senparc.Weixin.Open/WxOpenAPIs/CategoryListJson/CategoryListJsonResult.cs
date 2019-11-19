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
  
    文件名：Register.cs
    文件功能描述：注册小程序信息
    
    
    创建标识：Senparc - 20180716

    修改标识：Senparc - 20190123
    修改描述：v3.3.5.1 修改“获取账号可以设置的所有类目”接口参数（官方文档错误）

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.Open.WxOpenAPIs.CategoryListJson
{
    /// <summary>
    /// 账号可以设置的所有类目
    /// </summary>
    [Serializable]
    public class CategoryListJsonResult : WxJsonResult
    {
        //微信文档写的参数名称是  category_list，实际上是 categories_list
        //public IList<Category> categories { get; set; }

        public CategoriesList categories_list { get; set; }
    }

    public class CategoriesList
    {
        public IList<Category> categories { get; set; }
    }

    public class Category
    {
        /// <summary>
        /// 类目Id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 类目名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public int level { get; set; }

        /// <summary>
        /// 类目父级Id
        /// </summary>
        public int father { get; set; }

        /// <summary>
        /// 子级类目Id
        /// </summary>
        public IList<int> children { get; set; } = new List<int>();

        /// <summary>
        /// 是否为敏感类目（1为敏感类目，需要提供相应资质审核；0为非敏感类目，无需审核
        /// </summary>
        public int sensitive_type { get; set; }

        public Qualify qualify { get; set; }
    }

    public class Qualify
    {
        public Qualify()
        {
            exter_list = new List<Exter>();
        }

        public IList<Exter> exter_list { get; set; }
    }

    public class Exter
    {
        public Exter()
        {
            inner_list = new List<Inner>();
        }

        public IList<Inner> inner_list { get; set; }
    }

    public class Inner
    {
        /// <summary>
        /// Sensitive_type为1的类目需要提供的资质文件名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 资质文件示例
        /// </summary>
        public string url { get; set; }
    }
}