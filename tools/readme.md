# Tools 文件夹
此文件夹将放置方便 Senparc.Weixin SDK 开发的小工具、批处理文件或补丁文件。

> 说明：这些工具转为本项目的需求而开发，开放源代码只做共享交流之用，对其他项目不具备通用性，请勿直接使用以免发生杯具。

## ClearDotNetCoreBadFiles
ClearDotNetCoreBadFiles.exe 用于解决当项目在.NET Core（或.NET Standard）环境下编译后，会在obj文件夹下生成一些文件，
导致无法让.NET Framework成功编译的问题。

使用方法：执行 ClearDotNetCoreBadFiles.exe，按照提示按任意键开始清理，再按任意键退出。


## SupportNet35And40
SupportNet35And40.exe 用于项目提供 .NET 3.5/4.0支持的文件自动处理，目前已经结束。

使用方法：执行 SupportNet35And40.exe，等待扫描结束，文件会被自动修改。

## SyncVersion
SyncVersion.exe 用于提供将 VS2017 的 Multi-Targeting 项目版本号同步到 .NET 4.5 项目的 AssemblyInfo.cs 文件中的方法。

使用方法：执行 SyncVersion.exe，结束后窗口将自动关闭，对应项目版本号将自动修改，请注意重新再 .NET 4.5 项目中编译 dll，以确保获得最新版本的文件。