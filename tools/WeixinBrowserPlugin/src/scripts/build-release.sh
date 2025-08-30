#!/bin/bash

# Senparc.Weixin.AI Chrome Extension - Release Build Script
# 用于创建发布版本的自动化脚本

set -e  # 遇到错误时退出

# 颜色定义
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# 显示彩色消息
print_message() {
    color=$1
    message=$2
    echo -e "${color}${message}${NC}"
}

# 获取版本号从manifest.json
get_version() {
    grep '"version"' manifest.json | cut -d'"' -f4
}

print_message $BLUE "🚀 开始构建发布版本..."

# 检查是否在项目根目录
if [ ! -f "manifest.json" ]; then
    print_message $RED "❌ 错误：请在项目根目录运行此脚本"
    exit 1
fi

# 获取版本号
VERSION=$(get_version)
print_message $GREEN "📦 当前版本：v$VERSION"

# 创建构建目录
BUILD_DIR="dist"
PACKAGE_NAME="weixin-ai-extension-v$VERSION"
PACKAGE_DIR="$BUILD_DIR/$PACKAGE_NAME"

print_message $YELLOW "🧹 清理旧的构建文件..."
rm -rf $BUILD_DIR
mkdir -p $PACKAGE_DIR

# 复制必要文件
print_message $YELLOW "📋 复制项目文件..."

# 核心文件
cp manifest.json $PACKAGE_DIR/
cp content.js $PACKAGE_DIR/
cp styles.css $PACKAGE_DIR/
cp popup.html $PACKAGE_DIR/
cp popup.js $PACKAGE_DIR/
cp icon.svg $PACKAGE_DIR/

# 图标文件夹
if [ -d "icons" ]; then
    cp -r icons $PACKAGE_DIR/
fi

# 创建缺失的图标尺寸（如果需要）
print_message $YELLOW "🎨 检查图标文件..."
ICONS_DIR="$PACKAGE_DIR/icons"
mkdir -p $ICONS_DIR

# 如果只有SVG图标，提醒用户创建PNG图标
if [ ! -f "$ICONS_DIR/icon16.png" ]; then
    print_message $YELLOW "⚠️  提醒：建议为Chrome Web Store创建以下PNG图标："
    echo "   - icons/icon16.png (16x16)"
    echo "   - icons/icon48.png (48x48)"
    echo "   - icons/icon128.png (128x128)"
fi

# 复制文档文件
print_message $YELLOW "📚 复制文档文件..."
cp README.md $PACKAGE_DIR/ 2>/dev/null || echo "README.md 不存在"
cp INSTALL.md $PACKAGE_DIR/ 2>/dev/null || echo "INSTALL.md 不存在"
cp USAGE.md $PACKAGE_DIR/ 2>/dev/null || echo "USAGE.md 不存在"
cp LICENSE $PACKAGE_DIR/ 2>/dev/null || echo "LICENSE 不存在"

# 创建版本信息文件
print_message $YELLOW "📝 创建版本信息..."
cat > $PACKAGE_DIR/VERSION.txt << EOF
Senparc.Weixin.AI Chrome Extension
版本：v$VERSION
构建时间：$(date)
构建环境：$(uname -s) $(uname -m)
EOF

# 验证manifest.json
print_message $YELLOW "✅ 验证manifest.json..."
if ! cat $PACKAGE_DIR/manifest.json | python3 -m json.tool > /dev/null 2>&1; then
    print_message $RED "❌ manifest.json 格式错误"
    exit 1
fi

# 创建ZIP包
print_message $YELLOW "📦 创建ZIP发布包..."
cd $BUILD_DIR
zip -r "$PACKAGE_NAME.zip" "$PACKAGE_NAME/"
cd ..

# 显示构建结果
print_message $GREEN "✅ 构建完成！"
echo ""
print_message $BLUE "📦 发布文件位置："
echo "   - 文件夹：$PACKAGE_DIR"
echo "   - ZIP包：$BUILD_DIR/$PACKAGE_NAME.zip"

# 显示文件大小
FOLDER_SIZE=$(du -sh "$PACKAGE_DIR" | cut -f1)
ZIP_SIZE=$(du -sh "$BUILD_DIR/$PACKAGE_NAME.zip" | cut -f1)
echo ""
print_message $BLUE "📊 文件大小："
echo "   - 文件夹：$FOLDER_SIZE"
echo "   - ZIP包：$ZIP_SIZE"

# 显示包含的文件
echo ""
print_message $BLUE "📋 包含的文件："
find "$PACKAGE_DIR" -type f | sed 's|^.*/||' | sort | sed 's/^/   - /'

# 创建安装说明
print_message $YELLOW "📄 创建安装说明..."
cat > "$BUILD_DIR/安装说明.txt" << EOF
Senparc.Weixin.AI Chrome Extension 安装说明

版本：v$VERSION
发布时间：$(date)

== 安装步骤 ==

1. 解压 $PACKAGE_NAME.zip 到本地文件夹

2. 打开Chrome浏览器，在地址栏输入：
   chrome://extensions/

3. 开启右上角的"开发者模式"开关

4. 点击"加载已解压的扩展程序"

5. 选择解压后的文件夹：$PACKAGE_NAME

6. 确认安装完成

== 使用方法 ==

1. 访问微信官方文档页面：https://developers.weixin.qq.com/doc/

2. 页面左上角会出现"Senparc.Weixin.AI"绿色按钮

3. 点击按钮打开AI助手浮窗

== 技术支持 ==

如遇问题，请查看项目文档：
- README.md - 项目介绍
- INSTALL.md - 详细安装指南
- USAGE.md - 使用方法
- TROUBLESHOOTING.md - 故障排除

或访问项目主页获取帮助。
EOF

echo ""
print_message $GREEN "🎉 发布包构建完成！"
print_message $BLUE "💡 下一步："
echo "   1. 测试 $PACKAGE_DIR 文件夹中的扩展"
echo "   2. 如果测试通过，可以发布 $BUILD_DIR/$PACKAGE_NAME.zip"
echo "   3. 查看 PUBLISH.md 了解详细发布流程"

# 提供快捷操作
echo ""
print_message $YELLOW "🚀 快捷操作："
echo "   打开构建目录： open $BUILD_DIR"
echo "   安装扩展页面： open chrome://extensions/"

