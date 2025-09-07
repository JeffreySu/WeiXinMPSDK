// 测试停靠功能修复的脚本
// 在微信开发者文档页面的控制台中运行: copy(document.querySelector('script[src*="test-dock-fix.js"]')?.textContent || '请先加载脚本')

console.log('🔧 测试停靠功能修复效果');

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

// 详细检查浮窗状态
function checkFloatingWindowState(stepName) {
  const floatingWindow = document.getElementById('senparc-weixin-ai-window');
  if (!floatingWindow) {
    console.log(`❌ ${stepName}: 浮窗元素未找到`);
    return null;
  }
  
  const rect = floatingWindow.getBoundingClientRect();
  const style = window.getComputedStyle(floatingWindow);
  const classList = Array.from(floatingWindow.classList);
  
  const info = {
    step: stepName,
    classList: classList,
    styles: {
      display: style.display,
      visibility: style.visibility,
      opacity: parseFloat(style.opacity),
      position: style.position,
      top: style.top,
      right: style.right,
      left: style.left,
      width: style.width,
      height: style.height,
      transform: style.transform,
      zIndex: style.zIndex
    },
    rect: {
      top: Math.round(rect.top),
      right: Math.round(rect.right),
      bottom: Math.round(rect.bottom),
      left: Math.round(rect.left),
      width: Math.round(rect.width),
      height: Math.round(rect.height)
    },
    viewport: {
      width: window.innerWidth,
      height: window.innerHeight
    }
  };
  
  // 判断是否正确停靠到右侧
  const isDockedCorrectly = 
    info.classList.includes('docked-mode') &&
    info.rect.right >= info.viewport.width - 10 && // 右边缘接近视口右边
    info.rect.left >= info.viewport.width * 0.55 && // 左边缘在60%右侧
    info.rect.width >= info.viewport.width * 0.35 && // 宽度约40%
    info.rect.height >= info.viewport.height * 0.9; // 高度接近全屏
  
  // 判断是否正确悬浮在中央
  const isFloatingCorrectly = 
    info.classList.includes('floating-mode') &&
    Math.abs(info.rect.left + info.rect.width/2 - info.viewport.width/2) < 50 && // 水平居中
    Math.abs(info.rect.top + info.rect.height/2 - info.viewport.height/2) < 50; // 垂直居中
  
  info.isCorrect = isDockedCorrectly || isFloatingCorrectly;
  info.expectedMode = info.classList.includes('docked-mode') ? 'docked' : 'floating';
  
  console.log(`📊 ${stepName}:`, {
    mode: info.expectedMode,
    isCorrect: info.isCorrect ? '✅' : '❌',
    classes: info.classList,
    position: `${info.rect.left},${info.rect.top} ${info.rect.width}x${info.rect.height}`,
    styles: info.styles
  });
  
  return info;
}

// 测试修复后的停靠功能
async function testDockFix() {
  console.log('\n🎯 开始测试停靠功能修复...');
  
  try {
    const assistant = await waitForPlugin();
    
    // 1. 清理初始状态
    console.log('\n1️⃣ 清理初始状态...');
    if (assistant.isWindowOpen) {
      assistant.closeFloatingWindow();
      await new Promise(resolve => setTimeout(resolve, 500));
    }
    
    // 2. 打开浮窗（悬浮模式）
    console.log('\n2️⃣ 打开浮窗（悬浮模式）...');
    assistant.openFloatingWindow();
    await new Promise(resolve => setTimeout(resolve, 1000));
    
    const floatingState = checkFloatingWindowState('悬浮模式');
    
    // 3. 第一次切换到停靠模式
    console.log('\n3️⃣ 第一次切换到停靠模式...');
    assistant.setDockMode();
    
    // 立即检查（可能还在动画中）
    await new Promise(resolve => setTimeout(resolve, 100));
    const dockedImmediateState = checkFloatingWindowState('停靠模式-立即检查');
    
    // 动画完成后检查
    await new Promise(resolve => setTimeout(resolve, 500));
    const dockedFinalState = checkFloatingWindowState('停靠模式-最终状态');
    
    // 4. 切换回悬浮模式
    console.log('\n4️⃣ 切换回悬浮模式...');
    assistant.setFloatingMode();
    await new Promise(resolve => setTimeout(resolve, 500));
    
    const floatingReturnState = checkFloatingWindowState('返回悬浮模式');
    
    // 5. 再次测试停靠模式
    console.log('\n5️⃣ 再次测试停靠模式...');
    assistant.setDockMode();
    await new Promise(resolve => setTimeout(resolve, 500));
    
    const dockedSecondState = checkFloatingWindowState('第二次停靠模式');
    
    // 6. 最终测试：多次快速切换
    console.log('\n6️⃣ 快速切换测试...');
    for (let i = 0; i < 3; i++) {
      assistant.setFloatingMode();
      await new Promise(resolve => setTimeout(resolve, 200));
      assistant.setDockMode();
      await new Promise(resolve => setTimeout(resolve, 200));
    }
    
    const finalState = checkFloatingWindowState('快速切换后');
    
    // 7. 检查页面内容是否收窄
    console.log('\n7️⃣ 检查页面布局...');
    const body = document.body;
    const bodyStyle = window.getComputedStyle(body);
    const bodyMarginRight = parseFloat(bodyStyle.marginRight);
    const expectedMargin = window.innerWidth * 0.4;
    
    console.log('页面布局检查:', {
      bodyMarginRight: `${bodyMarginRight}px`,
      expectedMargin: `${expectedMargin}px`,
      isCorrect: Math.abs(bodyMarginRight - expectedMargin) < 50 ? '✅' : '❌',
      hasDockedClass: body.classList.contains('senparc-docked') ? '✅' : '❌'
    });
    
    // 8. 总结报告
    console.log('\n📋 测试报告:');
    console.log('- 悬浮模式显示:', floatingState?.isCorrect ? '✅' : '❌');
    console.log('- 首次停靠显示:', dockedFinalState?.isCorrect ? '✅' : '❌');
    console.log('- 返回悬浮显示:', floatingReturnState?.isCorrect ? '✅' : '❌');
    console.log('- 二次停靠显示:', dockedSecondState?.isCorrect ? '✅' : '❌');
    console.log('- 快速切换稳定性:', finalState?.isCorrect ? '✅' : '❌');
    console.log('- 页面布局适配:', Math.abs(bodyMarginRight - expectedMargin) < 50 ? '✅' : '❌');
    
    const allPassed = [
      floatingState?.isCorrect,
      dockedFinalState?.isCorrect,
      floatingReturnState?.isCorrect,
      dockedSecondState?.isCorrect,
      finalState?.isCorrect,
      Math.abs(bodyMarginRight - expectedMargin) < 50
    ].every(Boolean);
    
    console.log(`\n🎉 总体测试结果: ${allPassed ? '✅ 全部通过' : '❌ 部分失败'}`);
    
    if (!allPassed) {
      console.log('\n🔍 问题详情:');
      if (!dockedFinalState?.isCorrect) {
        console.log('- 首次停靠位置不正确，可能需要进一步调整样式优先级');
      }
      if (Math.abs(bodyMarginRight - expectedMargin) >= 50) {
        console.log('- 页面布局收窄不正确，检查CSS规则');
      }
    }
    
  } catch (error) {
    console.error('❌ 测试过程中出现错误:', error);
  }
}

// 自动运行测试
testDockFix();

// 导出测试函数供手动调用
window.testDockFix = testDockFix;
window.checkFloatingWindowState = checkFloatingWindowState;

console.log('\n💡 提示:');
console.log('- 运行 testDockFix() 重新执行完整测试');
console.log('- 运行 checkFloatingWindowState("当前状态") 检查当前窗口状态');
