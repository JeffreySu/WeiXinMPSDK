// 关闭按钮终极测试和调试脚本
// 在微信开发者文档页面的控制台中运行此脚本

console.log('🔧 ===== 关闭按钮终极测试开始 =====');

// 全局变量
let testResults = {
  pluginLoaded: false,
  windowCreated: false,
  buttonFound: false,
  eventBound: false,
  floatingModeClose: false,
  dockedModeClose: false,
  layoutRestored: false
};

// 等待插件加载
async function waitForPlugin(maxWait = 10000) {
  const startTime = Date.now();
  
  return new Promise((resolve, reject) => {
    const check = () => {
      if (window.globalAssistantInstance) {
        console.log('✅ 插件已加载');
        testResults.pluginLoaded = true;
        resolve(window.globalAssistantInstance);
      } else if (Date.now() - startTime > maxWait) {
        console.log('❌ 插件加载超时');
        reject(new Error('插件加载超时'));
      } else {
        console.log('⏳ 等待插件加载...');
        setTimeout(check, 500);
      }
    };
    check();
  });
}

// 详细检查关闭按钮状态
function inspectCloseButton() {
  console.log('\n🔍 ===== 详细检查关闭按钮 =====');
  
  const floatingWindow = document.getElementById('senparc-weixin-ai-window');
  if (!floatingWindow) {
    console.log('❌ 浮窗不存在');
    return null;
  }
  
  const closeButton = floatingWindow.querySelector('#close-floating-window');
  if (!closeButton) {
    console.log('❌ 关闭按钮不存在');
    console.log('🔍 浮窗HTML结构:');
    console.log(floatingWindow.innerHTML.substring(0, 1000));
    return null;
  }
  
  const computedStyle = window.getComputedStyle(closeButton);
  const rect = closeButton.getBoundingClientRect();
  
  const buttonInfo = {
    exists: true,
    id: closeButton.id,
    className: closeButton.className,
    tagName: closeButton.tagName,
    
    // 位置和尺寸
    rect: {
      x: rect.x,
      y: rect.y,
      width: rect.width,
      height: rect.height,
      top: rect.top,
      left: rect.left,
      right: rect.right,
      bottom: rect.bottom
    },
    
    // 样式属性
    styles: {
      display: computedStyle.display,
      visibility: computedStyle.visibility,
      opacity: computedStyle.opacity,
      cursor: computedStyle.cursor,
      pointerEvents: computedStyle.pointerEvents,
      zIndex: computedStyle.zIndex,
      position: computedStyle.position,
      backgroundColor: computedStyle.backgroundColor,
      border: computedStyle.border
    },
    
    // 事件绑定
    events: {
      onclick: typeof closeButton.onclick,
      hasOnclick: closeButton.onclick !== null,
      onmousedown: typeof closeButton.onmousedown,
      hasEventListeners: closeButton.getAttribute('data-event-bound') === 'true'
    },
    
    // 可见性检查
    isVisible: rect.width > 0 && rect.height > 0 && 
               computedStyle.display !== 'none' && 
               computedStyle.visibility !== 'hidden' && 
               parseFloat(computedStyle.opacity) > 0.1,
    
    // 可点击性检查
    isClickable: computedStyle.pointerEvents !== 'none' && 
                 computedStyle.cursor === 'pointer'
  };
  
  console.log('📊 关闭按钮详细信息:');
  console.log('  🎯 基本信息:', {
    ID: buttonInfo.id,
    标签: buttonInfo.tagName,
    类名: buttonInfo.className
  });
  
  console.log('  📐 位置尺寸:', buttonInfo.rect);
  console.log('  🎨 样式属性:', buttonInfo.styles);
  console.log('  🔗 事件绑定:', buttonInfo.events);
  
  console.log('  ✅ 状态检查:');
  console.log('    - 可见:', buttonInfo.isVisible ? '✅' : '❌');
  console.log('    - 可点击:', buttonInfo.isClickable ? '✅' : '❌');
  console.log('    - 有onclick:', buttonInfo.events.hasOnclick ? '✅' : '❌');
  
  testResults.buttonFound = buttonInfo.exists;
  testResults.eventBound = buttonInfo.events.hasOnclick;
  
  return buttonInfo;
}

// 检查浮窗状态
function inspectFloatingWindow() {
  console.log('\n🔍 ===== 检查浮窗状态 =====');
  
  const floatingWindow = document.getElementById('senparc-weixin-ai-window');
  if (!floatingWindow) {
    console.log('❌ 浮窗不存在');
    return { exists: false };
  }
  
  const computedStyle = window.getComputedStyle(floatingWindow);
  const rect = floatingWindow.getBoundingClientRect();
  
  const windowInfo = {
    exists: true,
    classList: Array.from(floatingWindow.classList),
    
    styles: {
      display: computedStyle.display,
      visibility: computedStyle.visibility,
      opacity: parseFloat(computedStyle.opacity),
      zIndex: computedStyle.zIndex,
      position: computedStyle.position,
      transform: computedStyle.transform
    },
    
    rect: {
      width: rect.width,
      height: rect.height,
      x: rect.x,
      y: rect.y
    },
    
    isVisible: rect.width > 0 && rect.height > 0 && 
               computedStyle.display !== 'none' && 
               computedStyle.visibility !== 'hidden' && 
               parseFloat(computedStyle.opacity) > 0.1,
    
    mode: floatingWindow.classList.contains('docked-mode') ? 'docked' : 
          floatingWindow.classList.contains('floating-mode') ? 'floating' : 'unknown'
  };
  
  console.log('📊 浮窗详细信息:');
  console.log('  🏷️ CSS类:', windowInfo.classList);
  console.log('  🎨 样式:', windowInfo.styles);
  console.log('  📐 尺寸位置:', windowInfo.rect);
  console.log('  🔄 模式:', windowInfo.mode);
  console.log('  👁️ 可见:', windowInfo.isVisible ? '✅' : '❌');
  
  testResults.windowCreated = windowInfo.exists;
  
  return windowInfo;
}

// 模拟点击关闭按钮
function simulateCloseButtonClick() {
  console.log('\n🖱️ ===== 模拟点击关闭按钮 =====');
  
  const closeButton = document.querySelector('#close-floating-window');
  if (!closeButton) {
    console.log('❌ 关闭按钮不存在，无法点击');
    return false;
  }
  
  console.log('🎯 找到关闭按钮，开始模拟点击...');
  
  // 方法1: 直接调用onclick
  if (closeButton.onclick) {
    console.log('📞 方法1: 直接调用onclick');
    try {
      closeButton.onclick({ 
        preventDefault: () => console.log('preventDefault called'),
        stopPropagation: () => console.log('stopPropagation called')
      });
      console.log('✅ onclick调用成功');
    } catch (error) {
      console.error('❌ onclick调用失败:', error);
    }
  }
  
  // 方法2: 创建并触发click事件
  console.log('📞 方法2: 触发click事件');
  try {
    const clickEvent = new MouseEvent('click', {
      bubbles: true,
      cancelable: true,
      view: window,
      detail: 1,
      screenX: 0,
      screenY: 0,
      clientX: 0,
      clientY: 0,
      ctrlKey: false,
      altKey: false,
      shiftKey: false,
      metaKey: false,
      button: 0,
      buttons: 1
    });
    
    const result = closeButton.dispatchEvent(clickEvent);
    console.log('✅ click事件触发结果:', result);
  } catch (error) {
    console.error('❌ click事件触发失败:', error);
  }
  
  // 方法3: 模拟鼠标点击
  console.log('📞 方法3: 模拟鼠标点击');
  try {
    closeButton.click();
    console.log('✅ 鼠标点击模拟成功');
  } catch (error) {
    console.error('❌ 鼠标点击模拟失败:', error);
  }
  
  return true;
}

// 检查页面布局恢复
function checkLayoutRestoration() {
  console.log('\n🏗️ ===== 检查页面布局恢复 =====');
  
  const body = document.body;
  const bodyStyle = window.getComputedStyle(body);
  
  const layoutInfo = {
    hasDockedClass: body.classList.contains('senparc-docked'),
    marginRight: parseFloat(bodyStyle.marginRight),
    transition: bodyStyle.transition,
    boxSizing: bodyStyle.boxSizing,
    overflowX: bodyStyle.overflowX
  };
  
  console.log('📊 页面布局状态:');
  console.log('  🏷️ 停靠类:', layoutInfo.hasDockedClass ? '❌ 未清除' : '✅ 已清除');
  console.log('  📏 右边距:', layoutInfo.marginRight + 'px');
  console.log('  🔄 过渡:', layoutInfo.transition);
  console.log('  📦 盒模型:', layoutInfo.boxSizing);
  console.log('  📜 横向滚动:', layoutInfo.overflowX);
  
  const isRestored = !layoutInfo.hasDockedClass && layoutInfo.marginRight < 50;
  console.log('  ✅ 布局恢复:', isRestored ? '✅ 成功' : '❌ 失败');
  
  testResults.layoutRestored = isRestored;
  
  return layoutInfo;
}

// 完整的关闭测试流程
async function runCompleteCloseTest() {
  console.log('\n🎯 ===== 开始完整关闭测试流程 =====');
  
  try {
    // 1. 等待插件加载
    console.log('\n1️⃣ 等待插件加载...');
    const assistant = await waitForPlugin();
    
    // 2. 确保浮窗关闭
    console.log('\n2️⃣ 确保浮窗初始状态为关闭...');
    if (assistant.isWindowOpen) {
      assistant.closeFloatingWindow();
      await new Promise(resolve => setTimeout(resolve, 500));
    }
    
    // 3. 打开浮窗（悬浮模式）
    console.log('\n3️⃣ 打开浮窗（悬浮模式）...');
    assistant.openFloatingWindow();
    await new Promise(resolve => setTimeout(resolve, 1500));
    
    // 4. 检查浮窗和按钮状态
    console.log('\n4️⃣ 检查浮窗和按钮状态...');
    const windowInfo = inspectFloatingWindow();
    const buttonInfo = inspectCloseButton();
    
    if (!windowInfo.exists || !windowInfo.isVisible) {
      console.log('❌ 浮窗未正确显示，测试终止');
      return false;
    }
    
    if (!buttonInfo || !buttonInfo.exists || !buttonInfo.events.hasOnclick) {
      console.log('❌ 关闭按钮状态异常，测试终止');
      return false;
    }
    
    // 5. 测试悬浮模式关闭
    console.log('\n5️⃣ 测试悬浮模式关闭...');
    simulateCloseButtonClick();
    
    // 等待关闭完成
    await new Promise(resolve => setTimeout(resolve, 500));
    
    const floatingCloseResult = inspectFloatingWindow();
    testResults.floatingModeClose = !floatingCloseResult.isVisible;
    
    console.log('🎯 悬浮模式关闭结果:', testResults.floatingModeClose ? '✅ 成功' : '❌ 失败');
    
    // 6. 测试停靠模式关闭
    console.log('\n6️⃣ 测试停靠模式关闭...');
    
    // 重新打开浮窗
    assistant.openFloatingWindow();
    await new Promise(resolve => setTimeout(resolve, 1000));
    
    // 切换到停靠模式
    assistant.setDockMode();
    await new Promise(resolve => setTimeout(resolve, 500));
    
    // 检查停靠模式状态
    const dockedWindowInfo = inspectFloatingWindow();
    console.log('🔄 停靠模式状态:', dockedWindowInfo.mode);
    
    // 检查停靠模式下的关闭按钮
    const dockedButtonInfo = inspectCloseButton();
    
    if (dockedButtonInfo && dockedButtonInfo.events.hasOnclick) {
      // 点击关闭按钮
      simulateCloseButtonClick();
      
      // 等待关闭完成
      await new Promise(resolve => setTimeout(resolve, 500));
      
      const dockedCloseResult = inspectFloatingWindow();
      testResults.dockedModeClose = !dockedCloseResult.isVisible;
      
      console.log('🎯 停靠模式关闭结果:', testResults.dockedModeClose ? '✅ 成功' : '❌ 失败');
      
      // 检查页面布局恢复
      checkLayoutRestoration();
    } else {
      console.log('❌ 停靠模式下关闭按钮状态异常');
      testResults.dockedModeClose = false;
    }
    
    // 7. 生成测试报告
    console.log('\n📋 ===== 测试报告 =====');
    console.log('🔧 插件加载:', testResults.pluginLoaded ? '✅' : '❌');
    console.log('🏗️ 浮窗创建:', testResults.windowCreated ? '✅' : '❌');
    console.log('🔍 按钮发现:', testResults.buttonFound ? '✅' : '❌');
    console.log('🔗 事件绑定:', testResults.eventBound ? '✅' : '❌');
    console.log('🌊 悬浮模式关闭:', testResults.floatingModeClose ? '✅' : '❌');
    console.log('🔗 停靠模式关闭:', testResults.dockedModeClose ? '✅' : '❌');
    console.log('🏗️ 页面布局恢复:', testResults.layoutRestored ? '✅' : '❌');
    
    const allPassed = Object.values(testResults).every(result => result === true);
    console.log(`\n🎉 总体结果: ${allPassed ? '✅ 全部通过' : '❌ 存在问题'}`);
    
    if (!allPassed) {
      console.log('\n💡 故障排除建议:');
      if (!testResults.eventBound) {
        console.log('  - 检查事件绑定逻辑');
        console.log('  - 确认DOM元素正确创建');
      }
      if (!testResults.floatingModeClose || !testResults.dockedModeClose) {
        console.log('  - 检查关闭方法实现');
        console.log('  - 查看控制台错误信息');
      }
      if (!testResults.layoutRestored) {
        console.log('  - 检查页面布局恢复逻辑');
      }
    }
    
    return allPassed;
    
  } catch (error) {
    console.error('❌ 测试过程中出现错误:', error);
    return false;
  }
}

// 快速诊断函数
function quickDiagnosis() {
  console.log('\n🚀 ===== 快速诊断 =====');
  
  const assistant = window.globalAssistantInstance;
  if (!assistant) {
    console.log('❌ 插件未加载');
    return;
  }
  
  console.log('✅ 插件已加载');
  console.log('📊 插件状态:', {
    isWindowOpen: assistant.isWindowOpen,
    isDocked: assistant.isDocked
  });
  
  if (assistant.isWindowOpen) {
    inspectFloatingWindow();
    inspectCloseButton();
  } else {
    console.log('ℹ️ 浮窗当前未打开');
  }
}

// 强制修复函数
function forceFixCloseButton() {
  console.log('\n🔧 ===== 强制修复关闭按钮 =====');
  
  const assistant = window.globalAssistantInstance;
  if (!assistant || !assistant.floatingWindow) {
    console.log('❌ 插件或浮窗不存在');
    return;
  }
  
  const closeButton = assistant.floatingWindow.querySelector('#close-floating-window');
  if (!closeButton) {
    console.log('❌ 关闭按钮不存在');
    return;
  }
  
  console.log('🔧 强制重新绑定关闭按钮事件...');
  
  // 强制绑定关闭事件
  closeButton.onclick = function(e) {
    console.log('🖱️ 强制修复的关闭按钮被点击！');
    if (e) {
      e.preventDefault();
      e.stopPropagation();
    }
    
    try {
      assistant.closeFloatingWindow();
      console.log('✅ 强制关闭执行完成');
    } catch (error) {
      console.error('❌ 强制关闭失败:', error);
    }
    
    return false;
  };
  
  // 强制设置样式
  closeButton.style.cssText = `
    pointer-events: auto !important;
    cursor: pointer !important;
    z-index: 99999 !important;
    position: relative !important;
    background: rgba(255, 0, 0, 0.8) !important;
    border: 2px solid red !important;
    border-radius: 8px !important;
    width: 32px !important;
    height: 32px !important;
    display: flex !important;
    align-items: center !important;
    justify-content: center !important;
    color: white !important;
  `;
  
  console.log('✅ 强制修复完成，关闭按钮现在应该是红色的');
}

// 运行主测试
runCompleteCloseTest();

// 导出函数
window.runCompleteCloseTest = runCompleteCloseTest;
window.quickDiagnosis = quickDiagnosis;
window.inspectCloseButton = inspectCloseButton;
window.inspectFloatingWindow = inspectFloatingWindow;
window.simulateCloseButtonClick = simulateCloseButtonClick;
window.forceFixCloseButton = forceFixCloseButton;
window.checkLayoutRestoration = checkLayoutRestoration;

console.log('\n💡 可用的调试函数:');
console.log('- runCompleteCloseTest() - 运行完整测试');
console.log('- quickDiagnosis() - 快速诊断');
console.log('- inspectCloseButton() - 检查关闭按钮');
console.log('- inspectFloatingWindow() - 检查浮窗状态');
console.log('- simulateCloseButtonClick() - 模拟点击关闭按钮');
console.log('- forceFixCloseButton() - 强制修复关闭按钮');
console.log('- checkLayoutRestoration() - 检查页面布局恢复');
