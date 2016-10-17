/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc

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

    /// <summary>
    /// 生成JSON时忽略NULL对象
    /// </summary>
    public interface IJsonIgnoreNull : IEntityBase
    {

    }

    public class JsonIgnoreNull : IJsonIgnoreNull
    {

    }

    /// <summary>
    /// 类中有枚举在序列化的时候，需要转成字符串
    /// </summary>
    public interface IJsonEnumString : IEntityBase
    {

    }

}
