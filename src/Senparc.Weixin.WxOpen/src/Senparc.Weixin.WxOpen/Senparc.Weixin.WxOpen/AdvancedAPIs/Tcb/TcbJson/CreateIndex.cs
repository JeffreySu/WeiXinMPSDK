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
  
    文件名：CreateIndex.cs
    文件功能描述：新增索引
    
    
    创建标识：lishewen - 20200318
----------------------------------------------------------------*/
namespace Senparc.Weixin.WxOpen.AdvancedAPIs.Tcb
{
    /// <summary>
    /// 新增索引
    /// </summary>
    public class CreateIndex
    {
        /// <summary>
        /// 索引名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 是否唯一
        /// </summary>
        public bool unique { get; set; }
        /// <summary>
        /// 索引字段
        /// </summary>
        public Key[] keys { get; set; }
    }
    /// <summary>
    /// 索引字段
    /// </summary>
    public class Key
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 字段排序
        /// "1"        升序
        /// "-1"        降序
        /// "2dsphere"        地理位置
        /// </summary>
        public string direction { get; set; }
    }
    /// <summary>
    /// 删除索引
    /// </summary>
    public class DropIndex
    {
        /// <summary>
        /// 索引名
        /// </summary>
        public string name { get; set; }
    }

}
