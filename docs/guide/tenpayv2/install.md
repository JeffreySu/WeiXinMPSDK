# How to install

You can either directly reference the Senparc.Weixin source code for development, or you can reference the packaged dlls (via Nuget packages, which is recommended) to get official updates at any time. Note: You can only use either the source code or the Nuget package.

## Referencing assemblies (recommended)

You can install Nuget packages automatically via `Visual Studio`, `Visual Studio Code`, `dotnet command line` and many other ways.

### Visual Studio

In the [Solution Explorer] of the development project, right click on the module you need to add Senparc.Weixin.Tenpay, click [Manage Nuget Packages], enter **Senparc.Weixin.Tenpay** in the [Browse] tab, and click the [Install] button on the right side. As shown in the picture below:

![通过 Visual Studio 安装](https://sdk.weixin.senparc.com/Docs/TenPayV2/images/home-install-01.png)

### Visual Studio Code

First, make sure you have installed [VS Code](https://code.visualstudio.com/) and dotnet command line (it will be installed automatically after installing [.NET SDK](https://dotnet.microsoft.com/en-us/download)).

Then, open the solution or project directory and press Ctrl+~ to open the terminal panel:

![通过 VS Code 安装](https://sdk.weixin.senparc.com/Docs/TenPayV2/images/home-install-03.png)

Go to the directory of the project where you need to add the module Senparc.Weixin.Tenpay and type:

> ```cs
> dotnet add package Senparc.Weixin.Tenpay
> ```

![通过 VS Code 安装](https://sdk.weixin.senparc.com/Docs/TenPayV2/images/home-install-04.png)

After the installation is complete, you can see the corresponding .csproj file that was added with a reference such as:

```cs
<ItemGroup>
    <PackageReference Include="Senparc.Weixin.Tenpay" Version="16.17.7" />
</ItemGroup>
```

### dotnet command line

First of all, make sure that you have installed the dotnet command line (it will be installed automatically after installing the [.NET SDK](https://dotnet.microsoft.com/en-us/download)).

Go to the directory of the project where you want to add the Senparc.Weixin.Tenpay module and enter:

> ```cs
> dotnet add package Senparc.Weixin.Tenpay
> ```

![通过 dotnet CLI 安装](https://sdk.weixin.senparc.com/Docs/TenPayV2/images/home-install-02.png)

After the installation is complete, you can see the corresponding .csproj file, which is being added with references such as:

```cs
<ItemGroup>
    <PackageReference Include="Senparc.Weixin.TenPay" Version="16.17.7" />
</ItemGroup>

```
