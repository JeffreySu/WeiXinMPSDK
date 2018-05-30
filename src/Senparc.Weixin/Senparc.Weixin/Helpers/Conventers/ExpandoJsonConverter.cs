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

/*----------------------------------------------------------------
    Copyright (C) 2018 Senparc
    
    文件名：ExpandoJsonConverter.cs
    文件功能描述：Expando-JSON字符串转换
    
    
    创建标识：Senparc - 20151002
    
----------------------------------------------------------------*/

#if NET40 || NET45

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Web.Script.Serialization;

namespace Senparc.Weixin.Helpers
{
    /// <summary>
    /// Allows JSON serialization of Expando objects into expected results (e.g., "x: 1, y: 2") instead of the default dictionary serialization.
    /// </summary>
    public class ExpandoJsonConverter : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            // See source code link for this extension method at the bottom of this post (/Helpers/IDictionaryExtensions.cs)
            return dictionary.ToExpando();
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var result = new Dictionary<string, object>();
            var dictionary = obj as IDictionary<string, object>;
            foreach (var item in dictionary)
                result.Add(item.Key, item.Value);
            return result;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                return new ReadOnlyCollection<Type>(new Type[] { typeof(ExpandoObject) });
            }
        }
    }
}
#endif
