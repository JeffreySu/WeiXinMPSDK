module.exports = {
  base: "/docs/",
  lang: "zh-CN",
  title: '微信 .NET SDK文档',
  description: "使用 Senparc.Weixin，您可以方便快速地开发微信全平台的应用（包括微信公众号、小程序、小游戏、企业号、开放平台、微信支付、JS-SDK、微信硬件/蓝牙，等等）。本项目的 Demo 同样适合初学者进行 .NET 编程学习。",
  head: [
    ["link", { rel: "shortcut icon", type: "image/x-icon", href: "/icon.jpg" }],
    ["script", {}, ``],
  ],
  locales: {
    "/en/": {
      lang: "en-US",
      title: "Wechat.NET SDK",
      description: "With Senparc.Weixin, you can easily and quickly develop applications for all platforms of wechat (including wechat public number, mini program, mini game, enterprise number, open platform, wechat Pay, JS-SDK, wechat hardware/Bluetooth, etc.). The Demo of this project is also suitable for beginners to learn.NET programming.",
    },
    "/zh/": {
      lang: "zh-CN",
      title: "微信 .NET SDK文档",
      description: "使用 Senparc.Weixin，您可以方便快速地开发微信全平台的应用（包括微信公众号、小程序、小游戏、企业号、开放平台、微信支付、JS-SDK、微信硬件/蓝牙，等等）。本项目的 Demo 同样适合初学者进行 .NET 编程学习。",
    },
  },
  // evergreen: true,
  port: 8080,
  host: "localhost",
  open: true,
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
      "/en/": {
        label: "English",
        repoLabel: "Github",
        selectText: "Languages",
        ariaLabel: "Select language",
        editLinkText: "Edit this page on GitHub",
        lastUpdated: "Last Updated",
        nav: [
          { text: "Guide", link: "/en/guide/" },
          {
            text: "Ecosystem",
            items: [
              {
                text: "Project",
                items: [
                  { text: "Dynamic WebApi", link: "/en/dynamic-webapi/index" },
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
          "/en/guide/": [
            {
              title: "Guide",
              collapsable: false,
              children: ["", "lib"],
            },
            {
              title: "MP",
              collapsable: false,
              children: [
                "mp/source-code",
                "mp/install",
                "mp/register",
                "mp/messagehandler",
                "mp/advanced-interface",
                "mp/jssdk",
                "mp/oauth2.0",
                "mp/menu",
                "mp/advanced",
              ],
            },
            {
              title: "WxOpen",
              collapsable: false,
              children: [
                "wxopen/source-code",
                "wxopen/install",
                "wxopen/register",
                "wxopen/messagehandler",
                "wxopen/advanced-interface",
                "wxopen/client-development",
                "wxopen/request-service",
                "wxopen/login",
                "wxopen/get-phone-number",
                "wxopen/other",
                "wxopen/advanced",
              ],
            },
            {
              title: "Work",
              collapsable: false,
              children: [
                "work/source-code",
                "work/install",
                "work/register",
                "work/messagehandler",
                "work/advanced-interface",
                "work/jssdk-general",
                "work/jssdk-agent-config",
                "work/oauth2.0",
                "work/menu",
                "work/advanced",
              ],
            },
            {
              title: "TenpayV3",
              collapsable: false,
              children: [
                "tenpayv3/source-code",
                "tenpayv3/install",
                "tenpayv3/register",
                "tenpayv3/jssdk",
                "tenpayv3/callback",
                "tenpayv3/nativepay",
                "tenpayv3/refund",
                "tenpayv3/advanced",
              ],
            },
            {
              title: "TenpayV2",
              collapsable: false,
              children: [
                "tenpayv2/source-code",
                "tenpayv2/install",
                "tenpayv2/register",
                "tenpayv2/jssdk",
                "tenpayv2/callback",
                "tenpayv2/nativepay",
                "tenpayv2/refund",
                "tenpayv2/advanced",
              ],
            },
            {
              title: "Newly Release",
              collapsable: false,
              children: [
                "/en/guide/release/new-function",
                "/en/guide/release/log",
              ],
            },
          ],
          "/en/dynamic-webapi/": [
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
              children: ["", "lib"],
            },
            {
              title: "公众号",
              collapsable: false,
              children: [
                "mp/source-code",
                "mp/install",
                "mp/register",
                "mp/messagehandler",
                "mp/advanced-interface",
                "mp/jssdk",
                "mp/oauth2.0",
                "mp/menu",
                "mp/advanced",
              ],
            },
            {
              title: "小程序",
              collapsable: false,
              children: [
                "wxopen/source-code",
                "wxopen/install",
                "wxopen/register",
                "wxopen/messagehandler",
                "wxopen/advanced-interface",
                "wxopen/client-development",
                "wxopen/request-service",
                "wxopen/login",
                "wxopen/get-phone-number",
                "wxopen/other",
                "wxopen/advanced",
              ],
            },
            {
              title: "企业微信",
              collapsable: false,
              children: [
                "work/source-code",
                "work/install",
                "work/register",
                "work/messagehandler",
                "work/advanced-interface",
                "work/jssdk-general",
                "work/jssdk-agent-config",
                "work/oauth2.0",
                "work/menu",
                "work/advanced",
              ],
            },
            {
              title: "微信支付V3",
              collapsable: false,
              children: [
                "tenpayv3/source-code",
                "tenpayv3/install",
                "tenpayv3/register",
                "tenpayv3/jssdk",
                "tenpayv3/callback",
                "tenpayv3/nativepay",
                "tenpayv3/refund",
                "tenpayv3/advanced",
              ],
            },
            {
              title: "微信支付V2",
              collapsable: false,
              children: [
                "tenpayv2/source-code",
                "tenpayv2/install",
                "tenpayv2/register",
                "tenpayv2/jssdk",
                "tenpayv2/callback",
                "tenpayv2/nativepay",
                "tenpayv2/refund",
                "tenpayv2/advanced",
              ],
            },
            {
              title: "新发布",
              collapsable: false,
              children: [
                "/zh/guide/release/new-function",
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
