# Tools文件夹
此文件夹将放置方便 Senparc.Weixin SDK 开发的小工具、批处理文件或补丁文件。

## ClearDotNetCoreBadFiles
ClearDotNetCoreBadFiles.exe 用于解决当项目在.NET Core（或.NET Standard）环境下编译后，会在obj文件夹下生成一些文件，
导致无法让.NET Framework成功编译的问题。

使用方法：执行ClearDotNetCoreBadFiles.exe，按照提示按任意键开始清理，再按任意键退出。
