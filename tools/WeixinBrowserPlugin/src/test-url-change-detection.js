// URL变化检测和iframe重新加载功能测试脚本
// 此文件用于验证插件的URL变化检测功能是否正常工作

(function() {
  'use strict';

  // 测试配置
  const TEST_CONFIG = {
    enableDebug: true,
    testDuration: 10000, // 测试持续时间（毫秒）
    urlChangeInterval: 3000, // URL变化间隔（毫秒）
  };

  // 测试状态
  let testResults = {
    totalTests: 0,
    passedTests: 0,
    failedTests: 0,
    errors: []
  };

  // 日志函数
  function log(level, message, ...args) {
    if (TEST_CONFIG.enableDebug) {
      const timestamp = new Date().toLocaleTimeString();
      console[level](`[URL_TEST ${timestamp}] ${message}`, ...args);
    }
  }

  // 测试助手函数
  function assert(condition, message) {
    testResults.totalTests++;
    if (condition) {
      testResults.passedTests++;
      log('info', `✅ PASS: ${message}`);
    } else {
      testResults.failedTests++;
      testResults.errors.push(message);
      log('error', `❌ FAIL: ${message}`);
    }
  }

  // 模拟URL变化
  function simulateUrlChange(newUrl) {
    log('info', `🔄 模拟URL变化: ${window.location.href} -> ${newUrl}`);
    
    // 使用pushState模拟SPA的URL变化
    history.pushState({}, '', newUrl);
    
    // 触发popstate事件以确保插件检测到变化
    window.dispatchEvent(new PopStateEvent('popstate', { state: {} }));
  }

  // 等待函数
  function wait(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

  // 检查插件实例是否存在
  function checkPluginInstance() {
    const instance = window.globalAssistantInstance;
    assert(instance !== null && instance !== undefined, '插件实例应该存在');
    return instance;
  }

  // 检查URL变化检测方法
  function testUrlChangeDetection(instance) {
    log('info', '🧪 测试URL变化检测功能...');
    
    // 记录初始URL
    const initialUrl = window.location.href;
    log('info', `📍 初始URL: ${initialUrl}`);
    
    // 检查hasUrlChanged方法是否存在
    assert(typeof instance.hasUrlChanged === 'function', 'hasUrlChanged方法应该存在');
    
    // 初始状态应该返回false（URL未变化）
    const initialCheck = instance.hasUrlChanged();
    assert(initialCheck === false, '初始状态URL应该未变化');
    
    // 模拟URL变化
    const newUrl = initialUrl + '#test-change-' + Date.now();
    simulateUrlChange(newUrl);
    
    // 检查是否检测到URL变化
    const afterChange = instance.hasUrlChanged();
    assert(afterChange === true, 'URL变化后应该被检测到');
    
    // 更新记录的URL
    instance.updateLastUrl();
    
    // 再次检查应该返回false
    const afterUpdate = instance.hasUrlChanged();
    assert(afterUpdate === false, '更新lastUrl后应该不再检测到变化');
  }

  // 测试iframe重新加载功能
  async function testIframeReload(instance) {
    log('info', '🧪 测试iframe重新加载功能...');
    
    // 检查reloadIframeContent方法是否存在
    assert(typeof instance.reloadIframeContent === 'function', 'reloadIframeContent方法应该存在');
    
    // 如果浮窗不存在，先打开它
    if (!instance.isWindowOpen) {
      log('info', '📂 打开浮窗进行测试...');
      instance.openFloatingWindow();
      await wait(1000); // 等待浮窗创建
    }
    
    // 检查浮窗是否存在
    assert(instance.floatingWindow !== null, '浮窗应该存在');
    
    if (instance.floatingWindow) {
      const iframe = instance.floatingWindow.querySelector('#senparc-ai-iframe');
      assert(iframe !== null, 'iframe元素应该存在');
      
      if (iframe) {
        // 记录当前iframe的src
        const originalSrc = iframe.src;
        log('info', `📝 原始iframe URL: ${originalSrc}`);
        
        // 模拟URL变化
        const newPageUrl = window.location.href + '#iframe-test-' + Date.now();
        simulateUrlChange(newPageUrl);
        
        // 调用重新加载iframe内容
        instance.reloadIframeContent();
        
        // 等待重新加载完成
        await wait(2000);
        
        // 检查iframe的src是否已更新
        const newSrc = iframe.src;
        log('info', `🔄 新iframe URL: ${newSrc}`);
        
        const shouldContainNewUrl = encodeURIComponent(newPageUrl);
        assert(newSrc.includes(shouldContainNewUrl), 'iframe URL应该包含新的页面URL');
        assert(newSrc !== originalSrc, 'iframe URL应该已经改变');
      }
    }
  }

  // 测试浮窗重新打开时的URL检查
  async function testWindowReopenUrlCheck(instance) {
    log('info', '🧪 测试浮窗重新打开时的URL检查...');
    
    // 确保浮窗是打开的
    if (!instance.isWindowOpen) {
      instance.openFloatingWindow();
      await wait(1000);
    }
    
    // 记录当前URL
    const currentUrl = window.location.href;
    
    // 关闭浮窗
    log('info', '🚪 关闭浮窗...');
    instance.closeFloatingWindow();
    await wait(500);
    
    // 验证浮窗已关闭
    assert(instance.isWindowOpen === false, '浮窗应该已关闭');
    
    // 模拟用户导航到新页面
    const newUrl = currentUrl + '#reopen-test-' + Date.now();
    log('info', `🌐 模拟导航到新URL: ${newUrl}`);
    simulateUrlChange(newUrl);
    
    // 重新打开浮窗
    log('info', '📂 重新打开浮窗...');
    instance.openFloatingWindow();
    await wait(2000); // 等待iframe重新加载
    
    // 检查浮窗是否已打开
    assert(instance.isWindowOpen === true, '浮窗应该已重新打开');
    
    // 检查iframe的URL是否正确更新
    if (instance.floatingWindow) {
      const iframe = instance.floatingWindow.querySelector('#senparc-ai-iframe');
      if (iframe) {
        const iframeSrc = iframe.src;
        const expectedUrlParam = encodeURIComponent(newUrl);
        assert(iframeSrc.includes(expectedUrlParam), 'iframe应该加载新URL对应的内容');
        log('info', `✅ iframe正确加载了新URL: ${iframeSrc}`);
      }
    }
  }

  // 运行所有测试
  async function runAllTests() {
    log('info', '🚀 开始URL变化检测和iframe重新加载功能测试...');
    
    try {
      // 等待插件初始化
      await wait(1000);
      
      // 检查插件实例
      const instance = checkPluginInstance();
      if (!instance) {
        log('error', '❌ 插件实例不存在，无法继续测试');
        return;
      }
      
      // 运行各项测试
      testUrlChangeDetection(instance);
      await wait(1000);
      
      await testIframeReload(instance);
      await wait(1000);
      
      await testWindowReopenUrlCheck(instance);
      
      // 输出测试结果
      log('info', '🏁 测试完成！');
      log('info', `📊 测试结果: ${testResults.passedTests}/${testResults.totalTests} 通过`);
      
      if (testResults.failedTests > 0) {
        log('error', '❌ 失败的测试:');
        testResults.errors.forEach(error => {
          log('error', `  - ${error}`);
        });
      } else {
        log('info', '🎉 所有测试都通过了！');
      }
      
      // 清理测试环境
      if (instance && instance.isWindowOpen) {
        log('info', '🧹 清理测试环境，关闭浮窗...');
        instance.closeFloatingWindow();
      }
      
    } catch (error) {
      log('error', '💥 测试过程中发生错误:', error);
      testResults.errors.push(`测试异常: ${error.message}`);
    }
  }

  // 开始测试
  log('info', '⏰ 将在页面加载完成后开始测试...');
  
  if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', () => {
      setTimeout(runAllTests, 2000); // 给插件更多时间初始化
    });
  } else {
    setTimeout(runAllTests, 2000);
  }

  // 导出测试函数供手动调用
  window.runUrlChangeTests = runAllTests;
  window.testResults = testResults;
  
  log('info', '🔧 测试脚本已加载，可以手动调用 window.runUrlChangeTests() 来运行测试');

})();
