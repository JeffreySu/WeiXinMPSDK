using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Senparc.Weixin.Sample.NetCore3.Models.VD
{
    public class Home_IndexVD
    {
        public Dictionary<Home_IndexVD_GroupInfo, List<Home_IndexVD_AssemblyModel>> AssemblyModelCollection { get; set; }
    }

    public class Home_IndexVD_GroupInfo
    {
        public string Title { get; set; }
        public string Description { get; set; }
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
        public string GitHubUrl { get; set; }

        public Home_IndexVD_AssemblyModel(string title, string name, Type versionType, string nugetName = null, string gitHubUrl = null, bool supportNet45 = true, bool supportStandard20 = true, bool supportStandard21 = true)
        {
            Title = title;
            AssemblyName = name;
            Version = GetTypeVersionInfo(versionType);
            NugetName = nugetName ?? AssemblyName;
            SupportNet45 = supportNet45;
            SupportStandard20 = supportStandard20;
            SupportStandard21 = supportStandard21;
            GitHubUrl = gitHubUrl;
        }

        internal static Func<Version, string> GetDisplayVersion = version => Regex.Match(version.ToString(), @"\d+\.\d+\.\d+").Value;

        internal static Func<Type, string> GetTypeVersionInfo = type =>
         {
             var version = System.Reflection.Assembly.GetAssembly(type).GetName().Version;
             return GetDisplayVersion(version);
         };
    }
}
