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

        string Name { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        INeuralNode ParentNode { get; }
        /// <summary>
        /// 所有子节点
        /// </summary>
        IList<INeuralNode> ChildrenNodes { get; }

        /// <summary>
        /// 设置父节点
        /// </summary>
        /// <param name="childNode"></param>
        void SetParentNode(INeuralNode childNode);
        /// <summary>
        /// 设置子节点
        /// </summary>
        /// <param name="childNode"></param>
        void SetChildNode(INeuralNode childNode);

    }
}
