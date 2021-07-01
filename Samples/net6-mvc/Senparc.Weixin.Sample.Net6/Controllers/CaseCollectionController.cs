using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Senparc.Weixin.Sample.NetCore3.Controllers
{
    public class CaseCollectionController : BaseController
    {
        private readonly IHostingEnvironment _env;

        public CaseCollectionController(IHostingEnvironment env)
        {
            this._env = env;
        }

        /// <summary>
        /// 收集案例页面
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {



            return View();
        }

        ///// <summary>
        ///// 提交案例申请
        ///// </summary>
        ///// <param name="file"></param>
        ///// <param name="accountName"></param>
        ///// <param name="intruduce"></param>
        ///// <param name="phone"></param>
        ///// <param name="qq"></param>
        ///// <param name="wechat"></param>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<IActionResult> Index(IFormFile file, string accountName, string intruduce, string phone, string qq, string wechat, string name)
        //{
        //    var files = Request.Form.Files;
        //    long size = files.Sum(f => f.Length);
        //    string webRootPath = _env.WebRootPath;
        //    string contentRootPath = _env.ContentRootPath;



        //    return View();
        //}
    }
}
