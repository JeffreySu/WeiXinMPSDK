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
    
    文件名：CreateStoreData.cs
    文件功能描述：创建门店需要Post的数据
    
    
    创建标识：Senparc - 20150513
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AdvancedAPIs.Poi
{
    /// <summary>
    /// 创建门店需要Post的数据
    /// </summary>
    public class CreateStoreData
    {
        public CreateStore_Business business { get; set; }
    }

    public class CreateStore_Business
    {
        /// <summary>
        /// 门店信息
        /// </summary>
        public StoreBaseInfo base_info { get; set; }
    }

    /// <summary>
    /// 修改门店服务信息需要Post的数据
    /// </summary>
    public class UpdateStoreData
    {
        public UpdateStore_Business business { get; set; }
    }

    public class UpdateStore_Business
    {
        public UpdateStore_BaseInfo base_info { get; set; }
    }

    public class UpdateStore_BaseInfo : StoreBaseInfoCanBeUpdate
    {
        /// <summary>
        /// 微信的门店ID，微信内门店唯一标示ID
        /// </summary>
        public string poi_id { get; set; }
    }
}
