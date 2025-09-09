// Logo按钮和重新打开功能测试脚本
// 在微信开发者文档页面的控制台中运行此脚本

console.log('🔧 ===== Logo按钮重新打开功能测试 =====');

// 等待插件加载
async function waitForPlugin(maxWait = 10000) {
  const startTime = Date.now();
  
  return new Promise((resolve, reject) => {
    const check = () => {
      if (window.globalAssistantInstance) {
        console.log('✅ 插件已加载');
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

// 检查Logo按钮状态
function inspectLogoButton() {
  console.log('\n🔍 ===== 检查Logo按钮状态 =====');
  
  const logoButton = document.getElementById('senparc-weixin-ai-button');
  if (!logoButton) {
    console.log('❌ Logo按钮不存在');
    return { exists: false };
  }
  
  const computedStyle = window.getComputedStyle(logoButton);
  const rect = logoButton.getBoundingClientRect();
  
  const buttonInfo = {
    exists: true,
    id: logoButton.id,
    className: logoButton.className,
    
    // 位置和尺寸
    rect: {
      x: rect.x,
      y: rect.y,
      width: rect.width,
      height: rect.height
    },
    
    // 样式属性
    styles: {
      display: computedStyle.display,
      visibility: computedStyle.visibility,
      opacity: computedStyle.opacity,
      cursor: computedStyle.cursor,
      pointerEvents: computedStyle.pointerEvents,
      zIndex: computedStyle.zIndex
    },
    
    // 事件绑定
    events: {
      onclick: typeof logoButton.onclick,
      hasOnclick: logoButton.onclick !== null
    },
    
    // 可见性和可点击性
    isVisible: rect.width > 0 && rect.height > 0 && 
               computedStyle.display !== 'none' && 
               computedStyle.visibility !== 'hidden' && 
               parseFloat(computedStyle.opacity) > 0.1,
    
    isClickable: computedStyle.pointerEvents !== 'none' && 
                 computedStyle.cursor === 'pointer'
  };
  
  console.log('📊 Logo按钮详细信息:');
  console.log('  🎯 基本信息:', {
    ID: buttonInfo.id,
    类名: buttonInfo.className
  });
  console.log('  📐 位置尺寸:', buttonInfo.rect);
  console.log('  🎨 样式属性:', buttonInfo.styles);
  console.log('  🔗 事件绑定:', buttonInfo.events);
  
  console.log('  ✅ 状态检查:');
  console.log('    - 可见:', buttonInfo.isVisible ? '✅' : '❌');
  console.log('    - 可点击:', buttonInfo.isClickable ? '✅' : '❌');
  console.log('    - 有onclick:', buttonInfo.events.hasOnclick ? '✅' : '❌');
  
  return buttonInfo;
}

// 检查浮窗状态
function checkFloatingWindowState() {
  const floatingWindow = document.getElementById('senparc-weixin-ai-window');
  if (!floatingWindow) {
    return { exists: false, visible: false };
  }
  
  const computedStyle = window.getComputedStyle(floatingWindow);
  const rect = floatingWindow.getBoundingClientRect();
  
  const isVisible = rect.width > 0 && rect.height > 0 && 
                   computedStyle.display !== 'none' && 
                   computedStyle.visibility !== 'hidden' && 
                   parseFloat(computedStyle.opacity) > 0.1;
  
  return {
    exists: true,
    visible: isVisible,
    display: computedStyle.display,
    opacity: parseFloat(computedStyle.opacity),
    visibility: computedStyle.visibility,
    classList: Array.from(floatingWindow.classList)
  };
}

// 模拟点击Logo按钮
function simulateLogoButtonClick() {
  console.log('\n🖱️ ===== 模拟点击Logo按钮 =====');
  
  const logoButton = document.getElementById('senparc-weixin-ai-button');
  if (!logoButton) {
    console.log('❌ Logo按钮不存在，无法点击');
    return false;
  }
  
  console.log('🎯 找到Logo按钮，开始模拟点击...');
  
  // 方法1: 直接调用onclick
  if (logoButton.onclick) {
    console.log('📞 方法1: 直接调用onclick');
    try {
      logoButton.onclick({ 
        preventDefault: () => console.log('preventDefault called'),
        stopPropagation: () => console.log('stopPropagation called')
      });
      console.log('✅ onclick调用成功');
    } catch (error) {
      console.error('❌ onclick调用失败:', error);
    }
  } else {
    console.log('⚠️ Logo按钮没有onclick处理器');
  }
  
  // 方法2: 创建并触发click事件
  console.log('📞 方法2: 触发click事件');
  try {
    const clickEvent = new MouseEvent('click', {
      bubbles: true,
      cancelable: true,
      view: window
    });
    
    const result = logoButton.dispatchEvent(clickEvent);
    console.log('✅ click事件触发结果:', result);
  } catch (error) {
    console.error('❌ click事件触发失败:', error);
  }
  
  // 方法3: 直接调用click方法
  console.log('📞 方法3: 调用click方法');
  try {
    logoButton.click();
    console.log('✅ click方法调用成功');
  } catch (error) {
    console.error('❌ click方法调用失败:', error);
  }
  
  return true;
}

// 测试开关循环
async function testOpenCloseLoop() {
  console.log('\n🔄 ===== 测试开关循环 =====');
  
  try {
    const assistant = await waitForPlugin();
    
    // 确保初始状态为关闭
    if (assistant.isWindowOpen) {
      console.log('🔄 先关闭已打开的浮窗...');
      assistant.closeFloatingWindow();
      await new Promise(resolve => setTimeout(resolve, 500));
    }
    
    const results = [];
    
    for (let i = 1; i <= 3; i++) {
      console.log(`\n🔄 第${i}次循环测试:`);
      
      // 检查初始状态
      const initialState = checkFloatingWindowState();
      console.log(`  📊 初始状态: 存在=${initialState.exists}, 可见=${initialState.visible}`);
      
      // 点击Logo按钮打开
      console.log(`  🖱️ 点击Logo按钮打开浮窗...`);
      simulateLogoButtonClick();
      
      // 等待打开完成
      await new Promise(resolve => setTimeout(resolve, 1000));
      
      // 检查打开后状态
      const openState = checkFloatingWindowState();
      console.log(`  📊 打开后状态: 存在=${openState.exists}, 可见=${openState.visible}`);
      
      const openSuccess = openState.exists && openState.visible;
      console.log(`  🎯 打开结果: ${openSuccess ? '✅ 成功' : '❌ 失败'}`);
      
      if (openSuccess) {
        // 点击关闭按钮
        console.log(`  🖱️ 点击关闭按钮...`);
        const closeButton = document.querySelector('#close-floating-window');
        if (closeButton && closeButton.onclick) {
          closeButton.onclick({ preventDefault: () => {}, stopPropagation: () => {} });
        }
        
        // 等待关闭完成
        await new Promise(resolve => setTimeout(resolve, 500));
        
        // 检查关闭后状态
        const closeState = checkFloatingWindowState();
        console.log(`  📊 关闭后状态: 存在=${closeState.exists}, 可见=${closeState.visible}`);
        
        const closeSuccess = !closeState.visible;
        console.log(`  🎯 关闭结果: ${closeSuccess ? '✅ 成功' : '❌ 失败'}`);
        
        results.push({
          cycle: i,
          openSuccess: openSuccess,
          closeSuccess: closeSuccess,
          overall: openSuccess && closeSuccess
        });
      } else {
        results.push({
          cycle: i,
          openSuccess: false,
          closeSuccess: false,
          overall: false
        });
      }
      
      // 短暂等待
      await new Promise(resolve => setTimeout(resolve, 200));
    }
    
    // 生成测试报告
    console.log('\n📋 ===== 循环测试报告 =====');
    results.forEach(result => {
      console.log(`第${result.cycle}次: 打开=${result.openSuccess ? '✅' : '❌'}, 关闭=${result.closeSuccess ? '✅' : '❌'}, 整体=${result.overall ? '✅' : '❌'}`);
    });
    
    const allSuccess = results.every(result => result.overall);
    console.log(`\n🎉 总体结果: ${allSuccess ? '✅ 全部成功' : '❌ 存在问题'}`);
    
    return results;
    
  } catch (error) {
    console.error('❌ 循环测试过程中出现错误:', error);
    return [];
  }
}

// 强制修复Logo按钮
function forceFixLogoButton() {
  console.log('\n🔧 ===== 强制修复Logo按钮 =====');
  
  const assistant = window.globalAssistantInstance;
  if (!assistant) {
    console.log('❌ 插件未加载');
    return;
  }
  
  const logoButton = document.getElementById('senparc-weixin-ai-button');
  if (!logoButton) {
    console.log('❌ Logo按钮不存在');
    return;
  }
  
  console.log('🔧 强制重新绑定Logo按钮事件...');
  
  // 强制绑定点击事件
  logoButton.onclick = function(e) {
    console.log('🖱️ 强制修复的Logo按钮被点击！');
    if (e) {
      e.preventDefault();
      e.stopPropagation();
    }
    
    try {
      assistant.toggleFloatingWindow();
      console.log('✅ 强制切换执行完成');
    } catch (error) {
      console.error('❌ 强制切换失败:', error);
    }
    
    return false;
  };
  
  // 强制设置样式（添加红色边框便于识别）
  logoButton.style.border = '2px solid red';
  logoButton.style.cursor = 'pointer';
  logoButton.style.pointerEvents = 'auto';
  
  console.log('✅ 强制修复完成，Logo按钮现在应该有红色边框');
}

// 快速诊断
function quickDiagnosis() {
  console.log('\n🚀 ===== 快速诊断Logo按钮问题 =====');
  
  const assistant = window.globalAssistantInstance;
  if (!assistant) {
    console.log('❌ 插件未加载');
    return;
  }
  
  console.log('✅ 插件已加载');
  console.log('📊 插件状态:', {
    isWindowOpen: assistant.isWindowOpen,
    isDocked: assistant.isDocked,
    logoButtonExists: !!assistant.logoButton,
    floatingWindowExists: !!assistant.floatingWindow
  });
  
  // 检查Logo按钮
  inspectLogoButton();
  
  // 检查浮窗状态
  const windowState = checkFloatingWindowState();
  console.log('📊 浮窗状态:', windowState);
}

// 运行主测试
testOpenCloseLoop();

// 导出函数
window.testOpenCloseLoop = testOpenCloseLoop;
window.quickDiagnosis = quickDiagnosis;
window.inspectLogoButton = inspectLogoButton;
window.simulateLogoButtonClick = simulateLogoButtonClick;
window.forceFixLogoButton = forceFixLogoButton;
window.checkFloatingWindowState = checkFloatingWindowState;

console.log('\n💡 可用的测试函数:');
console.log('- testOpenCloseLoop() - 测试开关循环');
console.log('- quickDiagnosis() - 快速诊断');
console.log('- inspectLogoButton() - 检查Logo按钮');
console.log('- simulateLogoButtonClick() - 模拟点击Logo按钮');
console.log('- forceFixLogoButton() - 强制修复Logo按钮');
console.log('- checkFloatingWindowState() - 检查浮窗状态');
