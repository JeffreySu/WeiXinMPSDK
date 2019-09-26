using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Cache.Redis;
using Senparc.Weixin.Containers;
using Senparc.Weixin.Helpers;
using Senparc.CO2NET.MessageQueue;
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.Cache.Redis;
using Senparc.CO2NET.Trace;

namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    [Serializable]
    internal class TestContainerBag1 : BaseContainerBag
    {
        //private DateTimeOffset _dateTime;

        public DateTimeOffset DateTime { get; set; }
        //{
        //    get { return _dateTime; }
        //    set { this.SetContainerProperty(ref _dateTime, value); }
        //}
    }

    //[Serializable]
    //internal class TestContainerBag2 : BaseContainerBag
    //{
    //    private DateTimeOffset _dateTime;
    //    public DateTimeOffset DateTime
    //    {
    //        get { return _dateTime; }
    //        set { this.SetContainerProperty(ref _dateTime, value); }

    //    }
    //}

    internal class TestContainer1 : BaseContainer<TestContainerBag1>
    {
    }

    //internal class TestContainer2 : BaseContainer<TestContainerBag2>
    //{
    //}

    /// <summary>
    /// 测试缓存
    /// </summary>
    public class CacheController : BaseController
    {
        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Redis(int id = 1)
        {
            //测试Redis ItemCollection缓存更新功能

            var sb = new StringBuilder();
            if (id == 1)
            {
                sb.Append("使用Redis<br>");
                CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisObjectCacheStrategy.Instance);
            }
            else
            {
                sb.Append("使用本地缓存<br>");
                CacheStrategyFactory.RegisterObjectCacheStrategy(null);//注意：此处不能输入()=>null，这样仍然是一个有内容的委托！
            }


            //var cacheKey = TestContainer1.GetContainerCacheKey();
            var containerCacheStrategy = ContainerCacheStrategyFactory.GetContainerCacheStrategyInstance()/*.ContainerCacheStrategy*/;
            sb.AppendFormat("ContainerCacheStrategy：{0}<br />", containerCacheStrategy.GetType());

            var itemCollection = containerCacheStrategy.GetAll<TestContainerBag1>();

            sb.AppendFormat("Count1：{0}<br />", itemCollection != null ? itemCollection.Count() : -1);


            var bagKey = "Redis." + SystemTime.Now.ToString("yyyy-MM-dd--HH-mm-ss-ffff");
            var bag = new TestContainerBag1()
            {
                Key = bagKey,
                DateTime = SystemTime.Now
            };

            TestContainer1.Update(bagKey, bag,TimeSpan.FromHours(1));//更新到缓存（立即更新）

            itemCollection = containerCacheStrategy.GetAll<TestContainerBag1>();

            sb.AppendFormat("Count2：{0}<br />", itemCollection != null ? itemCollection.Count() : -1);

            if (itemCollection != null)
            {
                itemCollection[SystemTime.Now.Ticks.ToString()] = bag;
            }

            itemCollection = containerCacheStrategy.GetAll<TestContainerBag1>();//如果是分布式缓存，这里的数字通常不会变

            sb.AppendFormat("Count3：{0}<br />", itemCollection != null ? itemCollection.Count() : -1);

            return Content(sb.ToString(), "text/html; charset=utf-8");
        }

        [HttpPost]
        public ActionResult RunTest()
        {
            var sb = new StringBuilder();
            //var containerCacheStrategy = CacheStrategyFactory.GetContainerCacheStrategyInstance();
            var containerCacheStrategy = ContainerCacheStrategyFactory.GetContainerCacheStrategyInstance()/*.ContainerCacheStrategy*/;
            var baseCacheStrategy = containerCacheStrategy.BaseCacheStrategy();
            sb.AppendFormat("{0}：{1}<br />", "当前缓存策略", containerCacheStrategy.GetType().Name);

            var finalExisted = false;
            for (int i = 0; i < 3; i++)
            {
                sb.AppendFormat("<br />====== {0}：{1} ======<br /><br />", "开始一轮测试", i + 1);
                var shortBagKey = SystemTime.Now.ToString("yyyyMMdd-HHmmss");
                var finalBagKey = baseCacheStrategy.GetFinalKey(ContainerHelper.GetItemCacheKey(typeof(TestContainerBag1), shortBagKey));//获取最终缓存中的键
                var bag = new TestContainerBag1()
                {
                    Key = shortBagKey,
                    DateTime = SystemTime.Now
                };
                TestContainer1.Update(shortBagKey, bag,TimeSpan.FromHours(1)); //更新到缓存（立即更新）
                sb.AppendFormat("{0}：{1}<br />", "bag.DateTime", bag.DateTime.ToString("o"));

                Thread.Sleep(1);

                bag.DateTime = SystemTime.Now; //进行修改

                //读取队列
                var mq = new SenparcMessageQueue();
                var mqKey = SenparcMessageQueue.GenerateKey("ContainerBag", bag.GetType(), bag.Key, "UpdateContainerBag");
                var mqItem = mq.GetItem(mqKey);
                sb.AppendFormat("{0}：{1}<br />", "bag.DateTime", bag.DateTime.ToString("o"));
                sb.AppendFormat("{0}：{1}<br />", "已经加入队列", mqItem != null);
                sb.AppendFormat("{0}：{1}<br />", "当前消息队列数量（未更新缓存）", mq.GetCount());

                if (mq.GetCount() >= 3)
                {
                    //超过3是不正常的，或有其他外部干扰
                    sb.AppendFormat("<br>=====MQ队列（{0}）start=======<br>", mq.GetCount());
                    foreach (var item in SenparcMessageQueue.MessageQueueDictionary)
                    {
                        sb.AppendFormat("{0}：{1}<br />", item.Key, item.Value.Key);
                    }

                    sb.AppendFormat("=====MQ队列（{0}）=======<br><br>", mq.GetCount());
                }

                var itemCollection = containerCacheStrategy.GetAll<TestContainerBag1>();

                var existed = itemCollection.ContainsKey(finalBagKey);
                sb.AppendFormat("{0}：{1}<br />", "当前缓存是否存在", existed);
                sb.AppendFormat("{0}：{1}<br />", "插入缓存时间",
                    !existed ? "不存在" : itemCollection[finalBagKey].CacheTime.ToString("o")); //应为0

                var waitSeconds = i;
                sb.AppendFormat("{0}：{1}<br />", "操作", "等待" + waitSeconds + "秒");
                Thread.Sleep(waitSeconds * 1000); //队列线程默认轮询等待时间为1秒，当等待时间超过1秒时，应该都已经处理完毕

                sb.AppendFormat("{0}：{1}<br />", "当前消息队列数量（未更新缓存）", mq.GetCount());

                itemCollection = containerCacheStrategy.GetAll<TestContainerBag1>();
                existed = itemCollection.ContainsKey(finalBagKey);
                finalExisted = existed && itemCollection[finalBagKey].CacheTime.Date == SystemTime.Now.Date;
                sb.AppendFormat("{0}：{1}<br />", "当前缓存是否存在", existed);
                sb.AppendFormat("{0}：{1}<br />", "插入缓存时间",
                    !existed ? "不存在" : itemCollection[finalBagKey].CacheTime.ToString("o")); //应为当前加入到缓存的最新时间

            }

            sb.AppendFormat("<br />============<br /><br />");
            sb.AppendFormat("{0}：{1}<br />", "测试结果", !finalExisted ? "失败" : "成功");

            return Content(sb.ToString());
            //ViewData["Result"] = sb.ToString();
            //return View();
        }
    }
}