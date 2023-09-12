module.exports = {
  base: "/docs/",
  head: [
    ["link", { rel: "shortcut icon", type: "image/x-icon", href: "/icon.jpg" }],
    ["script", {}, ``],
  ],
  locales: {
    "/": {
      lang: "en-US",
      title: "Wechat.NET SDK",
      description:
        "With Senparc.Weixin, you can easily and quickly develop applications for all platforms of wechat (including wechat public number, mini program, mini game, enterprise number, open platform, wechat Pay, JS-SDK, wechat hardware/Bluetooth, etc.). The Demo of this project is also suitable for beginners to learn.NET programming.",
    },
    "/zh/": {
      lang: "zh-CN",
      title: "微信 .NET SDK文档",
      description:
        "使用 Senparc.Weixin，您可以方便快速地开发微信全平台的应用（包括微信公众号、小程序、小游戏、企业号、开放平台、微信支付、JS-SDK、微信硬件/蓝牙，等等）。本项目的 Demo 同样适合初学者进行 .NET 编程学习。",
    },
  },
  // evergreen: true,
  port: 8080,
  plugins: ["@vuepress/back-to-top"],
  themeConfig: {
    repo: "JeffreySu/WeiXinMPSDK",
    // 自定义仓库链接文字。默认从 `themeConfig.repo` 中自动推断为
    // "GitHub"/"GitLab"/"Bitbucket" 其中之一，或是 "Source"。
    // repoLabel: '查看源码',

    // 以下为可选的编辑链接选项

    // 假如你的文档仓库和项目本身不在一个仓库：
    // docsRepo: 'vuejs/vuepress',
    // 假如文档不是放在仓库的根目录下：
    docsDir: "docs",
    // 假如文档放在一个特定的分支下：
    docsBranch: "master",
    // 默认是 false, 设置为 true 来启用
    editLinks: true,
    // 默认为 "Edit this page"
    // editLinkText: '帮助我们改善此页面！',
    locales: {
      "/": {
        label: "English",
        repoLabel: "Github",
        selectText: "Languages",
        ariaLabel: "Select language",
        editLinkText: "Edit this page on GitHub",
        lastUpdated: "Last Updated",
        nav: [
          { text: "Guide", link: "/guide/" },
          {
            text: "Ecosystem",
            items: [
              {
                text: "Project",
                items: [
                  { text: "Dynamic WebApi", link: "/dynamic-webapi/index" },
                ],
              },
              {
                text: "Help",
                items: [
                  {
                    text: "Online Resources",
                    link: "https://sdk.weixin.senparc.com/",
                  },
                  { text: "FAQ", link: "https://weixin.senparc.com/QA" },
                  {
                    text: "QQ Group(342319110)",
                    link: "http://qm.qq.com/cgi-bin/qm/qr?_wv=1027&k=U6bvyUB9eRZDX3I4Cd1jwx2ig_sMiQxJ&authKey=qZ5NScUhiwa%2B2nvBWQk%2BKQJbZbjxCF8U6F7DFCTox1DEWnif3ZzK5jUhGuBoHieV&noverify=0&group_code=342319110",
                  },
                  { text: "NeuCharFramework", link: "https://www.ncf.pub/" },
                ],
              },
            ],
          },
          { text: "Gitee", link: "https://gitee.com/JeffreySu/WeiXinMPSDK" },
        ],
        sidebar: {
          "/guide/": [
            {
              title: "Guide",
              collapsable: false,
              children: ["", "lab"],
            },
            {
              title: "MP",
              collapsable: false,
              children: [
                "mp/source_code",
                "mp/install",
                "mp/registration",
                "mp/MessageHandler",
                "mp/AdvancedInterface",
                "mp/JSSDK",
                "mp/OAuth2.0",
                "mp/MenuSetup",
                "mp/Advanced",
              ],
            },
            {
              title: "WXOpen",
              collapsable: false,
              children: [
                "wxopen/Source_code",
                "wxopen/install",
                "wxopen/Registration",
                "wxopen/MessageHandler",
                "wxopen/Advanced_Interface",
                "wxopen/Client_Development",
                "wxopen/Applet_Request_Service",
                "wxopen/Sign_in",
                "wxopen/Get_phone_number",
                "wxopen/Other",
                "wxopen/Advanced",
              ],
            },
            {
              title: "Work",
              collapsable: false,
              children: [
                "work/Source_code",
                "work/install",
                "work/Registration",
                "work/MessageHandler",
                "work/Advanced_Interface",
                "work/JSSDK_General",
                "work/JSSDK_agentConfig",
                "work/OAuth2.0",
                "work/MenuSetting",
                "work/Advanced",
              ],
            },
            {
              title: "TenpayV3",
              collapsable: false,
              children: [
                "tenpayv3/Source_code",
                "tenpayv3/install",
                "tenpayv3/Registration",
                "tenpayv3/JASPI",
                "tenpayv3/Payment_callbacks",
                "tenpayv3/Native_Payments",
                "tenpayv3/Refunds",
                "tenpayv3/Advanced",
              ],
            },
            {
              title: "TenpayV2",
              collapsable: false,
              children: [
                "tenpayv2/Source_code",
                "tenpayv2/install",
                "tenpayv2/Registration",
                "tenpayv2/JASPI_Payment",
                "tenpayv2/Payment_callbacks",
                "tenpayv2/Native_Payment",
                "tenpayv2/Refunds",
                "tenpayv2/Advanced",
              ],
            },
            {
              title: "Newly Release",
              collapsable: false,
              children: ["/guide/release/new_function", "/guide/release/log"],
            },
          ],
          "/dynamic-webapi/": [
            {
              title: "Dynamic WebApi",
              collapsable: false,
            },
          ],
        },
      },
      "/zh/": {
        label: "简体中文",
        repoLabel: "查看源码",
        selectText: "选择语言",
        ariaLabel: "选择语言",
        editLinkText: "在 GitHub 上编辑此页",
        lastUpdated: "上次更新",
        nav: [
          { text: "指南", link: "/zh/guide/" },
          {
            text: "生态系统",
            items: [
              {
                text: "项目",
                items: [
                  { text: "动态WebApi", link: "/zh/dynamic-webapi/index" },
                ],
              },
              {
                text: "帮助",
                items: [
                  { text: "在线资源", link: "https://sdk.weixin.senparc.com/" },
                  { text: "问答社区", link: "https://weixin.senparc.com/QA" },
                  {
                    text: "QQ群(342319110)",
                    link: "http://qm.qq.com/cgi-bin/qm/qr?_wv=1027&k=U6bvyUB9eRZDX3I4Cd1jwx2ig_sMiQxJ&authKey=qZ5NScUhiwa%2B2nvBWQk%2BKQJbZbjxCF8U6F7DFCTox1DEWnif3ZzK5jUhGuBoHieV&noverify=0&group_code=342319110",
                  },
                  { text: "NeuCharFramework", link: "https://www.ncf.pub/" },
                ],
              },
            ],
          },
          { text: "Gitee", link: "https://gitee.com/JeffreySu/WeiXinMPSDK" },
        ],
        sidebar: {
          "/zh/guide/": [
            {
              title: "概要",
              collapsable: false,
              children: ["", "库和组件"],
            },
            {
              title: "公众号",
              collapsable: false,
              children: [
                "MP/源码",
                "MP/如何安装",
                "MP/注册",
                "MP/MessageHandler",
                "MP/高级接口",
                "MP/JSSDK",
                "MP/OAuth2.0",
                "MP/菜单设置",
                "MP/进阶",
              ],
            },
            {
              title: "小程序",
              collapsable: false,
              children: [
                "WxOpen/源码",
                "WxOpen/如何安装",
                "WxOpen/注册",
                "WxOpen/MessageHandler",
                "WxOpen/高级接口",
                "WxOpen/客户端开发",
                "WxOpen/小程序服务请求",
                "WxOpen/登录",
                "WxOpen/获取手机号",
                "WxOpen/其他",
                "WxOpen/进阶",
              ],
            },
            {
              title: "企业微信",
              collapsable: false,
              children: [
                "Work/源码",
                "Work/如何安装",
                "Work/注册",
                "Work/MessageHandler",
                "Work/高级接口",
                "Work/JSSDK常规",
                "Work/JSSDK_agentConfig",
                "Work/OAuth2.0",
                "Work/菜单设置",
                "Work/进阶",
              ],
            },
            {
              title: "微信支付V3",
              collapsable: false,
              children: [
                "TenPayV3/源码",
                "TenPayV3/如何安装",
                "TenPayV3/注册",
                "TenPayV3/JASPI",
                "TenPayV3/支付回调",
                "TenPayV3/Native支付",
                "TenPayV3/退款",
                "TenPayV3/进阶",
              ],
            },
            {
              title: "微信支付V2",
              collapsable: false,
              children: [
                "TenPayV2/源码",
                "TenPayV2/如何安装",
                "TenPayV2/注册",
                "TenPayV2/JASPI",
                "TenPayV2/支付回调",
                "TenPayV2/Native支付",
                "TenPayV2/退款",
                "TenPayV2/进阶",
              ],
            },
            {
              title: "新发布",
              collapsable: false,
              children: [
                "/zh/guide/release/new_function",
                "/zh/guide/release/log",
              ],
            },
          ],
          "/zh/dynamic-webapi/": [
            {
              title: "动态WebApi",
              collapsable: false,
            },
          ],
        },
      },
    },
  },
};
