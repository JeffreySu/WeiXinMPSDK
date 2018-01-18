
#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2018 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

#if !NET35
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace Senparc.Weixin.Helpers
{
    /// <summary>
    /// IDictionary扩展方法
    /// 代码引用自：https://bitbucket.org/patridge/expandoobject-json-serialization-tests/src/eab6ef0b18434278a5c2c1424145c47d1def2838/ExpandoJsonMvcStub/Helpers/IDictionaryExtensions.cs?at=default&fileviewer=file-view-default
    /// 更多解决方案：http://www.patridgedev.com/2011/08/24/getting-dynamic-expandoobject-to-serialize-to-json-as-expected/
    /// </summary>
    public static class IDictionaryExtensions
    {
        /// <summary>
        /// Extension method that turns a dictionary of string and object to an ExpandoObject
        /// Snagged from http://theburningmonk.com/2011/05/idictionarystring-object-to-expandoobject-extension-method/
        /// </summary>
        public static ExpandoObject ToExpando(this IDictionary<string, object> dictionary)
        {
            var expando = new ExpandoObject();
            var expandoDic = (IDictionary<string, object>)expando;

            // go through the items in the dictionary and copy over the key value pairs)
            foreach (var kvp in dictionary)
            {
                // if the value can also be turned into an ExpandoObject, then do it!
                if (kvp.Value is IDictionary<string, object>)
                {
                    var expandoValue = ((IDictionary<string, object>)kvp.Value).ToExpando();
                    expandoDic.Add(kvp.Key, expandoValue);
                }
                else if (kvp.Value is ICollection)
                {
                    // iterate through the collection and convert any strin-object dictionaries
                    // along the way into expando objects
                    var itemList = new List<object>();
                    foreach (var item in (ICollection)kvp.Value)
                    {
                        if (item is IDictionary<string, object>)
                        {
                            var expandoItem = ((IDictionary<string, object>)item).ToExpando();
                            itemList.Add(expandoItem);
                        }
                        else
                        {
                            itemList.Add(item);
                        }
                    }

                    expandoDic.Add(kvp.Key, itemList);
                }
                else
                {
                    expandoDic.Add(kvp);
                }
            }

            return expando;
        }
    }
}
#endif