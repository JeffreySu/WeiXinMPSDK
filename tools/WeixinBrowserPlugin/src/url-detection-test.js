// URL检测性能测试脚本
// 用于验证不同URL检测方案的性能差异

class UrlDetectionTester {
  constructor() {
    this.testResults = {
      historyApi: { calls: 0, time: 0 },
      mutationObserver: { calls: 0, time: 0 }
    };
  }

  // 测试 History API 方案
  testHistoryApiPerformance(iterations = 1000) {
    console.log('🧪 开始测试 History API 方案性能...');
    
    const startTime = performance.now();
    
    // 模拟URL变化
    for (let i = 0; i < iterations; i++) {
      // 模拟 pushState 调用
      const mockUrl = `https://developers.weixin.qq.com/doc/test-${i}`;
      history.pushState({}, '', mockUrl);
    }
    
    const endTime = performance.now();
    this.testResults.historyApi.time = endTime - startTime;
    this.testResults.historyApi.calls = iterations;
    
    console.log(`✅ History API 测试完成: ${iterations} 次调用，耗时 ${this.testResults.historyApi.time.toFixed(2)}ms`);
  }

  // 测试 MutationObserver 方案（模拟）
  testMutationObserverPerformance(iterations = 1000) {
    console.log('🧪 开始测试 MutationObserver 方案性能...');
    
    const startTime = performance.now();
    
    // 模拟DOM变化
    for (let i = 0; i < iterations; i++) {
      const div = document.createElement('div');
      div.textContent = `Test element ${i}`;
      document.body.appendChild(div);
      
      // 立即移除，避免DOM污染
      document.body.removeChild(div);
    }
    
    const endTime = performance.now();
    this.testResults.mutationObserver.time = endTime - startTime;
    this.testResults.mutationObserver.calls = iterations;
    
    console.log(`✅ MutationObserver 测试完成: ${iterations} 次DOM变化，耗时 ${this.testResults.mutationObserver.time.toFixed(2)}ms`);
  }

  // 运行完整测试套件
  runFullTest(iterations = 1000) {
    console.log('🚀 开始URL检测性能对比测试...');
    console.log(`📊 测试规模: ${iterations} 次迭代`);
    
    this.testHistoryApiPerformance(iterations);
    this.testMutationObserverPerformance(iterations);
    
    this.generateReport();
  }

  // 生成性能报告
  generateReport() {
    console.log('\n📈 性能测试报告');
    console.log('=' .repeat(50));
    
    const historyTime = this.testResults.historyApi.time;
    const mutationTime = this.testResults.mutationObserver.time;
    
    console.log(`History API 方案:`);
    console.log(`  - 调用次数: ${this.testResults.historyApi.calls}`);
    console.log(`  - 总耗时: ${historyTime.toFixed(2)}ms`);
    console.log(`  - 平均耗时: ${(historyTime / this.testResults.historyApi.calls).toFixed(4)}ms/次`);
    
    console.log(`\nMutationObserver 方案:`);
    console.log(`  - DOM变化次数: ${this.testResults.mutationObserver.calls}`);
    console.log(`  - 总耗时: ${mutationTime.toFixed(2)}ms`);
    console.log(`  - 平均耗时: ${(mutationTime / this.testResults.mutationObserver.calls).toFixed(4)}ms/次`);
    
    const performanceRatio = mutationTime / historyTime;
    console.log(`\n🏆 性能对比:`);
    if (performanceRatio > 1) {
      console.log(`  History API 比 MutationObserver 快 ${performanceRatio.toFixed(2)}x`);
    } else {
      console.log(`  MutationObserver 比 History API 快 ${(1/performanceRatio).toFixed(2)}x`);
    }
    
    console.log('\n💡 建议:');
    if (historyTime < mutationTime) {
      console.log('  推荐使用 History API 方案，性能更优');
    } else {
      console.log('  推荐使用 MutationObserver 方案，性能更优');
    }
    
    console.log('=' .repeat(50));
  }

  // 实时性能监控
  startRealTimeMonitoring() {
    console.log('📊 开始实时性能监控...');
    
    setInterval(() => {
      if (window.UrlDetectionPerformanceMonitor) {
        const stats = window.UrlDetectionPerformanceMonitor.getStats();
        console.log('📈 实时统计:', {
          'History API调用': stats.historyApiCalls,
          'MutationObserver调用': stats.mutationObserverCalls,
          'URL变化次数': stats.urlChanges,
          '上次变化时间': new Date(stats.lastUrlChangeTime).toLocaleTimeString()
        });
      }
    }, 10000); // 每10秒输出一次
  }
}

// 导出测试器
window.UrlDetectionTester = UrlDetectionTester;

// 自动运行测试（如果在开发环境）
if (window.location.hostname === 'localhost' || window.location.hostname.includes('127.0.0.1')) {
  console.log('🔧 检测到开发环境，自动运行性能测试...');
  const tester = new UrlDetectionTester();
  
  // 延迟运行，确保页面加载完成
  setTimeout(() => {
    tester.runFullTest(100); // 减少测试规模避免影响开发
    tester.startRealTimeMonitoring();
  }, 2000);
}

console.log('✅ URL检测性能测试脚本已加载');
console.log('💡 使用方法: const tester = new UrlDetectionTester(); tester.runFullTest();');
