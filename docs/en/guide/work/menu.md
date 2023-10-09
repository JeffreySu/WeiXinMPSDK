# Menu Setting

Custom menu is an important element of the enterprise WeChat application interface, the use of the menu is divided into **Setup** and **Use** two links.

## Setting

Method 1: In the background of enterprise WeChat [Application Management], in an application setting interface, [Function] area [Custom Menu] to set (without using the development mode), here is omitted.

Method 2: Use the code to set (only need to execute once, it is recommended to put it in the administrator background and run it manually), such as:

```cs
public async Task CreateMenuTest()
{
    ButtonGroup bg = new ButtonGroup();

    // Click
    bg.button.Add(new SingleClickButton()
    {
        name = "OneClick Test",
        key = "OneClick",
        type = MenuButtonType.click.ToString(), //This type is already set by default, this is only for demonstration purposes
    });

    //Second level menu
    var subButton = new SubButton()
    {
        name = "Secondary Menu"
    }; var subButton = new SubButton() { name = "Secondary Menu"; }
    subButton.sub_button.Add(new SingleClickButton()
    {
        key = "SubClickRoot_Text",
        name = "Return Text"
    });
    subButton.sub_button.Add(new SingleViewButton()
    {
        url = "https://weixin.senparc.com",
        name = "Url Jump"
    });
    bg.button.Add(subButton);

    var workWeixinSetting = Config.SenparcWeixinSetting.WorkSetting;
    var appKey = AccessTokenContainer.BuildingKey(workWeixinSetting);
    int agentId;
    if (!int.TryParse(workWeixinSetting.WeixinCorpAgentId, out agentId))
    {
        throw new WeixinException("WeixinCorpAgentId must be an integer!") ;
    }
    var result = await CommonApi.CreateMenuAsync(appKey, agentId, bg);

    Assert.IsNotNull(result);
    Assert.AreEqual("ok", result.errmsg);
}
```

## Use

After setting up the menu, when the client clicks on the menu, the WeChat server will automatically push the response callback information to the message URL (i.e., within the MessageHandler that has been set up), just override (`override`) the corresponding method in the WorkCustomMessageHandler. For example, for the above **Method 2** menu that has already been set to take effect, when the user clicks the [Click to Test] button, we can receive and process it in the WorkCustomMessageHandler:

```cs
public override IWorkResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
{
    var replyMessage = CreateResponseMessage();
    if (requestMessage.EventKey == "SubClickRoot_Text")
    {
        reponseMessage.Content = "You clicked the [Back to Text] button";
    }
    else
    {
        reponseMessage.Content = "You clicked the Other Events button";
    }
    return reponseMessage;
}
```

> Reference file for this project:
>
> /MessageHandlers/**_WorkCustomMessageHandler.cs_**
