#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2017 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2017 Senparc

    文件名：MediaFile.cs
    文件功能描述：媒体文件载体(用于推送文件)


    创建标识：mccj - 20170902

----------------------------------------------------------------*/


using Senparc.Weixin.Helpers;
using System;
using System.IO;

namespace Senparc.Weixin.HttpUtility
{
    /// <summary>
    /// 媒体文件接口
    /// </summary>
    public interface IMedia
    {
        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <returns></returns>
        string GetFileName();
        /// <summary>
        /// 获取文件数据流
        /// </summary>
        /// <returns></returns>
        Stream GetStream();
    }
    /// <summary>
    /// 基于文件的媒体实现
    /// </summary>
    public class MediaFile : IMedia
    {
        private readonly string _fileName;
        public MediaFile(string fileName)
        {
            _fileName = fileName;
        }
        public string GetFileName()
        {
            return _fileName;
        }

        public Stream GetStream()
        {
            return FileHelper.GetFileStream(_fileName);
        }
    }
    /// <summary>
    /// 基于字节的媒体实现
    /// </summary>
    public class MediaBytes : IMedia
    {
        private readonly Func<byte[]> _getBytes;
        public MediaBytes(byte[] bytes) : this(() => bytes) { }
        public MediaBytes(Func<byte[]> getBytes)
        {
            _getBytes = getBytes;
        }
        public string GetFileName()
        {
            return System.Guid.NewGuid().ToString("N");
        }
        public Stream GetStream()
        {
            var stream = new MemoryStream();
            var bytes = _getBytes();
            stream.Write(bytes, 0, bytes.Length);
            return stream;
        }
    }
    /// <summary>
    /// 基于流的媒体实现
    /// </summary>
    public class MediaStream : IMedia
    {
        private readonly Func<Stream> _getStream;
        public MediaStream(Stream stream) : this(() => stream) { }
        public MediaStream(Func<Stream> getStream)
        {
            _getStream = getStream;
        }
        public string GetFileName()
        {
            return System.Guid.NewGuid().ToString("N");
        }
        public Stream GetStream()
        {
            var stream = _getStream();
            stream.Position = 0;
            return stream;
        }
    }
}
