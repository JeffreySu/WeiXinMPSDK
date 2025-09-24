module.exports = {
  // 发布模式（production/development）
  mode: 'production',
  
  // 调试开关配置
  debug: {
    // 是否启用调试日志（开发模式）
    enabled: false,
    // 调试日志级别 (verbose, info, warn, error)
    level: 'error',
    // 开发者模式触发关键字（在URL中包含此字符串时启用调试）
    trigger: 'senparc-debug'
  },

  // 生产环境配置（Chrome Web Store 发布）
  production: {
    // 完全禁用调试信息
    debug: {
      enabled: false,
      level: 'error',
      // 隐藏调试开关：需要特殊参数才能启用
      trigger: 'senparc_debug_mode=true'
    },
    // 代码压缩选项
    minify: {
      removeConsole: true,
      removeDebugger: true,
      removeComments: true
    },
    // 文件过滤（排除测试和调试文件）
    excludePatterns: [
      'test-*.js',
      'debug*.js',
      '*.md',
      'test.html',
      'scripts/',
      'assets/'
    ]
  },

  // 构建配置
  build: {
    // 源文件目录
    srcDir: 'src',
    // 输出目录
    outDir: 'dist',
    // 需要复制的静态文件
    staticFiles: ['manifest.json', 'styles.css', 'popup.html', 'popup.js', 'icons/icon16.png', 'icons/icon48.png', 'icons/icon128.png'],
    // 需要压缩的JS文件
    jsFiles: ['content.js'],
    // 版本号（与manifest.json保持一致）
    version: '0.1.1'
  }
};
