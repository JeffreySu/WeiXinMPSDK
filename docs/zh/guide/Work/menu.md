# 菜单设置

自定义菜单是企业微信应用界面的重要元素，菜单的使用分为 **设置** 和 **使用** 两个环节。

## 设置

方法一：在企业微信后台【应用管理】中，某个应用设置界面中，【功能】区域【自定义菜单】中设置（不使用开发模式），此处略。

方法二：使用代码进行设置（只需要执行一次，建议放在管理员后台，手动运行），如：

```cs
public async Task CreateMenuTest()
{
    ButtonGroup bg = new ButtonGroup();

    //单击
    bg.button.Add(new SingleClickButton()
    {
        name = "单击测试",
        key = "OneClick",
        type = MenuButtonType.click.ToString(),//默认已经设为此类型，这里只作为演示
    });

    //二级菜单
    var subButton = new SubButton()
    {
        name = "二级菜单"
    };
    subButton.sub_button.Add(new SingleClickButton()
    {
        key = "SubClickRoot_Text",
        name = "返回文本"
    });
    subButton.sub_button.Add(new SingleViewButton()
    {
        url = "https://weixin.senparc.com",
        name = "Url跳转"
    });
    bg.button.Add(subButton);

    var workWeixinSetting = Config.SenparcWeixinSetting.WorkSetting;
    var appKey = AccessTokenContainer.BuildingKey(workWeixinSetting);
    int agentId;
    if (!int.TryParse(workWeixinSetting.WeixinCorpAgentId, out agentId))
    {
        throw new WeixinException("WeixinCorpAgentId 必须为整数！");
    }
    var result = await CommonApi.CreateMenuAsync(appKey, agentId, bg);

    Assert.IsNotNull(result);
    Assert.AreEqual("ok", result.errmsg);
}
```

## 使用

设置完菜单后，当客户端点击菜单时，微信服务器会自动推送响应的回调信息到消息 URL（即已经设置好的 MessageHandler 内），只需在 WorkCustomMessageHandler 中重写（`override`）对应的方法即可。如针对上述 **方法二** 已经设定生效的菜单，当用户点击【单击测试】按钮时，我们可以在 WorkCustomMessageHandler 中进行接收和处理：

```cs
public override IWorkResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
{
    var reponseMessage = CreateResponseMessage();

    if (requestMessage.EventKey == "SubClickRoot_Text")
    {
        reponseMessage.Content = "您点击了【返回文本】按钮";
    }
    else
    {
        reponseMessage.Content = "您点击了其他事件按钮";
    }

    return reponseMessage;
}
```

> 本项目参考文件：
>
> /MessageHandlers/**_WorkCustomMessageHandler.cs_**
