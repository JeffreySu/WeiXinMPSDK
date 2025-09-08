// 测试关闭按钮修复效果的脚本
// 在微信开发者文档页面的控制台中运行

console.log('🔧 测试关闭按钮修复效果');

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
    return { exists: false, isVisible: false };
  }
  
  const rect = floatingWindow.getBoundingClientRect();
  const style = window.getComputedStyle(floatingWindow);
  const classList = Array.from(floatingWindow.classList);
  
  const info = {
    exists: true,
    isVisible: rect.width > 0 && rect.height > 0 && style.opacity > 0.5 && style.display !== 'none',
    classList: classList,
    display: style.display,
    opacity: parseFloat(style.opacity),
    visibility: style.visibility,
    hasShowClass: classList.includes('show')
  };
  
  console.log(`📊 ${stepName}:`, {
    exists: info.exists ? '✅' : '❌',
    visible: info.isVisible ? '✅' : '❌',
    display: info.display,
    opacity: info.opacity,
    hasShow: info.hasShowClass ? '✅' : '❌',
    mode: info.classList.includes('docked-mode') ? 'docked' : 'floating'
  });
  
  return info;
}

// 检查按钮事件是否绑定
function checkButtonEvents() {
  const closeButton = document.querySelector('#close-floating-window');
  const dockButton = document.querySelector('#dock-toggle-button');
  
  console.log('🔍 检查按钮事件绑定状态:');
  console.log('- 关闭按钮存在:', closeButton ? '✅' : '❌');
  console.log('- 停靠按钮存在:', dockButton ? '✅' : '❌');
  
  if (closeButton) {
    // 检查是否有事件监听器（虽然无法直接检测，但可以检查元素状态）
    console.log('- 关闭按钮ID:', closeButton.id);
    console.log('- 关闭按钮类:', closeButton.className);
  }
  
  if (dockButton) {
    console.log('- 停靠按钮ID:', dockButton.id);
    console.log('- 停靠按钮类:', dockButton.className);
  }
  
  return { closeButton, dockButton };
}

// 模拟点击关闭按钮
function clickCloseButton() {
  const closeButton = document.querySelector('#close-floating-window');
  if (closeButton) {
    console.log('🖱️ 点击关闭按钮...');
    closeButton.click();
    return true;
  } else {
    console.log('❌ 找不到关闭按钮');
    return false;
  }
}

// 测试关闭功能
async function testCloseFunction() {
  console.log('\n🎯 开始测试关闭功能...');
  
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
    
    const floatingState = checkWindowState('悬浮模式');
    const floatingButtons = checkButtonEvents();
    
    // 3. 测试悬浮模式下的关闭按钮
    console.log('\n3️⃣ 测试悬浮模式下的关闭按钮...');
    const floatingCloseSuccess = clickCloseButton();
    
    if (floatingCloseSuccess) {
      // 检查关闭效果
      await new Promise(resolve => setTimeout(resolve, 100));
      const immediateState = checkWindowState('点击关闭后立即检查');
      
      await new Promise(resolve => setTimeout(resolve, 400));
      const finalState = checkWindowState('关闭动画完成后');
      
      console.log('悬浮模式关闭测试:', !finalState.isVisible ? '✅ 成功' : '❌ 失败');
    }
    
    // 4. 重新打开，测试停靠模式
    console.log('\n4️⃣ 重新打开，切换到停靠模式...');
    assistant.openFloatingWindow();
    await new Promise(resolve => setTimeout(resolve, 1000));
    
    assistant.setDockMode();
    await new Promise(resolve => setTimeout(resolve, 500));
    
    const dockedState = checkWindowState('停靠模式');
    const dockedButtons = checkButtonEvents();
    
    // 5. 测试停靠模式下的关闭按钮
    console.log('\n5️⃣ 测试停靠模式下的关闭按钮...');
    const dockedCloseSuccess = clickCloseButton();
    
    if (dockedCloseSuccess) {
      // 检查关闭效果
      await new Promise(resolve => setTimeout(resolve, 400));
      const dockedCloseState = checkWindowState('停靠模式关闭后');
      
      // 检查页面布局是否恢复
      const body = document.body;
      const bodyStyle = window.getComputedStyle(body);
      const bodyMarginRight = parseFloat(bodyStyle.marginRight);
      const hasDockedClass = body.classList.contains('senparc-docked');
      
      console.log('页面布局恢复检查:', {
        marginRight: `${bodyMarginRight}px`,
        hasDockedClass: hasDockedClass ? '❌ 未清除' : '✅ 已清除',
        layoutRestored: bodyMarginRight < 50 && !hasDockedClass ? '✅' : '❌'
      });
      
      console.log('停靠模式关闭测试:', !dockedCloseState.isVisible ? '✅ 成功' : '❌ 失败');
    }
    
    // 6. 多次开关测试
    console.log('\n6️⃣ 多次开关测试...');
    for (let i = 0; i < 3; i++) {
      console.log(`第${i+1}次循环: 打开 → 关闭`);
      
      // 打开
      assistant.openFloatingWindow();
      await new Promise(resolve => setTimeout(resolve, 500));
      
      const openState = checkWindowState(`第${i+1}次打开`);
      
      // 关闭
      const closeSuccess = clickCloseButton();
      if (closeSuccess) {
        await new Promise(resolve => setTimeout(resolve, 400));
        const closeState = checkWindowState(`第${i+1}次关闭`);
        
        console.log(`第${i+1}次循环结果:`, 
          openState.isVisible && !closeState.isVisible ? '✅' : '❌');
      }
    }
    
    // 7. 快速连续点击测试
    console.log('\n7️⃣ 快速连续点击测试...');
    assistant.openFloatingWindow();
    await new Promise(resolve => setTimeout(resolve, 1000));
    
    console.log('快速连续点击关闭按钮...');
    for (let i = 0; i < 5; i++) {
      clickCloseButton();
      await new Promise(resolve => setTimeout(resolve, 50));
    }
    
    await new Promise(resolve => setTimeout(resolve, 500));
    const rapidClickState = checkWindowState('快速连续点击后');
    console.log('快速连续点击测试:', !rapidClickState.isVisible ? '✅ 正常' : '❌ 异常');
    
    // 8. 测试报告
    console.log('\n📋 关闭功能测试报告:');
    console.log('- 悬浮模式关闭按钮:', floatingCloseSuccess ? '✅ 响应' : '❌ 无响应');
    console.log('- 停靠模式关闭按钮:', dockedCloseSuccess ? '✅ 响应' : '❌ 无响应');
    console.log('- 页面布局恢复:', '需手动检查上面的输出');
    console.log('- 多次开关稳定性:', '需查看上面的循环测试结果');
    console.log('- 快速点击处理:', !rapidClickState.isVisible ? '✅ 正常' : '❌ 异常');
    
    const allWorking = floatingCloseSuccess && dockedCloseSuccess;
    console.log(`\n🎉 关闭功能总体评估: ${allWorking ? '✅ 修复成功' : '❌ 仍有问题'}`);
    
    if (!allWorking) {
      console.log('\n🔧 故障排除建议:');
      if (!floatingCloseSuccess || !dockedCloseSuccess) {
        console.log('- 检查按钮元素是否正确创建');
        console.log('- 检查事件绑定是否成功');
        console.log('- 检查控制台是否有JavaScript错误');
      }
    }
    
  } catch (error) {
    console.error('❌ 测试过程中出现错误:', error);
  }
}

// 测试事件绑定
async function testEventBinding() {
  console.log('\n🔗 测试事件绑定专项...');
  
  try {
    const assistant = await waitForPlugin();
    
    // 打开窗口
    assistant.openFloatingWindow();
    await new Promise(resolve => setTimeout(resolve, 1000));
    
    // 检查事件绑定
    console.log('\n检查setupButtonEvents方法...');
    if (typeof assistant.setupButtonEvents === 'function') {
      console.log('✅ setupButtonEvents方法存在');
      
      // 手动调用事件绑定
      assistant.setupButtonEvents();
      
      // 检查按钮
      checkButtonEvents();
      
      // 测试点击
      console.log('\n手动重新绑定后测试点击...');
      setTimeout(() => {
        clickCloseButton();
      }, 100);
      
    } else {
      console.log('❌ setupButtonEvents方法不存在');
    }
    
  } catch (error) {
    console.error('❌ 事件绑定测试失败:', error);
  }
}

// 自动运行测试
testCloseFunction();

// 导出测试函数
window.testCloseFunction = testCloseFunction;
window.testEventBinding = testEventBinding;
window.checkWindowState = checkWindowState;
window.checkButtonEvents = checkButtonEvents;

console.log('\n💡 提示:');
console.log('- 运行 testCloseFunction() 执行完整关闭功能测试');
console.log('- 运行 testEventBinding() 测试事件绑定');
console.log('- 运行 checkButtonEvents() 检查按钮状态');
console.log('- 如果关闭按钮仍无效，请检查控制台错误信息');
