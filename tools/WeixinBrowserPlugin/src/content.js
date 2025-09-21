// Senparc.Weixin.AI Chrome Extension Content Script
// 监控微信文档页面并添加AI助手功能

class WeixinAIAssistant {
  constructor() {
    this.logoButton = null;
    this.floatingWindow = null;
    this.isWindowOpen = false;
    this.isDocked = false;
    this.originalBodyStyle = '';
    this.debug = window.__SENPARC_DEBUG__ || {
      enabled: false,
      level: 'error',
      trigger: 'senparc-debug'
    };
    this.init();
  }

  log(level, ...args) {
    if (this.debug.enabled || window.location.href.includes(this.debug.trigger)) {
      if (this.debug.level === 'verbose' || 
          (this.debug.level === 'info' && level !== 'verbose') ||
          (this.debug.level === 'warn' && (level === 'warn' || level === 'error')) ||
          (this.debug.level === 'error' && level === 'error')) {
        console[level === 'verbose' ? 'log' : level](...args);
      }
    }
  }

  // 初始化插件
  init() {
    this.log('info', 'Senparc.Weixin.AI 插件开始初始化...');
    this.log('verbose', '当前页面URL:', window.location.href);
    this.log('verbose', '当前域名:', window.location.hostname);
    
    // 检查是否是微信文档页面
    if (this.isWeixinDocPage()) {
      this.log('info', '✅ 检测到微信文档页面，初始化AI助手...');
      this.createLogoButton();
      this.setupEventListeners();
    } else {
      this.log('warn', '❌ 当前页面不在支持列表中');
      this.log('info', '仅支持以下页面:');
      this.log('info', '  - https://developers.weixin.qq.com/');
      this.log('info', '  - https://developer.work.weixin.qq.com/document');
      this.log('info', '  - https://pay.weixin.qq.com/doc');
    }
  }

  // 检查是否是微信文档页面
  isWeixinDocPage() {
    const url = window.location.href;
    const hostname = window.location.hostname;
    
    // 只允许以下两个特定地址
    const allowedUrls = [
      'developers.weixin.qq.com',
      'developer.work.weixin.qq.com',
      'pay.weixin.qq.com'
    ];
    
    // 检查域名匹配
    const isAllowedDomain = allowedUrls.some(domain => hostname === domain);
    
    // developer.work.weixin.qq.com，额外检查必须是/document路径
    if (hostname === 'developer.work.weixin.qq.com') {
      return url.includes('/document');
    }

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

    // 添加拖拽功能
    this.setupDragFeature();
  }

  // 设置拖拽功能
  setupDragFeature() {
    if (!this.logoButton) return;

    let isDragging = false;
    let hasMoved = false;
    let mouseDownTime = 0;
    let initialX;
    let initialY;
    let xOffset = 0;
    let yOffset = 0;
    let buttonWidth;
    let buttonHeight;
    let maxX;
    let maxY;

    // 从localStorage获取保存的位置
    const savedPosition = localStorage.getItem('senparcAiButtonPosition');
    if (savedPosition) {
      try {
        const { x, y } = JSON.parse(savedPosition);
        this.logoButton.style.left = x + 'px';
        this.logoButton.style.top = y + 'px';
        xOffset = x;
        yOffset = y;
      } catch (e) {
        console.error('恢复按钮位置失败:', e);
      }
    }

    const dragStart = (e) => {
      if (e.target === this.logoButton || this.logoButton.contains(e.target)) {
        isDragging = true;
        hasMoved = false;
        mouseDownTime = Date.now();

        // 预先计算按钮尺寸和边界
        buttonWidth = this.logoButton.offsetWidth;
        buttonHeight = this.logoButton.offsetHeight;
        maxX = window.innerWidth - buttonWidth;
        maxY = window.innerHeight - buttonHeight;

        // 计算初始偏移
        const currentLeft = parseInt(this.logoButton.style.left) || 0;
        const currentTop = parseInt(this.logoButton.style.top) || 0;
        
        if (e.type === "mousedown") {
          initialX = e.clientX - currentLeft;
          initialY = e.clientY - currentTop;
        } else {
          initialX = e.touches[0].clientX - currentLeft;
          initialY = e.touches[0].clientY - currentTop;
        }

        // 添加拖动状态类名，禁用所有过渡效果
        this.logoButton.classList.add('dragging');
        this.logoButton.style.cursor = 'grabbing';
        e.preventDefault();
        e.stopPropagation();
      }
    };

    const dragEnd = (e) => {
      if (!isDragging) return;
      
      const mouseUpTime = Date.now();
      const pressDuration = mouseUpTime - mouseDownTime;
      
      isDragging = false;
      this.logoButton.classList.remove('dragging');
      this.logoButton.style.cursor = 'grab';

      // 如果有实际移动或按住时间超过200ms，则视为拖拽
      if (hasMoved || pressDuration > 200) {
        e.preventDefault();
        e.stopPropagation();
        
        // 标记这次操作为拖拽
        this.logoButton.setAttribute('data-just-dragged', 'true');
        
        // 一段时间后移除标记
        setTimeout(() => {
          this.logoButton.removeAttribute('data-just-dragged');
        }, 100);

        // 保存位置到localStorage（使用防抖，延迟保存）
        if (this._savePositionTimeout) {
          clearTimeout(this._savePositionTimeout);
        }
        this._savePositionTimeout = setTimeout(() => {
          localStorage.setItem('senparcAiButtonPosition', JSON.stringify({
            x: xOffset,
            y: yOffset
          }));
        }, 500);
      }
    };

    const drag = (e) => {
      if (!isDragging) return;

      e.preventDefault();
      e.stopPropagation();

      // 获取新位置
      const clientX = e.type === "mousemove" ? e.clientX : e.touches[0].clientX;
      const clientY = e.type === "mousemove" ? e.clientY : e.touches[0].clientY;

      // 计算新位置（使用预先计算的边界）
      let newX = Math.min(Math.max(0, clientX - initialX), maxX);
      let newY = Math.min(Math.max(0, clientY - initialY), maxY);

      // 只在第一次移动时检查
      if (!hasMoved && (Math.abs(newX - xOffset) > 5 || Math.abs(newY - yOffset) > 5)) {
        hasMoved = true;
      }

      // 更新位置（直接修改style属性，避免创建字符串）
      if (newX !== xOffset) {
        this.logoButton.style.left = newX + 'px';
        xOffset = newX;
      }
      if (newY !== yOffset) {
        this.logoButton.style.top = newY + 'px';
        yOffset = newY;
      }
    };

    // 添加事件监听器
    this.logoButton.addEventListener('mousedown', dragStart, { capture: true });
    document.addEventListener('mousemove', drag, { capture: true });
    document.addEventListener('mouseup', dragEnd, { capture: true });

    // 触摸事件支持
    this.logoButton.addEventListener('touchstart', dragStart, { capture: true, passive: false });
    document.addEventListener('touchmove', drag, { capture: true, passive: false });
    document.addEventListener('touchend', dragEnd, { capture: true });

    // 设置初始样式
    this.logoButton.style.position = 'fixed';
    this.logoButton.style.cursor = 'grab';
    this.logoButton.style.userSelect = 'none';
    this.logoButton.style.zIndex = '10000';
    this.logoButton.style.touchAction = 'none';
  }

  // 设置事件监听器
  setupEventListeners() {
    if (this.logoButton) {
      console.log('🔗 绑定Logo按钮点击事件...');
      
      // 清除可能存在的旧事件
      this.logoButton.onclick = null;
      
      // 使用单一的点击事件处理器
      const handleClick = (e) => {
        // 如果按钮刚被拖拽过，不触发点击事件
        if (this.logoButton.getAttribute('data-just-dragged') === 'true') {
          return;
        }
        
        console.log('🖱️ Logo按钮被点击！');
        e.preventDefault();
        e.stopPropagation();
        this.toggleFloatingWindow();
      };

      // 使用捕获阶段绑定点击事件
      this.logoButton.addEventListener('click', handleClick, { capture: true });
      
      console.log('✅ Logo按钮事件绑定完成');
    } else {
      console.error('❌ Logo按钮不存在，无法绑定事件');
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
    console.log('🔄 ===== 切换浮窗显示状态 =====');
    console.log('🔍 当前状态:', {
      isWindowOpen: this.isWindowOpen,
      isDocked: this.isDocked,
      floatingWindowExists: !!this.floatingWindow
    });
    
    if (this.isWindowOpen) {
      console.log('📤 当前浮窗已打开，执行关闭操作...');
      this.closeFloatingWindow();
    } else {
      console.log('📥 当前浮窗已关闭，执行打开操作...');
      this.openFloatingWindow();
    }
    
    console.log('✅ 切换操作完成，新状态:', this.isWindowOpen);
  }

  // 打开浮窗
  openFloatingWindow() {
    console.log('🚀 ===== 打开AI助手浮窗 =====');
    console.log('🔍 当前状态:', {
      isWindowOpen: this.isWindowOpen,
      isDocked: this.isDocked,
      floatingWindowExists: !!this.floatingWindow
    });
    
    if (this.floatingWindow) {
      console.log('♻️ 重新显示已存在的浮窗...');
      
      // 完全恢复显示状态
      this.floatingWindow.style.display = 'flex';
      this.floatingWindow.style.opacity = '1';
      this.floatingWindow.style.visibility = 'visible';
      
      // 恢复必要的CSS类
      this.floatingWindow.classList.add('floating-mode');
      
      // 更新状态
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
      this.setupButtonEvents();
        
      // 立即添加显示动画
      requestAnimationFrame(() => {
        if (this.floatingWindow) {
          this.floatingWindow.classList.add('show');
          console.log('✨ 显示动画已启动');
          
          // 动画完成后重新计算iframe尺寸
          setTimeout(() => {
            this.recalculateIframeSize();
          }, 350); // 等待动画完成
        }
      });
      
      console.log('✅ 浮窗重新显示完成');
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

    // 立即设置按钮事件，不延迟
    this.setupButtonEvents();

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

    // 立即绑定事件，不使用延迟
    this.bindButtonEvents();
    
    // 如果第一次绑定失败，再尝试一次
    if (!this.isButtonEventsBound()) {
      console.log('🔄 第一次绑定失败，重试...');
      setTimeout(() => {
        this.bindButtonEvents();
      }, 50);
    }
  }

  // 检查按钮事件是否已绑定
  isButtonEventsBound() {
    const closeButton = this.floatingWindow?.querySelector('#close-floating-window');
    return closeButton && typeof closeButton.onclick === 'function';
  }

  // 绑定按钮事件的具体实现
  bindButtonEvents() {
    if (!this.floatingWindow) {
      console.error('❌ 浮窗不存在，无法绑定按钮事件');
      return;
    }

    console.log('🔗 开始绑定按钮事件...');

    // 设置关闭按钮事件 - 使用多种方法确保事件绑定成功
    const closeButton = this.floatingWindow.querySelector('#close-floating-window');
    if (closeButton) {
      console.log('🔍 找到关闭按钮，ID:', closeButton.id, '类名:', closeButton.className);
      
      // 创建关闭函数
      const closeHandler = (e) => {
        console.log('🖱️ 关闭按钮被点击！开始执行关闭操作...');
        if (e) {
          e.preventDefault();
          e.stopPropagation();
        }
        
        // 直接调用关闭方法
        try {
          this.closeFloatingWindow();
          console.log('✅ 关闭操作执行完成');
        } catch (error) {
          console.error('❌ 关闭操作失败:', error);
        }
        
        return false;
      };

      // 方法1: 使用onclick属性
      closeButton.onclick = closeHandler;
      
      // 方法2: 使用addEventListener作为备用
      closeButton.addEventListener('click', closeHandler, { capture: true, once: false });
      
      // 方法3: 使用mousedown作为额外保险
      closeButton.addEventListener('mousedown', (e) => {
        console.log('🖱️ 关闭按钮mousedown事件');
        e.preventDefault();
        setTimeout(() => closeHandler(e), 10);
      });

      // 确保按钮样式和属性正确
      closeButton.style.cssText = `
        pointer-events: auto !important;
        cursor: pointer !important;
        z-index: 10003 !important;
        position: relative !important;
        background: rgba(255, 255, 255, 0.2) !important;
        border: none !important;
        border-radius: 8px !important;
        width: 32px !important;
        height: 32px !important;
        display: flex !important;
        align-items: center !important;
        justify-content: center !important;
        color: white !important;
      `;
      
      // 添加调试属性
      closeButton.setAttribute('data-event-bound', 'true');
      closeButton.setAttribute('data-bind-time', Date.now().toString());
      
      console.log('✅ 关闭按钮事件已绑定 (多重保险)');
      console.log('   - onclick:', typeof closeButton.onclick);
      console.log('   - 样式cursor:', window.getComputedStyle(closeButton).cursor);
      console.log('   - z-index:', window.getComputedStyle(closeButton).zIndex);
    } else {
      console.error('❌ 找不到关闭按钮元素');
      console.log('🔍 浮窗内容:', this.floatingWindow.innerHTML.substring(0, 500));
      
      // 尝试重新查找
      setTimeout(() => {
        console.log('🔄 重试查找关闭按钮...');
        this.bindButtonEvents();
      }, 200);
      return;
    }

    // 设置停靠按钮事件
    const dockButton = this.floatingWindow.querySelector('#dock-toggle-button');
    if (dockButton) {
      console.log('🔍 找到停靠按钮，开始绑定事件...');
      
      // 创建停靠切换函数
      const dockHandler = (e) => {
        console.log('🖱️ 停靠按钮被点击！');
        if (e) {
          e.preventDefault();
          e.stopPropagation();
        }
        
        try {
          this.toggleDockMode();
          console.log('✅ 停靠模式切换完成');
        } catch (error) {
          console.error('❌ 停靠模式切换失败:', error);
        }
        
        return false;
      };

      // 多重事件绑定
      dockButton.onclick = dockHandler;
      dockButton.addEventListener('click', dockHandler, { capture: true });

      // 确保按钮样式正确
      dockButton.style.cssText = `
        pointer-events: auto !important;
        cursor: pointer !important;
        z-index: 10003 !important;
        position: relative !important;
        background: rgba(255, 255, 255, 0.2) !important;
        border: none !important;
        border-radius: 8px !important;
        width: 32px !important;
        height: 32px !important;
        display: flex !important;
        align-items: center !important;
        justify-content: center !important;
        color: white !important;
        margin-right: 4px !important;
      `;
      
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
    console.log('🚪 ===== 开始关闭浮窗 =====');
    console.log('🔍 当前状态:', {
      isWindowOpen: this.isWindowOpen,
      isDocked: this.isDocked,
      floatingWindowExists: !!this.floatingWindow,
      windowDisplay: this.floatingWindow ? window.getComputedStyle(this.floatingWindow).display : 'N/A',
      windowOpacity: this.floatingWindow ? window.getComputedStyle(this.floatingWindow).opacity : 'N/A'
    });
    
    if (!this.floatingWindow) {
      console.log('⚠️ 浮窗元素不存在，无需关闭');
      this.isWindowOpen = false;
      return;
    }

    // 立即更新状态，防止重复点击
    this.isWindowOpen = false;
    
    try {
      // 如果当前处于停靠模式，先恢复页面布局
      if (this.isDocked) {
        console.log('🔄 从停靠模式关闭，恢复页面布局...');
        
        // 移除停靠相关的样式和类
        document.body.classList.remove('senparc-docked');
        document.body.style.marginRight = '';
        document.body.style.transition = '';
        document.body.style.boxSizing = '';
        document.body.style.overflowX = '';
        
        // 清除微信文档特殊样式
        this.clearWeixinDocsSpecificStyles();
        
        // 更新停靠状态
        this.isDocked = false;
        console.log('✅ 页面布局已恢复');
      }
      
      // 立即隐藏浮窗，不等待动画
      console.log('🎬 立即隐藏浮窗...');
      this.floatingWindow.style.display = 'none';
      this.floatingWindow.style.opacity = '0';
      this.floatingWindow.style.visibility = 'hidden';
      
      // 移除显示相关的类
      this.floatingWindow.classList.remove('show', 'floating-mode', 'docked-mode');
      
      console.log('👻 浮窗已立即隐藏');
      
      // 可选：延迟重置一些状态（不影响关闭效果）
      setTimeout(() => {
        if (this.floatingWindow) {
          // 保持iframe的内容状态
          const iframe = this.floatingWindow.querySelector('#senparc-ai-iframe');
          if (iframe) {
            iframe.style.display = 'block';
            console.log('🔧 保持iframe状态');
          }
        }
      }, 100);
      
    } catch (error) {
      console.error('❌ 关闭浮窗过程中出现错误:', error);
      
      // 强制关闭
      if (this.floatingWindow) {
        this.floatingWindow.style.display = 'none';
        this.floatingWindow.style.opacity = '0';
      }
      
      // 强制恢复页面状态
      document.body.classList.remove('senparc-docked');
      document.body.style.marginRight = '';
      this.isDocked = false;
    }
    
    console.log('✅ ===== 浮窗关闭完成 =====');
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

// 监听页面URL变化（SPA应用）- 优化版本
let lastUrl = location.href;
let urlChangeTimeout = null;

// 方案1：基于 History API 的检测（推荐）
function setupHistoryAPIDetection() {
  // 保存原始的 pushState 和 replaceState 方法
  const originalPushState = history.pushState;
  const originalReplaceState = history.replaceState;
  
  function handleUrlChange() {
    PerformanceMonitor.recordHistoryApiCall();
    
    const url = location.href;
    if (url !== lastUrl) {
      lastUrl = url;
      PerformanceMonitor.recordUrlChange();
      
      if (URL_DETECTION_CONFIG.enableDebugLog) {
        console.log('🔄 检测到页面URL变化 (History API):', url);
      }
      
      // 使用防抖，避免频繁重新初始化
      if (urlChangeTimeout) {
        clearTimeout(urlChangeTimeout);
      }
      
      urlChangeTimeout = setTimeout(() => {
        initializeAssistant();
      }, URL_DETECTION_CONFIG.debounceDelay);
    }
  }
  
  // 重写 pushState 方法
  history.pushState = function(...args) {
    originalPushState.apply(history, args);
    handleUrlChange();
  };
  
  // 重写 replaceState 方法
  history.replaceState = function(...args) {
    originalReplaceState.apply(history, args);
    handleUrlChange();
  };
  
  // 监听 popstate 事件（浏览器前进后退）
  window.addEventListener('popstate', handleUrlChange);
  
  // 监听 hashchange 事件（hash变化）
  window.addEventListener('hashchange', handleUrlChange);
}

// 方案2：优化的 MutationObserver（备选方案）
function setupOptimizedMutationObserver() {
  // 使用节流函数减少执行频率
  let throttleTimeout = null;
  
  function throttledUrlCheck() {
    PerformanceMonitor.recordMutationObserverCall();
    
    if (throttleTimeout) return;
    
    throttleTimeout = setTimeout(() => {
      const url = location.href;
      if (url !== lastUrl) {
        lastUrl = url;
        PerformanceMonitor.recordUrlChange();
        
        if (URL_DETECTION_CONFIG.enableDebugLog) {
          console.log('🔄 检测到页面URL变化 (MutationObserver):', url);
        }
        
        if (urlChangeTimeout) {
          clearTimeout(urlChangeTimeout);
        }
        
        urlChangeTimeout = setTimeout(() => {
          initializeAssistant();
        }, URL_DETECTION_CONFIG.debounceDelay);
      }
      throttleTimeout = null;
    }, URL_DETECTION_CONFIG.throttleDelay);
  }
  
  // 只监听特定的变化类型，减少触发频率
  new MutationObserver(throttledUrlCheck).observe(document.body, {
    childList: true,
    subtree: false // 只监听直接子节点，不监听所有后代
  });
}

// URL检测方案配置
const URL_DETECTION_CONFIG = {
  // 检测方案：'history' | 'mutation' | 'hybrid'
  method: 'history', // 默认使用 History API 方案
  
  // 防抖延迟时间（毫秒）
  debounceDelay: 500,
  
  // 节流延迟时间（毫秒，仅用于 MutationObserver）
  throttleDelay: 100,
  
  // 是否启用调试日志
  enableDebugLog: true
};

// 初始化URL检测
function initUrlDetection() {
  console.log(`🔧 初始化URL检测，使用方案: ${URL_DETECTION_CONFIG.method}`);
  
  switch (URL_DETECTION_CONFIG.method) {
    case 'history':
      setupHistoryAPIDetection();
      break;
    case 'mutation':
      setupOptimizedMutationObserver();
      break;
    case 'hybrid':
      // 混合方案：优先使用 History API，MutationObserver 作为备选
      setupHistoryAPIDetection();
      setupOptimizedMutationObserver();
      break;
    default:
      console.warn('⚠️ 未知的URL检测方案，使用默认的 History API 方案');
      setupHistoryAPIDetection();
  }
}

// 性能监控
const PerformanceMonitor = {
  stats: {
    historyApiCalls: 0,
    mutationObserverCalls: 0,
    urlChanges: 0,
    lastUrlChangeTime: 0
  },
  
  recordHistoryApiCall() {
    this.stats.historyApiCalls++;
  },
  
  recordMutationObserverCall() {
    this.stats.mutationObserverCalls++;
  },
  
  recordUrlChange() {
    this.stats.urlChanges++;
    this.stats.lastUrlChangeTime = Date.now();
  },
  
  getStats() {
    return { ...this.stats };
  },
  
  reset() {
    this.stats = {
      historyApiCalls: 0,
      mutationObserverCalls: 0,
      urlChanges: 0,
      lastUrlChangeTime: 0
    };
  },
  
  logStats() {
    console.log('📊 URL检测性能统计:', this.getStats());
  }
};

// 导出性能监控器供调试使用
window.UrlDetectionPerformanceMonitor = PerformanceMonitor;

// 启动URL检测
initUrlDetection();

// 定期输出性能统计（仅在调试模式下）
if (URL_DETECTION_CONFIG.enableDebugLog) {
  setInterval(() => {
    PerformanceMonitor.logStats();
  }, 30000); // 每30秒输出一次统计
}

// 导出给其他脚本使用
window.WeixinAIAssistant = WeixinAIAssistant;
window.initializeAssistant = initializeAssistant;
Object.defineProperty(window, 'globalAssistantInstance', {
  get: () => globalAssistantInstance
});
