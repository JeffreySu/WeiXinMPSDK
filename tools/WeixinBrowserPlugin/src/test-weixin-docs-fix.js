// 测试微信开发者文档停靠功能修复
// 在微信开发者文档页面的控制台中运行

console.log('🧪 测试微信开发者文档停靠功能修复');
console.log('当前页面:', window.location.href);

// 等待插件加载
function waitForPlugin() {
  return new Promise((resolve) => {
    const check = () => {
      if (window.globalAssistantInstance) {
        console.log('✅ 插件已加载');
        resolve(window.globalAssistantInstance);
      } else {
        console.log('⏳ 等待插件加载...');
        setTimeout(check, 1000);
      }
    };
    check();
  });
}

// 测试停靠功能
async function testDockFunction() {
  console.log('\n🔧 开始测试停靠功能...');
  
  try {
    const assistant = await waitForPlugin();
    
    // 1. 先打开浮窗
    if (!assistant.isWindowOpen) {
      console.log('1️⃣ 打开浮窗...');
      assistant.openFloatingWindow();
      await new Promise(resolve => setTimeout(resolve, 1000));
    }
    
    // 2. 检查初始状态
    console.log('2️⃣ 检查初始状态...');
    const initialBodyMargin = window.getComputedStyle(document.body).marginRight;
    console.log(`初始body margin-right: ${initialBodyMargin}`);
    
    // 3. 切换到停靠模式
    console.log('3️⃣ 切换到停靠模式...');
    assistant.setDockMode();
    
    // 等待动画完成
    await new Promise(resolve => setTimeout(resolve, 500));
    
    // 4. 检查停靠后的状态
    console.log('4️⃣ 检查停靠后状态...');
    const dockedBodyMargin = window.getComputedStyle(document.body).marginRight;
    const dockedBodyMarginValue = parseFloat(dockedBodyMargin);
    const expectedMargin = window.innerWidth * 0.4;
    
    console.log(`停靠后body margin-right: ${dockedBodyMargin}`);
    console.log(`期望值: ${expectedMargin}px`);
    console.log(`是否正确: ${Math.abs(dockedBodyMarginValue - expectedMargin) < 50 ? '✅' : '❌'}`);
    
    // 5. 检查页面内容是否收窄
    console.log('5️⃣ 检查页面内容收窄情况...');
    const bodyRect = document.body.getBoundingClientRect();
    const effectiveWidth = bodyRect.width;
    const viewportWidth = window.innerWidth;
    const expectedWidth = viewportWidth * 0.6;
    
    console.log(`页面有效宽度: ${effectiveWidth}px`);
    console.log(`视口宽度: ${viewportWidth}px`);
    console.log(`期望宽度: ${expectedWidth}px`);
    console.log(`内容是否收窄: ${effectiveWidth <= expectedWidth * 1.1 ? '✅' : '❌'}`);
    
    // 6. 检查浮窗是否正确显示
    console.log('6️⃣ 检查浮窗显示状态...');
    const floatingWindow = document.getElementById('senparc-weixin-ai-window');
    if (floatingWindow) {
      const windowRect = floatingWindow.getBoundingClientRect();
      const windowStyle = window.getComputedStyle(floatingWindow);
      
      console.log('浮窗状态:');
      console.log(`- 显示: ${windowStyle.display !== 'none' ? '✅' : '❌'}`);
      console.log(`- 可见: ${windowStyle.visibility !== 'hidden' ? '✅' : '❌'}`);
      console.log(`- 透明度: ${parseFloat(windowStyle.opacity) > 0.5 ? '✅' : '❌'}`);
      console.log(`- 位置: right=${window.innerWidth - windowRect.right}px`);
      console.log(`- 尺寸: ${windowRect.width}x${windowRect.height}`);
    } else {
      console.log('❌ 浮窗元素未找到');
    }
    
    // 7. 测试切换回悬浮模式
    console.log('7️⃣ 测试切换回悬浮模式...');
    assistant.setFloatingMode();
    
    await new Promise(resolve => setTimeout(resolve, 500));
    
    const restoredBodyMargin = window.getComputedStyle(document.body).marginRight;
    console.log(`恢复后body margin-right: ${restoredBodyMargin}`);
    console.log(`是否恢复: ${parseFloat(restoredBodyMargin) < 50 ? '✅' : '❌'}`);
    
    // 8. 再次测试停靠
    console.log('8️⃣ 再次测试停靠...');
    assistant.setDockMode();
    
    await new Promise(resolve => setTimeout(resolve, 500));
    
    const secondDockMargin = window.getComputedStyle(document.body).marginRight;
    console.log(`二次停靠margin-right: ${secondDockMargin}`);
    console.log(`二次停靠是否成功: ${Math.abs(parseFloat(secondDockMargin) - expectedMargin) < 50 ? '✅' : '❌'}`);
    
    console.log('\n🎉 停靠功能测试完成!');
    
  } catch (error) {
    console.error('❌ 测试过程中出现错误:', error);
  }
}

// 分析页面结构
function analyzePageStructure() {
  console.log('\n📋 分析页面结构...');
  
  const importantSelectors = [
    'body',
    '#app',
    '.page-container',
    '.doc-container',
    '.main-container',
    '.main-content',
    '.doc-content'
  ];
  
  importantSelectors.forEach(selector => {
    const element = document.querySelector(selector);
    if (element) {
      const rect = element.getBoundingClientRect();
      const style = window.getComputedStyle(element);
      
      console.log(`${selector}:`, {
        width: `${rect.width}px`,
        position: style.position,
        zIndex: style.zIndex,
        marginRight: style.marginRight,
        maxWidth: style.maxWidth
      });
    } else {
      console.log(`${selector}: 未找到`);
    }
  });
}

// 手动应用修复
function manualFix() {
  console.log('\n🔧 手动应用修复...');
  
  // 强制应用停靠样式
  document.body.classList.add('senparc-docked');
  document.body.style.setProperty('margin-right', '40%', 'important');
  document.body.style.setProperty('box-sizing', 'border-box', 'important');
  document.body.style.setProperty('overflow-x', 'hidden', 'important');
  
  // 处理可能的容器
  const containers = document.querySelectorAll('#app, .page-container, .doc-container, .main-container, .main-content, .doc-content');
  containers.forEach(container => {
    container.style.setProperty('width', '100%', 'important');
    container.style.setProperty('max-width', '100%', 'important');
    container.style.setProperty('margin-right', '0', 'important');
    container.style.setProperty('box-sizing', 'border-box', 'important');
  });
  
  console.log('✅ 手动修复已应用');
  
  setTimeout(() => {
    const bodyMargin = window.getComputedStyle(document.body).marginRight;
    console.log('手动修复结果:', bodyMargin);
  }, 200);
}

// 清除手动修复
function clearManualFix() {
  console.log('\n🧹 清除手动修复...');
  
  document.body.classList.remove('senparc-docked');
  document.body.style.marginRight = '';
  document.body.style.boxSizing = '';
  document.body.style.overflowX = '';
  
  const containers = document.querySelectorAll('#app, .page-container, .doc-container, .main-container, .main-content, .doc-content');
  containers.forEach(container => {
    container.style.width = '';
    container.style.maxWidth = '';
    container.style.marginRight = '';
    container.style.boxSizing = '';
  });
  
  console.log('✅ 手动修复已清除');
}

// 导出测试函数
window.testWeixinDockFix = {
  test: testDockFunction,
  analyze: analyzePageStructure,
  manualFix: manualFix,
  clearFix: clearManualFix
};

console.log('\n🎮 可用的测试命令:');
console.log('  testWeixinDockFix.test() - 完整功能测试');
console.log('  testWeixinDockFix.analyze() - 分析页面结构');
console.log('  testWeixinDockFix.manualFix() - 手动应用修复');
console.log('  testWeixinDockFix.clearFix() - 清除手动修复');

console.log('\n💡 建议先运行: testWeixinDockFix.analyze()');
console.log('然后运行: testWeixinDockFix.test()');

// 自动开始分析
analyzePageStructure();
