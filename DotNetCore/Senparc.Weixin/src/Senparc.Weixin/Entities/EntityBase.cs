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
}
