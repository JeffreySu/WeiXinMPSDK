# 企业微信数据与智能专区 API 使用说明

## 概述

DataIntelligenceApi 提供了企业微信数据与智能专区的相关接口，包括会话记录获取和消息统计功能。这些API主要用于数据分析和智能化应用。

## 主要功能

### 1. 获取会话记录 (GetConversationRecords)

获取企业微信中的会话记录数据，支持分页查询。

#### 基本用法

```csharp
// 使用参数方式调用
var result = DataIntelligenceApi.GetConversationRecords(
    accessToken,           // AccessToken或AppKey
    "your_chat_id",       // 会话ID
    DateTime.Now.AddDays(-7), // 开始时间
    DateTime.Now,         // 结束时间
    "",                   // 分页cursor，首次查询传空字符串
    100                   // 每页数量，最大1000
);

// 使用请求对象方式调用
var request = new GetConversationRecordsRequest
{
    chatid = "your_chat_id",
    starttime = DateTimeOffset.Now.AddDays(-7).ToUnixTimeSeconds(),
    endtime = DateTimeOffset.Now.ToUnixTimeSeconds(),
    cursor = "",
    limit = 100
};
var result = DataIntelligenceApi.GetConversationRecords(accessToken, request);
```

#### 异步调用

```csharp
// 异步方式调用
var result = await DataIntelligenceApi.GetConversationRecordsAsync(
    accessToken,
    "your_chat_id",
    DateTime.Now.AddDays(-7),
    DateTime.Now
);
```

#### 分页处理

```csharp
string cursor = "";
var allRecords = new List<ConversationRecord>();

do
{
    var result = DataIntelligenceApi.GetConversationRecords(
        accessToken, "your_chat_id", startTime, endTime, cursor, 1000);
    
    if (result.errcode == ReturnCode_Work.请求成功)
    {
        allRecords.AddRange(result.records);
        cursor = result.next_cursor;
        
        // 检查是否还有更多数据
        if (!result.has_more)
            break;
    }
    else
    {
        Console.WriteLine($"错误: {result.errmsg}");
        break;
    }
} while (!string.IsNullOrEmpty(cursor));
```

### 2. 获取消息统计 (GetMessageStatistics)

获取企业微信中的消息统计信息，支持按天、周、月统计。

#### 基本用法

```csharp
// 获取过去30天的日统计数据
var result = DataIntelligenceApi.GetMessageStatistics(
    accessToken,
    DateTime.Now.AddDays(-30), // 开始时间
    DateTime.Now,             // 结束时间
    "day",                    // 统计类型：day/week/month
    null,                     // 应用ID，null表示全部应用
    null                      // 用户ID列表，null表示全部用户
);

// 获取特定应用和用户的统计
var result2 = DataIntelligenceApi.GetMessageStatistics(
    accessToken,
    DateTime.Now.AddDays(-7),
    DateTime.Now,
    "day",
    "your_agent_id",
    new[] { "user1", "user2", "user3" }
);
```

#### 使用请求对象

```csharp
var request = new GetMessageStatisticsRequest
{
    starttime = DateTimeOffset.Now.AddDays(-30).ToUnixTimeSeconds(),
    endtime = DateTimeOffset.Now.ToUnixTimeSeconds(),
    type = "week",
    agentid = "your_agent_id",
    userids = new[] { "user1", "user2" }
};

var result = DataIntelligenceApi.GetMessageStatistics(accessToken, request);
```

## 返回数据结构

### ConversationRecord（会话记录）

```csharp
public class ConversationRecord
{
    public string msgid { get; set; }        // 消息ID
    public string msgtype { get; set; }      // 消息类型
    public string from { get; set; }         // 发送者
    public string to { get; set; }           // 接收者（群聊时为空）
    public string roomid { get; set; }       // 群聊ID（单聊时为空）
    public long timestamp { get; set; }      // 发送时间
    public ConversationContent content { get; set; } // 消息内容
}
```

### MessageStatistics（消息统计）

```csharp
public class MessageStatistics
{
    public long date { get; set; }           // 统计日期
    public int total_send { get; set; }      // 发送消息总数
    public int total_receive { get; set; }   // 接收消息总数
    public int text_count { get; set; }      // 各类型消息数量
    public int image_count { get; set; }
    public int voice_count { get; set; }
    public int video_count { get; set; }
    public int file_count { get; set; }
    public int link_count { get; set; }
    public int location_count { get; set; }
    public int active_users { get; set; }    // 活跃用户数
    public int active_groups { get; set; }   // 活跃群聊数
}
```

## 注意事项

1. **权限要求**: 需要企业微信管理员开通数据与智能专区相关权限
2. **数据范围**: 只能获取已授权的会话和用户数据
3. **分页限制**: 会话记录查询单次最多返回1000条记录
4. **时间范围**: 建议查询时间范围不超过31天
5. **频率限制**: 遵循企业微信API调用频率限制

## 错误处理

```csharp
try
{
    var result = DataIntelligenceApi.GetConversationRecords(accessToken, chatId, startTime, endTime);
    
    if (result.errcode == ReturnCode_Work.请求成功)
    {
        // 处理成功结果
        foreach (var record in result.records)
        {
            Console.WriteLine($"消息: {record.content.text}");
        }
    }
    else
    {
        Console.WriteLine($"API调用失败: {result.errcode} - {result.errmsg}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"调用异常: {ex.Message}");
}
```

## 示例场景

### 1. 会话记录分析
- 获取客服会话记录进行服务质量分析
- 导出重要会议记录进行归档
- 分析团队沟通模式

### 2. 消息统计分析
- 统计各部门消息活跃度
- 分析沟通工具使用情况
- 生成企业沟通报表

这些API为企业提供了强大的数据分析能力，帮助企业更好地了解和优化内部沟通效率。