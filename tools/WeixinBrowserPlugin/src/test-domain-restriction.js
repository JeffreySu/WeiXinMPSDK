// 测试域名限制功能 - 在浏览器控制台中运行

console.log('=== 测试域名限制功能 ===');

// 模拟不同URL的检测
function testUrlDetection() {
  console.log('🧪 测试URL检测功能...');
  
  // 测试用例
  const testCases = [
    // 应该支持的页面
    { url: 'https://developers.weixin.qq.com/', expected: true, desc: '微信开发者主页' },
    { url: 'https://developers.weixin.qq.com/doc/', expected: true, desc: '微信开发者文档' },
    { url: 'https://developers.weixin.qq.com/miniprogram/dev/', expected: true, desc: '小程序开发文档' },
    { url: 'https://pay.weixin.qq.com/doc', expected: true, desc: '微信支付文档根目录' },
    { url: 'https://pay.weixin.qq.com/doc/api/jsapi.php', expected: true, desc: '微信支付具体文档' },
    
    // 不应该支持的页面
    { url: 'https://weixin.qq.com/', expected: false, desc: '微信主页' },
    { url: 'https://mp.weixin.qq.com/', expected: false, desc: '微信公众平台' },
    { url: 'https://work.weixin.qq.com/', expected: false, desc: '企业微信' },
    { url: 'https://pay.weixin.qq.com/', expected: false, desc: '微信支付主页(非/doc)' },
    { url: 'https://pay.weixin.qq.com/index.html', expected: false, desc: '微信支付其他页面' },
    { url: 'https://open.weixin.qq.com/', expected: false, desc: '微信开放平台' },
    { url: 'https://example.com/', expected: false, desc: '其他网站' }
  ];
  
  // 获取检测函数
  let detectionFunction;
  
  // 尝试从插件实例获取
  if (window.globalAssistantInstance && window.globalAssistantInstance.isWeixinDocPage) {
    detectionFunction = window.globalAssistantInstance.isWeixinDocPage.bind(window.globalAssistantInstance);
  } else {
    // 如果没有实例，创建模拟函数
    detectionFunction = function() {
      const url = window.location.href;
      const hostname = window.location.hostname;
      
      const allowedUrls = [
        'developers.weixin.qq.com',
        'pay.weixin.qq.com'
      ];
      
      const isAllowedDomain = allowedUrls.some(domain => hostname === domain);
      
      if (hostname === 'pay.weixin.qq.com') {
        return url.includes('/doc');
      }
      
      return isAllowedDomain;
    };
  }
  
  console.log('📊 测试结果:');
  console.log('格式: [状态] 描述 - URL');
  
  let passCount = 0;
  
  testCases.forEach(testCase => {
    // 模拟URL环境
    const originalLocation = window.location;
    const mockUrl = new URL(testCase.url);
    
    // 临时替换location对象
    Object.defineProperty(window, 'location', {
      value: {
        href: testCase.url,
        hostname: mockUrl.hostname,
        pathname: mockUrl.pathname
      },
      writable: true
    });
    
    try {
      const result = detectionFunction();
      const passed = result === testCase.expected;
      
      if (passed) passCount++;
      
      console.log(
        `${passed ? '✅' : '❌'} ${testCase.desc} - ${testCase.url}`
      );
      
      if (!passed) {
        console.log(`  期望: ${testCase.expected}, 实际: ${result}`);
      }
    } catch (error) {
      console.error(`❌ ${testCase.desc} - 检测出错:`, error);
    }
    
    // 恢复原始location
    Object.defineProperty(window, 'location', {
      value: originalLocation,
      writable: true
    });
  });
  
  const successRate = (passCount / testCases.length * 100).toFixed(1);
  console.log(`\n🎯 测试通过率: ${passCount}/${testCases.length} (${successRate}%)`);
  
  return { passCount, total: testCases.length, successRate };
}

// 检查当前页面状态
function checkCurrentPageStatus() {
  console.log('\n🔍 检查当前页面状态...');
  
  const url = window.location.href;
  const hostname = window.location.hostname;
  
  console.log('当前URL:', url);
  console.log('当前域名:', hostname);
  
  // 检查是否应该支持
  const supportedPages = [
    'https://developers.weixin.qq.com',
    'https://pay.weixin.qq.com/doc'
  ];
  
  const shouldSupport = supportedPages.some(page => {
    if (page.includes('/doc')) {
      return url.startsWith(page);
    } else {
      return url.startsWith(page);
    }
  });
  
  console.log('应该支持:', shouldSupport);
  
  // 检查插件实际状态
  const logoButton = document.getElementById('senparc-weixin-ai-button');
  const actualSupport = !!logoButton;
  
  console.log('实际支持:', actualSupport);
  console.log('状态匹配:', shouldSupport === actualSupport);
  
  return {
    url,
    hostname,
    shouldSupport,
    actualSupport,
    isMatching: shouldSupport === actualSupport
  };
}

// 检查按钮位置
function checkButtonPosition() {
  console.log('\n📍 检查按钮位置...');
  
  const button = document.getElementById('senparc-weixin-ai-button');
  
  if (!button) {
    console.log('❌ 未找到Logo按钮');
    return null;
  }
  
  const rect = button.getBoundingClientRect();
  const style = window.getComputedStyle(button);
  
  const position = {
    top: style.top,
    left: style.left,
    actualTop: rect.top,
    actualLeft: rect.left
  };
  
  console.log('按钮位置:', position);
  
  // 检查是否在合理范围内（避免遮挡网站Logo）
  const isReasonablePosition = rect.top >= 60; // 至少60px，避免遮挡
  
  console.log('位置合理:', isReasonablePosition);
  
  return { position, isReasonablePosition };
}

// 导出测试函数
window.domainRestrictionTest = {
  testUrlDetection,
  checkCurrentPage: checkCurrentPageStatus,
  checkButtonPosition
};

console.log('\n🎮 可用测试命令:');
console.log('  domainRestrictionTest.testUrlDetection() - 测试URL检测');
console.log('  domainRestrictionTest.checkCurrentPage() - 检查当前页面');
console.log('  domainRestrictionTest.checkButtonPosition() - 检查按钮位置');

console.log('\n💡 建议运行: domainRestrictionTest.testUrlDetection()');
