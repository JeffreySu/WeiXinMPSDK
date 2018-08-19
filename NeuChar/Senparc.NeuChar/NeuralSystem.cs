using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.NeuChar
{
    /// <summary>
    /// 神经系统，整个系统数据的根节点
    /// </summary>
    public class NeuralSystem
    {
        #region 单例

        /// <summary>
        /// Redis 缓存策略
        /// </summary>
        NeuralSystem() : base()
        {

        }

        //静态SearchCache
        public static NeuralSystem Instance
        {
            get
            {
                return Nested.instance;//返回Nested类中的静态成员instance
            }
        }

        class Nested
        {
            static Nested()
            {
            }
            //将instance设为一个初始化的BaseCacheStrategy新实例
            internal static readonly NeuralSystem instance = new NeuralSystem();
        }

        #endregion

    }
}
