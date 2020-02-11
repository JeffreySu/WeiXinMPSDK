using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senparc.Weixin.Sample.NetCore3.Models.VD
{
    public class Home_IndexVD
    {
        public Dictionary<string, List<Home_IndexVD_AssemblyModel>> AssemblyModelCollection { get; set; }
    }

    public class Home_IndexVD_AssemblyModel
    {
        public string Title { get; set; }
        public string AssemblyName { get; set; }
        public string NugetName { get; set; }
        public string Version { get; set; }
        public bool SupportNet45 { get; set; }
        public bool SupportStandard20 { get; set; }
        public bool SupportStandard21 { get; set; }
        public bool SupportNetCore22 => SupportStandard20;
        public bool SupportNetCore31 => SupportStandard21;

        public Home_IndexVD_AssemblyModel(string title, string name, Type versionType, string nugetName = null, bool supportNet45 = true, bool supportStandard20 = true, bool supportStandard21 = true)
        {
            Func<Version, string> getDisplayVersion = version => Regex.Match(version.ToString(), @"\d+\.\d+\.\d+").Value;


            Func<Type, string> getTypeVersionInfo = type =>
            {
                var version = System.Reflection.Assembly.GetAssembly(type).GetName().Version;
                return getDisplayVersion(version);
            };


            Title = title;
            AssemblyName = name;
            Version = getTypeVersionInfo(versionType);
            NugetName = nugetName ?? AssemblyName;
            SupportNet45 = supportNet45;
            SupportStandard20 = supportStandard20;
            SupportStandard21 = supportStandard21;
        }
    }
}
