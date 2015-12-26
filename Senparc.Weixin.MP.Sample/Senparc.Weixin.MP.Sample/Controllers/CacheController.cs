using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.Containers;
using Senparc.Weixin.MessageQueue;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    internal class TestContainerBag1 : BaseContainerBag
    {
        private DateTime _dateTime;

        public DateTime DateTime
        {
            get { return _dateTime; }
            set { this.SetContainerProperty(ref _dateTime, value); }
        }
    }

    internal class TestContainerBag2 : BaseContainerBag
    {
        private DateTime _dateTime;

        public DateTime DateTime
        {
            get { return _dateTime; }
            set { this.SetContainerProperty(ref _dateTime, value); }

        }
    }

    internal class TestContainer1 : BaseContainer<TestContainerBag1>
    {
    }
    internal class TestContainer2 : BaseContainer<TestContainerBag2>
    {
    }

    /// <summary>
    /// 测试缓存
    /// </summary>
    public class CacheController : Controller
    {
        public ActionResult Test()
        {
            var sb = new StringBuilder();

            var key = DateTime.Now.Ticks.ToString();
            var bag = new TestContainerBag1()
            {
                Key = key,
                DateTime = DateTime.Now
            };
            TestContainer1.Update(key, bag);
            sb.AppendFormat("{0}：{1}<br />", "bag.DateTime", bag.DateTime);

            bag.DateTime = DateTime.MinValue;//进行修改

            //读取列队
            var mq = new SenparcMessageQueue();
            var mqKey = BaseContainerBag.GenerateKey(typeof(TestContainerBag1), bag);
            var mqItem = mq.GetItem(mqKey);
            sb.AppendFormat("{0}：{1}<br />", "bag.DateTime", bag.DateTime);
            sb.AppendFormat("{0}：{1}<br />", "已经加入列队", mqItem != null);

            if (mqItem!=null)
            {

            }

            return Content(sb.ToString());
        }
    }
}