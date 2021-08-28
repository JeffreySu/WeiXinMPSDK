using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.TenPayV3.Entities
{
    /// <summary>
    /// 储存公钥
    /// <para>Key：serial_no，Value：Key</para>
    /// </summary>
    public class PublicKeyCollection : ConcurrentDictionary<string, string>
    {
    }
}
