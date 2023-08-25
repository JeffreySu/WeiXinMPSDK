# How to install?

You can either directly reference the Senparc.Weixin source code for development, or you can reference the packaged dlls (via Nuget packages, recommended) to get official updates at any time. Note: You can only use either the source code or the Nuget package.

## Referencing the source code

You can open **. /All/net6-mvc/** before the solution (full advanced example), in the **Libraries** directory, will need to refer to the reference assembly reference (copy) to your development environment in the solution, please note that you need to refer to dependent projects at the same time, such as `Senparc.Weixin` project is all the projects need to rely on.

The current example project uses a direct reference to the source code by default, which can be seen in the .csproj file:

```cs
<ProjectReference Include="... \... \... \src\Senparc.Weixin.AspNet\Senparc.Weixin.AspNet.net6.csproj" />
		<ProjectReference Include="... \... \... \src\Senparc.Weixin.WxOpen.Middleware\Senparc.Weixin.WxOpen.Middleware.net6.csproj" />
		<ProjectReference Include="... \... \... \src\Senparc.Weixin.WxOpen\src\Senparc.Weixin.WxOpen\Senparc.Weixin.WxOpen.net6.csproj" />

```

## Referencing assemblies (recommended)

You can install Nuget packages automatically via `Visual Studio`, `Visual Studio Code`, `dotnet command line`, and many other ways.

### Visual Studio

In the [Solution Explorer] of the development project, right-click on the module you need to add Senparc.Weixin.WxOpen, click [Manage Nuget Packages], enter **Senparc.Weixin.TenPay** in the [Browse] tab, and click the [Install] button on the right side. As shown in the picture below:

![通过 Visual Studio 安装](https://sdk.weixin.senparc.com/Docs/WxOpen/images/home-install-01.png)

### Visual Studio Code

First, make sure you have installed [VS Code](https://code.visualstudio.com/) and dotnet command line (it will be installed automatically after installing [.NET SDK](https://dotnet.microsoft.com/en-us/download)).

Then, open the solution or project directory and press Ctrl+~ to open the terminal panel:

![通过 VS Code 安装](https://sdk.weixin.senparc.com/Docs/WxOpen/images/home-install-03.png "打开 VS Code 终端面板")

Go to the directory of the project where you need to add the module Senparc.Weixin.WxOpen and type:

> ```cs
> dotnet add package Senparc.Weixin.WxOpen
> ```

![通过 VS Code 安装](https://sdk.weixin.senparc.com/Docs/WxOpen/images/home-install-04.png "安装 Senparc.Weixin.WxOpen 模块")

After the installation is complete, you can see the corresponding .csproj file, being added references such as:

```cs
<ItemGroup>
    <PackageReference Include="Senparc.Weixin.WxOpen" Version="3.14.10.1" />
</ItemGroup>
```

### dotnet command line

First of all, make sure that the dotnet command line is installed (it will be installed automatically after installing the [.NET SDK](https://dotnet.microsoft.com/en-us/download)).

Go to the directory of the project where you want to add the Senparc.Weixin.WxOpen module and type:

> ```cs
> dotnet add package Senparc.Weixin.WxOpen
> ```

![通过 dotnet CLI 安装](https://sdk.weixin.senparc.com/Docs/WxOpen/images/home-install-02.png "通过 dotnet CLI 安装")

After the installation is complete, you can see the corresponding .csproj file that was added with a reference such as:

```cs
<ItemGroup>
    <PackageReference Include="Senparc.Weixin.WxOpen" Version="3.14.10.1" />
</ItemGroup>
```
