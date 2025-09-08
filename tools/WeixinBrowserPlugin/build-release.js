const fs = require('fs');
const path = require('path');
const archiver = require('archiver');
const terser = require('terser');

// 生产环境配置
const RELEASE_CONFIG = {
  // 源文件目录
  srcDir: 'src',
  // 输出目录
  outDir: 'release',
  // 调试模式（生产环境关闭）
  debug: {
    enabled: false,
    level: 'error',
    // 隐藏调试开关：在URL中包含此参数时启用调试模式
    trigger: 'senparc_debug_mode=true'
  },
  // 需要包含的文件（白名单模式）
  includeFiles: [
    'manifest.json',
    'content.js',
    'popup.html',
    'popup.js',
    'styles.css',
    'icon.svg'
  ],
  // 需要包含的目录
  includeDirs: [
    'icons'
  ],
  // 需要排除的文件模式（黑名单）
  excludePatterns: [
    /^test-.*\.js$/,           // 所有测试文件
    /^debug.*\.js$/,           // 所有调试文件
    /\.md$/,                   // 所有 Markdown 文档
    /^INSTALL\.md$/,
    /^PUBLISH\.md$/,
    /^README\.md$/,
    /^TROUBLESHOOTING\.md$/,
    /^USAGE\.md$/,
    /^test\.html$/,            // 测试HTML文件
    /^scripts\//,              // scripts 目录
    /^assets\//,               // assets 目录
    /\.DS_Store$/              // macOS 系统文件
  ],
  // 版本号
  version: '0.1.0'
};

class ChromeExtensionBuilder {
  constructor() {
    this.config = RELEASE_CONFIG;
    this.releaseDir = this.config.outDir;
  }

  // 清理输出目录
  cleanOutputDir() {
    if (fs.existsSync(this.releaseDir)) {
      fs.rmSync(this.releaseDir, { recursive: true, force: true });
    }
    fs.mkdirSync(this.releaseDir, { recursive: true });
    console.log('🧹 已清理输出目录');
  }

  // 检查文件是否应该被排除
  shouldExcludeFile(filename) {
    return this.config.excludePatterns.some(pattern => pattern.test(filename));
  }

  // 处理 JavaScript 文件（移除调试信息并压缩）
  async processJavaScript(filename, srcPath, destPath) {
    console.log(`📦 处理 JavaScript 文件: ${filename}`);
    
    let content = fs.readFileSync(srcPath, 'utf8');
    
    // 注入调试配置（隐藏模式）
    const debugConfig = `
// Senparc Debug Configuration (Hidden Mode)
(function() {
  const urlParams = new URLSearchParams(window.location.search);
  const hashParams = new URLSearchParams(window.location.hash.substring(1));
  
  window.__SENPARC_DEBUG__ = {
    enabled: ${this.config.debug.enabled} || 
             urlParams.has('senparc_debug_mode') || 
             hashParams.has('senparc_debug_mode') ||
             window.location.href.includes('${this.config.debug.trigger}'),
    level: '${this.config.debug.level}',
    trigger: '${this.config.debug.trigger}'
  };
})();
`;

    // 将调试配置添加到文件开头
    content = debugConfig + '\n' + content;
    
    // 包装所有 console 语句，只在调试模式下执行
    content = content.replace(
      /console\.(log|info|warn|error)\s*\((.*?)\);?/g, 
      (match, level, args) => {
        return `if (window.__SENPARC_DEBUG__ && window.__SENPARC_DEBUG__.enabled) { console.${level}(${args}); }`;
      }
    );

    // 移除单行注释中的调试信息
    content = content.replace(/\/\/.*(?:debug|test|todo|fixme|hack).*$/gmi, '');
    
    // 移除多行注释中的调试信息
    content = content.replace(/\/\*[\s\S]*?(?:debug|test|todo|fixme|hack)[\s\S]*?\*\//gi, '');

    // 生产环境压缩代码
    try {
      const minified = await terser.minify(content, {
        compress: {
          drop_console: !this.config.debug.enabled, // 生产环境移除 console
          drop_debugger: true,                      // 移除 debugger 语句
          dead_code: true,                          // 移除死代码
          unused: true                              // 移除未使用的变量
        },
        mangle: {
          toplevel: false,  // 不混淆顶级变量名
          reserved: ['WeixinAIAssistant'] // 保留主要类名
        },
        format: {
          comments: false,  // 移除注释
          beautify: false   // 不美化代码
        }
      });

      if (minified.error) {
        console.warn(`⚠️  压缩 ${filename} 时出现警告:`, minified.error);
        fs.writeFileSync(destPath, content);
      } else {
        fs.writeFileSync(destPath, minified.code);
      }
    } catch (error) {
      console.warn(`⚠️  压缩 ${filename} 失败，使用原始代码:`, error.message);
      fs.writeFileSync(destPath, content);
    }
  }

  // 处理 JSON 文件（清理和验证）
  processJSON(filename, srcPath, destPath) {
    console.log(`📄 处理 JSON 文件: ${filename}`);
    
    const content = fs.readFileSync(srcPath, 'utf8');
    const jsonData = JSON.parse(content);
    
    // 验证 manifest.json
    if (filename === 'manifest.json') {
      this.validateManifest(jsonData);
    }
    
    // 重新格式化 JSON（移除多余空格）
    fs.writeFileSync(destPath, JSON.stringify(jsonData, null, 2));
  }

  // 验证 manifest.json
  validateManifest(manifest) {
    const requiredFields = ['manifest_version', 'name', 'version', 'description'];
    const missingFields = requiredFields.filter(field => !manifest[field]);
    
    if (missingFields.length > 0) {
      throw new Error(`Manifest 缺少必需字段: ${missingFields.join(', ')}`);
    }

    // 检查版本号格式
    const versionRegex = /^\d+\.\d+\.\d+$/;
    if (!versionRegex.test(manifest.version)) {
      console.warn('⚠️  版本号格式建议使用 x.y.z 格式');
    }

    // 检查权限
    if (manifest.permissions && manifest.permissions.includes('*://*/*')) {
      console.warn('⚠️  检测到过于宽泛的权限，可能影响审核');
    }

    console.log('✅ Manifest 验证通过');
  }

  // 复制静态文件
  copyStaticFile(filename, srcPath, destPath) {
    console.log(`📋 复制静态文件: ${filename}`);
    fs.copyFileSync(srcPath, destPath);
  }

  // 递归复制目录
  copyDirectory(srcDir, destDir) {
    if (!fs.existsSync(destDir)) {
      fs.mkdirSync(destDir, { recursive: true });
    }

    const files = fs.readdirSync(srcDir);
    
    for (const file of files) {
      const srcPath = path.join(srcDir, file);
      const destPath = path.join(destDir, file);
      const stat = fs.statSync(srcPath);

      if (stat.isDirectory()) {
        this.copyDirectory(srcPath, destPath);
      } else if (!this.shouldExcludeFile(file)) {
        this.copyStaticFile(file, srcPath, destPath);
      }
    }
  }

  // 处理文件
  async processFiles() {
    console.log('📦 开始处理文件...');
    
    // 处理包含的文件
    for (const filename of this.config.includeFiles) {
      const srcPath = path.join(this.config.srcDir, filename);
      const destPath = path.join(this.releaseDir, filename);
      
      if (!fs.existsSync(srcPath)) {
        console.warn(`⚠️  文件不存在: ${srcPath}`);
        continue;
      }

      const ext = path.extname(filename).toLowerCase();
      
      if (ext === '.js') {
        await this.processJavaScript(filename, srcPath, destPath);
      } else if (ext === '.json') {
        this.processJSON(filename, srcPath, destPath);
      } else {
        this.copyStaticFile(filename, srcPath, destPath);
      }
    }

    // 处理包含的目录
    for (const dirname of this.config.includeDirs) {
      const srcDir = path.join(this.config.srcDir, dirname);
      const destDir = path.join(this.releaseDir, dirname);
      
      if (fs.existsSync(srcDir)) {
        console.log(`📁 处理目录: ${dirname}`);
        this.copyDirectory(srcDir, destDir);
      }
    }
  }

  // 创建发布包
  async createReleasePackage() {
    console.log('🗜️  创建发布包...');
    
    const packageName = `senparc-weixin-ai-v${this.config.version}-release.zip`;
    const packagePath = path.join(this.releaseDir, packageName);
    
    return new Promise((resolve, reject) => {
      const output = fs.createWriteStream(packagePath);
      const archive = archiver('zip', {
        zlib: { level: 9 } // 最高压缩级别
      });

      output.on('close', () => {
        const sizeInMB = (archive.pointer() / 1024 / 1024).toFixed(2);
        console.log(`✅ 发布包创建完成: ${packageName}`);
        console.log(`📦 包大小: ${sizeInMB} MB`);
        resolve(packagePath);
      });

      archive.on('error', (err) => {
        reject(err);
      });

      archive.pipe(output);

      // 添加所有文件到压缩包
      const files = fs.readdirSync(this.releaseDir);
      for (const file of files) {
        const filePath = path.join(this.releaseDir, file);
        const stat = fs.statSync(filePath);
        
        if (stat.isFile() && !file.endsWith('.zip')) {
          archive.file(filePath, { name: file });
        } else if (stat.isDirectory()) {
          archive.directory(filePath, file);
        }
      }

      archive.finalize();
    });
  }

  // 验证发布包
  validateReleasePackage() {
    console.log('🔍 验证发布包...');
    
    const requiredFiles = ['manifest.json', 'content.js'];
    const missingFiles = [];
    
    for (const file of requiredFiles) {
      const filePath = path.join(this.releaseDir, file);
      if (!fs.existsSync(filePath)) {
        missingFiles.push(file);
      }
    }
    
    if (missingFiles.length > 0) {
      throw new Error(`发布包缺少必需文件: ${missingFiles.join(', ')}`);
    }
    
    // 检查文件大小
    const manifestPath = path.join(this.releaseDir, 'manifest.json');
    const manifestSize = fs.statSync(manifestPath).size;
    
    if (manifestSize > 10240) { // 10KB
      console.warn('⚠️  manifest.json 文件过大，可能包含不必要的内容');
    }
    
    console.log('✅ 发布包验证通过');
  }

  // 生成发布报告
  generateReleaseReport() {
    console.log('\n📊 发布报告');
    console.log('='.repeat(50));
    
    const files = fs.readdirSync(this.releaseDir);
    let totalSize = 0;
    
    console.log('📁 包含的文件:');
    files.forEach(file => {
      const filePath = path.join(this.releaseDir, file);
      const stat = fs.statSync(filePath);
      
      if (stat.isFile()) {
        const sizeKB = (stat.size / 1024).toFixed(2);
        totalSize += stat.size;
        console.log(`   ${file} (${sizeKB} KB)`);
      }
    });
    
    const totalSizeKB = (totalSize / 1024).toFixed(2);
    console.log(`\n📦 总大小: ${totalSizeKB} KB`);
    console.log(`🎯 版本: ${this.config.version}`);
    console.log(`🔧 调试模式: ${this.config.debug.enabled ? '启用' : '禁用'}`);
    console.log(`🔑 调试触发器: ${this.config.debug.trigger}`);
    
    console.log('\n✅ 准备发布到 Chrome Web Store!');
    console.log('\n📋 发布清单:');
    console.log('   1. 上传 release/ 目录中的 .zip 文件');
    console.log('   2. 填写商店描述和截图');
    console.log('   3. 设置隐私政策（如需要）');
    console.log('   4. 提交审核');
  }

  // 主构建流程
  async build() {
    console.log('🚀 开始构建 Chrome Web Store 发布包...\n');
    
    try {
      // 1. 清理输出目录
      this.cleanOutputDir();
      
      // 2. 处理文件
      await this.processFiles();
      
      // 3. 验证发布包
      this.validateReleasePackage();
      
      // 4. 创建压缩包
      await this.createReleasePackage();
      
      // 5. 生成报告
      this.generateReleaseReport();
      
    } catch (error) {
      console.error('\n❌ 构建失败:', error.message);
      process.exit(1);
    }
  }
}

// 执行构建
const builder = new ChromeExtensionBuilder();
builder.build();
