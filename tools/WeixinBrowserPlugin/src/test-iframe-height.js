// 测试iframe高度问题修复 - 在浏览器控制台中运行

console.log('=== 测试iframe高度修复 ===');

// 测试函数：检查iframe高度
function checkIframeHeight() {
  const iframe = document.getElementById('senparc-ai-iframe');
  if (!iframe) {
    console.log('❌ 未找到iframe');
    return null;
  }
  
  const computedStyle = window.getComputedStyle(iframe);
  const rect = iframe.getBoundingClientRect();
  
  console.log('iframe样式信息:');
  console.log('  display:', computedStyle.display);
  console.log('  width:', computedStyle.width);
  console.log('  height:', computedStyle.height);
  console.log('  position:', computedStyle.position);
  console.log('  实际尺寸:', rect.width, 'x', rect.height);
  
  return {
    display: computedStyle.display,
    width: computedStyle.width,
    height: computedStyle.height,
    position: computedStyle.position,
    actualWidth: rect.width,
    actualHeight: rect.height
  };
}

// 测试函数：检查浮窗内容区域
function checkFloatingWindowContent() {
  const content = document.querySelector('.floating-window-content');
  if (!content) {
    console.log('❌ 未找到浮窗内容区域');
    return null;
  }
  
  const computedStyle = window.getComputedStyle(content);
  const rect = content.getBoundingClientRect();
  
  console.log('浮窗内容区域信息:');
  console.log('  flex:', computedStyle.flex);
  console.log('  height:', computedStyle.height);
  console.log('  min-height:', computedStyle.minHeight);
  console.log('  实际尺寸:', rect.width, 'x', rect.height);
  
  return {
    flex: computedStyle.flex,
    height: computedStyle.height,
    minHeight: computedStyle.minHeight,
    actualWidth: rect.width,
    actualHeight: rect.height
  };
}

// 自动测试：多次开关浮窗
async function testMultipleOpenClose() {
  console.log('🧪 开始多次开关测试...');
  
  const button = document.getElementById('senparc-weixin-ai-button');
  if (!button) {
    console.error('❌ 找不到Logo按钮');
    return;
  }
  
  for (let i = 1; i <= 3; i++) {
    console.log(`\n--- 第${i}次测试 ---`);
    
    // 打开浮窗
    console.log('打开浮窗...');
    button.click();
    
    // 等待浮窗打开
    await new Promise(resolve => setTimeout(resolve, 1000));
    
    // 检查iframe状态
    const iframeInfo = checkIframeHeight();
    const contentInfo = checkFloatingWindowContent();
    
    if (iframeInfo && contentInfo) {
      // 检查是否正常
      const isNormal = iframeInfo.actualHeight > 400 && contentInfo.actualHeight > 400;
      console.log(isNormal ? '✅ 高度正常' : '❌ 高度异常');
    }
    
    // 关闭浮窗
    console.log('关闭浮窗...');
    const closeBtn = document.querySelector('.close-button');
    if (closeBtn) {
      closeBtn.click();
    }
    
    // 等待浮窗关闭
    await new Promise(resolve => setTimeout(resolve, 500));
  }
  
  console.log('\n🎉 多次开关测试完成');
}

// 手动检查函数
function manualCheck() {
  console.log('\n=== 手动检查当前状态 ===');
  
  const window = document.getElementById('senparc-weixin-ai-window');
  if (!window) {
    console.log('❌ 浮窗未打开');
    return;
  }
  
  if (window.style.display === 'none' || !window.classList.contains('show')) {
    console.log('❌ 浮窗处于隐藏状态');
    return;
  }
  
  console.log('✅ 浮窗正在显示');
  
  checkFloatingWindowContent();
  checkIframeHeight();
}

// 强制修复函数
function forceFixIframe() {
  console.log('🔧 强制修复iframe...');
  
  const iframe = document.getElementById('senparc-ai-iframe');
  if (!iframe) {
    console.log('❌ 未找到iframe');
    return;
  }
  
  // 强制设置样式
  iframe.style.cssText = `
    width: 100% !important;
    height: 100% !important;
    border: none !important;
    display: block !important;
    min-height: 500px !important;
    position: absolute !important;
    top: 0 !important;
    left: 0 !important;
    right: 0 !important;
    bottom: 0 !important;
    z-index: 1 !important;
  `;
  
  // 检查父容器
  const content = document.querySelector('.floating-window-content');
  if (content) {
    content.style.cssText = `
      flex: 1 !important;
      position: relative !important;
      overflow: hidden !important;
      min-height: 500px !important;
      display: flex !important;
      flex-direction: column !important;
    `;
  }
  
  console.log('✅ 强制修复完成');
  
  // 重新检查
  setTimeout(() => {
    checkIframeHeight();
    checkFloatingWindowContent();
  }, 100);
}

// 导出测试函数
window.iframeHeightTest = {
  check: manualCheck,
  autoTest: testMultipleOpenClose,
  forceFix: forceFixIframe,
  checkIframe: checkIframeHeight,
  checkContent: checkFloatingWindowContent
};

console.log('\n可用命令:');
console.log('  iframeHeightTest.check() - 手动检查当前状态');
console.log('  iframeHeightTest.autoTest() - 自动多次开关测试');
console.log('  iframeHeightTest.forceFix() - 强制修复iframe');
console.log('  iframeHeightTest.checkIframe() - 检查iframe');
console.log('  iframeHeightTest.checkContent() - 检查内容区域');

console.log('\n💡 建议：先打开浮窗，然后运行 iframeHeightTest.check()');
