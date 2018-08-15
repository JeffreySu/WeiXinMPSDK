using System;

namespace Senparc.NeuChar
{
    /// <summary>
    /// 基础模型
    /// </summary>
    public class BaseModel
    {
        public string Version { get; set; }
        public object ApiData { get; set; }
        public object ApiDataKey { get; set; }
        public object ExtData { get; set; }
        public object ExtDataKey { get; set; }
        public DataType DataType { get; set; }
        public ApiType ApiType { get; set; }
    }
}
