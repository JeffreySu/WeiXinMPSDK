// 测试停靠功能 - 在浏览器控制台中运行

console.log('=== 测试停靠功能 ===');

// 检查停靠功能状态
function checkDockFeature() {
  console.log('🔍 检查停靠功能状态...');
  
  const floatingWindow = document.getElementById('senparc-weixin-ai-window');
  const dockButton = document.getElementById('dock-toggle-button');
  const pageWrapper = document.getElementById('senparc-page-wrapper');
  
  if (!floatingWindow) {
    console.log('❌ 浮窗未找到，请先打开AI助手');
    return null;
  }
  
  const status = {
    hasFloatingWindow: !!floatingWindow,
    hasDockButton: !!dockButton,
    hasPageWrapper: !!pageWrapper,
    isDocked: floatingWindow.classList.contains('docked-mode'),
    isFloating: floatingWindow.classList.contains('floating-mode'),
    isVisible: floatingWindow.classList.contains('show'),
    windowClasses: Array.from(floatingWindow.classList),
    bodyStyle: document.body.style.cssText,
    pageWrapperStyle: pageWrapper ? pageWrapper.style.cssText : null
  };
  
  console.log('📊 停靠功能状态:');
  console.table(status);
  
  return status;
}

// 测试停靠切换
async function testDockToggle() {
  console.log('🧪 测试停靠切换功能...');
  
  const dockButton = document.getElementById('dock-toggle-button');
  if (!dockButton) {
    console.error('❌ 找不到停靠按钮');
    return;
  }
  
  console.log('🔄 测试停靠模式切换...');
  
  // 记录初始状态
  const initialStatus = checkDockFeature();
  console.log('初始状态:', initialStatus.isDocked ? '停靠' : '悬浮');
  
  // 点击切换
  dockButton.click();
  
  // 等待动画完成
  await new Promise(resolve => setTimeout(resolve, 500));
  
  // 检查切换后状态
  const afterToggleStatus = checkDockFeature();
  console.log('切换后状态:', afterToggleStatus.isDocked ? '停靠' : '悬浮');
  
  // 验证切换是否成功
  const toggleSuccessful = initialStatus.isDocked !== afterToggleStatus.isDocked;
  console.log('切换是否成功:', toggleSuccessful ? '✅ 是' : '❌ 否');
  
  return { initialStatus, afterToggleStatus, toggleSuccessful };
}

// 测试页面布局
function testPageLayout() {
  console.log('📐 测试页面布局...');
  
  const floatingWindow = document.getElementById('senparc-weixin-ai-window');
  const pageWrapper = document.getElementById('senparc-page-wrapper');
  
  if (!floatingWindow) {
    console.error('❌ 找不到浮窗');
    return;
  }
  
  const isDocked = floatingWindow.classList.contains('docked-mode');
  
  const layout = {
    windowMode: isDocked ? 'docked' : 'floating',
    viewportWidth: window.innerWidth,
    viewportHeight: window.innerHeight,
    floatingWindow: {
      rect: floatingWindow.getBoundingClientRect(),
      styles: {
        position: window.getComputedStyle(floatingWindow).position,
        width: window.getComputedStyle(floatingWindow).width,
        height: window.getComputedStyle(floatingWindow).height,
        top: window.getComputedStyle(floatingWindow).top,
        right: window.getComputedStyle(floatingWindow).right
      }
    }
  };
  
  if (pageWrapper) {
    layout.pageWrapper = {
      rect: pageWrapper.getBoundingClientRect(),
      styles: {
        width: window.getComputedStyle(pageWrapper).width,
        height: window.getComputedStyle(pageWrapper).height
      }
    };
  }
  
  console.log('📊 页面布局信息:');
  console.log(layout);
  
  // 验证布局正确性
  if (isDocked) {
    const windowWidth = layout.floatingWindow.rect.width;
    const expectedWidth = window.innerWidth * 0.4; // 40%
    const widthCorrect = Math.abs(windowWidth - expectedWidth) < 50; // 允许50px误差
    
    console.log('停靠模式布局检查:');
    console.log('  窗口宽度:', windowWidth);
    console.log('  期望宽度:', expectedWidth);
    console.log('  宽度正确:', widthCorrect ? '✅' : '❌');
  }
  
  return layout;
}

// 测试响应式设计
function testResponsiveDesign() {
  console.log('📱 测试响应式设计...');
  
  const dockButton = document.getElementById('dock-toggle-button');
  const viewportWidth = window.innerWidth;
  
  console.log('当前视口宽度:', viewportWidth);
  
  if (viewportWidth <= 480) {
    // 移动端
    const isButtonHidden = window.getComputedStyle(dockButton).display === 'none';
    console.log('移动端停靠按钮隐藏:', isButtonHidden ? '✅ 是' : '❌ 否');
    return { device: 'mobile', buttonHidden: isButtonHidden };
  } else if (viewportWidth <= 768) {
    // 平板端
    console.log('当前设备: 平板');
    return { device: 'tablet' };
  } else {
    // 桌面端
    console.log('当前设备: 桌面');
    return { device: 'desktop' };
  }
}

// 测试iframe尺寸
function testIframeSize() {
  console.log('🖼️ 测试iframe尺寸...');
  
  const iframe = document.getElementById('senparc-ai-iframe');
  const contentArea = document.querySelector('.floating-window-content');
  
  if (!iframe || !contentArea) {
    console.error('❌ 找不到iframe或内容区域');
    return;
  }
  
  const iframeRect = iframe.getBoundingClientRect();
  const contentRect = contentArea.getBoundingClientRect();
  
  const sizeInfo = {
    iframe: {
      width: iframeRect.width,
      height: iframeRect.height
    },
    contentArea: {
      width: contentRect.width,
      height: contentRect.height
    },
    matching: {
      width: Math.abs(iframeRect.width - contentRect.width) < 5,
      height: Math.abs(iframeRect.height - contentRect.height) < 5
    }
  };
  
  console.log('📊 iframe尺寸信息:');
  console.table(sizeInfo);
  
  const isCorrect = sizeInfo.matching.width && sizeInfo.matching.height;
  console.log('iframe尺寸正确:', isCorrect ? '✅' : '❌');
  
  return sizeInfo;
}

// 完整的停靠功能测试
async function runDockTests() {
  console.log('🚀 开始完整的停靠功能测试...');
  
  // 1. 检查初始状态
  console.log('\n1️⃣ 检查初始状态');
  const initialCheck = checkDockFeature();
  if (!initialCheck) return;
  
  // 2. 测试布局
  console.log('\n2️⃣ 测试页面布局');
  testPageLayout();
  
  // 3. 测试切换功能
  console.log('\n3️⃣ 测试停靠切换');
  await testDockToggle();
  
  // 4. 再次测试布局
  console.log('\n4️⃣ 切换后布局检查');
  testPageLayout();
  
  // 5. 测试iframe尺寸
  console.log('\n5️⃣ 测试iframe尺寸');
  testIframeSize();
  
  // 6. 测试响应式
  console.log('\n6️⃣ 测试响应式设计');
  testResponsiveDesign();
  
  // 7. 再次切换回去
  console.log('\n7️⃣ 切换回原始状态');
  await testDockToggle();
  
  console.log('\n🎉 停靠功能测试完成！');
}

// 手动控制函数
function manualDock() {
  const instance = window.globalAssistantInstance;
  if (instance && instance.setDockMode) {
    instance.setDockMode();
    console.log('✅ 手动切换到停靠模式');
  } else {
    console.error('❌ 找不到插件实例');
  }
}

function manualFloat() {
  const instance = window.globalAssistantInstance;
  if (instance && instance.setFloatingMode) {
    instance.setFloatingMode();
    console.log('✅ 手动切换到悬浮模式');
  } else {
    console.error('❌ 找不到插件实例');
  }
}

// 导出测试函数
window.dockFeatureTest = {
  check: checkDockFeature,
  testToggle: testDockToggle,
  testLayout: testPageLayout,
  testResponsive: testResponsiveDesign,
  testIframe: testIframeSize,
  runAll: runDockTests,
  manualDock,
  manualFloat
};

console.log('\n🎮 可用测试命令:');
console.log('  dockFeatureTest.check() - 检查停靠功能状态');
console.log('  dockFeatureTest.testToggle() - 测试切换功能');
console.log('  dockFeatureTest.testLayout() - 测试页面布局');
console.log('  dockFeatureTest.testResponsive() - 测试响应式设计');
console.log('  dockFeatureTest.testIframe() - 测试iframe尺寸');
console.log('  dockFeatureTest.runAll() - 运行所有测试');
console.log('  dockFeatureTest.manualDock() - 手动切换到停靠模式');
console.log('  dockFeatureTest.manualFloat() - 手动切换到悬浮模式');

console.log('\n💡 建议先运行: dockFeatureTest.runAll()');
