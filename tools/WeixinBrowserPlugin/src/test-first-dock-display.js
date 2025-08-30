// 测试第一次停靠显示问题的专用脚本
// 在微信开发者文档页面的控制台中运行

console.log('🧪 测试第一次停靠显示问题');

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

// 检查浮窗显示状态
function checkFloatingWindowDisplay(stepName) {
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
    zIndex: style.zIndex,
    classList: classList,
    isVisible: rect.width > 0 && rect.height > 0 && style.opacity > 0.5 && style.display !== 'none',
    rect: {
      top: rect.top,
      right: rect.right,
      bottom: rect.bottom,
      left: rect.left,
      width: rect.width,
      height: rect.height
    }
  };
  
  console.log(`📊 ${stepName} - 浮窗状态:`, info);
  return info;
}

// 测试第一次停靠流程
async function testFirstDockDisplay() {
  console.log('\n🎯 开始测试第一次停靠显示问题...');
  
  try {
    const assistant = await waitForPlugin();
    
    // 0. 确保初始状态干净
    console.log('\n0️⃣ 清理初始状态...');
    if (assistant.isWindowOpen) {
      assistant.closeFloatingWindow();
      await new Promise(resolve => setTimeout(resolve, 500));
    }
    
    // 1. 打开浮窗
    console.log('\n1️⃣ 打开浮窗...');
    assistant.openFloatingWindow();
    await new Promise(resolve => setTimeout(resolve, 1000));
    
    const step1Info = checkFloatingWindowDisplay('步骤1 - 打开浮窗后');
    
    // 2. 第一次点击停靠按钮
    console.log('\n2️⃣ 第一次点击停靠按钮...');
    
    // 记录停靠前的状态
    const beforeDockInfo = checkFloatingWindowDisplay('停靠前');
    
    // 执行停靠
    assistant.setDockMode();
    
    // 立即检查状态（无延迟）
    const immediateAfterDockInfo = checkFloatingWindowDisplay('停靠后立即检查');
    
    // 短延迟后检查
    await new Promise(resolve => setTimeout(resolve, 100));
    const shortDelayInfo = checkFloatingWindowDisplay('停靠后100ms');
    
    // 动画完成后检查
    await new Promise(resolve => setTimeout(resolve, 400));
    const animationCompleteInfo = checkFloatingWindowDisplay('停靠后500ms(动画完成)');
    
    // 3. 分析结果
    console.log('\n3️⃣ 分析停靠显示问题...');
    
    const visibilitySteps = [
      { name: '停靠前', info: beforeDockInfo },
      { name: '停靠后立即', info: immediateAfterDockInfo },
      { name: '停靠后100ms', info: shortDelayInfo },
      { name: '停靠后500ms', info: animationCompleteInfo }
    ];
    
    visibilitySteps.forEach(step => {
      if (step.info) {
        const visible = step.info.isVisible;
        const hasDockedClass = step.info.classList.includes('docked-mode');
        const hasShowClass = step.info.classList.includes('show');
        
        console.log(`${step.name}:`, {
          visible: visible ? '✅' : '❌',
          dockedClass: hasDockedClass ? '✅' : '❌',
          showClass: hasShowClass ? '✅' : '❌',
          position: `${step.info.position}`,
          display: `${step.info.display}`,
          opacity: `${step.info.opacity}`,
          transform: `${step.info.transform}`
        });
      }
    });
    
    // 4. 如果停靠后不可见，尝试手动修复
    if (animationCompleteInfo && !animationCompleteInfo.isVisible) {
      console.log('\n4️⃣ 检测到停靠后不可见，尝试手动修复...');
      await manualFixDockDisplay(assistant);
    }
    
    // 5. 测试切换回悬浮模式
    console.log('\n5️⃣ 测试切换回悬浮模式...');
    assistant.setFloatingMode();
    await new Promise(resolve => setTimeout(resolve, 500));
    
    const floatingRestoreInfo = checkFloatingWindowDisplay('恢复悬浮模式后');
    
    // 6. 再次测试停靠
    console.log('\n6️⃣ 再次测试停靠（验证是否重复出现问题）...');
    assistant.setDockMode();
    await new Promise(resolve => setTimeout(resolve, 500));
    
    const secondDockInfo = checkFloatingWindowDisplay('第二次停靠后');
    
    console.log('\n🎉 测试完成！');
    
    // 总结
    console.log('\n📋 测试总结:');
    console.log('- 第一次停靠可见:', animationCompleteInfo?.isVisible ? '✅' : '❌');
    console.log('- 悬浮模式恢复:', floatingRestoreInfo?.isVisible ? '✅' : '❌');
    console.log('- 第二次停靠可见:', secondDockInfo?.isVisible ? '✅' : '❌');
    
  } catch (error) {
    console.error('❌ 测试过程中出现错误:', error);
  }
}

// 手动修复停靠显示问题
async function manualFixDockDisplay(assistant) {
  console.log('🔧 应用手动修复...');
  
  const floatingWindow = document.getElementById('senparc-weixin-ai-window');
  if (!floatingWindow) return;
  
  // 强制应用停靠样式
  floatingWindow.style.display = 'flex';
  floatingWindow.style.opacity = '1';
  floatingWindow.style.visibility = 'visible';
  floatingWindow.style.position = 'fixed';
  floatingWindow.style.top = '0';
  floatingWindow.style.right = '0';
  floatingWindow.style.left = 'auto';
  floatingWindow.style.width = '40%';
  floatingWindow.style.height = '100vh';
  floatingWindow.style.transform = 'none';
  floatingWindow.style.zIndex = '10000';
  
  // 确保class正确
  floatingWindow.classList.remove('floating-mode');
  floatingWindow.classList.add('docked-mode');
  floatingWindow.classList.add('show');
  
  await new Promise(resolve => setTimeout(resolve, 200));
  
  const fixedInfo = checkFloatingWindowDisplay('手动修复后');
  console.log('手动修复结果:', fixedInfo?.isVisible ? '✅ 成功' : '❌ 失败');
}

// 模拟停靠按钮点击
function simulateDockButtonClick() {
  console.log('\n🖱️ 模拟停靠按钮点击...');
  
  const dockButton = document.querySelector('#dock-toggle-button');
  if (dockButton) {
    console.log('找到停靠按钮，模拟点击...');
    dockButton.click();
    return true;
  } else {
    console.log('❌ 未找到停靠按钮');
    return false;
  }
}

// 完整测试流程（包括模拟用户操作）
async function testUserFlow() {
  console.log('\n👤 测试用户操作流程...');
  
  const assistant = await waitForPlugin();
  
  // 1. 用户点击左上角按钮打开浮窗
  console.log('1. 用户点击左上角按钮...');
  assistant.openFloatingWindow();
  await new Promise(resolve => setTimeout(resolve, 1000));
  
  checkFloatingWindowDisplay('用户打开浮窗后');
  
  // 2. 用户点击停靠按钮
  console.log('2. 用户点击停靠按钮...');
  const clicked = simulateDockButtonClick();
  
  if (clicked) {
    await new Promise(resolve => setTimeout(resolve, 500));
    const afterUserDock = checkFloatingWindowDisplay('用户点击停靠后');
    
    if (!afterUserDock?.isVisible) {
      console.log('⚠️ 检测到用户点击停靠后浮窗不可见！');
      
      // 3. 用户再次点击左上角按钮（用户的解决方法）
      console.log('3. 用户再次点击左上角按钮...');
      assistant.openFloatingWindow();
      await new Promise(resolve => setTimeout(resolve, 500));
      
      checkFloatingWindowDisplay('用户再次点击左上角按钮后');
    }
  }
}

// 导出测试函数
window.testFirstDock = {
  full: testFirstDockDisplay,
  userFlow: testUserFlow,
  check: checkFloatingWindowDisplay,
  manualFix: manualFixDockDisplay,
  simulateClick: simulateDockButtonClick
};

console.log('\n🎮 可用的测试命令:');
console.log('  testFirstDock.full() - 完整测试流程');
console.log('  testFirstDock.userFlow() - 模拟用户操作');
console.log('  testFirstDock.check("步骤名") - 检查当前状态');
console.log('  testFirstDock.manualFix(assistant) - 手动修复');
console.log('  testFirstDock.simulateClick() - 模拟点击停靠按钮');

console.log('\n💡 建议运行: testFirstDock.full()');

