using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{
    public class ShelvesResult
    {
        public int errcode { get; set; }//错误码
        public string errmsg { get; set; }//错误信息
    }

    /// <summary>
    /// 添加货架返回结果
    /// </summary>
    public class AddShelvesResult : ShelvesResult
    {
        public int shelf_id { get; set; }//货架ID
    }

    /// <summary>
    /// 删除货架返回结果
    /// </summary>
    public class DeleteShelvesResult : ShelvesResult
    {
    }

    /// <summary>
    /// 修改货架返回结果
    /// </summary>
    public class ModShelvesResult : ShelvesResult
    {
    }


    public class GetAllShelvesResult : ShelvesResult
    {
        public List<Shelf> shelves { get; set; }
    }

    public class Shelf
    {
        public ShelfInfo shelf_info { get; set; }
        public string shelf_banner { get; set; }
        public string shelf_name { get; set; }
        public int shelf_id { get; set; }
    }

    public class ShelfInfo
    {
        public List<Shelf_ModuleInfo> module_infos { get; set; }
    }

    public class Shelf_ModuleInfo
    {
        public Shelf_GroupInfo group_infos { get; set; }
        public int eid { get; set; }
    }

    public class Shelf_GroupInfo
    {
        public List<Shelf_Group> groups { get; set; }
        public string img_background { get; set; }
    }

    public class Shelf_Group
    {
        public int group_id { get; set; }
    }
}
//{
//    "errcode": 0,
//    "errmsg": "success",
//    "shelves": [
//        {
//          "shelf_info": {
//            "module_infos": [
//              {
//                "group_infos": {
//                  "groups": [
//                    {
//                      "group_id": 200080093
//                    },
//                    {
//                      "group_id": 200080118
//                    },
//                    {
//                      "group_id": 200080119
//                    },
//                    {
//                      "group_id": 200080135
//                    }
//                  ],
//                  "img_background": "http://mmbiz.qpic.cn/mmbiz/4whpV1VZl294FzPwnf9dAcaN7ButStztAZyy2yHY8pW6sTQKicIhAy5F0a2CqmrvDBjMFLtc2aEhAQ7uHsPow9A/0"
//                },
//                "eid": 5
//              }
//            ]
//          },
//          "shelf_banner": "http://mmbiz.qpic.cn/mmbiz/4whpV1VZl294FzPwnf9dAcaN7ButStztAZyy2yHY8pW6sTQKicIhAy5F0a2CqmrvDBjMFLtc2aEhAQ7uHsPow9A/0",
//          "shelf_name": "新新人类",
//          "shelf_id": 22
//        },
//        {
//          "shelf_info": {
//            "module_infos": [
//              {
//                "group_info": {
//                  "group_id": 200080119,
//                  "filter": {
//                    "count": 4
//                  }
//                },
//                "eid": 1
//              }
//            ]
//          },
//          "shelf_banner": "http://mmbiz.qpic.cn/mmbiz/4whpV1VZl294FzPwnf9dAcaN7ButStztAZyy2yHY8pW6sTQKicIhAy5F0a2CqmrvDBjMFLtc2aEhAQ7uHsPow9A/0",
//          "shelf_name": "店铺",
//          "shelf_id": 23
//        }
//    ]
//}


