using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.NeuChar
{
    /// <summary>
    /// 神经节点
    /// </summary>
    public class NeuralNode
    {
        public NeuralNode ParentNode { get; set; }
        public IList<NeuralNode> ChildrenNodes { get; set; }
    }
}
