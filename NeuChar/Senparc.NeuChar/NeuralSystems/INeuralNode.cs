using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.NeuChar
{
    public interface INeuralNode
    {

        string Version { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        INeuralNode ParentNode { get; set; }
        /// <summary>
        /// 所有子节点
        /// </summary>
        IList<INeuralNode> ChildrenNodes { get; set; }
    }
}
