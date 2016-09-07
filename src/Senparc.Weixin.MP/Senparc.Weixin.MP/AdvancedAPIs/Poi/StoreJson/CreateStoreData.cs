/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
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
