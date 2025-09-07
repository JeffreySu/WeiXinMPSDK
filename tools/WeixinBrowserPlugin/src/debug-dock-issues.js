// 调试停靠问题的脚本 - 在浏览器控制台中运行

console.log('=== 调试停靠问题 ===');

// 检查body样式和class
function debugBodyState() {
  console.log('🔍 检查body状态...');
  
  const body = document.body;
  const computedStyle = window.getComputedStyle(body);
  
  const info = {
    hasDockClass: body.classList.contains('senparc-docked'),
    classList: Array.from(body.classList),
    marginRight: computedStyle.marginRight,
    width: computedStyle.width,
    boxSizing: computedStyle.boxSizing,
    clientWidth: body.clientWidth,
    scrollWidth: body.scrollWidth,
    offsetWidth: body.offsetWidth,
    viewportWidth: window.innerWidth
  };
  
  console.log('📊 Body状态:');
  console.table(info);
  
  // 检查margin-right是否生效
  const expectedMargin = window.innerWidth * 0.4;
  const actualMargin = parseFloat(computedStyle.marginRight);
  console.log(`期望右边距: ${expectedMargin}px`);
  console.log(`实际右边距: ${actualMargin}px`);
  console.log(`边距是否正确: ${Math.abs(actualMargin - expectedMargin) < 50 ? '✅' : '❌'}`);
  
  return info;
}

// 检查所有子元素的宽度
function debugChildElements() {
  console.log('🔍 检查子元素...');
  
  const children = Array.from(document.body.children);
  const problematicElements = [];
  
  children.forEach((child, index) => {
    if (child.id === 'senparc-weixin-ai-window' || child.id === 'senparc-weixin-ai-button') {
      return; // 跳过我们的插件元素
    }
    
    const rect = child.getBoundingClientRect();
    const style = window.getComputedStyle(child);
    
    const elementInfo = {
      index,
      tagName: child.tagName,
      id: child.id,
      className: child.className,
      position: style.position,
      width: rect.width,
      right: rect.right,
      viewportWidth: window.innerWidth,
      overflowing: rect.right > window.innerWidth * 0.6
    };
    
    if (elementInfo.overflowing) {
      problematicElements.push(elementInfo);
    }
  });
  
  console.log('📊 有问题的元素 (超出60%区域):');
  console.table(problematicElements);
  
  return problematicElements;
}

// 强制应用停靠样式
function forceApplyDockStyles() {
  console.log('🔧 强制应用停靠样式...');
  
  // 添加body class
  document.body.classList.add('senparc-docked');
  
  // 直接设置内联样式作为后备
  document.body.style.marginRight = '40%';
  document.body.style.transition = 'margin-right 0.3s ease';
  document.body.style.boxSizing = 'border-box';
  
  // 强制所有直接子元素
  Array.from(document.body.children).forEach(child => {
    if (child.id !== 'senparc-weixin-ai-window' && child.id !== 'senparc-weixin-ai-button') {
      child.style.maxWidth = '100%';
      child.style.boxSizing = 'border-box';
    }
  });
  
  console.log('✅ 强制停靠样式已应用');
  
  setTimeout(() => {
    debugBodyState();
    debugChildElements();
  }, 500);
}

// 移除停靠样式
function removeDockStyles() {
  console.log('🧹 移除停靠样式...');
  
  document.body.classList.remove('senparc-docked');
  document.body.style.marginRight = '';
  document.body.style.transition = '';
  document.body.style.boxSizing = '';
  
  Array.from(document.body.children).forEach(child => {
    if (child.id !== 'senparc-weixin-ai-window' && child.id !== 'senparc-weixin-ai-button') {
      child.style.maxWidth = '';
      child.style.boxSizing = '';
    }
  });
  
  console.log('✅ 停靠样式已移除');
}

// 检查CSS是否加载
function debugCSSLoading() {
  console.log('🔍 检查CSS加载状态...');
  
  const styleSheets = Array.from(document.styleSheets);
  const extensionStyles = styleSheets.filter(sheet => {
    try {
      return sheet.href && sheet.href.includes('chrome-extension');
    } catch (e) {
      return false;
    }
  });
  
  console.log('扩展样式表数量:', extensionStyles.length);
  
  if (extensionStyles.length > 0) {
    extensionStyles.forEach((sheet, index) => {
      console.log(`样式表 ${index + 1}:`, sheet.href);
      
      try {
        const rules = Array.from(sheet.cssRules);
        const dockRules = rules.filter(rule => 
          rule.selectorText && rule.selectorText.includes('senparc-docked')
        );
        console.log('停靠相关规则数量:', dockRules.length);
        dockRules.forEach(rule => {
          console.log('规则:', rule.cssText);
        });
      } catch (e) {
        console.log('无法访问样式表规则:', e.message);
      }
    });
  } else {
    console.warn('⚠️ 未找到扩展样式表');
  }
}

// 检查浮窗状态
function debugFloatingWindow() {
  console.log('🪟 检查浮窗状态...');
  
  const floatingWindow = document.getElementById('senparc-weixin-ai-window');
  if (!floatingWindow) {
    console.error('❌ 浮窗元素未找到');
    return null;
  }
  
  const rect = floatingWindow.getBoundingClientRect();
  const style = window.getComputedStyle(floatingWindow);
  
  const info = {
    display: style.display,
    visibility: style.visibility,
    opacity: style.opacity,
    position: style.position,
    zIndex: style.zIndex,
    top: rect.top,
    right: window.innerWidth - rect.right,
    width: rect.width,
    height: rect.height,
    classList: Array.from(floatingWindow.classList),
    isVisible: rect.width > 0 && rect.height > 0 && style.opacity !== '0'
  };
  
  console.log('📊 浮窗状态:');
  console.table(info);
  
  return info;
}

// 完整诊断
function runCompleteDiagnosis() {
  console.log('🚀 运行完整诊断...');
  
  console.log('\n1️⃣ CSS加载状态');
  debugCSSLoading();
  
  console.log('\n2️⃣ Body状态');
  debugBodyState();
  
  console.log('\n3️⃣ 子元素检查');
  debugChildElements();
  
  console.log('\n4️⃣ 浮窗状态');
  debugFloatingWindow();
  
  console.log('\n🎉 诊断完成');
}

// 测试停靠切换
async function testDockToggleWithDebug() {
  console.log('🧪 测试停靠切换（带调试）...');
  
  console.log('\n--- 切换前状态 ---');
  runCompleteDiagnosis();
  
  // 强制应用停靠
  console.log('\n--- 应用停靠 ---');
  forceApplyDockStyles();
  
  // 等待一下
  await new Promise(resolve => setTimeout(resolve, 1000));
  
  console.log('\n--- 停靠后状态 ---');
  debugBodyState();
  debugChildElements();
  
  // 移除停靠
  console.log('\n--- 移除停靠 ---');
  removeDockStyles();
  
  await new Promise(resolve => setTimeout(resolve, 500));
  
  console.log('\n--- 恢复后状态 ---');
  debugBodyState();
}

// 导出调试函数
window.dockDebug = {
  checkBody: debugBodyState,
  checkChildren: debugChildElements,
  checkCSS: debugCSSLoading,
  checkWindow: debugFloatingWindow,
  forceApply: forceApplyDockStyles,
  remove: removeDockStyles,
  diagnose: runCompleteDiagnosis,
  testToggle: testDockToggleWithDebug
};

console.log('\n🎮 可用调试命令:');
console.log('  dockDebug.checkBody() - 检查body状态');
console.log('  dockDebug.checkChildren() - 检查子元素');
console.log('  dockDebug.checkCSS() - 检查CSS加载');
console.log('  dockDebug.checkWindow() - 检查浮窗状态');
console.log('  dockDebug.forceApply() - 强制应用停靠');
console.log('  dockDebug.remove() - 移除停靠样式');
console.log('  dockDebug.diagnose() - 完整诊断');
console.log('  dockDebug.testToggle() - 测试切换');

console.log('\n💡 建议先运行: dockDebug.diagnose()');
