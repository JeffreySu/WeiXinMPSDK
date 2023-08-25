# How to install?

You can either directly reference the Senparc.Weixin source code for development, or you can reference the packaged dlls (via Nuget packages, which is recommended) to get official updates at any time. Note: You can only use either the source code or the Nuget package.

## Referencing assemblies (recommended)

You can install Nuget packages automatically via `Visual Studio`, `Visual Studio Code`, `dotnet command line`, and many other ways.

### Visual Studio

In the [Solution Explorer] of the development project, right click on the module you need to add Senparc.Weixin.TenpayV3, click [Manage Nuget Packages], enter **Senparc.Weixin.TenpayV3** in the [Browse] tab, and click the [Install] button on the right side. As shown in the picture below:

![通过 Visual Studio 安装](https://sdk.weixin.senparc.com/Docs/TenPayV3/images/home-install-01.png)

### Visual Studio Code

First, make sure you have installed [VS Code](https://code.visualstudio.com/) and dotnet command line (it will be installed automatically after installing [.NET SDK](https://dotnet.microsoft.com/en-us/download)).

Then, open the solution or project directory and press Ctrl+~ to open the terminal panel:

![通过 VS Code 安装](https://sdk.weixin.senparc.com/Docs/TenPayV3/images/home-install-03.png)

Go to the directory of the project where you need to add the module Senparc.Weixin.Tenpay and type:

> ```cs
> dotnet add package Senparc.Weixin.TenpayV3
> ```

![通过 VS Code 安装](https://sdk.weixin.senparc.com/Docs/TenPayV3/images/home-install-04.png)

After the installation is complete, you can see the corresponding .csproj file, which is being added with references such as:

```cs
<ItemGroup>
    <PackageReference Include="Senparc.Weixin.TenpayV3" Version="0.6.4-beta1" />
</ItemGroup>
```

### dotnet command line

First of all, make sure that the dotnet command line is installed (it will be installed automatically after installing [.NET SDK](https://dotnet.microsoft.com/en-us/download)).

Go to the directory of the project where you want to add the module Senparc.Weixin.TenpayV3 and enter:

> ```cs
> dotnet add package Senparc.Weixin.TenpayV3
> ```

! [Install via dotnet CLI](https://sdk.weixin.senparc.com/Docs/TenPayV3/images/home-install-02.png)

After the installation is complete, you can see the corresponding .csproj file, which is being added with references such as:

```cs
<ItemGroup>
    <PackageReference Include="Senparc.Weixin.TenPayV3" Version="0.6.4-beta1" />
</ItemGroup>

```
