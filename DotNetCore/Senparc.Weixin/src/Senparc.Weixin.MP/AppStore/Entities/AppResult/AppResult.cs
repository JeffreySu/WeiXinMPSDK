/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
  
    文件名：AppResult.cs
    文件功能描述：获取App返回结果
    
    
    创建标识：Senparc - 20150319
----------------------------------------------------------------*/

namespace Senparc.Weixin.MP.AppStore
{
    public interface IAppResult<T> where T : IAppResultData
    {
        AppResultKind Result { get; set; }
        string ErrorMessage { get; set; }
        T Data { get; set; }
        long RunTime { get; set; }
    }

    public interface IAppResultData
    {
    }

    public class AppResultData : IAppResultData
    {

    }

    /// <summary>
    /// JSON返回结果（用于菜单接口等）
    /// </summary>
    public class AppResult<T> : IAppResult<T> where T : IAppResultData
    {
        public AppResultKind Result { get; set; }
        /// <summary>
        /// 如果结果未成功，则Data为期望的类型
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 错误信息，如果结果为成功，错误信息为Null
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Api执行时间
        /// </summary>
        public long RunTime { get; set; }
        //public T SData
        //{
        //    get { return Data as T; }
        //}
    }
}
