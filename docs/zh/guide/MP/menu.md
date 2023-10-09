# 菜单设置

公众号菜单是公众号界面的重要元素，菜单的使用分为 **设置** 和 **使用** 两个环节。

## 设置

方法一：使用微信公众号后台界面设置（不使用开发模式），此处略。

方法二（推荐）：使用可视化编辑器（No Code）：[点击查看介绍](https://sdk.weixin.senparc.com/Menu)。

方法三：使用代码进行设置（只需要执行一次，建议放在管理员后台，手动运行），如：

```cs
public async Task CreateMenuAsync()
{
    ButtonGroup bg = new ButtonGroup();

    //定义一级菜单
    var subButton = new SubButton()
    {
        name = "一级菜单"
    };
    bg.button.Add(subButton);

    //下属二级菜单
    subButton.sub_button.Add(new SingleViewButton()
    {
        url = "https://book.weixin.senparc.com/book/link?code=SenparcRobotMenu",
        name = "《微信开发深度解析》"
    });
    subButton.sub_button.Add(new SingleClickButton()
    {
        key = "OneClick",
        name = "单击测试"
    });
    subButton.sub_button.Add(new SingleViewButton()
    {
        url = "https://weixin.senparc.com/",
        name = "Url跳转"
    });

    //最多可添加 3 个一级自定义菜单，每个菜单下最多 5 个子菜单

    var result = await CommonApi.CreateMenuAsync(appId, bg);
}
```

## 使用

设置完菜单后，当客户端点击菜单时，微信服务器会自动推送响应的回调信息到消息 URL（即已经设置好的 MessageHandler 内），只需在 CustomMessageHandler 中重写（`override`）对应的方法即可。如针对上述 **方法三** 已经设定生效的菜单，当用户点击【单击测试】按钮时，我们可以在 CustomMessageHandler 中进行接收和处理：

```cs
public override async Task OnEvent_ClickRequestAsync(RequestMessageEvent_Click requestMessage)
{
    var reponseMessage = CreateResponseMessage();

    if (requestMessage.EventKey == "OneClick")
    {
        reponseMessage.Content = "您点击了【单击测试】按钮";
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
> /MessageHandlers/**_CustomMessageHandler_Events.cs_**
