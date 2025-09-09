@echo off
chcp 65001 >nul
setlocal enabledelayedexpansion

:: Senparc.Weixin.AI Chrome Extension - Release Build Script (Windows)
:: Windows版本的发布构建脚本

title 发布包构建工具 - Senparc.Weixin.AI Chrome Extension

echo.
echo ████████████████████████████████████████████████████
echo.
echo    🚀 Senparc.Weixin.AI Chrome Extension
echo       发布包构建工具 - Windows版本
echo.
echo ████████████████████████████████████████████████████
echo.

:: 检查是否在项目根目录
if not exist "manifest.json" (
    echo ❌ 错误：请在项目根目录运行此脚本
    echo.
    pause
    exit /b 1
)

:: 获取版本号（简单提取）
for /f "tokens=2 delims=:," %%a in ('findstr "version" manifest.json') do (
    set version_line=%%a
    set version_line=!version_line: =!
    set version_line=!version_line:"=!
    set VERSION=!version_line!
)

echo 📦 当前版本：v%VERSION%
echo.

:: 创建构建目录
set BUILD_DIR=dist
set PACKAGE_NAME=weixin-ai-extension-v%VERSION%
set PACKAGE_DIR=%BUILD_DIR%\%PACKAGE_NAME%

echo 🧹 清理旧的构建文件...
if exist "%BUILD_DIR%" rmdir /s /q "%BUILD_DIR%"
mkdir "%PACKAGE_DIR%"

:: 复制必要文件
echo 📋 复制项目文件...

copy "manifest.json" "%PACKAGE_DIR%\" >nul
copy "content.js" "%PACKAGE_DIR%\" >nul
copy "styles.css" "%PACKAGE_DIR%\" >nul
copy "popup.html" "%PACKAGE_DIR%\" >nul
copy "popup.js" "%PACKAGE_DIR%\" >nul
copy "icon.svg" "%PACKAGE_DIR%\" >nul

:: 复制图标文件夹
if exist "icons" (
    xcopy "icons" "%PACKAGE_DIR%\icons\" /e /i /q >nul
)

:: 复制文档文件
echo 📚 复制文档文件...
if exist "README.md" copy "README.md" "%PACKAGE_DIR%\" >nul
if exist "INSTALL.md" copy "INSTALL.md" "%PACKAGE_DIR%\" >nul
if exist "USAGE.md" copy "USAGE.md" "%PACKAGE_DIR%\" >nul
if exist "LICENSE" copy "LICENSE" "%PACKAGE_DIR%\" >nul

:: 创建版本信息文件
echo 📝 创建版本信息...
(
echo Senparc.Weixin.AI Chrome Extension
echo 版本：v%VERSION%
echo 构建时间：%date% %time%
echo 构建环境：Windows
) > "%PACKAGE_DIR%\VERSION.txt"

:: 创建ZIP包（需要PowerShell）
echo 📦 创建ZIP发布包...
powershell -command "Compress-Archive -Path '%PACKAGE_DIR%' -DestinationPath '%BUILD_DIR%\%PACKAGE_NAME%.zip' -Force"

:: 显示构建结果
echo.
echo ✅ 构建完成！
echo.
echo 📦 发布文件位置：
echo    - 文件夹：%PACKAGE_DIR%
echo    - ZIP包：%BUILD_DIR%\%PACKAGE_NAME%.zip
echo.

:: 显示包含的文件
echo 📋 包含的文件：
dir /b "%PACKAGE_DIR%"

:: 创建安装说明
echo 📄 创建安装说明...
(
echo Senparc.Weixin.AI Chrome Extension 安装说明
echo.
echo 版本：v%VERSION%
echo 发布时间：%date% %time%
echo.
echo == 安装步骤 ==
echo.
echo 1. 解压 %PACKAGE_NAME%.zip 到本地文件夹
echo.
echo 2. 打开Chrome浏览器，在地址栏输入：
echo    chrome://extensions/
echo.
echo 3. 开启右上角的"开发者模式"开关
echo.
echo 4. 点击"加载已解压的扩展程序"
echo.
echo 5. 选择解压后的文件夹：%PACKAGE_NAME%
echo.
echo 6. 确认安装完成
echo.
echo == 使用方法 ==
echo.
echo 1. 访问微信官方文档页面：https://developers.weixin.qq.com/doc/
echo.
echo 2. 页面左上角会出现"Senparc.Weixin.AI"绿色按钮
echo.
echo 3. 点击按钮打开AI助手浮窗
echo.
echo == 技术支持 ==
echo.
echo 如遇问题，请查看项目文档：
echo - README.md - 项目介绍
echo - INSTALL.md - 详细安装指南
echo - USAGE.md - 使用方法
echo - TROUBLESHOOTING.md - 故障排除
echo.
echo 或访问项目主页获取帮助。
) > "%BUILD_DIR%\安装说明.txt"

echo.
echo 🎉 发布包构建完成！
echo.
echo 💡 下一步：
echo    1. 测试 %PACKAGE_DIR% 文件夹中的扩展
echo    2. 如果测试通过，可以发布 %BUILD_DIR%\%PACKAGE_NAME%.zip
echo    3. 查看 PUBLISH.md 了解详细发布流程
echo.
echo 🚀 快捷操作：
echo    打开构建目录： explorer %BUILD_DIR%
echo    安装扩展页面： start chrome://extensions/
echo.

pause

