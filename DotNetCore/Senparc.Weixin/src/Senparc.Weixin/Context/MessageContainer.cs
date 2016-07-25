/*----------------------------------------------------------------
    Copyright (C) 2016 Senparc
    
    文件名：MessageContainer.cs
    文件功能描述：微信消息容器
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System.Collections.Generic;

namespace Senparc.Weixin.Context
{
    public class MessageContainer<T> : List<T> 
        //where T : IMessageBase
    {
        /// <summary>
        /// 最大记录条数（保留尾部），如果小于等于0则不限制
        /// </summary>
        public int MaxRecordCount { get; set; }

        public MessageContainer()
        {
        }

        public MessageContainer(int maxRecordCount)
        {
            MaxRecordCount = maxRecordCount;
        }

        new public void Add(T item)
        {
            base.Add(item);
            RemoveExpressItems();
        }

        private void RemoveExpressItems()
        {
            if (MaxRecordCount > 0 && base.Count > MaxRecordCount)
            {
                base.RemoveRange(0, base.Count - MaxRecordCount);
            }
        }

        new public void AddRange(IEnumerable<T> collection)
        {
            base.AddRange(collection);
            RemoveExpressItems();
        }

        new public void Insert(int index, T item)
        {
            base.Insert(index, item);
            RemoveExpressItems();
        }

        new public void InsertRange(int index, IEnumerable<T> collection)
        {
            base.InsertRange(index, collection);
            RemoveExpressItems();
        }
    }
}
