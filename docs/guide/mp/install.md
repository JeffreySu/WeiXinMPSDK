# How to install?

You can either directly reference the Senparc.Weixin source code for development, or you can reference the packaged dlls (via Nuget packages, recommended) to get official updates at any time. Note: You can only use either the source code or the Nuget package.

## Referencing the source code

You can reference (copy) the assemblies you need to reference in the current solution, under the **Libraries** directory, to the solution of your development environment, please note that you need to reference the dependent projects at the same time, e.g., `Senparc.Weixin` is a project that all projects need to depend on.

The current example project uses a direct reference to the source code by default, which can be seen in the .csproj file:

```cs
<ItemGroup>
    <ProjectReference Include="..\..\..\src\Senparc.Weixin.MP.Middleware\Senparc.Weixin.MP.Middleware.net6.csproj" />
    <ProjectReference Include="..\..\..\src\Senparc.Weixin.MP.MvcExtension\Senparc.Weixin.MP.MvcExtension\Senparc.Weixin.MP.MvcExtension.net6.csproj" />
    <ProjectReference Include="..\..\..\src\Senparc.Weixin.MP\Senparc.Weixin.MP\Senparc.Weixin.MP.net6.csproj" />
</ItemGroup>
```

## Referencing assemblies (recommended)

You can install Nuget packages automatically through `Visual Studio`, `Visual Studio Code`, `dotnet command line`, and many other ways.

### Visual Studio

In the development project [Solution Explorer], right click on the module where you need to add `Senparc.Weixin.MP`, click [Manage Nuget Packages], enter **Senparc.Weixin.MP** in the [Browse] tab, and click the [Install] button on the right side. MP, and then click the [Install] button on the right side:
![通过 Visual Studio 安装](https://sdk.weixin.senparc.com/Docs/MP/images/home-install-01.png)

### Visual Studio Code

First of all, make sure that you have installed [VS Code](https://code.visualstudio.com/) and the dotnet command line (it will be installed automatically after installing the [.NET SDK ](https://dotnet.microsoft.com/en-us/download)).

Then, open the solution or project directory and press Ctrl+~ to open the Terminal panel:

![通过 VS Code 安装](https://sdk.weixin.senparc.com/Docs/MP/images/home-install-03.png)

Go to the directory of the project where you need to add the module Senparc.Weixin.MP and type:

> ```bash
> dotnet add package Senparc.Weixin.MP
> ```

To install the Senparc.Weixin.MP module

![Install via VS Code](https://sdk.weixin.senparc.com/Docs/MP/images/home-install-04.png)

After the installation is complete, you can see the corresponding .csproj file, being added references such as:

```cs
<ItemGroup>
    <PackageReference Include="Senparc.Weixin.MP" Version="16.17.7" />
</ItemGroup>
```

### dotnet command line

First of all, make sure that you have installed the dotnet command line (it will be installed automatically after installing the [.NET SDK](https://dotnet.microsoft.com/en-us/download)).

Go to the directory of the project where you want to add the module Senparc.Weixin.MP and enter:

> ```cs
> dotnet add package Senparc.Weixin.MP
> ```

![Install via dotnet CLI](https://sdk.weixin.senparc.com/Docs/MP/images/home-install-02.png)

After the installation is complete, you can view the corresponding .csproj file that was added to the reference, for example:

```cs
<ItemGroup>
  <PackageReference Include="Senparc.Weixin.MP" Version="16.17.7" />
</ItemGroup>
```
