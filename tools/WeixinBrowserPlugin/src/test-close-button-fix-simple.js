// 简化的关闭按钮修复测试脚本
// 在微信开发者文档页面的控制台中运行此脚本

console.log('🔧 开始测试关闭按钮修复效果');

// 等待插件加载的简化版本
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

// 检查关闭按钮是否存在并可点击
function checkCloseButton() {
  const closeButton = document.querySelector('#close-floating-window');
  if (closeButton) {
    console.log('✅ 找到关闭按钮');
    console.log('- 按钮ID:', closeButton.id);
    console.log('- 按钮可见:', closeButton.offsetWidth > 0 && closeButton.offsetHeight > 0);
    console.log('- 按钮样式:', window.getComputedStyle(closeButton).display);
    return closeButton;
  } else {
    console.log('❌ 未找到关闭按钮');
    return null;
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
                   style.opacity > 0.5 && style.display !== 'none';
  
  return {
    exists: true,
    visible: isVisible,
    display: style.display,
    opacity: parseFloat(style.opacity),
    classList: Array.from(floatingWindow.classList)
  };
}

// 主测试函数
async function testCloseButtonFix() {
  try {
    console.log('\n🎯 开始关闭按钮修复测试...');
    
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
    await new Promise(resolve => setTimeout(resolve, 1500)); // 等待更长时间确保完全加载
    
    // 4. 检查浮窗状态
    const windowState = checkWindowState();
    console.log('🔍 浮窗状态:', windowState);
    
    if (!windowState.exists) {
      console.log('❌ 浮窗未创建，测试失败');
      return;
    }
    
    if (!windowState.visible) {
      console.log('❌ 浮窗不可见，测试失败');
      return;
    }
    
    // 5. 检查关闭按钮
    console.log('\n🔍 检查关闭按钮...');
    const closeButton = checkCloseButton();
    
    if (!closeButton) {
      console.log('❌ 关闭按钮不存在，测试失败');
      return;
    }
    
    // 6. 测试点击关闭按钮
    console.log('\n🖱️ 点击关闭按钮...');
    closeButton.click();
    
    // 7. 检查关闭效果
    console.log('⏳ 等待关闭动画完成...');
    await new Promise(resolve => setTimeout(resolve, 500));
    
    const finalState = checkWindowState();
    console.log('🔍 关闭后状态:', finalState);
    
    // 8. 评估测试结果
    const success = !finalState.visible;
    console.log(`\n🎉 关闭按钮测试结果: ${success ? '✅ 成功' : '❌ 失败'}`);
    
    if (success) {
      console.log('✅ 关闭按钮修复成功！浮窗可以正常关闭');
    } else {
      console.log('❌ 关闭按钮仍有问题，需要进一步调试');
      console.log('💡 建议检查:');
      console.log('  - 控制台是否有JavaScript错误');
      console.log('  - 事件绑定是否成功');
      console.log('  - CSS样式是否冲突');
    }
    
    // 9. 额外测试：停靠模式
    if (success) {
      console.log('\n🔄 测试停靠模式下的关闭功能...');
      
      // 重新打开浮窗
      assistant.openFloatingWindow();
      await new Promise(resolve => setTimeout(resolve, 1000));
      
      // 切换到停靠模式
      assistant.setDockMode();
      await new Promise(resolve => setTimeout(resolve, 500));
      
      // 检查停靠模式下的关闭按钮
      const dockedCloseButton = checkCloseButton();
      if (dockedCloseButton) {
        console.log('🖱️ 点击停靠模式下的关闭按钮...');
        dockedCloseButton.click();
        
        await new Promise(resolve => setTimeout(resolve, 500));
        const dockedFinalState = checkWindowState();
        
        const dockedSuccess = !dockedFinalState.visible;
        console.log(`🎯 停靠模式关闭测试: ${dockedSuccess ? '✅ 成功' : '❌ 失败'}`);
        
        // 检查页面布局是否恢复
        const bodyStyle = window.getComputedStyle(document.body);
        const marginRight = parseFloat(bodyStyle.marginRight);
        const hasDockedClass = document.body.classList.contains('senparc-docked');
        
        console.log('📐 页面布局恢复检查:', {
          marginRight: `${marginRight}px`,
          hasDockedClass: hasDockedClass ? '❌ 未清除' : '✅ 已清除'
        });
      }
    }
    
  } catch (error) {
    console.error('❌ 测试过程中出现错误:', error);
  }
}

// 运行测试
testCloseButtonFix();

// 导出测试函数供手动调用
window.testCloseButtonFix = testCloseButtonFix;

console.log('\n💡 提示:');
console.log('- 如果测试失败，请检查控制台的错误信息');
console.log('- 可以手动运行 testCloseButtonFix() 重新测试');
console.log('- 确保在微信开发者文档页面运行此脚本');
