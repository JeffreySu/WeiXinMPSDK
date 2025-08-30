// 测试改进后的停靠功能 - 在浏览器控制台中运行

console.log('=== 测试改进后的停靠功能 ===');

// 检查body停靠状态
function checkBodyDockState() {
  console.log('🔍 检查body停靠状态...');
  
  const hasDockClass = document.body.classList.contains('senparc-docked');
  const bodyStyle = window.getComputedStyle(document.body);
  const marginRight = bodyStyle.marginRight;
  
  const state = {
    hasDockClass,
    marginRight,
    computedWidth: parseFloat(marginRight),
    expectedWidth: window.innerWidth * 0.4, // 40%
    isCorrectWidth: Math.abs(parseFloat(marginRight) - window.innerWidth * 0.4) < 50
  };
  
  console.log('📊 Body停靠状态:');
  console.table(state);
  
  return state;
}

// 检查浮窗状态
function checkFloatingWindowState() {
  console.log('🪟 检查浮窗状态...');
  
  const floatingWindow = document.getElementById('senparc-weixin-ai-window');
  if (!floatingWindow) {
    console.error('❌ 浮窗未找到');
    return null;
  }
  
  const rect = floatingWindow.getBoundingClientRect();
  const isDocked = floatingWindow.classList.contains('docked-mode');
  const isFloating = floatingWindow.classList.contains('floating-mode');
  
  const state = {
    isDocked,
    isFloating,
    position: {
      top: rect.top,
      right: window.innerWidth - rect.right,
      width: rect.width,
      height: rect.height
    },
    expectedWidth: window.innerWidth * 0.4,
    isCorrectPosition: isDocked ? (rect.right >= window.innerWidth - 10) : true
  };
  
  console.log('📊 浮窗状态:');
  console.table(state);
  
  return state;
}

// 测试停靠切换功能
async function testDockToggle() {
  console.log('🧪 测试停靠切换功能...');
  
  const dockButton = document.getElementById('dock-toggle-button');
  if (!dockButton) {
    console.error('❌ 找不到停靠按钮');
    return;
  }
  
  // 记录初始状态
  console.log('\n--- 初始状态 ---');
  const initialBody = checkBodyDockState();
  const initialWindow = checkFloatingWindowState();
  
  console.log('\n🔄 执行切换...');
  dockButton.click();
  
  // 等待动画完成
  await new Promise(resolve => setTimeout(resolve, 500));
  
  // 检查切换后状态
  console.log('\n--- 切换后状态 ---');
  const afterBody = checkBodyDockState();
  const afterWindow = checkFloatingWindowState();
  
  // 验证切换是否成功
  const bodyStateChanged = initialBody.hasDockClass !== afterBody.hasDockClass;
  const windowStateChanged = initialWindow.isDocked !== afterWindow.isDocked;
  
  console.log('\n📊 切换结果:');
  console.log('Body状态变化:', bodyStateChanged ? '✅ 是' : '❌ 否');
  console.log('浮窗状态变化:', windowStateChanged ? '✅ 是' : '❌ 否');
  console.log('切换成功:', (bodyStateChanged && windowStateChanged) ? '✅ 是' : '❌ 否');
  
  return {
    success: bodyStateChanged && windowStateChanged,
    initialBody,
    afterBody,
    initialWindow,
    afterWindow
  };
}

// 测试多次切换
async function testMultipleToggle() {
  console.log('🔄 测试多次切换...');
  
  const results = [];
  
  for (let i = 1; i <= 3; i++) {
    console.log(`\n=== 第${i}次切换 ===`);
    const result = await testDockToggle();
    results.push(result);
    
    // 等待一下再进行下一次测试
    await new Promise(resolve => setTimeout(resolve, 300));
  }
  
  const successCount = results.filter(r => r.success).length;
  console.log(`\n📊 多次切换结果: ${successCount}/${results.length} 成功`);
  
  return results;
}

// 测试页面内容是否正确调整
function testPageContentAdjustment() {
  console.log('📄 测试页面内容调整...');
  
  const bodyRect = document.body.getBoundingClientRect();
  const isDocked = document.body.classList.contains('senparc-docked');
  
  const info = {
    isDocked,
    bodyWidth: bodyRect.width,
    viewportWidth: window.innerWidth,
    availableWidth: isDocked ? window.innerWidth * 0.6 : window.innerWidth,
    isContentWidthCorrect: isDocked ? 
      Math.abs(bodyRect.width - window.innerWidth * 0.6) < 50 :
      Math.abs(bodyRect.width - window.innerWidth) < 50
  };
  
  console.log('📊 页面内容调整信息:');
  console.table(info);
  
  if (isDocked) {
    console.log('停靠模式下内容宽度:', info.isContentWidthCorrect ? '✅ 正确' : '❌ 错误');
  } else {
    console.log('悬浮模式下内容宽度:', info.isContentWidthCorrect ? '✅ 正确' : '❌ 错误');
  }
  
  return info;
}

// 测试状态持久性
async function testStatePersistence() {
  console.log('💾 测试状态持久性...');
  
  const dockButton = document.getElementById('dock-toggle-button');
  if (!dockButton) {
    console.error('❌ 找不到停靠按钮');
    return;
  }
  
  // 切换到停靠模式
  console.log('1️⃣ 切换到停靠模式');
  dockButton.click();
  await new Promise(resolve => setTimeout(resolve, 500));
  
  const dockedState = checkBodyDockState();
  console.log('停靠状态:', dockedState.hasDockClass ? '✅' : '❌');
  
  // 切换回悬浮模式
  console.log('2️⃣ 切换回悬浮模式');
  dockButton.click();
  await new Promise(resolve => setTimeout(resolve, 500));
  
  const floatingState = checkBodyDockState();
  console.log('悬浮状态:', !floatingState.hasDockClass ? '✅' : '❌');
  
  // 再次切换到停靠模式
  console.log('3️⃣ 再次切换到停靠模式');
  dockButton.click();
  await new Promise(resolve => setTimeout(resolve, 500));
  
  const finalState = checkBodyDockState();
  console.log('最终停靠状态:', finalState.hasDockClass ? '✅' : '❌');
  
  const allWorking = dockedState.hasDockClass && !floatingState.hasDockClass && finalState.hasDockClass;
  console.log('状态持久性测试:', allWorking ? '✅ 通过' : '❌ 失败');
  
  return allWorking;
}

// 完整测试套件
async function runCompleteTests() {
  console.log('🚀 运行完整测试套件...');
  
  console.log('\n1️⃣ 检查初始状态');
  checkBodyDockState();
  checkFloatingWindowState();
  
  console.log('\n2️⃣ 测试页面内容调整');
  testPageContentAdjustment();
  
  console.log('\n3️⃣ 测试单次切换');
  await testDockToggle();
  
  console.log('\n4️⃣ 测试多次切换');
  await testMultipleToggle();
  
  console.log('\n5️⃣ 测试状态持久性');
  await testStatePersistence();
  
  console.log('\n🎉 完整测试套件完成！');
}

// 手动控制函数
function forceDockMode() {
  document.body.classList.add('senparc-docked');
  const floatingWindow = document.getElementById('senparc-weixin-ai-window');
  if (floatingWindow) {
    floatingWindow.classList.add('docked-mode');
    floatingWindow.classList.remove('floating-mode');
  }
  console.log('✅ 强制切换到停靠模式');
}

function forceFloatingMode() {
  document.body.classList.remove('senparc-docked');
  const floatingWindow = document.getElementById('senparc-weixin-ai-window');
  if (floatingWindow) {
    floatingWindow.classList.remove('docked-mode');
    floatingWindow.classList.add('floating-mode');
  }
  console.log('✅ 强制切换到悬浮模式');
}

// 清理状态
function cleanupDockState() {
  document.body.classList.remove('senparc-docked');
  const floatingWindow = document.getElementById('senparc-weixin-ai-window');
  if (floatingWindow) {
    floatingWindow.classList.remove('docked-mode');
    floatingWindow.classList.add('floating-mode');
  }
  console.log('🧹 清理停靠状态完成');
}

// 导出测试函数
window.dockV2Test = {
  checkBody: checkBodyDockState,
  checkWindow: checkFloatingWindowState,
  testToggle: testDockToggle,
  testMultiple: testMultipleToggle,
  testContent: testPageContentAdjustment,
  testPersistence: testStatePersistence,
  runAll: runCompleteTests,
  forceDock: forceDockMode,
  forceFloat: forceFloatingMode,
  cleanup: cleanupDockState
};

console.log('\n🎮 可用测试命令:');
console.log('  dockV2Test.checkBody() - 检查body停靠状态');
console.log('  dockV2Test.checkWindow() - 检查浮窗状态');
console.log('  dockV2Test.testToggle() - 测试单次切换');
console.log('  dockV2Test.testMultiple() - 测试多次切换');
console.log('  dockV2Test.testContent() - 测试页面内容调整');
console.log('  dockV2Test.testPersistence() - 测试状态持久性');
console.log('  dockV2Test.runAll() - 运行完整测试');
console.log('  dockV2Test.forceDock() - 强制停靠模式');
console.log('  dockV2Test.forceFloat() - 强制悬浮模式');
console.log('  dockV2Test.cleanup() - 清理停靠状态');

console.log('\n💡 建议运行: dockV2Test.runAll()');
