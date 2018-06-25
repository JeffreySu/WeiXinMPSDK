#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2018 Senparc

    文件名：EntityBase.cs
    文件功能描述：EntityBase


    创建标识：Senparc - 20150928

    修改标识：Senparc - 20161012
    修改描述：v4.8.1 添加IJsonEnumString

----------------------------------------------------------------*/





namespace Senparc.Weixin.Entities
{
    /// <summary>
    /// 所有微信自定义实体的基础接口
    /// </summary>
    public interface IEntityBase
    {
    }

    //public class EntityBase : IEntityBase
    //{

    //}

    ///// <summary>
    ///// 接口：生成JSON时忽略NULL对象
    ///// </summary>
    //public interface IJsonIgnoreNull : IEntityBase
    //{

    //}

    ///// <summary>
    ///// 生成JSON时忽略NULL对象
    ///// </summary>
    //public class JsonIgnoreNull : IJsonIgnoreNull
    //{

    //}

    ///// <summary>
    ///// 接口：类中有枚举在序列化的时候，需要转成字符串
    ///// </summary>
    //public interface IJsonEnumString : IEntityBase
    //{

    //}

}
