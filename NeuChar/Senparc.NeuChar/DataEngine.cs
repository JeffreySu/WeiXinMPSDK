using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Senparc.NeuChar
{
    /// <summary>
    /// 数据处理引擎
    /// </summary>
    public class DataEngine
    {
        public string FullFilePath { get; set; }

        public NeuralSystem NeuralSystem { get { return NeuralSystem.Instance; } }

        /// <summary>
        /// 数据处理引擎 构造函数
        /// </summary>
        /// <param name="file">文件相对路径</param>
        public DataEngine(string filePath = "~/App_Data/NeuChar")
        {
            FullFilePath = filePath;



        }
    }
}
