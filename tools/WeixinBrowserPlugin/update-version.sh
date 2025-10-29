#!/bin/bash
# update-version.sh
# 自动更新插件版本号的脚本
# 使用方法: ./update-version.sh 0.1.3

# 颜色定义
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# 检查参数
if [ -z "$1" ]; then
    echo -e "${RED}❌ 请提供版本号${NC}"
    echo -e "${YELLOW}使用方法: ./update-version.sh 0.1.3${NC}"
    exit 1
fi

NEW_VERSION=$1

# 验证版本号格式 (x.y.z)
if ! [[ $NEW_VERSION =~ ^[0-9]+\.[0-9]+\.[0-9]+$ ]]; then
    echo -e "${RED}❌ 版本号格式错误，请使用 x.y.z 格式，例如: 0.1.3${NC}"
    exit 1
fi

# 检查必要文件是否存在
FILES=("src/manifest.json" "package.json" "build.config.js")
for file in "${FILES[@]}"; do
    if [ ! -f "$file" ]; then
        echo -e "${RED}❌ 文件不存在: $file${NC}"
        exit 1
    fi
done

echo -e "${BLUE}🔄 更新版本号到 $NEW_VERSION ...${NC}"

# 备份当前版本号
OLD_VERSION_MANIFEST=$(grep -o '"version": "[^"]*"' src/manifest.json | grep -o '[0-9]*\.[0-9]*\.[0-9]*')
OLD_VERSION_PACKAGE=$(grep -o '"version": "[^"]*"' package.json | grep -o '[0-9]*\.[0-9]*\.[0-9]*')
OLD_VERSION_BUILD=$(grep -o "version: '[^']*'" build.config.js | grep -o '[0-9]*\.[0-9]*\.[0-9]*')

echo -e "${YELLOW}📋 当前版本号:${NC}"
echo -e "   manifest.json: $OLD_VERSION_MANIFEST"
echo -e "   package.json: $OLD_VERSION_PACKAGE"
echo -e "   build.config.js: $OLD_VERSION_BUILD"

# 检查版本号是否一致
if [ "$OLD_VERSION_MANIFEST" != "$OLD_VERSION_PACKAGE" ] || [ "$OLD_VERSION_PACKAGE" != "$OLD_VERSION_BUILD" ]; then
    echo -e "${YELLOW}⚠️  警告：当前版本号不一致，即将统一到 $NEW_VERSION${NC}"
fi

# 更新 manifest.json
echo -e "${BLUE}📝 更新 src/manifest.json...${NC}"
if sed -i '' "s/\"version\": \".*\"/\"version\": \"$NEW_VERSION\"/" src/manifest.json; then
    echo -e "${GREEN}✅ manifest.json 更新成功${NC}"
else
    echo -e "${RED}❌ manifest.json 更新失败${NC}"
    exit 1
fi

# 更新 package.json
echo -e "${BLUE}📝 更新 package.json...${NC}"
if sed -i '' "s/\"version\": \".*\"/\"version\": \"$NEW_VERSION\"/" package.json; then
    echo -e "${GREEN}✅ package.json 更新成功${NC}"
else
    echo -e "${RED}❌ package.json 更新失败${NC}"
    exit 1
fi

# 更新 build.config.js
echo -e "${BLUE}📝 更新 build.config.js...${NC}"
if sed -i '' "s/version: '.*'/version: '$NEW_VERSION'/" build.config.js; then
    echo -e "${GREEN}✅ build.config.js 更新成功${NC}"
else
    echo -e "${RED}❌ build.config.js 更新失败${NC}"
    exit 1
fi

echo -e "${GREEN}✅ 版本号更新完成${NC}"

# 验证更新结果
echo -e "${BLUE}🔍 验证更新结果...${NC}"
NEW_VERSION_MANIFEST=$(grep -o '"version": "[^"]*"' src/manifest.json | grep -o '[0-9]*\.[0-9]*\.[0-9]*')
NEW_VERSION_PACKAGE=$(grep -o '"version": "[^"]*"' package.json | grep -o '[0-9]*\.[0-9]*\.[0-9]*')
NEW_VERSION_BUILD=$(grep -o "version: '[^']*'" build.config.js | grep -o '[0-9]*\.[0-9]*\.[0-9]*')

echo -e "${YELLOW}📋 新版本号:${NC}"
echo -e "   manifest.json: $NEW_VERSION_MANIFEST"
echo -e "   package.json: $NEW_VERSION_PACKAGE"
echo -e "   build.config.js: $NEW_VERSION_BUILD"

# 检查是否更新成功
if [ "$NEW_VERSION_MANIFEST" = "$NEW_VERSION" ] && [ "$NEW_VERSION_PACKAGE" = "$NEW_VERSION" ] && [ "$NEW_VERSION_BUILD" = "$NEW_VERSION" ]; then
    echo -e "${GREEN}✅ 版本号验证通过${NC}"
else
    echo -e "${RED}❌ 版本号验证失败${NC}"
    exit 1
fi

# 构建测试
echo -e "${BLUE}🔨 运行构建测试...${NC}"
if npm run build > /dev/null 2>&1; then
    echo -e "${GREEN}✅ 构建成功！${NC}"
    echo -e "${GREEN}📦 发布包: dist/senparc-weixin-ai-$NEW_VERSION.zip${NC}"
else
    echo -e "${RED}❌ 构建失败，请检查错误信息${NC}"
    npm run build
    exit 1
fi

# Git操作提示
echo -e "${BLUE}🏷️  Git操作建议:${NC}"
echo -e "${YELLOW}   git add .${NC}"
echo -e "${YELLOW}   git commit -m \"Release version $NEW_VERSION\"${NC}"
echo -e "${YELLOW}   git tag -a \"v$NEW_VERSION\" -m \"Release version $NEW_VERSION\"${NC}"
echo -e "${YELLOW}   git push origin main${NC}"
echo -e "${YELLOW}   git push origin v$NEW_VERSION${NC}"

# 询问是否自动执行Git操作
echo -e "${BLUE}❓ 是否自动执行Git操作？ (y/n)${NC}"
read -r response

if [[ "$response" =~ ^([yY][eE][sS]|[yY])$ ]]; then
    echo -e "${BLUE}🔄 执行Git操作...${NC}"
    
    # 添加文件到暂存区
    git add src/manifest.json package.json build.config.js
    
    # 提交更改
    if git commit -m "Release version $NEW_VERSION"; then
        echo -e "${GREEN}✅ Git提交成功${NC}"
    else
        echo -e "${YELLOW}⚠️  Git提交失败或没有需要提交的更改${NC}"
    fi
    
    # 创建标签
    if git tag -a "v$NEW_VERSION" -m "Release version $NEW_VERSION"; then
        echo -e "${GREEN}✅ Git标签创建成功${NC}"
        
        echo -e "${BLUE}❓ 是否推送到远程仓库？ (y/n)${NC}"
        read -r push_response
        
        if [[ "$push_response" =~ ^([yY][eE][sS]|[yY])$ ]]; then
            echo -e "${BLUE}🚀 推送到远程仓库...${NC}"
            
            if git push origin main && git push origin "v$NEW_VERSION"; then
                echo -e "${GREEN}✅ 推送成功${NC}"
            else
                echo -e "${RED}❌ 推送失败，请手动执行${NC}"
            fi
        else
            echo -e "${YELLOW}ℹ️  请手动推送: git push origin main && git push origin v$NEW_VERSION${NC}"
        fi
    else
        echo -e "${RED}❌ Git标签创建失败${NC}"
        exit 1
    fi
else
    echo -e "${YELLOW}ℹ️  请手动执行上述Git命令${NC}"
fi

echo -e "${GREEN}🎉 版本发布准备完成！${NC}"
echo -e "${BLUE}📋 接下来可以:${NC}"
echo -e "   1. 测试发布包功能"
echo -e "   2. 创建GitHub Release"
echo -e "   3. 上传到Chrome Web Store"
