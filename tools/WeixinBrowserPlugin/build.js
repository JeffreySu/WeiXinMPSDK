const fs = require('fs');
const path = require('path');
const terser = require('terser');
const config = require('./build.config.js');

// 确保输出目录存在
if (!fs.existsSync(config.build.outDir)) {
  fs.mkdirSync(config.build.outDir, { recursive: true });
}

// 处理JavaScript文件
async function processJavaScript(filename) {
  const content = fs.readFileSync(path.join(config.build.srcDir, filename), 'utf8');
  
  // 注入调试配置
  const debugConfig = `
    window.__SENPARC_DEBUG__ = {
      enabled: ${config.debug.enabled},
      level: '${config.debug.level}',
      trigger: '${config.debug.trigger}'
    };
  `;
  
  // 替换console语句 - 使用更安全的方式
  let debugWrappedContent = content;
  if (config.debug.enabled) {
    debugWrappedContent = debugConfig + '\n' + content;
  } else {
    // 在生产模式下，简单地添加调试配置但不修改console语句
    debugWrappedContent = debugConfig + '\n' + content;
  }
  
  // 压缩代码
  const minified = await terser.minify(debugWrappedContent, {
    compress: {
      drop_console: !config.debug.enabled,
      drop_debugger: true
    },
    mangle: true,
    format: {
      comments: false
    }
  });

  fs.writeFileSync(
    path.join(config.build.outDir, filename),
    minified.code
  );
}

// 复制静态文件
function copyStaticFiles() {
  config.build.staticFiles.forEach(file => {
    const srcPath = path.join(config.build.srcDir, file);
    const destPath = path.join(config.build.outDir, file);
    
    // 确保目标目录存在
    const destDir = path.dirname(destPath);
    if (!fs.existsSync(destDir)) {
      fs.mkdirSync(destDir, { recursive: true });
    }
    
    const content = fs.readFileSync(srcPath);
    fs.writeFileSync(destPath, content);
  });
}

// 创建zip文件
async function createZip() {
  const archiver = require('archiver');
  const output = fs.createWriteStream(
    path.join(config.build.outDir, `senparc-weixin-ai-${config.build.version}.zip`)
  );
  const archive = archiver('zip', { zlib: { level: 9 } });
  
  archive.pipe(output);
  
  // 添加所有文件到zip
  fs.readdirSync(config.build.outDir).forEach(file => {
    if (file.endsWith('.zip')) return;
    archive.file(path.join(config.build.outDir, file), { name: file });
  });
  
  await archive.finalize();
}

// 主构建流程
async function build() {
  console.log('🚀 开始构建插件...');
  
  try {
    // 处理JavaScript文件
    console.log('📦 处理JavaScript文件...');
    for (const file of config.build.jsFiles) {
      await processJavaScript(file);
    }
    
    // 复制静态文件
    console.log('📄 复制静态文件...');
    copyStaticFiles();
    
    // 创建zip包
    console.log('🗜️ 创建发布包...');
    await createZip();
    
    console.log('✅ 构建完成！');
    console.log(`📦 发布包已生成: dist/senparc-weixin-ai-${config.build.version}.zip`);
  } catch (error) {
    console.error('❌ 构建失败:', error);
    process.exit(1);
  }
}

// 执行构建
build();

