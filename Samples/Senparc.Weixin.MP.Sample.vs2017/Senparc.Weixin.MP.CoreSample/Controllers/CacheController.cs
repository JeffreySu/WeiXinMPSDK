﻿using System;
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
using Senparc.Weixin.MessageQueue;

namespace Senparc.Weixin.MP.CoreSample.Controllers
{
    [Serializable]
    internal class TestContainerBag1 : BaseContainerBag
    {
        private DateTime _dateTime;

        public DateTime DateTime
        {
            get { return _dateTime; }
            set { this.SetContainerProperty(ref _dateTime, value); }
        }
    }

    [Serializable]
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
    public class CacheController : BaseController
    {

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Redis(int id = 1)
        {
            //测试Redis ItemCollection缓存更新功能

            if (id == 1)
            {
                CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisObjectCacheStrategy.Instance);
            }
            else
            {
                CacheStrategyFactory.RegisterObjectCacheStrategy(() => null);
            }

            var sb = new StringBuilder();
            //var cacheKey = TestContainer1.GetContainerCacheKey();
            var containerCacheStrategy = CacheStrategyFactory.GetObjectCacheStrategyInstance().ContainerCacheStrategy;
            var itemCollection = containerCacheStrategy.GetAll<TestContainerBag1>();

            sb.AppendFormat("Count1：{0}<br />", itemCollection != null ? itemCollection.Count() : -1);


            var bagKey = "Redis." + DateTime.Now.ToString("yyyy-MM-dd:HH-mm-ss-ffff");
            var bag = new TestContainerBag1()
            {
                Key = bagKey,
                DateTime = DateTime.Now
            };
            TestContainer1.Update(bagKey, bag);//更新到缓存（队列）

            itemCollection = containerCacheStrategy.GetAll<TestContainerBag1>();

            sb.AppendFormat("Count2：{0}<br />", itemCollection != null ? itemCollection.Count() : -1);

            if (itemCollection != null)
            {
                itemCollection[DateTime.Now.Ticks.ToString()] = bag;
            }

            sb.AppendFormat("Count3：{0}<br />", itemCollection != null ? itemCollection.Count() : -1);

            return Content(sb.ToString());
        }

        [HttpPost]
        public ActionResult RunTest()
        {
            var sb = new StringBuilder();
            //var containerCacheStrategy = CacheStrategyFactory.GetContainerCacheStrategyInstance();
            var containerCacheStrategy = CacheStrategyFactory.GetObjectCacheStrategyInstance().ContainerCacheStrategy;
            sb.AppendFormat("{0}：{1}<br />", "当前缓存策略", containerCacheStrategy.GetType().Name);

            var finalExisted = false;
            for (int i = 0; i < 3; i++)
            {
                sb.AppendFormat("<br />====== {0}：{1} ======<br /><br />", "开始一轮测试", i + 1);
                var shortBagKey = DateTime.Now.Ticks.ToString();
                var finalBagKey = containerCacheStrategy.GetFinalKey(ContainerHelper.GetItemCacheKey(typeof(TestContainerBag1), shortBagKey));//获取最终缓存中的键
                var bag = new TestContainerBag1()
                {
                    Key = shortBagKey,
                    DateTime = DateTime.Now
                };
                TestContainer1.Update(shortBagKey, bag); //更新到缓存（队列）
                sb.AppendFormat("{0}：{1}（Ticks：{2}）<br />", "bag.DateTime", bag.DateTime.ToLongTimeString(),
                    bag.DateTime.Ticks);

                Thread.Sleep(1);

                bag.DateTime = DateTime.Now; //进行修改

                //读取队列
                var mq = new SenparcMessageQueue();
                var mqKey = SenparcMessageQueue.GenerateKey("ContainerBag", bag.GetType(), bag.Key, "UpdateContainerBag");
                var mqItem = mq.GetItem(mqKey);
                sb.AppendFormat("{0}：{1}（Ticks：{2}）<br />", "bag.DateTime", bag.DateTime.ToLongTimeString(),
                    bag.DateTime.Ticks);
                sb.AppendFormat("{0}：{1}<br />", "已经加入队列", mqItem != null);
                sb.AppendFormat("{0}：{1}<br />", "当前消息队列数量（未更新缓存）", mq.GetCount());

                var itemCollection = containerCacheStrategy.GetAll<TestContainerBag1>();
                var existed = itemCollection.ContainsKey(finalBagKey);
                sb.AppendFormat("{0}：{1}<br />", "当前缓存是否存在", existed);
                sb.AppendFormat("{0}：{1}<br />", "插入缓存时间",
                    !existed ? "不存在" : itemCollection[finalBagKey].CacheTime.Ticks.ToString()); //应为0

                var waitSeconds = i;
                sb.AppendFormat("{0}：{1}<br />", "操作", "等待" + waitSeconds + "秒");
                Thread.Sleep(waitSeconds * 1000); //线程默认轮询等待时间为2秒
                sb.AppendFormat("{0}：{1}<br />", "当前消息队列数量（未更新缓存）", mq.GetCount());

                itemCollection = containerCacheStrategy.GetAll<TestContainerBag1>();
                existed = itemCollection.ContainsKey(finalBagKey);
                finalExisted = existed;
                sb.AppendFormat("{0}：{1}<br />", "当前缓存是否存在", existed);
                sb.AppendFormat("{0}：{1}（Ticks：{2}）<br />", "插入缓存时间",
                    !existed ? "不存在" : itemCollection[finalBagKey].CacheTime.ToLongTimeString(),
                    !existed ? "不存在" : itemCollection[finalBagKey].CacheTime.Ticks.ToString()); //应为当前加入到缓存的最新时间

            }

            sb.AppendFormat("<br />============<br /><br />");
            sb.AppendFormat("{0}：{1}<br />", "测试结果", !finalExisted ? "失败" : "成功");

            return Content(sb.ToString());
            //ViewData["Result"] = sb.ToString();
            //return View();
        }
    }
}