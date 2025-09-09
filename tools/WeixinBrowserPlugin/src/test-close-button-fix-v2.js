// 关闭按钮修复验证脚本 v2.0
// 在微信开发者文档页面的控制台中运行此脚本

console.log('🔧 开始验证关闭按钮修复效果 v2.0');

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

// 检查关闭按钮状态
function checkCloseButton() {
  const closeButton = document.querySelector('#close-floating-window');
  if (closeButton) {
    const style = window.getComputedStyle(closeButton);
    console.log('✅ 关闭按钮状态检查:');
    console.log('  - 元素存在:', true);
    console.log('  - 可见性:', style.display !== 'none' && style.visibility !== 'hidden');
    console.log('  - 鼠标样式:', style.cursor);
    console.log('  - z-index:', style.zIndex);
    console.log('  - pointer-events:', style.pointerEvents);
    console.log('  - 位置:', style.position);
    
    // 检查事件绑定
    console.log('  - onclick绑定:', typeof closeButton.onclick === 'function');
    
    return {
      exists: true,
      visible: style.display !== 'none' && style.visibility !== 'hidden',
      hasClickHandler: typeof closeButton.onclick === 'function',
      element: closeButton
    };
  } else {
    console.log('❌ 关闭按钮不存在');
    return { exists: false, visible: false, hasClickHandler: false };
  }
}

// 检查浮窗状态
function checkWindowState() {
  const floatingWindow = document.getElementById('senparc-weixin-ai-window');
  if (!floatingWindow) {
    return { exists: false, visible: false };
  }
  
  const rect = floatingWindow.getBoundingClientRect();
  const style = window.getComputedStyle(floatingWindow);
  const isVisible = rect.width > 0 && rect.height > 0 && 
                   parseFloat(style.opacity) > 0.5 && style.display !== 'none';
  
  return {
    exists: true,
    visible: isVisible,
    display: style.display,
    opacity: parseFloat(style.opacity),
    classList: Array.from(floatingWindow.classList)
  };
}

// 测试关闭按钮点击
function testCloseButtonClick() {
  const buttonInfo = checkCloseButton();
  
  if (!buttonInfo.exists) {
    console.log('❌ 关闭按钮不存在，无法测试');
    return false;
  }
  
  if (!buttonInfo.hasClickHandler) {
    console.log('❌ 关闭按钮没有点击事件处理器');
    return false;
  }
  
  console.log('🖱️ 模拟点击关闭按钮...');
  
  // 创建并触发点击事件
  const clickEvent = new MouseEvent('click', {
    bubbles: true,
    cancelable: true,
    view: window
  });
  
  buttonInfo.element.dispatchEvent(clickEvent);
  
  // 也尝试直接调用onclick
  if (buttonInfo.element.onclick) {
    buttonInfo.element.onclick(clickEvent);
  }
  
  return true;
}

// 主测试函数
async function runCloseButtonTest() {
  try {
    console.log('\n🎯 开始关闭按钮修复验证...');
    
    // 1. 等待插件加载
    const assistant = await waitForPlugin();
    
    // 2. 确保浮窗关闭
    if (assistant.isWindowOpen) {
      console.log('🔄 先关闭已打开的浮窗...');
      assistant.closeFloatingWindow();
      await new Promise(resolve => setTimeout(resolve, 500));
    }
    
    // 3. 打开浮窗
    console.log('\n📂 打开浮窗...');
    assistant.openFloatingWindow();
    await new Promise(resolve => setTimeout(resolve, 1000));
    
    // 4. 检查浮窗状态
    const windowState = checkWindowState();
    console.log('\n🔍 浮窗状态:', {
      exists: windowState.exists ? '✅' : '❌',
      visible: windowState.visible ? '✅' : '❌',
      display: windowState.display,
      opacity: windowState.opacity
    });
    
    if (!windowState.exists || !windowState.visible) {
      console.log('❌ 浮窗未正确显示，测试终止');
      return;
    }
    
    // 5. 检查关闭按钮
    console.log('\n🔍 检查关闭按钮...');
    const buttonInfo = checkCloseButton();
    
    if (!buttonInfo.exists || !buttonInfo.visible || !buttonInfo.hasClickHandler) {
      console.log('❌ 关闭按钮状态异常，测试失败');
      return;
    }
    
    // 6. 测试点击关闭
    console.log('\n🖱️ 测试关闭按钮点击...');
    const clickSuccess = testCloseButtonClick();
    
    if (!clickSuccess) {
      console.log('❌ 关闭按钮点击测试失败');
      return;
    }
    
    // 7. 检查关闭效果
    console.log('⏳ 等待关闭动画完成...');
    await new Promise(resolve => setTimeout(resolve, 500));
    
    const finalState = checkWindowState();
    console.log('\n🔍 关闭后状态:', {
      exists: finalState.exists ? '✅' : '❌',
      visible: finalState.visible ? '❌' : '✅',
      display: finalState.display,
      opacity: finalState.opacity
    });
    
    // 8. 评估测试结果
    const success = !finalState.visible;
    console.log(`\n🎉 关闭按钮修复验证结果: ${success ? '✅ 成功' : '❌ 失败'}`);
    
    if (success) {
      console.log('🎊 恭喜！关闭按钮修复成功，浮窗可以正常关闭了！');
      
      // 额外测试：停靠模式
      console.log('\n🔄 测试停靠模式下的关闭功能...');
      assistant.openFloatingWindow();
      await new Promise(resolve => setTimeout(resolve, 1000));
      
      assistant.setDockMode();
      await new Promise(resolve => setTimeout(resolve, 500));
      
      const dockedButtonInfo = checkCloseButton();
      if (dockedButtonInfo.exists && dockedButtonInfo.hasClickHandler) {
        testCloseButtonClick();
        await new Promise(resolve => setTimeout(resolve, 500));
        
        const dockedFinalState = checkWindowState();
        const dockedSuccess = !dockedFinalState.visible;
        console.log(`🎯 停靠模式关闭测试: ${dockedSuccess ? '✅ 成功' : '❌ 失败'}`);
        
        // 检查页面布局恢复
        const bodyStyle = window.getComputedStyle(document.body);
        const marginRight = parseFloat(bodyStyle.marginRight);
        const hasDockedClass = document.body.classList.contains('senparc-docked');
        
        console.log('📐 页面布局恢复:', {
          marginRight: `${marginRight}px`,
          dockedClassRemoved: !hasDockedClass ? '✅' : '❌'
        });
      }
    } else {
      console.log('❌ 关闭按钮仍有问题，需要进一步调试');
      console.log('💡 建议检查:');
      console.log('  - 浏览器控制台是否有JavaScript错误');
      console.log('  - 事件绑定是否被其他代码覆盖');
      console.log('  - CSS样式是否存在冲突');
    }
    
  } catch (error) {
    console.error('❌ 测试过程中出现错误:', error);
  }
}

// 快速诊断函数
function quickDiagnosis() {
  console.log('\n🔍 快速诊断关闭按钮问题...');
  
  const assistant = window.globalAssistantInstance;
  if (!assistant) {
    console.log('❌ 插件未加载');
    return;
  }
  
  const floatingWindow = document.getElementById('senparc-weixin-ai-window');
  if (!floatingWindow) {
    console.log('❌ 浮窗不存在');
    return;
  }
  
  const closeButton = document.querySelector('#close-floating-window');
  if (!closeButton) {
    console.log('❌ 关闭按钮不存在');
    return;
  }
  
  console.log('✅ 基本元素都存在');
  console.log('📊 详细信息:');
  console.log('  - 插件状态:', assistant.isWindowOpen ? '窗口已打开' : '窗口已关闭');
  console.log('  - 停靠状态:', assistant.isDocked ? '已停靠' : '悬浮模式');
  console.log('  - 按钮onclick:', typeof closeButton.onclick);
  console.log('  - 按钮样式:', window.getComputedStyle(closeButton).cursor);
  
  // 尝试手动触发关闭
  console.log('\n🔧 尝试手动触发关闭...');
  if (typeof closeButton.onclick === 'function') {
    closeButton.onclick({ preventDefault: () => {}, stopPropagation: () => {} });
    console.log('✅ 手动触发完成');
  } else {
    console.log('❌ 没有onclick处理器');
  }
}

// 运行测试
runCloseButtonTest();

// 导出函数供手动调用
window.runCloseButtonTest = runCloseButtonTest;
window.quickDiagnosis = quickDiagnosis;
window.checkCloseButton = checkCloseButton;
window.checkWindowState = checkWindowState;

console.log('\n💡 可用的测试函数:');
console.log('- runCloseButtonTest() - 完整的关闭按钮测试');
console.log('- quickDiagnosis() - 快速诊断问题');
console.log('- checkCloseButton() - 检查关闭按钮状态');
console.log('- checkWindowState() - 检查浮窗状态');
