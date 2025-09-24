@echo off
rem update-version.bat
rem 自动更新插件版本号的脚本 (Windows版本)
rem 使用方法: update-version.bat 0.1.3

setlocal enabledelayedexpansion
chcp 65001 >nul

rem 检查参数
if "%1"=="" (
    echo ❌ 请提供版本号
    echo 使用方法: update-version.bat 0.1.3
    exit /b 1
)

set NEW_VERSION=%1

rem 验证版本号格式 (简单检查)
echo %NEW_VERSION% | findstr /R "^[0-9]*\.[0-9]*\.[0-9]*$" >nul
if errorlevel 1 (
    echo ❌ 版本号格式错误，请使用 x.y.z 格式，例如: 0.1.3
    exit /b 1
)

rem 检查必要文件是否存在
if not exist "src\manifest.json" (
    echo ❌ 文件不存在: src\manifest.json
    exit /b 1
)
if not exist "package.json" (
    echo ❌ 文件不存在: package.json
    exit /b 1
)
if not exist "build.config.js" (
    echo ❌ 文件不存在: build.config.js
    exit /b 1
)

echo 🔄 更新版本号到 %NEW_VERSION% ...

rem 获取当前版本号
for /f "tokens=2 delims=:" %%a in ('findstr "version" src\manifest.json') do (
    set manifest_line=%%a
    goto :get_manifest_version
)
:get_manifest_version
for /f "tokens=2 delims=^"" %%b in ("!manifest_line!") do set OLD_VERSION_MANIFEST=%%b

for /f "tokens=2 delims=:" %%a in ('findstr "version" package.json') do (
    set package_line=%%a
    goto :get_package_version
)
:get_package_version
for /f "tokens=2 delims=^"" %%b in ("!package_line!") do set OLD_VERSION_PACKAGE=%%b

echo 📋 当前版本号:
echo    manifest.json: %OLD_VERSION_MANIFEST%
echo    package.json: %OLD_VERSION_PACKAGE%

rem 创建临时文件进行替换
echo 📝 更新 src\manifest.json...
powershell -Command "(Get-Content 'src\manifest.json') -replace '\"version\": \".*\"', '\"version\": \"%NEW_VERSION%\"' | Set-Content 'src\manifest.json'"
if errorlevel 1 (
    echo ❌ manifest.json 更新失败
    exit /b 1
)
echo ✅ manifest.json 更新成功

echo 📝 更新 package.json...
powershell -Command "(Get-Content 'package.json') -replace '\"version\": \".*\"', '\"version\": \"%NEW_VERSION%\"' | Set-Content 'package.json'"
if errorlevel 1 (
    echo ❌ package.json 更新失败
    exit /b 1
)
echo ✅ package.json 更新成功

echo 📝 更新 build.config.js...
powershell -Command "(Get-Content 'build.config.js') -replace \"version: '.*'\", \"version: '%NEW_VERSION%'\" | Set-Content 'build.config.js'"
if errorlevel 1 (
    echo ❌ build.config.js 更新失败
    exit /b 1
)
echo ✅ build.config.js 更新成功

echo ✅ 版本号更新完成

rem 构建测试
echo 🔨 运行构建测试...
call npm run build >nul 2>&1
if errorlevel 1 (
    echo ❌ 构建失败，请检查错误信息
    call npm run build
    exit /b 1
)

echo ✅ 构建成功！
echo 📦 发布包: dist\senparc-weixin-ai-%NEW_VERSION%.zip

echo.
echo 🏷️ Git操作建议:
echo    git add .
echo    git commit -m "Release version %NEW_VERSION%"
echo    git tag -a "v%NEW_VERSION%" -m "Release version %NEW_VERSION%"
echo    git push origin main
echo    git push origin v%NEW_VERSION%

echo.
echo 🎉 版本发布准备完成！
echo 📋 接下来可以:
echo    1. 测试发布包功能
echo    2. 创建GitHub Release
echo    3. 上传到Chrome Web Store

pause
