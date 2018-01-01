## 异步方法标记

严格按照以下换行格式：

``` C#
#if !NET35 && !NET40
        #region 异步方法
        
        /// <summary>
        /// 【异步方法】修改服务器地址 接口
        /// </summary>
        public static async Task<ModifyDomainResultJson> ModifyDomainAsync()
        {
            //...
        }
        
        #endregion
#endif

```

> 重点注意：<br>
> 1、`#if` 和 `#region 异步方法` 中间没有空行，结束标签也是如此。<br>
> 2、方法名称备注的 summary 信息必须以 `【异步方法】` 开头。<br>
> 3、 #region 的下一行、#endregion 的上一行，应该为空行。
