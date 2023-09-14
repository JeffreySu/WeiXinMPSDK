# Menu Setup

The public menu is an important element of the public interface, and the use of the menu is divided into two parts: **Setup** and **Use**.

## Setting

Method 1 (recommended): Use the background interface of WeChat public number to set up the menu (without using the development mode), which is omitted here.

Method 2 (recommended): Use visual editor (No Code): [Click to view the introduction](https://sdk.weixin.senparc.com/Menu).

Method 3: Setup using code (only needs to be executed once, it is recommended to put it in the administrator background and run it manually), e.g.:

```CS
public async Task CreateMenuAsync()
{
    ButtonGroup bg = new ButtonGroup();

      // Define the first level menu
    var subButton = new SubButton()
    {
        name = "First level menu"
    };
    bg.button.Add(subButton);

    // Subordinate secondary menu
    subButton.sub_button.Add(new SingleViewButton()
    {
        url = "https://book.weixin.senparc.com/book/link?code=SenparcRobotMenu",
        name = "WeChat Development In-Depth Analysis"
    });
    subButton.sub_button.Add(new SingleClickButton()
    {
        key = "OneClick",
        name = "OneClick Test"
    });
    subButton.sub_button.Add(new SingleViewButton()
    {
        url = "https://weixin.senparc.com/",
        name = "Url Jump"
    });

        //Add up to 3 first level custom menus and up to 5 submenus under each menu.

    var result = await CommonApi.CreateMenuAsync(appId, bg);
}
```

## Use

After setting up the menu, when the client clicks on the menu, the WeChat server will automatically push the response callback information to the message URL (i.e., within the MessageHandler that has been set up), just override (`override`) the corresponding method in the CustomMessageHandler. For example, for the above **Method 3** menu that has already been set to take effect, when the user clicks the [Click to Test] button, we can receive and process it in the CustomMessageHandler:

```cs
public override async Task OnEvent_ClickRequestAsync(RequestMessageEvent_Click requestMessage)
{
    var requestMessage = CreateResponseMessage();

    if (requestMessage.EventKey == "OneClick")
    {
        reponseMessage.Content = "You clicked on the [OneClick Test] button";
    }
    else
    {
        reponseMessage.Content = "You clicked the other event button";
    }

    return reponseMessage;
}
```

> Reference document for this project:
>
> /MessageHandlers/**_CustomMessageHandler_Events.cs_**
