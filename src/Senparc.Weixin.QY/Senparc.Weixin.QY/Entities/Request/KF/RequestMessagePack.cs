using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Senparc.Weixin.Entities;

namespace Senparc.Weixin.QY.Entities.Request.KF
{
    public class RequestPack : IEntityBase
    {
        public AgentType AgentType { get; set; }
        public string ToUserName { get; set; }
        public int ItemCount { get; set; }
        public List<RequestBase> Items { get; set; }
        public long PackageId { get; set; }
    }

    public enum AgentType
    {
        /// <summary>
        /// 内部客服
        /// </summary>
        kf_internal,
        /// <summary>
        /// 外部客服
        /// </summary>
        kf_external
    }
}
