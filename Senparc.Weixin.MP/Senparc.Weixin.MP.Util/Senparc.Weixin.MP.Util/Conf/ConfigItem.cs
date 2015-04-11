
using System;
using System.Runtime.InteropServices;
using AtNet.DevFw.Framework;
using System.IO;
using System.Text.RegularExpressions;

namespace Senparc.Weixin.MP.Util.Conf
{
    public class ConfigItem
    {
        public  string Token;

        public  string AppId;
        public String AppName;

        public  string AppSecret;

        /// <summary>
        /// 解密字符串
        /// </summary>
        public  string AppEncodeString;
        public  string ApiDomain;         //
        public  string MenuButtons;       //自定义菜单
        public  string WxEnterMessage; //微信进入文字
        public  string WxDefaultResponseMessage;  //微信默认自动回复消息
        public  bool DebugMode = false;
        private readonly string _filePath;
        private string _key;
        private readonly SettingFile _file;
        private static readonly Regex FileNameParttner = new Regex("/([^/]+).config");

        /// <summary>
        /// 欢迎文字
        /// </summary>
        public string WxWelcomeMessage;

        public ConfigItem(string filePath)
        {
            this._filePath = filePath.Replace("\\","/");
            if (FileNameParttner.IsMatch(this._filePath))
            {
                bool isExists = File.Exists(this._filePath);
                this._file = new SettingFile(this._filePath);
                if (isExists)
                {
                    this.Deserialize();
                }
            }
        }

        /// <summary>
        /// 解码
        /// </summary>
        private void Deserialize()
        {
            this.AppId = this._file.Get("Weixin_AppId")??"";
            this.AppName = this._file.Get("Weixin_AppName") ?? "";
            this.AppSecret = this._file.Get("Weixin_AppSecret")??"";
            this.Token = this._file.Get("Weixin_Token") ?? "";
            this.AppEncodeString =this._file.Get("Weixin_AppEncodeString")??"";
            this.ApiDomain = this._file.Get("Weixin_ApiDomain")??"";
            this.MenuButtons = this._file.Get("Weixin_MenuButtons")??"[]";
            this.WxWelcomeMessage = this._file.Get("Weixin_WelcomeMessage") ?? "";
            this.WxEnterMessage = this._file.Get("Weixin_EnterMessage") ?? "";
            this.WxDefaultResponseMessage = this._file.Get("Weixin_DefaultResponseMessage")?? "";
        }

        /// <summary>
        /// 编码
        /// </summary>
        private void Serialize()
        {
            this._file.Set("Weixin_AppId", this.AppId);
            this._file.Set("Weixin_AppName", this.AppName);
            this._file.Set("Weixin_AppSecret", this.AppSecret);
            this._file.Set("Weixin_Token",this.Token);
            this._file.Set("Weixin_AppEncodeString",this.AppEncodeString);
            this._file.Set("Weixin_WelcomeMessage",this.WxWelcomeMessage);
            this._file.Set("Weixin_EnterMessage",this.WxEnterMessage);
            this._file.Set("Weixin_DefaultResponseMessage",this.WxDefaultResponseMessage);
            this._file.Set("Weixin_ApiDomain", this.ApiDomain);
            this._file.Set("Weixin_MenuButtons",this.MenuButtons??"[]");
        }

        /// <summary>
        /// 
        /// </summary>
        public void Flush()
        {
            this.Serialize();
            this._file.Flush();
        }

        #region 按钮模板

        /*
         {
     "button":[
     {    
          "type":"click",
          "name":"今日歌曲",
          "key":"V1001_TODAY_MUSIC"
      },
      {
           "name":"菜单",
           "sub_button":[
           {    
               "type":"view",
               "name":"搜索",
               "url":"http://www.soso.com/"
            }
           ]
       }]
 }
         */

        #endregion

        /// <summary>
        /// Get Weixin Key
        /// </summary>
        /// <returns></returns>
        public string GetKey()
        {
            if (this._key == null)
            {
                this._key = FileNameParttner.Match(this._filePath).Groups[1].Value;
            }
            return this._key;
        }

        /// <summary>
        /// 自定义处理
        /// </summary>
        public IAppCustomHandler CustomHandler { get; set; }
    }
}
