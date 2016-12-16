namespace Senparc.Weixin.Cache
{
    /// <summary>
    /// 所有以String类型为Key的缓存策略接口
    /// </summary>
    public interface IObjectCacheStrategy : IBaseCacheStrategy<string, object>
    {
        IContainerCacheStrategy ContainerCacheStrategy { get; }
    }
}