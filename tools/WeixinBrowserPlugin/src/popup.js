// Senparc.Weixin.AI Chrome Extension Popup Script

class PopupController {
  constructor() {
    this.init();
  }

  async init() {
    await this.checkCurrentTab();
    this.setupEventListeners();
  }

  // 检查是否是支持的页面
  isSupportedPage(url) {
    if (!url) return false;
    
    // 支持的页面列表
    const supportedPages = [
      'https://developers.weixin.qq.com',
      'https://pay.weixin.qq.com/doc'
    ];
    
    return supportedPages.some(page => {
      if (page.includes('/doc')) {
        // 对于包含/doc的，需要精确匹配路径
        return url.startsWith(page);
      } else {
        // 对于其他的，匹配域名即可
        return url.startsWith(page);
      }
    });
  }

  // 检查当前标签页状态
  async checkCurrentTab() {
    try {
      const [tab] = await chrome.tabs.query({ active: true, currentWindow: true });
      const statusElement = document.getElementById('status');
      const statusTextElement = document.getElementById('status-text');
      const openAiDocButton = document.getElementById('open-ai-doc');

      // 检查是否是支持的页面
      const isSupported = this.isSupportedPage(tab.url);
      
      if (isSupported) {
        // 当前在支持的微信文档页面
        statusElement.classList.add('active');
        statusTextElement.textContent = '已检测到支持的微信文档页面';
        openAiDocButton.textContent = '打开AI助手';
        openAiDocButton.disabled = false;
      } else {
        // 不在支持的页面
        statusTextElement.textContent = '请访问支持的微信文档页面';
        openAiDocButton.textContent = '请先访问微信文档';
        openAiDocButton.disabled = true;
        openAiDocButton.style.opacity = '0.5';
        openAiDocButton.style.cursor = 'not-allowed';
      }
    } catch (error) {
      console.error('检查标签页状态失败:', error);
      const statusTextElement = document.getElementById('status-text');
      statusTextElement.textContent = '状态检测失败';
    }
  }

  // 设置事件监听器
  setupEventListeners() {
    const openAiDocButton = document.getElementById('open-ai-doc');
    
    openAiDocButton.addEventListener('click', async () => {
      if (openAiDocButton.disabled) {
        // 如果按钮被禁用，打开微信文档首页
        chrome.tabs.create({ url: 'https://developers.weixin.qq.com/doc/' });
        return;
      }

      try {
        // 获取当前活动标签页
        const [tab] = await chrome.tabs.query({ active: true, currentWindow: true });
        
        if (tab && this.isSupportedPage(tab.url)) {
          // 向内容脚本发送消息，打开AI助手
          await chrome.tabs.sendMessage(tab.id, { 
            action: 'openAIAssistant',
            url: tab.url 
          });
          
          // 关闭弹窗
          window.close();
        } else {
          // 重定向到微信文档页面
          chrome.tabs.create({ url: 'https://developers.weixin.qq.com/doc/' });
        }
      } catch (error) {
        console.error('打开AI助手失败:', error);
        
        // 如果内容脚本未加载，尝试重新加载页面
        try {
          const [tab] = await chrome.tabs.query({ active: true, currentWindow: true });
          if (tab && this.isSupportedPage(tab.url)) {
            chrome.tabs.reload(tab.id);
            setTimeout(() => {
              window.close();
            }, 1000);
          }
        } catch (reloadError) {
          console.error('重新加载页面失败:', reloadError);
        }
      }
    });

    // 监听标签页变化
    if (chrome.tabs && chrome.tabs.onActivated) {
      chrome.tabs.onActivated.addListener(() => {
        this.checkCurrentTab();
      });
    }

    if (chrome.tabs && chrome.tabs.onUpdated) {
      chrome.tabs.onUpdated.addListener((tabId, changeInfo, tab) => {
        if (changeInfo.status === 'complete') {
          this.checkCurrentTab();
        }
      });
    }
  }
}

// 页面加载完成后初始化
document.addEventListener('DOMContentLoaded', () => {
  new PopupController();
});

// 处理来自内容脚本的消息
if (chrome.runtime && chrome.runtime.onMessage) {
  chrome.runtime.onMessage.addListener((request, sender, sendResponse) => {
    if (request.action === 'updatePopupStatus') {
      // 更新弹窗状态
      const statusElement = document.getElementById('status');
      const statusTextElement = document.getElementById('status-text');
      
      if (request.isWeixinPage) {
        statusElement.classList.add('active');
        statusTextElement.textContent = '已检测到微信文档页面';
      } else {
        statusElement.classList.remove('active');
        statusTextElement.textContent = '请访问微信文档页面';
      }
      
      sendResponse({ success: true });
    }
  });
}
