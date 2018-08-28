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

        //TODO：开发流程：实体->JSON/XML->General


        public INeuralNode Root { get; set; }

        /// <summary>
        /// NeuChar 核心神经系统，包含所有神经节点信息
        /// </summary>
        NeuralSystem()
        {
            //获取所有配置并初始化

            var path = "~/App_Data/NeuChar/";
            var file = "json.json";//TODO：后期通过自动扫描得到

            //TODO:当配置文件多了之后可以遍历所有的配置文件

            //TODO:解密

            INeuralNode root = new RootNeuralNode();

            Root = root;
        }

        /// <summary>
        /// 获取指定Name的节点
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public INeuralNode GetNode(string name, INeuralNode parentNode = null)
        {
            if (parentNode == null)
            {
                parentNode = Root;
            }

            INeuralNode foundNode = null;

            foreach (var node in parentNode.ChildrenNodes)
            {
                if (node.Name == name)
                {
                    foundNode = node;
                    break;
                }

                foundNode = GetNode(name, node);
                if (foundNode != null)
                {
                    break;
                }
            }

            return foundNode;
        }
    }
}
