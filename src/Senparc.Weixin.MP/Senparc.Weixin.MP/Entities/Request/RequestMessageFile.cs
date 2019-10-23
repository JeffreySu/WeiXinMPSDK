#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

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
    Copyright (C) 2019 Senparc
    
    文件名：RequestMessageFile.cs
    文件功能描述：接收文件消息
    
    
    创建标识：Senparc - 20180227
    
----------------------------------------------------------------*/

using Senparc.NeuChar;
using Senparc.NeuChar.Entities;

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageFile : RequestMessageBase, IRequestMessageFile
    {
        public override RequestMsgType MsgType
        {
            get { return RequestMsgType.File; }
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string FileKey { get; set; }
        public string FileMd5 { get; set; }
        public long FileTotalLen { get; set; }
    }
}
