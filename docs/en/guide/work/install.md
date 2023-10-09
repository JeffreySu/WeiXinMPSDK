# How to install?

You can either directly reference the Senparc.Weixin source code for development, or you can reference the packaged dlls (via Nuget packages, which is recommended) to get official updates at any time. Note: You can only use either the source code or the Nuget package.

## Referencing source code

You can reference (copy) the assemblies you need to reference in the current solution, under the **Libraries** directory, to the solution of your development environment, please note that you need to reference the dependent projects at the same time, for example, the `Senparc.Weixin` project is a dependency of all projects.

The current example project uses a direct reference to the source code by default, which can be seen in the .csproj file:

```cs
<ItemGroup>
    <ProjectReference Include="... \... \... \src\Senparc.Weixin.AspNet\Senparc.Weixin.AspNet.net6.csproj" />
    <ProjectReference Include="... \... \... \src\Senparc.Weixin.Work.Middleware\Senparc.Weixin.Work.Middleware.net6.csproj" />
    <ProjectReference Include="... \... \... \src\Senparc.Weixin.Work\Senparc.Weixin.Work\Senparc.Weixin.Work.net6.csproj" />
</ItemGroup>
```

## Referencing assemblies (recommended)

You can install Nuget packages automatically via `Visual Studio`, `Visual Studio Code`, `dotnet command line`, and many other ways.

### Visual Studio

In the Solution Explorer of the development project, right-click the module you want to add Senparc.Weixin.Work, click Manage Nuget Packages, enter **Senparc.Weixin.Work** in the Browse tab, and click the Install button on the right side. As shown in the following figure:

![通过 Visual Studio 安装](https://sdk.weixin.senparc.com/Docs/Work/images/home-install-01.png)

## Visual Studio Code

First, make sure you have installed [VS Code](https://code.visualstudio.com/) and dotnet command line (it will be installed automatically after installing [.NET SDK](https://dotnet.microsoft.com/en-us/download)).

Then, open the directory where the solution or project is located, press Ctrl+~ to open the Terminal panel, go to the directory of the project where you need to add the module Senparc.Weixin.Work and type:

> ```cs
> dotnet add package Senparc.Weixin.Work
> ```

![通过 VS Code 安装](https://sdk.weixin.senparc.com/Docs/Work/images/home-install-03.png)

After the installation is complete, you can see the corresponding .csproj file, which has been added as a reference, for example:

```cs
<ItemGroup>
    <PackageReference Include="Senparc.Weixin.Work" Version="3.14.10" />
</ItemGroup>
```

## dotnet command line

First of all, make sure that you have installed the dotnet command line (it will be installed automatically after installing the [.NET SDK](https://dotnet.microsoft.com/en-us/download)).

Go to the directory of the project where you want to add the Senparc.Weixin.Work module and type:

> ```cs
> dotnet add package Senparc.Weixin.Work
> ```

![通过 dotnet CLI 安装](https://sdk.weixin.senparc.com/Docs/Work/images/home-install-02.png)

After the installation is complete, you can see the corresponding .csproj file, which is added with references such as:

```cs
<ItemGroup>
    <PackageReference Include="Senparc.Weixin.Work" Version="3.14.10" />
</ItemGroup>
```
