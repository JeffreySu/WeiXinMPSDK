using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.Containers
{
    /// <summary>
    /// 储存 Container 注册过程方法的集合
    /// </summary>
    /// <typeparam name="TBag"></typeparam>
    public class BaseContainerRegisterFuncCollection<TBag> : Dictionary<string, Func<Task<TBag>>>
         where TBag : class, IBaseContainerBag, new()
    {


    }
}
