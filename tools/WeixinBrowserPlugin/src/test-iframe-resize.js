// 测试iframe重新打开尺寸问题的修复 - 在浏览器控制台中运行

console.log('=== 测试iframe重新打开尺寸修复 ===');

// 获取详细的尺寸信息
function getDetailedSizeInfo() {
  const floatingWindow = document.getElementById('senparc-weixin-ai-window');
  const contentArea = document.querySelector('.floating-window-content');
  const iframe = document.getElementById('senparc-ai-iframe');
  
  if (!floatingWindow || !contentArea || !iframe) {
    console.log('❌ 找不到相关元素');
    return null;
  }
  
  const windowRect = floatingWindow.getBoundingClientRect();
  const contentRect = contentArea.getBoundingClientRect();
  const iframeRect = iframe.getBoundingClientRect();
  
  const info = {
    floatingWindow: {
      width: windowRect.width,
      height: windowRect.height,
      isVisible: floatingWindow.classList.contains('show')
    },
    contentArea: {
      width: contentRect.width,
      height: contentRect.height,
      computedStyle: {
        display: window.getComputedStyle(contentArea).display,
        flex: window.getComputedStyle(contentArea).flex,
        height: window.getComputedStyle(contentArea).height
      }
    },
    iframe: {
      width: iframeRect.width,
      height: iframeRect.height,
      computedStyle: {
        display: window.getComputedStyle(iframe).display,
        width: window.getComputedStyle(iframe).width,
        height: window.getComputedStyle(iframe).height,
        position: window.getComputedStyle(iframe).position
      }
    }
  };
  
  console.log('📊 详细尺寸信息:');
  console.table(info);
  
  // 检查尺寸匹配
  const heightMatch = Math.abs(contentRect.height - iframeRect.height) < 5;
  const widthMatch = Math.abs(contentRect.width - iframeRect.width) < 5;
  
  console.log('🔍 尺寸匹配检查:');
  console.log('  宽度匹配:', widthMatch, `(差异: ${Math.abs(contentRect.width - iframeRect.width)}px)`);
  console.log('  高度匹配:', heightMatch, `(差异: ${Math.abs(contentRect.height - iframeRect.height)}px)`);
  
  return {
    info,
    isMatching: heightMatch && widthMatch
  };
}

// 模拟多次开关测试
async function testMultipleReopen() {
  console.log('\n🧪 开始多次重新打开测试...');
  
  const button = document.getElementById('senparc-weixin-ai-button');
  if (!button) {
    console.error('❌ 找不到Logo按钮');
    return;
  }
  
  const results = [];
  
  for (let i = 1; i <= 5; i++) {
    console.log(`\n--- 第${i}次测试 ---`);
    
    // 确保浮窗关闭
    const existingWindow = document.getElementById('senparc-weixin-ai-window');
    if (existingWindow && existingWindow.style.display !== 'none') {
      const closeBtn = document.querySelector('.close-button');
      if (closeBtn) closeBtn.click();
      await new Promise(resolve => setTimeout(resolve, 500));
    }
    
    // 打开浮窗
    console.log('🚀 打开浮窗...');
    button.click();
    
    // 等待打开动画完成
    await new Promise(resolve => setTimeout(resolve, 1000));
    
    // 检查尺寸
    const result = getDetailedSizeInfo();
    if (result) {
      results.push({
        test: i,
        isMatching: result.isMatching,
        contentHeight: result.info.contentArea.height,
        iframeHeight: result.info.iframe.height,
        heightDiff: Math.abs(result.info.contentArea.height - result.info.iframe.height)
      });
      
      console.log(result.isMatching ? '✅ 尺寸匹配正常' : '❌ 尺寸不匹配');
    }
    
    // 等待一下再进行下一次测试
    await new Promise(resolve => setTimeout(resolve, 500));
  }
  
  // 汇总结果
  console.log('\n📈 测试结果汇总:');
  console.table(results);
  
  const successCount = results.filter(r => r.isMatching).length;
  console.log(`\n🎯 成功率: ${successCount}/${results.length} (${(successCount/results.length*100).toFixed(1)}%)`);
  
  return results;
}

// 手动强制修复
function forceResizeIframe() {
  console.log('🔧 手动强制修复iframe尺寸...');
  
  const contentArea = document.querySelector('.floating-window-content');
  const iframe = document.getElementById('senparc-ai-iframe');
  
  if (!contentArea || !iframe) {
    console.error('❌ 找不到相关元素');
    return;
  }
  
  // 获取内容区域的实际尺寸
  const rect = contentArea.getBoundingClientRect();
  console.log('📏 内容区域实际尺寸:', rect.width, 'x', rect.height);
  
  // 强制设置iframe尺寸
  iframe.style.width = rect.width + 'px';
  iframe.style.height = rect.height + 'px';
  iframe.style.display = 'block';
  iframe.style.visibility = 'visible';
  
  console.log('✅ 强制修复完成');
  
  // 验证修复效果
  setTimeout(() => {
    getDetailedSizeInfo();
  }, 100);
}

// 调用插件的重新计算方法
function callPluginRecalculate() {
  console.log('🔧 调用插件的重新计算方法...');
  
  if (window.globalAssistantInstance && window.globalAssistantInstance.recalculateIframeSize) {
    window.globalAssistantInstance.recalculateIframeSize();
    console.log('✅ 已调用插件重新计算方法');
    
    setTimeout(() => {
      getDetailedSizeInfo();
    }, 200);
  } else {
    console.error('❌ 找不到插件实例或重新计算方法');
  }
}

// 比较第一次和后续打开的差异
async function compareFirstAndSubsequent() {
  console.log('\n🔍 比较第一次和后续打开的差异...');
  
  const button = document.getElementById('senparc-weixin-ai-button');
  if (!button) {
    console.error('❌ 找不到Logo按钮');
    return;
  }
  
  // 确保浮窗关闭
  const existingWindow = document.getElementById('senparc-weixin-ai-window');
  if (existingWindow) {
    const closeBtn = document.querySelector('.close-button');
    if (closeBtn) closeBtn.click();
    await new Promise(resolve => setTimeout(resolve, 500));
  }
  
  // 第一次打开（模拟首次创建）
  console.log('🥇 第一次打开...');
  button.click();
  await new Promise(resolve => setTimeout(resolve, 1500));
  
  const firstResult = getDetailedSizeInfo();
  console.log('第一次打开结果:', firstResult?.isMatching ? '✅ 正常' : '❌ 异常');
  
  // 关闭
  const closeBtn = document.querySelector('.close-button');
  if (closeBtn) closeBtn.click();
  await new Promise(resolve => setTimeout(resolve, 500));
  
  // 第二次打开（重新打开）
  console.log('🥈 第二次打开...');
  button.click();
  await new Promise(resolve => setTimeout(resolve, 1500));
  
  const secondResult = getDetailedSizeInfo();
  console.log('第二次打开结果:', secondResult?.isMatching ? '✅ 正常' : '❌ 异常');
  
  // 比较结果
  if (firstResult && secondResult) {
    console.log('\n📊 差异对比:');
    console.log('高度差异:', Math.abs(firstResult.info.iframe.height - secondResult.info.iframe.height), 'px');
    console.log('宽度差异:', Math.abs(firstResult.info.iframe.width - secondResult.info.iframe.width), 'px');
  }
  
  return { firstResult, secondResult };
}

// 导出测试函数
window.iframeResizeTest = {
  getSizeInfo: getDetailedSizeInfo,
  testMultiple: testMultipleReopen,
  forceResize: forceResizeIframe,
  callRecalculate: callPluginRecalculate,
  compareOpenings: compareFirstAndSubsequent
};

console.log('\n🎮 可用测试命令:');
console.log('  iframeResizeTest.getSizeInfo() - 获取当前尺寸信息');
console.log('  iframeResizeTest.testMultiple() - 多次重新打开测试');
console.log('  iframeResizeTest.forceResize() - 手动强制修复');
console.log('  iframeResizeTest.callRecalculate() - 调用插件重新计算');
console.log('  iframeResizeTest.compareOpenings() - 比较第一次和后续打开');

console.log('\n💡 建议先运行: iframeResizeTest.compareOpenings()');
