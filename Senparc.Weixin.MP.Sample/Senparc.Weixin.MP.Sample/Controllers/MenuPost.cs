using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Senparc.Weixin.MP.Entities.Menu;

namespace Senparc.Weixin.MP.Sample.Controllers
{
    public class MenuPost
    {
        public MenuPost_ButtonGroup menu { get; set; }
    }

    public class MenuPost_ButtonGroup
    {
        public List<MenuPost_RootButton> button { get; set; }
    }

    public class MenuPost_RootButton
    {
        public string type { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public List<SingleButton> sub_button { get; set; }
    }
}