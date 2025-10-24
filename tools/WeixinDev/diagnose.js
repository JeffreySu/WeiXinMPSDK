const fs = require('fs');
const path = require('path');

console.log('🔍 WeixinDev 扩展诊断工具');
console.log('================================');

// 检查文件是否存在
const files = [
    'package.json',
    'out/extension.js',
    'out/sidebarProvider.js',
    'out/apiService.js',
    'out/interfaceInserter.js'
];

console.log('\n📁 文件检查:');
files.forEach(file => {
    const exists = fs.existsSync(file);
    console.log(`${exists ? '✅' : '❌'} ${file}`);
});

// 检查package.json内容
console.log('\n📋 Package.json 关键配置:');
try {
    const pkg = JSON.parse(fs.readFileSync('package.json', 'utf8'));
    console.log(`✅ Name: ${pkg.name}`);
    console.log(`✅ Publisher: ${pkg.publisher}`);
    console.log(`✅ Version: ${pkg.version}`);
    console.log(`✅ Main: ${pkg.main}`);
    console.log(`✅ Activation Events: ${JSON.stringify(pkg.activationEvents)}`);
    console.log(`✅ Commands: ${pkg.contributes.commands.length} 个`);
    console.log(`✅ Views: ${Object.keys(pkg.contributes.views || {}).length} 个容器`);
} catch (e) {
    console.log(`❌ 读取package.json失败: ${e.message}`);
}

console.log('\n🎯 建议的测试步骤:');
console.log('1. 重新加载VSCode窗口 (Cmd+Shift+P -> "Developer: Reload Window")');
console.log('2. 打开命令面板 (Cmd+Shift+P) 搜索 "WeixinDev"');
console.log('3. 查找左侧活动栏的代码图标 (</>)');
console.log('4. 在.cs文件中右键查找 "插入微信接口"');
console.log('\n如果仍然有问题，请提供VSCode的开发者控制台错误信息。');

