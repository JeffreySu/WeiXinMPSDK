// 测试还原按钮修复效果的脚本
// 在微信开发者文档页面的控制台中运行

console.log('🔧 测试还原按钮修复效果');

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

// 检查窗口状态
function checkWindowState(stepName) {
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
    isVisible: rect.width > 0 && rect.height > 0 && style.opacity > 0.5 && style.display !== 'none',
    classList: classList,
    display: style.display,
    opacity: parseFloat(style.opacity),
    visibility: style.visibility,
    position: `${Math.round(rect.left)},${Math.round(rect.top)} ${Math.round(rect.width)}x${Math.round(rect.height)}`,
    transform: style.transform
  };
  
  console.log(`📊 ${stepName}:`, {
    visible: info.isVisible ? '✅' : '❌',
    mode: info.classList.includes('docked-mode') ? 'docked' : 'floating',
    classes: info.classList,
    display: info.display,
    opacity: info.opacity,
    position: info.position
  });
  
  return info;
}

// 模拟点击还原按钮
function clickRestoreButton() {
  const dockButton = document.querySelector('#dock-toggle-button');
  if (dockButton) {
    console.log('🖱️ 点击还原按钮...');
    dockButton.click();
    return true;
  } else {
    console.log('❌ 找不到还原按钮');
    return false;
  }
}

// 测试还原功能
async function testRestoreFunction() {
  console.log('\n🎯 开始测试还原功能...');
  
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
    
    const initialState = checkWindowState('初始悬浮状态');
    
    // 3. 切换到停靠模式
    console.log('\n3️⃣ 切换到停靠模式...');
    assistant.setDockMode();
    await new Promise(resolve => setTimeout(resolve, 500));
    
    const dockedState = checkWindowState('停靠状态');
    
    // 4. 点击还原按钮（这是问题发生的地方）
    console.log('\n4️⃣ 点击还原按钮...');
    const buttonClicked = clickRestoreButton();
    
    if (!buttonClicked) {
      console.log('❌ 无法进行还原测试，找不到按钮');
      return;
    }
    
    // 立即检查状态
    await new Promise(resolve => setTimeout(resolve, 100));
    const immediateRestoreState = checkWindowState('点击还原后立即检查');
    
    // 动画完成后检查
    await new Promise(resolve => setTimeout(resolve, 500));
    const finalRestoreState = checkWindowState('还原后最终状态');
    
    // 5. 多次快速切换测试
    console.log('\n5️⃣ 快速切换测试...');
    for (let i = 0; i < 3; i++) {
      console.log(`第${i+1}次切换: 停靠 → 悬浮`);
      clickRestoreButton(); // 停靠 → 悬浮
      await new Promise(resolve => setTimeout(resolve, 200));
      
      console.log(`第${i+1}次切换: 悬浮 → 停靠`);
      clickRestoreButton(); // 悬浮 → 停靠
      await new Promise(resolve => setTimeout(resolve, 200));
    }
    
    const rapidSwitchState = checkWindowState('快速切换后');
    
    // 6. 最终还原测试
    console.log('\n6️⃣ 最终还原到悬浮模式...');
    if (document.getElementById('senparc-weixin-ai-window')?.classList.contains('docked-mode')) {
      clickRestoreButton();
      await new Promise(resolve => setTimeout(resolve, 500));
    }
    
    const finalFloatingState = checkWindowState('最终悬浮状态');
    
    // 7. 测试报告
    console.log('\n📋 测试报告:');
    console.log('- 初始悬浮模式:', initialState?.isVisible ? '✅' : '❌');
    console.log('- 停靠模式切换:', dockedState?.isVisible ? '✅' : '❌');
    console.log('- 还原按钮响应:', immediateRestoreState?.isVisible ? '✅' : '❌');
    console.log('- 还原后显示:', finalRestoreState?.isVisible ? '✅' : '❌');
    console.log('- 快速切换稳定性:', rapidSwitchState?.isVisible ? '✅' : '❌');
    console.log('- 最终状态正确:', finalFloatingState?.isVisible ? '✅' : '❌');
    
    const allPassed = [
      initialState?.isVisible,
      dockedState?.isVisible,
      immediateRestoreState?.isVisible,
      finalRestoreState?.isVisible,
      rapidSwitchState?.isVisible,
      finalFloatingState?.isVisible
    ].every(Boolean);
    
    console.log(`\n🎉 还原功能测试结果: ${allPassed ? '✅ 全部通过' : '❌ 部分失败'}`);
    
    if (!allPassed) {
      console.log('\n🔍 失败详情:');
      if (!finalRestoreState?.isVisible) {
        console.log('- 主要问题：点击还原按钮后窗口消失');
        console.log('- 建议：检查setFloatingMode方法中的样式设置');
      }
      if (!rapidSwitchState?.isVisible) {
        console.log('- 快速切换问题：连续操作可能导致状态混乱');
      }
    } else {
      console.log('\n🎊 还原功能已修复！点击还原按钮后窗口不再消失。');
    }
    
    // 8. 详细状态对比
    console.log('\n📊 详细状态对比:');
    const states = [
      { name: '初始悬浮', state: initialState },
      { name: '停靠模式', state: dockedState },
      { name: '还原后', state: finalRestoreState },
      { name: '最终状态', state: finalFloatingState }
    ];
    
    states.forEach(({ name, state }) => {
      if (state) {
        console.log(`${name}: ${state.isVisible ? '✅' : '❌'} | ${state.display} | opacity:${state.opacity} | ${state.classList.join(' ')}`);
      }
    });
    
  } catch (error) {
    console.error('❌ 测试过程中出现错误:', error);
  }
}

// 测试特定场景：从停靠模式直接还原
async function testDirectRestore() {
  console.log('\n🎯 测试直接还原场景...');
  
  try {
    const assistant = await waitForPlugin();
    
    // 确保处于停靠模式
    if (!assistant.isWindowOpen) {
      assistant.openFloatingWindow();
      await new Promise(resolve => setTimeout(resolve, 1000));
    }
    
    if (!assistant.isDocked) {
      assistant.setDockMode();
      await new Promise(resolve => setTimeout(resolve, 500));
    }
    
    console.log('当前状态：停靠模式');
    checkWindowState('停靠模式');
    
    // 直接点击还原
    console.log('直接点击还原按钮...');
    clickRestoreButton();
    
    // 检查还原效果
    await new Promise(resolve => setTimeout(resolve, 500));
    checkWindowState('直接还原后');
    
  } catch (error) {
    console.error('❌ 直接还原测试失败:', error);
  }
}

// 自动运行测试
testRestoreFunction();

// 导出测试函数
window.testRestoreFunction = testRestoreFunction;
window.testDirectRestore = testDirectRestore;
window.checkWindowState = checkWindowState;

console.log('\n💡 提示:');
console.log('- 运行 testRestoreFunction() 执行完整测试');
console.log('- 运行 testDirectRestore() 测试直接还原场景');
console.log('- 运行 checkWindowState("描述") 检查当前状态');
