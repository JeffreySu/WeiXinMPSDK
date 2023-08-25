# 如何安装？

您可以直接引用 Senparc.Weixin 的源码进行开发，也可以引用已经打包完成的 dll（通过 Nuget 包，推荐），以方便随时获取官方的更新。注意：直接引用源码和引用 Nuget 包只能二选一。

## 引用源码

您可以在当前解决方案中，**Libraries** 目录下，将所需要引用的程序集引用（复制）到您开发环境的解决方案中，请注意需要同时引用被依赖的项目，如 `Senparc.Weixin` 项目是所有项目都需要依赖的。

当前示例项目默认就使用了直接引用源码的方式，可从 .csproj 文件中看到引用方式：

```cs
<ItemGroup>
    <ProjectReference Include="..\..\..\src\Senparc.Weixin.MP.Middleware\Senparc.Weixin.MP.Middleware.net6.csproj" />
    <ProjectReference Include="..\..\..\src\Senparc.Weixin.MP.MvcExtension\Senparc.Weixin.MP.MvcExtension\Senparc.Weixin.MP.MvcExtension.net6.csproj" />
    <ProjectReference Include="..\..\..\src\Senparc.Weixin.MP\Senparc.Weixin.MP\Senparc.Weixin.MP.net6.csproj" />
</ItemGroup>
```

## 引用程序集（推荐）

您可以通过 `Visual Studio`、`Visual Studio Code`、`dotnet 命令行` 等多种方式自动安装 Nuget 包。

### Visual Studio

在开发项目【解决方案资源管理器】中，对需要添加 Senparc.Weixin.MP 的模块点击右键，点击【管理 Nuget 程序包】，在【浏览】标签中输入 **Senparc.Weixin.MP**，点击右侧【安装】按钮。如下图所示：

![通过 Visual Studio 安装](https://sdk.weixin.senparc.com/Docs/MP/images/home-install-01.png)

### Visual Studio Code

首先，确认已经安装好 [VS Code](https://code.visualstudio.com/) 以及 dotnet 命令行（安装 [.NET SDK](https://dotnet.microsoft.com/en-us/download) 后会自动安装）。

然后，打开解决方案或项目所在目录，按 Ctrl+~，打开终端面板：

![通过 VS Code 安装](https://sdk.weixin.senparc.com/Docs/MP/images/home-install-03.png)

进入需要添加 Senparc.Weixin.MP 的模块的项目的目录，输入：

> ```cs
> dotnet add package Senparc.Weixin.MP
> ```

安装 Senparc.Weixin.MP 模块

![通过 VS Code 安装](https://sdk.weixin.senparc.com/Docs/MP/images/home-install-04.png)

安装完成后，可查看对应 .csproj 文件，被添加引用，如：

```cs
<ItemGroup>
    <PackageReference Include="Senparc.Weixin.MP" Version="16.17.7" />
</ItemGroup>
```

### dotnet 命令行

首先，确认已经安装好 dotnet 命令行（安装 [.NET SDK](https://dotnet.microsoft.com/en-us/download) 后会自动安装）。

进入需要添加 Senparc.Weixin.MP 的模块的项目的目录，输入：

> ```cs
> dotnet add package Senparc.Weixin.MP
> ```

![通过 dotnet CLI 安装](https://sdk.weixin.senparc.com/Docs/MP/images/home-install-02.png)

安装完成后，可查看对应 .csproj 文件，被添加引用，如：

```cs
<ItemGroup>
    <PackageReference Include="Senparc.Weixin.MP" Version="16.17.7" />
</ItemGroup>
```
