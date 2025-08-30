// Senparc.Weixin.AI Chrome Extension Content Script
// 监控微信文档页面并添加AI助手功能

class WeixinAIAssistant {
  constructor() {
    this.logoButton = null;
    this.floatingWindow = null;
    this.isWindowOpen = false;
    this.isDocked = false;
    this.originalBodyStyle = '';
    this.init();
  }

  // 初始化插件
  init() {
    console.log('Senparc.Weixin.AI 插件开始初始化...');
    console.log('当前页面URL:', window.location.href);
    console.log('当前域名:', window.location.hostname);
    
    // 检查是否是微信文档页面
    if (this.isWeixinDocPage()) {
      console.log('✅ 检测到微信文档页面，初始化AI助手...');
      this.createLogoButton();
      this.setupEventListeners();
    } else {
      console.log('❌ 当前页面不在支持列表中');
      console.log('仅支持以下页面:');
      console.log('  - https://developers.weixin.qq.com/');
      console.log('  - https://pay.weixin.qq.com/doc');
    }
  }

  // 检查是否是微信文档页面
  isWeixinDocPage() {
    const url = window.location.href;
    const hostname = window.location.hostname;
    
    // 只允许以下两个特定地址
    const allowedUrls = [
      'developers.weixin.qq.com',
      'pay.weixin.qq.com'
    ];
    
    // 检查域名匹配
    const isAllowedDomain = allowedUrls.some(domain => hostname === domain);
    
    // 对于pay.weixin.qq.com，额外检查必须是/doc路径
    if (hostname === 'pay.weixin.qq.com') {
      return url.includes('/doc');
    }
    
    return isAllowedDomain;
  }

  // 创建Logo按钮
  createLogoButton() {
    console.log('🎨 开始创建Logo按钮...');
    
    // 检查是否已经存在按钮，避免重复创建
    const existingButton = document.getElementById('senparc-weixin-ai-button');
    if (existingButton) {
      console.log('⚠️ Logo按钮已存在，移除旧按钮');
      existingButton.remove();
    }
    
    // 创建按钮容器
    this.logoButton = document.createElement('div');
    this.logoButton.id = 'senparc-weixin-ai-button';
    this.logoButton.className = 'senparc-ai-logo-button';
    
    // 设置按钮内容
    this.logoButton.innerHTML = `
      <div class="logo-content">
        <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path d="M12 2L2 7L12 12L22 7L12 2Z" stroke="currentColor" stroke-width="2" stroke-linejoin="round"/>
          <path d="M2 17L12 22L22 17" stroke="currentColor" stroke-width="2" stroke-linejoin="round"/>
          <path d="M2 12L12 17L22 12" stroke="currentColor" stroke-width="2" stroke-linejoin="round"/>
        </svg>
        <span class="logo-text">Senparc.Weixin.AI</span>
      </div>
    `;

    // 添加到页面
    document.body.appendChild(this.logoButton);
    console.log('✅ Logo按钮已添加到页面');
  }

  // 设置事件监听器
  setupEventListeners() {
    if (this.logoButton) {
      this.logoButton.addEventListener('click', () => {
        this.toggleFloatingWindow();
      });
    }

    // 监听ESC键关闭浮窗
    document.addEventListener('keydown', (e) => {
      if (e.key === 'Escape' && this.isWindowOpen) {
        this.closeFloatingWindow();
      }
    });

    // 监听点击外部区域关闭浮窗
    document.addEventListener('click', (e) => {
      if (this.isWindowOpen && this.floatingWindow && !this.floatingWindow.contains(e.target) && !this.logoButton.contains(e.target)) {
        this.closeFloatingWindow();
      }
    });
  }

  // 切换浮窗显示状态
  toggleFloatingWindow() {
    if (this.isWindowOpen) {
      this.closeFloatingWindow();
    } else {
      this.openFloatingWindow();
    }
  }

  // 打开浮窗
  openFloatingWindow() {
    console.log('🚀 打开AI助手浮窗...');
    
    if (this.floatingWindow) {
      this.floatingWindow.style.display = 'block';
      this.isWindowOpen = true;
      
      // 确保iframe正确显示
      const iframe = this.floatingWindow.querySelector('#senparc-ai-iframe');
      const loadingIndicator = this.floatingWindow.querySelector('.loading-indicator');
      
      if (iframe) {
        // 重置iframe样式
        iframe.style.cssText = `
          width: 100% !important;
          height: 100% !important;
          border: none;
          display: block !important;
          min-height: 500px;
          position: absolute;
          top: 0;
          left: 0;
          right: 0;
          bottom: 0;
        `;
        
        // 强制确保iframe完全填充
        const parentContent = iframe.parentElement;
        if (parentContent) {
          const parentHeight = parentContent.offsetHeight;
          const parentWidth = parentContent.offsetWidth;
          console.log('🔧 父容器尺寸:', parentWidth, 'x', parentHeight);
          
          // 直接设置具体的像素值
          iframe.style.width = parentWidth + 'px';
          iframe.style.height = parentHeight + 'px';
        }
        
        // 确保iframe内容重新布局
        iframe.style.opacity = '0';
        setTimeout(() => {
          iframe.style.opacity = '1';
        }, 50);
        
        console.log('🔧 重置iframe样式');
      }
      
      if (loadingIndicator) {
        loadingIndicator.style.display = 'none';
                console.log('🔧 隐藏加载指示器');
      }
      
      // 重新绑定按钮事件（重要！）
      setTimeout(() => {
        this.setupButtonEvents();
      }, 100);
        
      // 重新添加显示动画
      setTimeout(() => {
        if (this.floatingWindow) {
          this.floatingWindow.classList.add('show');
          
          // 动画完成后重新计算iframe尺寸
          setTimeout(() => {
            this.recalculateIframeSize();
          }, 350); // 等待动画完成
        }
      }, 10);
      console.log('♻️ 重新显示已存在的浮窗');
      return;
    }

    // 创建浮窗容器
    this.floatingWindow = document.createElement('div');
    this.floatingWindow.id = 'senparc-weixin-ai-window';
    this.floatingWindow.className = 'senparc-ai-floating-window';

    // 获取当前页面URL作为query参数
    const currentUrl = encodeURIComponent(window.location.href);
    
    // 创建浮窗内容
    this.floatingWindow.innerHTML = `
      <div class="floating-window-header">
        <div class="window-title">
          <svg width="20" height="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M12 2L2 7L12 12L22 7L12 2Z" stroke="currentColor" stroke-width="2" stroke-linejoin="round"/>
            <path d="M2 17L12 22L22 17" stroke="currentColor" stroke-width="2" stroke-linejoin="round"/>
            <path d="M2 12L12 17L22 12" stroke="currentColor" stroke-width="2" stroke-linejoin="round"/>
          </svg>
          Senparc.Weixin.AI 助手
        </div>
        <div class="window-controls">
          <button class="dock-button" id="dock-toggle-button" title="停靠/悬浮切换">
            <svg width="16" height="16" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
              <rect x="3" y="3" width="18" height="18" rx="2" ry="2" stroke="currentColor" stroke-width="2"/>
              <path d="M9 3v18" stroke="currentColor" stroke-width="2"/>
            </svg>
          </button>
          <button class="close-button" id="close-floating-window">
            <svg width="16" height="16" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M18 6L6 18M6 6L18 18" stroke="currentColor" stroke-width="2" stroke-linecap="round"/>
            </svg>
          </button>
        </div>
      </div>
      <div class="floating-window-content">
        <div class="loading-indicator">
          <div class="spinner"></div>
          <p>正在加载AI助手...</p>
        </div>
        <iframe 
          id="senparc-ai-iframe" 
          src="https://sdk.weixin.senparc.com/AiDoc?query=${currentUrl}" 
          frameborder="0"
          allow="clipboard-read; clipboard-write"
          sandbox="allow-same-origin allow-scripts allow-forms allow-popups allow-top-navigation-by-user-activation">
        </iframe>
      </div>
      <div class="floating-window-footer">
        <span class="footer-text">Powered by Senparc.Weixin SDK</span>
      </div>
    `;

    // 添加到页面
    document.body.appendChild(this.floatingWindow);

    // 初始设置为悬浮模式
    this.floatingWindow.classList.add('floating-mode');

    // 确保DOM完全创建后再设置按钮事件
    setTimeout(() => {
      this.setupButtonEvents();
    }, 200);

    // 添加窗口大小变化监听器
    const resizeObserver = new ResizeObserver(() => {
      console.log('📐 检测到浮窗尺寸变化，重新计算iframe');
      this.recalculateIframeSize();
    });
    
    const contentArea = this.floatingWindow.querySelector('.floating-window-content');
    if (contentArea) {
      resizeObserver.observe(contentArea);
    }

    // 监听iframe加载完成
    const iframe = this.floatingWindow.querySelector('#senparc-ai-iframe');
    iframe.addEventListener('load', () => {
      const loadingIndicator = this.floatingWindow.querySelector('.loading-indicator');
      if (loadingIndicator) {
        loadingIndicator.style.display = 'none';
      }
      iframe.style.display = 'block';
      
      // 加载完成后重新计算尺寸
      setTimeout(() => {
        this.recalculateIframeSize();
      }, 100);
      
      console.log('🎯 iframe加载完成，尺寸已重新计算');
    });

    // 监听iframe加载错误
    iframe.addEventListener('error', () => {
      const loadingIndicator = this.floatingWindow.querySelector('.loading-indicator');
      if (loadingIndicator) {
        loadingIndicator.innerHTML = `
          <div class="error-message">
            <svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
              <circle cx="12" cy="12" r="10" stroke="currentColor" stroke-width="2"/>
              <path d="M15 9L9 15M9 9L15 15" stroke="currentColor" stroke-width="2" stroke-linecap="round"/>
            </svg>
            <p>加载失败，请检查网络连接</p>
            <button onclick="location.reload()" class="retry-button">重试</button>
          </div>
        `;
      }
    });

    this.isWindowOpen = true;

    // 添加动画效果
    setTimeout(() => {
      this.floatingWindow.classList.add('show');
    }, 10);
  }

  // 设置按钮事件
  setupButtonEvents() {
    if (!this.floatingWindow) {
      console.error('❌ 浮窗不存在，无法设置按钮事件');
      return;
    }

    console.log('🔗 设置浮窗按钮事件...');

    // 使用更可靠的方式等待DOM元素完全创建
    setTimeout(() => {
      this.bindButtonEvents();
    }, 100);
  }

  // 绑定按钮事件的具体实现
  bindButtonEvents() {
    if (!this.floatingWindow) {
      console.error('❌ 浮窗不存在，无法绑定按钮事件');
      return;
    }

    // 设置关闭按钮事件
    const closeButton = this.floatingWindow.querySelector('#close-floating-window');
    if (closeButton) {
      console.log('🔍 找到关闭按钮，开始绑定事件...');
      
      // 移除所有现有的事件监听器（如果有的话）
      closeButton.onclick = null;
      closeButton.onmousedown = null;
      closeButton.ontouchstart = null;
      
      // 使用简单直接的事件绑定
      closeButton.onclick = (e) => {
        console.log('🖱️ 关闭按钮被点击');
        e.preventDefault();
        e.stopPropagation();
        this.closeFloatingWindow();
        return false;
      };

      // 确保按钮样式正确
      closeButton.style.pointerEvents = 'auto';
      closeButton.style.cursor = 'pointer';
      closeButton.style.zIndex = '10002';
      closeButton.style.position = 'relative';
      
      console.log('✅ 关闭按钮事件已绑定');
    } else {
      console.error('❌ 找不到关闭按钮元素');
      // 尝试重新查找
      setTimeout(() => {
        console.log('🔄 重试查找关闭按钮...');
        this.bindButtonEvents();
      }, 500);
    }

    // 设置停靠按钮事件
    const dockButton = this.floatingWindow.querySelector('#dock-toggle-button');
    if (dockButton) {
      console.log('🔍 找到停靠按钮，开始绑定事件...');
      
      // 移除所有现有的事件监听器（如果有的话）
      dockButton.onclick = null;
      dockButton.ontouchstart = null;
      
      // 使用简单直接的事件绑定
      dockButton.onclick = (e) => {
        console.log('🖱️ 停靠按钮被点击');
        e.preventDefault();
        e.stopPropagation();
        this.toggleDockMode();
        return false;
      };

      // 确保按钮样式正确
      dockButton.style.pointerEvents = 'auto';
      dockButton.style.cursor = 'pointer';
      dockButton.style.zIndex = '10002';
      dockButton.style.position = 'relative';
      
      console.log('✅ 停靠按钮事件已绑定');
    } else {
      console.error('❌ 找不到停靠按钮元素');
    }
  }

  // 切换停靠模式
  toggleDockMode() {
    if (this.isDocked) {
      this.setFloatingMode();
    } else {
      this.setDockMode();
    }
  }

  // 设置停靠模式
  setDockMode() {
    console.log('🔗 切换到停靠模式...');
    
    if (!this.floatingWindow) return;
    
    // 保存原始页面样式
    this.originalBodyStyle = document.body.className;
    
    // 首先移除悬浮模式的动画类，避免样式冲突
    this.floatingWindow.classList.remove('show', 'floating-mode');
    
    // 强制清除悬浮模式的内联样式
    this.floatingWindow.style.transform = 'none !important';
    this.floatingWindow.style.left = 'auto !important';
    this.floatingWindow.style.top = '0 !important';
    this.floatingWindow.style.right = '0 !important';
    this.floatingWindow.style.width = '40% !important';
    this.floatingWindow.style.height = '100vh !important';
    this.floatingWindow.style.maxWidth = 'none !important';
    this.floatingWindow.style.maxHeight = 'none !important';
    this.floatingWindow.style.minHeight = '100vh !important';
    this.floatingWindow.style.borderRadius = '0 !important';
    this.floatingWindow.style.position = 'fixed !important';
    
    // 确保浮窗处于显示状态
    this.floatingWindow.style.display = 'flex !important';
    this.floatingWindow.style.opacity = '1 !important';
    this.floatingWindow.style.visibility = 'visible !important';
    this.floatingWindow.style.zIndex = '10000 !important';
    
    // 设置停靠模式的class
    this.floatingWindow.classList.add('docked-mode');
    
    // 使用requestAnimationFrame确保样式生效后再添加show类
    requestAnimationFrame(() => {
      this.floatingWindow.classList.add('show');
    });
    
    // 给body添加停靠模式的class
    document.body.classList.add('senparc-docked');
    
    // 强制应用停靠样式（作为CSS的补充）
    document.body.style.marginRight = '40%';
    document.body.style.transition = 'margin-right 0.3s cubic-bezier(0.4, 0, 0.2, 1)';
    document.body.style.boxSizing = 'border-box';
    document.body.style.overflowX = 'hidden';
    
    console.log('🎯 强制应用停靠样式作为补充');
    
    // 针对微信开发者文档的特殊处理
    this.applyWeixinDocsSpecificStyles();
    
    // 更新停靠按钮图标
    this.updateDockButtonIcon(true);
    
    this.isDocked = true;
    this.isWindowOpen = true; // 确保状态一致
    console.log('✅ 已切换到停靠模式');
    
    // 重新计算iframe尺寸
    setTimeout(() => {
      this.recalculateIframeSize();
    }, 100);
  }

  // 设置悬浮模式
  setFloatingMode() {
    console.log('🌊 切换到悬浮模式...');
    
    if (!this.floatingWindow) return;
    
    // 首先确保窗口可见，避免在切换过程中消失
    this.floatingWindow.style.display = 'flex !important';
    this.floatingWindow.style.opacity = '1 !important';
    this.floatingWindow.style.visibility = 'visible !important';
    
    // 移除停靠模式的class，但保留show类避免窗口消失
    this.floatingWindow.classList.remove('docked-mode');
    
    // 清除所有停靠模式的内联样式
    this.floatingWindow.style.transform = '';
    this.floatingWindow.style.left = '';
    this.floatingWindow.style.top = '';
    this.floatingWindow.style.right = '';
    this.floatingWindow.style.width = '';
    this.floatingWindow.style.height = '';
    this.floatingWindow.style.maxWidth = '';
    this.floatingWindow.style.maxHeight = '';
    this.floatingWindow.style.minHeight = '';
    this.floatingWindow.style.borderRadius = '';
    this.floatingWindow.style.position = '';
    this.floatingWindow.style.margin = '';
    this.floatingWindow.style.padding = '';
    this.floatingWindow.style.zIndex = '';
    
    // 设置悬浮模式样式
    this.floatingWindow.classList.add('floating-mode');
    
    // 确保show类存在，如果不存在则添加
    if (!this.floatingWindow.classList.contains('show')) {
      this.floatingWindow.classList.add('show');
    }
    
    // 使用双重确保机制
    requestAnimationFrame(() => {
      if (this.floatingWindow) {
        this.floatingWindow.classList.add('show');
        this.floatingWindow.style.display = 'flex';
        this.floatingWindow.style.opacity = '1';
        this.floatingWindow.style.visibility = 'visible';
      }
    });
    
    // 移除body的停靠模式class
    document.body.classList.remove('senparc-docked');
    
    // 清除强制应用的内联样式
    document.body.style.marginRight = '';
    document.body.style.transition = '';
    document.body.style.boxSizing = '';
    document.body.style.overflowX = '';
    
    console.log('🎯 清除强制停靠样式');
    
    // 清除微信文档特殊样式
    this.clearWeixinDocsSpecificStyles();
    
    // 更新停靠按钮图标
    this.updateDockButtonIcon(false);
    
    this.isDocked = false;
    this.isWindowOpen = true; // 确保状态一致
    console.log('✅ 已切换到悬浮模式');
    
    // 重新计算iframe尺寸
    setTimeout(() => {
      this.recalculateIframeSize();
    }, 100);
  }



  // 针对微信开发者文档的特殊样式处理
  applyWeixinDocsSpecificStyles() {
    console.log('📱 应用微信开发者文档特殊样式...');
    
    // 检查并处理常见的微信文档容器
    const weixinSelectors = [
      '#app',
      '.page-container',
      '.doc-container', 
      '.main-container',
      '.main-content',
      '.doc-content',
      '.content-wrapper',
      '[class*="layout"]',
      '[class*="page-"]',
      '[class*="container"]'
    ];
    
    weixinSelectors.forEach(selector => {
      const elements = document.querySelectorAll(selector);
      elements.forEach(element => {
        // 跳过我们的插件元素
        if (element.id === 'senparc-weixin-ai-window' || element.id === 'senparc-weixin-ai-button') {
          return;
        }
        
        element.style.setProperty('margin-right', '0', 'important');
        element.style.setProperty('width', '100%', 'important');
        element.style.setProperty('max-width', '100%', 'important');
        element.style.setProperty('box-sizing', 'border-box', 'important');
      });
    });
    
    // 特别处理可能的固定定位元素
    const fixedElements = document.querySelectorAll('[style*="position: fixed"], [style*="position:fixed"]');
    fixedElements.forEach(element => {
      if (element.id !== 'senparc-weixin-ai-window' && element.id !== 'senparc-weixin-ai-button') {
        const rect = element.getBoundingClientRect();
        if (rect.right > window.innerWidth * 0.6) {
          element.style.setProperty('right', '40%', 'important');
        }
      }
    });
    
    console.log('✅ 微信开发者文档特殊样式处理完成');
  }
  
  // 清除微信文档特殊样式
  clearWeixinDocsSpecificStyles() {
    console.log('🧹 清除微信开发者文档特殊样式...');
    
    const weixinSelectors = [
      '#app',
      '.page-container',
      '.doc-container', 
      '.main-container',
      '.main-content',
      '.doc-content',
      '.content-wrapper',
      '[class*="layout"]',
      '[class*="page-"]',
      '[class*="container"]'
    ];
    
    weixinSelectors.forEach(selector => {
      const elements = document.querySelectorAll(selector);
      elements.forEach(element => {
        if (element.id === 'senparc-weixin-ai-window' || element.id === 'senparc-weixin-ai-button') {
          return;
        }
        
        element.style.marginRight = '';
        element.style.width = '';
        element.style.maxWidth = '';
        element.style.boxSizing = '';
      });
    });
    
    console.log('✅ 微信开发者文档特殊样式清除完成');
  }

  // 更新停靠按钮图标
  updateDockButtonIcon(isDocked) {
    const dockButton = this.floatingWindow?.querySelector('#dock-toggle-button');
    if (!dockButton) return;
    
    if (isDocked) {
      // 悬浮图标
      dockButton.innerHTML = `
        <svg width="16" height="16" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
          <rect x="5" y="5" width="14" height="14" rx="2" ry="2" stroke="currentColor" stroke-width="2"/>
          <path d="M9 1v4M15 1v4M9 19v4M15 19v4M1 9h4M1 15h4M19 9h4M19 15h4" stroke="currentColor" stroke-width="2"/>
        </svg>
      `;
      dockButton.title = '切换到悬浮模式';
    } else {
      // 停靠图标
      dockButton.innerHTML = `
        <svg width="16" height="16" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
          <rect x="3" y="3" width="18" height="18" rx="2" ry="2" stroke="currentColor" stroke-width="2"/>
          <path d="M9 3v18" stroke="currentColor" stroke-width="2"/>
        </svg>
      `;
      dockButton.title = '切换到停靠模式';
    }
  }

  // 重新计算iframe尺寸
  recalculateIframeSize() {
    console.log('📐 重新计算iframe尺寸...');
    
    if (!this.floatingWindow) return;
    
    const iframe = this.floatingWindow.querySelector('#senparc-ai-iframe');
    const contentArea = this.floatingWindow.querySelector('.floating-window-content');
    
    if (iframe && contentArea) {
      // 强制重新布局
      contentArea.style.display = 'none';
      contentArea.offsetHeight; // 触发重流
      contentArea.style.display = 'flex';
      
      // 获取实际尺寸
      const rect = contentArea.getBoundingClientRect();
      console.log('📏 内容区域尺寸:', rect.width, 'x', rect.height);
      
      // 设置iframe尺寸
      iframe.style.width = rect.width + 'px';
      iframe.style.height = rect.height + 'px';
      
      // 确保iframe可见
      iframe.style.display = 'block';
      iframe.style.visibility = 'visible';
      
      console.log('✅ iframe尺寸重新计算完成');
    }
  }

  // 关闭浮窗
  closeFloatingWindow() {
    console.log('🚪 开始关闭浮窗...');
    console.log('🔍 当前状态:', {
      isWindowOpen: this.isWindowOpen,
      isDocked: this.isDocked,
      floatingWindowExists: !!this.floatingWindow
    });
    
    if (this.floatingWindow) {
      // 如果当前处于停靠模式，先恢复页面布局
      if (this.isDocked) {
        console.log('🔄 从停靠模式关闭，恢复页面布局...');
        document.body.classList.remove('senparc-docked');
        document.body.style.marginRight = '';
        document.body.style.transition = '';
        document.body.style.boxSizing = '';
        document.body.style.overflowX = '';
        this.clearWeixinDocsSpecificStyles();
        this.isDocked = false;
      }
      
      // 移除显示类开始关闭动画
      this.floatingWindow.classList.remove('show');
      console.log('🎬 开始关闭动画');
      
      // 延迟隐藏窗口
      setTimeout(() => {
        if (this.floatingWindow) {
          this.floatingWindow.style.display = 'none';
          console.log('👻 浮窗已隐藏');
          
          // 保持iframe的内容状态，但确保样式正确
          const iframe = this.floatingWindow.querySelector('#senparc-ai-iframe');
          if (iframe) {
            // 不重置src，保持内容，但确保样式正确
            iframe.style.display = 'block';
            console.log('🔧 保持iframe状态');
          }
        }
      }, 300);
    } else {
      console.log('⚠️ 浮窗元素不存在，无需关闭');
    }
    
    this.isWindowOpen = false;
    console.log('✅ 浮窗关闭完成，状态已更新');
  }

  // 销毁插件
  destroy() {
    console.log('🗑️ 销毁插件实例...');
    
    // 如果处于停靠模式，先恢复悬浮模式
    if (this.isDocked) {
      this.setFloatingMode();
    }
    
    // 确保清除停靠相关的class和样式
    document.body.classList.remove('senparc-docked');
    document.body.style.marginRight = '';
    document.body.style.transition = '';
    document.body.style.boxSizing = '';
    document.body.style.overflowX = '';
    
    // 清除微信文档特殊样式
    this.clearWeixinDocsSpecificStyles();
    
    // 清理Logo按钮
    if (this.logoButton) {
      this.logoButton.remove();
      this.logoButton = null;
    }
    
    // 清理浮窗
    if (this.floatingWindow) {
      this.floatingWindow.remove();
      this.floatingWindow = null;
    }
    
    // 重置状态
    this.isWindowOpen = false;
    this.isDocked = false;
    
    // 清理所有可能的重复按钮
    const existingButtons = document.querySelectorAll('#senparc-weixin-ai-button');
    existingButtons.forEach(button => {
      console.log('🧹 清理重复的Logo按钮');
      button.remove();
    });
    
    // 清理所有可能的重复浮窗
    const existingWindows = document.querySelectorAll('#senparc-weixin-ai-window');
    existingWindows.forEach(window => {
      console.log('🧹 清理重复的浮窗');
      window.remove();
    });
  }
}

// 全局实例管理
let globalAssistantInstance = null;

// 安全初始化函数
function initializeAssistant() {
  // 清理旧实例
  if (globalAssistantInstance) {
    console.log('🧹 清理旧的插件实例');
    globalAssistantInstance.destroy();
    globalAssistantInstance = null;
  }
  
  // 创建新实例
  try {
    globalAssistantInstance = new WeixinAIAssistant();
    console.log('✨ 插件实例创建成功');
  } catch (error) {
    console.error('❌ 插件实例创建失败:', error);
  }
}

// 页面加载完成后初始化
if (document.readyState === 'loading') {
  document.addEventListener('DOMContentLoaded', initializeAssistant);
} else {
  initializeAssistant();
}

// 监听页面URL变化（SPA应用）
let lastUrl = location.href;
let urlChangeTimeout = null;

new MutationObserver(() => {
  const url = location.href;
  if (url !== lastUrl) {
    lastUrl = url;
    console.log('🔄 检测到页面URL变化:', url);
    
    // 使用防抖，避免频繁重新初始化
    if (urlChangeTimeout) {
      clearTimeout(urlChangeTimeout);
    }
    
    urlChangeTimeout = setTimeout(() => {
      initializeAssistant();
    }, 1000);
  }
}).observe(document, { subtree: true, childList: true });

// 导出给其他脚本使用
window.WeixinAIAssistant = WeixinAIAssistant;
window.initializeAssistant = initializeAssistant;
Object.defineProperty(window, 'globalAssistantInstance', {
  get: () => globalAssistantInstance
});
