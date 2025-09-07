// 调试脚本 - 在浏览器控制台中运行此脚本来检查插件状态

console.log('=== Senparc.Weixin.AI 插件调试信息 ===');

// 1. 检查当前页面信息
console.log('1. 页面信息:');
console.log('  URL:', window.location.href);
console.log('  域名:', window.location.hostname);
console.log('  是否微信域名:', window.location.hostname.endsWith('weixin.qq.com'));

// 2. 检查插件是否加载
console.log('2. 插件状态:');
console.log('  WeixinAIAssistant类:', typeof window.WeixinAIAssistant);

// 3. 检查DOM元素
console.log('3. DOM元素:');
const logoButton = document.getElementById('senparc-weixin-ai-button');
console.log('  Logo按钮:', logoButton);
if (logoButton) {
  console.log('  按钮位置:', logoButton.getBoundingClientRect());
  console.log('  按钮样式:', window.getComputedStyle(logoButton));
}

// 4. 检查CSS样式
console.log('4. CSS样式:');
const styleSheets = Array.from(document.styleSheets);
const extensionStyles = styleSheets.find(sheet => {
  try {
    return sheet.href && sheet.href.includes('extension://');
  } catch (e) {
    return false;
  }
});
console.log('  扩展样式表:', extensionStyles);

// 5. 手动创建按钮测试
console.log('5. 手动测试:');
function createTestButton() {
  const testButton = document.createElement('div');
  testButton.id = 'test-button';
  testButton.style.cssText = `
    position: fixed;
    top: 20px;
    right: 20px;
    z-index: 10000;
    background: red;
    color: white;
    padding: 10px;
    border-radius: 5px;
    cursor: pointer;
  `;
  testButton.textContent = '测试按钮';
  testButton.onclick = () => alert('测试按钮点击成功！');
  document.body.appendChild(testButton);
  console.log('  测试按钮已创建');
}

// 6. 检查扩展权限
console.log('6. 扩展权限:');
if (typeof chrome !== 'undefined' && chrome.runtime) {
  console.log('  Chrome扩展API可用');
  console.log('  扩展ID:', chrome.runtime.id);
} else {
  console.log('  Chrome扩展API不可用');
}

// 7. 提供手动初始化函数
window.debugInit = function() {
  console.log('手动初始化插件...');
  if (window.WeixinAIAssistant) {
    new window.WeixinAIAssistant();
  } else {
    console.error('WeixinAIAssistant类未找到');
  }
};

console.log('=== 调试完成 ===');
console.log('可用命令:');
console.log('  createTestButton() - 创建测试按钮');
console.log('  debugInit() - 手动初始化插件');
