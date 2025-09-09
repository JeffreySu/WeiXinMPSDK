// 测试修复脚本 - 在浏览器控制台中运行来验证修复效果

console.log('=== 测试修复效果 ===');

// 测试1: 检查重复按钮问题
function testDuplicateButtons() {
  console.log('🧪 测试1: 检查重复按钮');
  
  const buttons = document.querySelectorAll('#senparc-weixin-ai-button');
  console.log('当前Logo按钮数量:', buttons.length);
  
  if (buttons.length > 1) {
    console.warn('⚠️ 发现重复按钮，应该只有一个');
    return false;
  } else if (buttons.length === 1) {
    console.log('✅ 按钮数量正常');
    return true;
  } else {
    console.log('❓ 没有找到按钮');
    return false;
  }
}

// 测试2: 模拟多次初始化
function testMultipleInit() {
  console.log('🧪 测试2: 模拟多次初始化');
  
  // 手动触发多次初始化
  for (let i = 0; i < 3; i++) {
    window.initializeAssistant();
  }
  
  setTimeout(() => {
    const result = testDuplicateButtons();
    if (result) {
      console.log('✅ 多次初始化测试通过');
    } else {
      console.error('❌ 多次初始化测试失败');
    }
  }, 1000);
}

// 测试3: 测试浮窗开关
function testFloatingWindow() {
  console.log('🧪 测试3: 测试浮窗开关');
  
  const button = document.getElementById('senparc-weixin-ai-button');
  if (!button) {
    console.error('❌ 找不到Logo按钮');
    return;
  }
  
  console.log('点击按钮打开浮窗...');
  button.click();
  
  setTimeout(() => {
    const window = document.getElementById('senparc-weixin-ai-window');
    if (window && window.style.display !== 'none') {
      console.log('✅ 浮窗打开成功');
      
      // 测试关闭
      console.log('测试关闭浮窗...');
      const closeBtn = window.querySelector('.close-button');
      if (closeBtn) {
        closeBtn.click();
        
        setTimeout(() => {
          // 测试重新打开
          console.log('测试重新打开浮窗...');
          button.click();
          
          setTimeout(() => {
            const reopenedWindow = document.getElementById('senparc-weixin-ai-window');
            if (reopenedWindow && reopenedWindow.classList.contains('show')) {
              console.log('✅ 浮窗重新打开成功');
            } else {
              console.error('❌ 浮窗重新打开失败');
            }
          }, 500);
        }, 500);
      }
    } else {
      console.error('❌ 浮窗打开失败');
    }
  }, 500);
}

// 测试4: 检查内存泄漏
function testMemoryLeaks() {
  console.log('🧪 测试4: 检查内存泄漏');
  
  const initialButtons = document.querySelectorAll('#senparc-weixin-ai-button').length;
  const initialWindows = document.querySelectorAll('#senparc-weixin-ai-window').length;
  
  console.log('初始状态 - 按钮:', initialButtons, '浮窗:', initialWindows);
  
  // 多次创建和销毁
  for (let i = 0; i < 5; i++) {
    window.initializeAssistant();
  }
  
  setTimeout(() => {
    const finalButtons = document.querySelectorAll('#senparc-weixin-ai-button').length;
    const finalWindows = document.querySelectorAll('#senparc-weixin-ai-window').length;
    
    console.log('最终状态 - 按钮:', finalButtons, '浮窗:', finalWindows);
    
    if (finalButtons <= 1 && finalWindows <= 1) {
      console.log('✅ 内存泄漏测试通过');
    } else {
      console.warn('⚠️ 可能存在内存泄漏');
    }
  }, 2000);
}

// 执行所有测试
function runAllTests() {
  console.log('🚀 开始执行所有测试...');
  
  testDuplicateButtons();
  
  setTimeout(() => testMultipleInit(), 1000);
  setTimeout(() => testFloatingWindow(), 3000);
  setTimeout(() => testMemoryLeaks(), 6000);
  
  console.log('⏰ 所有测试将在10秒内完成');
}

// 提供手动测试函数
window.testFixes = {
  runAll: runAllTests,
  duplicateButtons: testDuplicateButtons,
  multipleInit: testMultipleInit,
  floatingWindow: testFloatingWindow,
  memoryLeaks: testMemoryLeaks
};

console.log('测试函数已准备就绪，运行 testFixes.runAll() 开始测试');
console.log('或单独运行：');
console.log('- testFixes.duplicateButtons()');
console.log('- testFixes.multipleInit()');
console.log('- testFixes.floatingWindow()');
console.log('- testFixes.memoryLeaks()');
