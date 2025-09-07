@echo off
chcp 65001 >nul
echo.
echo ==========================================
echo    Chrome Web Store 发布包构建工具
echo ==========================================
echo.

:: 检查 Node.js 是否安装
node --version >nul 2>&1
if errorlevel 1 (
    echo ❌ 错误: 未检测到 Node.js，请先安装 Node.js
    echo 下载地址: https://nodejs.org/
    pause
    exit /b 1
)

:: 检查是否存在 package.json
if not exist package.json (
    echo ❌ 错误: 未找到 package.json 文件
    echo 请确保在正确的项目目录中运行此脚本
    pause
    exit /b 1
)

:: 安装依赖
echo 📦 检查并安装依赖...
if not exist node_modules (
    echo 正在安装依赖包...
    npm install
    if errorlevel 1 (
        echo ❌ 依赖安装失败
        pause
        exit /b 1
    )
)

:: 清理旧的发布文件
echo 🧹 清理旧的发布文件...
if exist release (
    rmdir /s /q release
)

:: 构建发布包
echo 🚀 开始构建 Chrome Web Store 发布包...
npm run build:release

if errorlevel 1 (
    echo.
    echo ❌ 构建失败！请检查错误信息
    pause
    exit /b 1
)

echo.
echo ✅ 构建完成！
echo.
echo 📁 发布文件位置:
echo    - release/ 目录包含所有发布文件
echo    - release/senparc-weixin-ai-v0.1.0-release.zip 为上传包
echo.
echo 📋 下一步操作:
echo    1. 打开 Chrome Web Store Developer Dashboard
echo    2. 上传 release 目录中的 .zip 文件
echo    3. 填写应用信息和截图
echo    4. 提交审核
echo.
echo 📖 详细发布指南请参考: CHROME_STORE_RELEASE.md
echo.

:: 询问是否打开发布目录
set /p choice="是否打开发布目录? (y/n): "
if /i "%choice%"=="y" (
    start explorer release
)

pause
