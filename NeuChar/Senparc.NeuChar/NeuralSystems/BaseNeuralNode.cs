using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.NeuChar
{
    public abstract class BaseNeuralNode : INeuralNode
    {
        //TODO:使用命名空间以及唯一标识Guid来区分
        //public abstract string Namespace { get; set; }

        public abstract string Version { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public virtual INeuralNode ParentNode { get; set; }
        /// <summary>
        /// 所有子节点
        /// </summary>
        public virtual IList<INeuralNode> ChildrenNodes { get; set; }


        //public object ApiData { get; set; }
        //public object ApiDataKey { get; set; }
        //public object ExtData { get; set; }
        //public object ExtDataKey { get; set; }
    }
}
