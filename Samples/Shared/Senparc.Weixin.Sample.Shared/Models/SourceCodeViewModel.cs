using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Weixin.Sample
{
    public class SourceCodeViewModel
    {
        public string LibName { get; set; }
        public string LibSourcePath { get; set; }
        public SourceCodeViewModel(string libName, string libSourcePath)
        {
            LibName = libName;
            LibSourcePath = libSourcePath;
        }
    }
}
