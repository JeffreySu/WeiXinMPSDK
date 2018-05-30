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
    
    文件名：ReflectionHelper.cs
    文件功能描述：反射帮助类
    参考文章：http://www.cnblogs.com/zfanlong1314/p/4197383.html
    
    创建标识：Senparc - 20170702

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Senparc.Weixin.Helpers
{
    /// <summary>
    /// 反射帮助类
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fullName">命名空间.类型名</param>
        /// <param name="assemblyName">程序集</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string fullName, string assemblyName)
        {
            string path = fullName + "," + assemblyName;//命名空间.类型名,程序集
            Type o = Type.GetType(path);//加载类型
            object obj = Activator.CreateInstance(o, true);//根据类型创建实例
            return (T)obj;//类型转换并返回
        }

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T">要创建对象的类型</typeparam>
        /// <param name="assemblyName">类型所在程序集名称</param>
        /// <param name="nameSpace">类型所在命名空间</param>
        /// <param name="className">类型名</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string assemblyName, string nameSpace, string className)
        {
            try
            {
                string fullName = nameSpace + "." + className;//命名空间.类型名
                                                              //此为第一种写法
#if NETSTANDARD1_6 || NETSTANDARD2_0 || NETCOREAPP2_0 || NETCOREAPP2_1
                object ect = Assembly.Load(new AssemblyName(assemblyName)).CreateInstance(fullName);//加载程序集，创建程序集里面的 命名空间.类型名 实例s
#else
                object ect = Assembly.Load(assemblyName).CreateInstance(fullName);//加载程序集，创建程序集里面的 命名空间.类型名 实例
#endif
                return (T)ect;//类型转换并返回
                //下面是第二种写法
                //string path = fullName + "," + assemblyName;//命名空间.类型名,程序集
                //Type o = Type.GetType(path);//加载类型
                //object obj = Activator.CreateInstance(o, true);//根据类型创建实例
                //return (T)obj;//类型转换并返回
            }
            catch
            {
                //发生异常，返回类型的默认值
                return default(T);
            }
        }
    }
}
