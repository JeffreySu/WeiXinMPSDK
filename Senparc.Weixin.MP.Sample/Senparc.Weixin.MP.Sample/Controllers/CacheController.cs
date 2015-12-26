using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Senparc.Weixin.Cache;
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
            var containerCacheStragegy = CacheStrategyFactory.GetContainerCacheStragegyInstance();
            sb.AppendFormat("{0}：{1}<br /><br />", "当前缓存策略", containerCacheStragegy.GetType().Name);


            for (int i = 0; i < 3; i++)
            {
                sb.AppendFormat("{0}：{1}<br /><br />", "开始一轮测试", i + 1);
                var bagKey = DateTime.Now.Ticks.ToString();
                var bag = new TestContainerBag1()
                {
                    Key = bagKey,
                    DateTime = DateTime.Now
                };
                TestContainer1.Update(bagKey, bag);
                sb.AppendFormat("{0}：{1}（Ticks：{2}）<br />", "bag.DateTime", bag.DateTime, bag.DateTime.Ticks);

                bag.DateTime = DateTime.Now;//进行修改

                //读取列队
                var mq = new SenparcMessageQueue();
                var mqKey = SenparcMessageQueue.GenerateKey("ContainerBag", bag.GetType(), bag.Key, "UpdateContainerBag");
                var mqItem = mq.GetItem(mqKey);
                sb.AppendFormat("{0}：{1}（Ticks：{2}）<br />", "bag.DateTime", bag.DateTime, bag.DateTime.Ticks);
                sb.AppendFormat("{0}：{1}<br />", "已经加入列队", mqItem != null);
                sb.AppendFormat("{0}：{1}<br />", "当前消息列队数量（未更新缓存）", mq.GetCount());

                var cacheKey = TestContainer1.GetCacheKey();
                var itemCollection = containerCacheStragegy.Get(cacheKey);
                sb.AppendFormat("{0}：{1}<br />", "当前缓存是否存在", itemCollection.ContainsKey(bagKey));
                sb.AppendFormat("{0}：{1}<br />", "当前缓存时间", itemCollection[bagKey].CacheTime.Ticks);//应为0

                var waitSeconds = i;
                sb.AppendFormat("{0}：{1}<br />", "操作", "等待" + waitSeconds + "秒");
                Thread.Sleep(waitSeconds * 1000);//线程默认轮询等待时间为2秒
                sb.AppendFormat("{0}：{1}<br />", "当前消息列队数量（未更新缓存）", mq.GetCount());

                itemCollection = containerCacheStragegy.Get(cacheKey);
                sb.AppendFormat("{0}：{1}<br />", "当前缓存是否存在", itemCollection.ContainsKey(bagKey));
                sb.AppendFormat("{0}：{1}（Ticks：{2}）<br />", "插入缓存时间", itemCollection[bagKey].CacheTime, itemCollection[bagKey].CacheTime.Ticks);//应为当前加入到缓存的最新时间
                sb.AppendLine("=============<br /><br />");
            }


            return Content(sb.ToString());
        }
    }
}