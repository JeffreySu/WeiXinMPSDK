using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Work.Entities
{
    [Serializable]
    public class FuncGetIdSecretResult
    {
        public string CorpId { get; set; }
        /// <summary>
        /// CorpSecret 2.可能是授权企业永久授权码 permanent_code 3.也可能是第三方应用secret
        /// </summary>
        public string Secret { get; set; }
    }
}
