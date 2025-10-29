#!/bin/bash
# validate-build.sh
# 验证构建包的完整性

# 颜色定义
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# 获取当前版本号
VERSION=$(grep -o '"version": "[^"]*"' src/manifest.json | grep -o '[0-9]*\.[0-9]*\.[0-9]*')
ZIP_FILE="dist/senparc-weixin-ai-${VERSION}.zip"

echo -e "${BLUE}🔍 验证构建包: $ZIP_FILE${NC}"

# 检查ZIP文件是否存在
if [ ! -f "$ZIP_FILE" ]; then
    echo -e "${RED}❌ ZIP文件不存在: $ZIP_FILE${NC}"
    echo -e "${YELLOW}请先运行构建: npm run build${NC}"
    exit 1
fi

echo -e "${GREEN}✅ ZIP文件存在${NC}"

# 获取ZIP文件内容列表
echo -e "${BLUE}📋 检查ZIP文件内容...${NC}"
ZIP_CONTENTS=$(unzip -l "$ZIP_FILE" 2>/dev/null)

# 必需的文件列表
REQUIRED_FILES=(
    "manifest.json"
    "content.js"
    "styles.css"
    "popup.html"
    "popup.js"
    "icons/icon16.png"
    "icons/icon48.png"
    "icons/icon128.png"
)

# 检查每个必需文件
MISSING_FILES=()
for file in "${REQUIRED_FILES[@]}"; do
    if echo "$ZIP_CONTENTS" | grep -q "$file"; then
        echo -e "${GREEN}✅ $file${NC}"
    else
        echo -e "${RED}❌ $file${NC}"
        MISSING_FILES+=("$file")
    fi
done

# 检查是否有缺失文件
if [ ${#MISSING_FILES[@]} -gt 0 ]; then
    echo -e "${RED}❌ 发现缺失文件:${NC}"
    for file in "${MISSING_FILES[@]}"; do
        echo -e "${RED}   - $file${NC}"
    done
    echo -e "${YELLOW}请检查构建配置和源文件${NC}"
    exit 1
fi

# 验证manifest.json中的图标路径
echo -e "${BLUE}🔍 验证manifest.json配置...${NC}"

# 解压manifest.json进行检查
TEMP_DIR=$(mktemp -d)
unzip -q "$ZIP_FILE" manifest.json -d "$TEMP_DIR"
MANIFEST_FILE="$TEMP_DIR/manifest.json"

# 检查图标配置
ICON_16=$(grep -o '"16": "[^"]*"' "$MANIFEST_FILE" | grep -o '[^"]*\.png')
ICON_48=$(grep -o '"48": "[^"]*"' "$MANIFEST_FILE" | grep -o '[^"]*\.png')
ICON_128=$(grep -o '"128": "[^"]*"' "$MANIFEST_FILE" | grep -o '[^"]*\.png')

echo -e "${BLUE}📁 manifest.json中的图标路径:${NC}"
echo -e "   16px: $ICON_16"
echo -e "   48px: $ICON_48"
echo -e "   128px: $ICON_128"

# 验证图标路径是否在ZIP中存在
ICON_ERRORS=()
for icon in "$ICON_16" "$ICON_48" "$ICON_128"; do
    if ! echo "$ZIP_CONTENTS" | grep -q "$icon"; then
        ICON_ERRORS+=("$icon")
    fi
done

if [ ${#ICON_ERRORS[@]} -gt 0 ]; then
    echo -e "${RED}❌ manifest.json中的图标路径在ZIP中不存在:${NC}"
    for icon in "${ICON_ERRORS[@]}"; do
        echo -e "${RED}   - $icon${NC}"
    done
    rm -rf "$TEMP_DIR"
    exit 1
fi

echo -e "${GREEN}✅ 图标路径验证通过${NC}"

# 检查版本号一致性
echo -e "${BLUE}🔍 验证版本号一致性...${NC}"

MANIFEST_VERSION=$(grep -o '"version": "[^"]*"' "$MANIFEST_FILE" | grep -o '[0-9]*\.[0-9]*\.[0-9]*')
PACKAGE_VERSION=$(grep -o '"version": "[^"]*"' package.json | grep -o '[0-9]*\.[0-9]*\.[0-9]*')
BUILD_VERSION=$(grep -o "version: '[^']*'" build.config.js | grep -o '[0-9]*\.[0-9]*\.[0-9]*')

echo -e "   manifest.json: $MANIFEST_VERSION"
echo -e "   package.json: $PACKAGE_VERSION" 
echo -e "   build.config.js: $BUILD_VERSION"
echo -e "   ZIP文件名: $VERSION"

if [ "$MANIFEST_VERSION" = "$PACKAGE_VERSION" ] && [ "$PACKAGE_VERSION" = "$BUILD_VERSION" ] && [ "$BUILD_VERSION" = "$VERSION" ]; then
    echo -e "${GREEN}✅ 版本号一致性验证通过${NC}"
else
    echo -e "${RED}❌ 版本号不一致${NC}"
    rm -rf "$TEMP_DIR"
    exit 1
fi

# 检查文件大小
echo -e "${BLUE}📊 文件大小信息:${NC}"
ZIP_SIZE=$(ls -lh "$ZIP_FILE" | awk '{print $5}')
echo -e "   ZIP包大小: $ZIP_SIZE"

# 显示详细内容
echo -e "${BLUE}📋 ZIP包完整内容:${NC}"
unzip -l "$ZIP_FILE" | grep -E "^\s*[0-9]+" | head -n -1

# 清理临时文件
rm -rf "$TEMP_DIR"

echo -e "${GREEN}🎉 构建包验证完成！${NC}"
echo -e "${BLUE}📦 可以安全上传到Chrome Web Store${NC}"

# 提供上传建议
echo -e "${YELLOW}💡 上传建议:${NC}"
echo -e "   1. 确保Chrome Web Store开发者账号已准备"
echo -e "   2. 准备应用截图和描述"
echo -e "   3. 上传文件: $ZIP_FILE"
echo -e "   4. 等待审核（通常1-3个工作日）"
