using Senparc.CO2NET.Extensions;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Trace;
using Senparc.CO2NET.Utilities;
using Senparc.NeuChar;
using Senparc.NeuChar.Entities;
using Senparc.NeuChar.MessageHandlers;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Weixin.MP.MessageHandlers
{
    public abstract partial class MessageHandler<TC>
    {
        static MessageHandler()
        {
            Senparc.NeuChar.Register.RegisterNeuralNode("MessageHandlerNode", typeof(MessageHandlerNode));
        }

        #region NeuChar 方法

        /// <summary>
        /// NeuChar 请求
        /// </summary>
        public virtual IResponseMessageBase OnNeuCharRequest(RequestMessageNeuChar requestMessage)
        {
            try
            {
                var path = ServerUtility.ContentRootMapPath("~/App_Data/NeuChar");
                var file = Path.Combine(path, "NeuCharRoot.config");
                string result = null;

                switch (requestMessage.NeuCharMessageType)
                {
                    case NeuCharActionType.GetConfig:
                        {
                            if (File.Exists(file))
                            {
                                using (var fs = FileHelper.GetFileStream(file))
                                {
                                    using (var sr = new StreamReader(fs, Encoding.UTF8))
                                    {
                                        var json = sr.ReadToEnd();
                                        result = json;
                                    }
                                }
                            }
                            else
                            {
                                result = "{}";//TODO:初始化一个对象
                            }
                        }
                        break;
                    case NeuCharActionType.SaveConfig:
                        {
                            var configRootJson = requestMessage.ConfigRoot;
                            SenparcTrace.SendCustomLog("收到NeuCharRequest", configRootJson);
                            var configRoot = SerializerHelper.GetObject<ConfigRoot>(configRootJson);//这里只做序列化校验

                            //TODO:进行验证


                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            var fileBak = Path.Combine(path, "NeuCharRoot.bak.config");
                            //TODO：后期也可以考虑把不同模块分离到不同的文件中

                            File.Delete(fileBak);

                            using (var fs = new FileStream(fileBak, FileMode.CreateNew))
                            {
                                using (var sw = new StreamWriter(fs))
                                {
                                    sw.Write(configRootJson);
                                    sw.Flush();
                                }
                            }

                            //替换备份文件
                            File.Delete(file);
                            File.Move(fileBak, file);

                            //刷新数据
                            var neuralSystem = NeuralSystem.Instance;
                            neuralSystem.ReloadNode();
                        }
                        break;
                    default:
                        break;
                }
          

                var successMsg = new
                {
                    success = true,
                    result = result
                };
                TextResponseMessage = successMsg.ToJson();
            }
            catch (Exception ex)
            {
                var errMsg = new
                {
                    success = false,
                    result = ex.Message
                };
                TextResponseMessage = errMsg.ToJson();
            }

            return null;
        }

        #endregion
    }
}
