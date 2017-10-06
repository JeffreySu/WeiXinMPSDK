using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportNet35And40
{
    class Program
    {
        static void Main(string[] args)
        {
            ReplaceCode("../src/");
        }

        private static void ReplaceCode(string root)
        {
            Console.WriteLine("开始扫描文件夹：" + root);
            var dirs = System.IO.Directory.GetDirectories(root);
            foreach (var dir in dirs)
            {
                var files = System.IO.Directory.GetFiles(dir, "*.cs");
                foreach (var file in files)
                {
                    Console.WriteLine("扫描文件：" + file);

                    using (var fs = new FileStream(file, FileMode.Open))
                    {
                        string newContent = null;
                        using (var sr = new StreamReader(fs))
                        {
                            var fileContent = sr.ReadToEnd();
                            var hit = fileContent.Contains("\r\n\r\n        #region 异步请求");
                            if (hit)
                            {
                                Console.WriteLine("命中，改写中");
                                newContent = fileContent.Replace("\r\n\r\n        #region 异步请求",
                                   "\r\n\r\n\r\n#if !NET35 && !NET40        #region 异步请求");

                                var endContent = "#endregion";
                                var endIndex = fileContent.LastIndexOf(endContent);
                                if (endIndex >= 0)
                                {
                                    var frontContent = fileContent.Substring(0, endIndex + endContent.Length);
                                    fileContent.Replace(frontContent, frontContent + "\r\n#endif");
                                }
                            }
                        }

                        if (newContent != null)
                        {
                            fs.Seek(0, SeekOrigin.Begin);
                            using (var sw = new StreamWriter(fs))
                            {
                                sw.Write(newContent);
                                sw.Flush();
                                Console.WriteLine("已保存");
                            }
                        }
                    }
                }
                ReplaceCode(dir);
            }
        }
    }
}
