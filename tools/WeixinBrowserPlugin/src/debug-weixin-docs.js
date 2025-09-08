// 专门针对微信开发者文档的调试脚本
// 在 https://developers.weixin.qq.com 页面的控制台中运行

console.log('🔍 微信开发者文档页面停靠功能调试');
console.log('当前页面:', window.location.href);

// 1. 分析页面DOM结构
function analyzePageStructure() {
  console.log('\n📋 === 分析页面DOM结构 ===');
  
  const body = document.body;
  const bodyStyle = window.getComputedStyle(body);
  
  console.log('Body基本信息:');
  console.log('- 宽度:', bodyStyle.width);
  console.log('- 高度:', bodyStyle.height);
  console.log('- 位置:', bodyStyle.position);
  console.log('- overflow:', bodyStyle.overflow);
  console.log('- margin:', bodyStyle.margin);
  console.log('- padding:', bodyStyle.padding);
  
  // 检查主要容器
  const containers = [
    '#app',
    '.container', 
    '.wrapper',
    '.main',
    '.content',
    'main',
    '[class*="container"]',
    '[class*="wrapper"]',
    '[class*="main"]',
    '[class*="layout"]'
  ];
  
  console.log('\n主要容器元素:');
  containers.forEach(selector => {
    const elements = document.querySelectorAll(selector);
    if (elements.length > 0) {
      elements.forEach((el, index) => {
        const rect = el.getBoundingClientRect();
        const style = window.getComputedStyle(el);
        console.log(`${selector}[${index}]:`, {
          width: rect.width,
          height: rect.height,
          position: style.position,
          zIndex: style.zIndex,
          overflow: style.overflow,
          transform: style.transform
        });
      });
    }
  });
  
  // 检查body的直接子元素
  console.log('\nBody直接子元素:');
  Array.from(body.children).forEach((child, index) => {
    if (child.id !== 'senparc-weixin-ai-window' && child.id !== 'senparc-weixin-ai-button') {
      const rect = child.getBoundingClientRect();
      const style = window.getComputedStyle(child);
      console.log(`Child[${index}] (${child.tagName}):`, {
        id: child.id,
        className: child.className.substring(0, 50),
        width: rect.width,
        position: style.position,
        zIndex: style.zIndex
      });
    }
  });
}

// 2. 检查CSS样式冲突
function checkCSSConflicts() {
  console.log('\n🎨 === 检查CSS样式冲突 ===');
  
  const body = document.body;
  
  // 临时添加停靠class
  body.classList.add('senparc-docked');
  
  setTimeout(() => {
    const style = window.getComputedStyle(body);
    console.log('添加停靠class后的body样式:');
    console.log('- margin-right:', style.marginRight);
    console.log('- box-sizing:', style.boxSizing);
    console.log('- width:', style.width);
    console.log('- overflow-x:', style.overflowX);
    
    // 检查是否有其他样式覆盖
    const allRules = Array.from(document.styleSheets).reduce((rules, sheet) => {
      try {
        return rules.concat(Array.from(sheet.cssRules || []));
      } catch (e) {
        return rules;
      }
    }, []);
    
    const bodyRules = allRules.filter(rule => 
      rule.selectorText && rule.selectorText.includes('body')
    );
    
    console.log('影响body的CSS规则数量:', bodyRules.length);
    bodyRules.slice(0, 5).forEach((rule, index) => {
      console.log(`规则[${index}]:`, rule.cssText);
    });
    
    body.classList.remove('senparc-docked');
  }, 100);
}

// 3. 强制应用停靠样式（增强版）
function forceApplyDockStylesEnhanced() {
  console.log('\n🔧 === 强制应用停靠样式（增强版） ===');
  
  const body = document.body;
  
  // 清除可能冲突的样式
  body.style.margin = '';
  body.style.padding = '';
  body.style.width = '';
  body.style.maxWidth = '';
  body.style.minWidth = '';
  
  // 添加class
  body.classList.add('senparc-docked');
  
  // 强制内联样式
  body.style.setProperty('margin-right', '40%', 'important');
  body.style.setProperty('transition', 'margin-right 0.3s ease', 'important');
  body.style.setProperty('box-sizing', 'border-box', 'important');
  body.style.setProperty('overflow-x', 'hidden', 'important');
  body.style.setProperty('width', 'auto', 'important');
  
  console.log('✅ 强制样式已应用');
  
  // 检查是否生效
  setTimeout(() => {
    const style = window.getComputedStyle(body);
    const marginRight = parseFloat(style.marginRight);
    const expectedMargin = window.innerWidth * 0.4;
    
    console.log('验证结果:');
    console.log('- 期望边距:', expectedMargin + 'px');
    console.log('- 实际边距:', marginRight + 'px');
    console.log('- 是否生效:', Math.abs(marginRight - expectedMargin) < 50 ? '✅ 成功' : '❌ 失败');
    
    if (Math.abs(marginRight - expectedMargin) >= 50) {
      console.log('⚠️ 停靠样式未生效，尝试更强制的方法...');
      tryAlternativeApproach();
    }
  }, 200);
}

// 4. 尝试替代方案
function tryAlternativeApproach() {
  console.log('\n🚀 === 尝试替代方案 ===');
  
  // 方案1: 直接修改body的transform
  document.body.style.setProperty('transform', 'translateX(-20%)', 'important');
  document.body.style.setProperty('width', '60%', 'important');
  
  console.log('已尝试transform方案');
  
  setTimeout(() => {
    const rect = document.body.getBoundingClientRect();
    console.log('Transform方案结果:');
    console.log('- Body宽度:', rect.width);
    console.log('- Body右边距:', window.innerWidth - rect.right);
    
    // 方案2: 创建遮罩层
    if (rect.right > window.innerWidth * 0.6) {
      console.log('Transform方案无效，尝试遮罩层方案...');
      createOverlayMask();
    }
  }, 200);
}

// 5. 创建遮罩层方案
function createOverlayMask() {
  console.log('\n🎭 === 创建遮罩层方案 ===');
  
  // 移除之前的遮罩
  const existingMask = document.getElementById('senparc-page-mask');
  if (existingMask) {
    existingMask.remove();
  }
  
  // 创建遮罩层
  const mask = document.createElement('div');
  mask.id = 'senparc-page-mask';
  mask.style.cssText = `
    position: fixed !important;
    top: 0 !important;
    right: 0 !important;
    width: 40% !important;
    height: 100vh !important;
    background: rgba(255, 255, 255, 0.95) !important;
    z-index: 999999 !important;
    pointer-events: none !important;
    border-left: 1px solid #e0e0e0 !important;
  `;
  
  document.body.appendChild(mask);
  console.log('✅ 遮罩层已创建');
}

// 6. 清除所有停靠效果
function clearAllDockEffects() {
  console.log('\n🧹 === 清除所有停靠效果 ===');
  
  const body = document.body;
  
  // 清除class
  body.classList.remove('senparc-docked');
  
  // 清除内联样式
  body.style.marginRight = '';
  body.style.transition = '';
  body.style.boxSizing = '';
  body.style.overflowX = '';
  body.style.width = '';
  body.style.transform = '';
  
  // 移除遮罩层
  const mask = document.getElementById('senparc-page-mask');
  if (mask) {
    mask.remove();
  }
  
  console.log('✅ 所有停靠效果已清除');
}

// 7. 完整测试流程
async function runCompleteTest() {
  console.log('\n🧪 === 完整测试流程 ===');
  
  console.log('步骤1: 分析页面结构');
  analyzePageStructure();
  
  await new Promise(resolve => setTimeout(resolve, 1000));
  
  console.log('\n步骤2: 检查CSS冲突');
  checkCSSConflicts();
  
  await new Promise(resolve => setTimeout(resolve, 1000));
  
  console.log('\n步骤3: 强制应用停靠');
  forceApplyDockStylesEnhanced();
  
  await new Promise(resolve => setTimeout(resolve, 2000));
  
  console.log('\n步骤4: 清除效果');
  clearAllDockEffects();
  
  console.log('\n🎉 测试完成');
}

// 8. 针对微信文档的特殊处理
function applyWeixinDocsSpecificFix() {
  console.log('\n📱 === 针对微信文档的特殊处理 ===');
  
  // 检查常见的微信文档结构
  const weixinSelectors = [
    '#app',
    '.page-container',
    '.doc-content',
    '.main-content',
    '.sidebar',
    '.header',
    '.navigation'
  ];
  
  weixinSelectors.forEach(selector => {
    const elements = document.querySelectorAll(selector);
    if (elements.length > 0) {
      console.log(`发现元素: ${selector}, 数量: ${elements.length}`);
      elements.forEach((el, index) => {
        const style = window.getComputedStyle(el);
        console.log(`  [${index}] position: ${style.position}, zIndex: ${style.zIndex}, width: ${el.getBoundingClientRect().width}`);
      });
    }
  });
  
  // 特殊处理方案
  const body = document.body;
  body.classList.add('senparc-docked');
  
  // 针对微信文档的强制样式
  const specificStyles = `
    body.senparc-docked {
      margin-right: 40% !important;
      transition: margin-right 0.3s ease !important;
    }
    
    body.senparc-docked #app {
      margin-right: 0 !important;
      width: 100% !important;
      max-width: 100% !important;
    }
    
    body.senparc-docked .page-container {
      margin-right: 0 !important;
      width: 100% !important;
      max-width: 100% !important;
    }
    
    body.senparc-docked .main-content {
      margin-right: 0 !important;
      width: 100% !important;
      max-width: 100% !important;
    }
  `;
  
  // 注入特殊样式
  const styleElement = document.createElement('style');
  styleElement.id = 'senparc-weixin-docs-fix';
  styleElement.textContent = specificStyles;
  document.head.appendChild(styleElement);
  
  console.log('✅ 微信文档特殊样式已注入');
}

// 导出调试函数
window.weixinDocsDebug = {
  analyze: analyzePageStructure,
  checkCSS: checkCSSConflicts,
  forceApply: forceApplyDockStylesEnhanced,
  tryAlternative: tryAlternativeApproach,
  createMask: createOverlayMask,
  clear: clearAllDockEffects,
  fullTest: runCompleteTest,
  weixinFix: applyWeixinDocsSpecificFix
};

console.log('\n🎮 可用的调试命令:');
console.log('  weixinDocsDebug.analyze() - 分析页面结构');
console.log('  weixinDocsDebug.checkCSS() - 检查CSS冲突');
console.log('  weixinDocsDebug.forceApply() - 强制应用停靠');
console.log('  weixinDocsDebug.tryAlternative() - 尝试替代方案');
console.log('  weixinDocsDebug.createMask() - 创建遮罩层');
console.log('  weixinDocsDebug.clear() - 清除所有效果');
console.log('  weixinDocsDebug.fullTest() - 完整测试流程');
console.log('  weixinDocsDebug.weixinFix() - 微信文档特殊修复');

console.log('\n💡 建议先运行: weixinDocsDebug.analyze()');
console.log('然后运行: weixinDocsDebug.weixinFix()');
