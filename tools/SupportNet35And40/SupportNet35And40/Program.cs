using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SupportNet35And40
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO：删除没有用的 using System.Threading.Tasks;

            ReplaceCode("../src/");
            Console.WriteLine("完成");
            Console.ReadKey();
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

                    string newContent = null;

                    using (var fs = new FileStream(file, FileMode.Open))
                    {
                        var sr = new StreamReader(fs,Encoding.UTF8);
                        var fileContent = sr.ReadToEnd();
                        var hit = fileContent.Contains("\r\n\r\n        #region 异步方法");
                        if (hit)
                        {
                            Console.WriteLine("命中，改写中");
                            newContent = fileContent.Replace("\r\n\r\n        #region 异步方法",
                               "\r\n\r\n#if !NET35 && !NET40\r\n        #region 异步方法");

                            var endContent = "#endregion";
                            var endIndex = newContent.LastIndexOf(endContent);
                            if (endIndex >= 0)
                            {
                                newContent = newContent.Insert(endIndex + endContent.Length, "\r\n#endif");

                                //var frontContent = newContent.Substring(0, endIndex + endContent.Length);
                                //newContent = newContent.Replace(frontContent, frontContent + "\r\n#endif");
                            }
                        }

                        //var resetHit = fileContent.Contains("#if !NET35\r\nusing System.Threading.Tasks;\r\n#endif");
                        //if (resetHit)
                        //{
                        //        newContent = fileContent.Replace("#if !NET35\r\nusing System.Threading.Tasks;\r\n#endif", "using System.Threading.Tasks;");
                        //}

                    }

                    if (newContent != null)
                    {
                        using (var fs = new FileStream(file, FileMode.Create))
                        {
                            var sw = new StreamWriter(fs, Encoding.UTF8);
                            sw.Write(newContent);
                            sw.Flush();
                            fs.Flush(true);
                        }
                        Console.WriteLine("已保存");
                    }
                }
                ReplaceCode(dir);
            }
        }
    }
}
