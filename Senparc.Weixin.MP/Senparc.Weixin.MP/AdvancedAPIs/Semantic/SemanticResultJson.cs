﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.AdvancedAPIs
{

    public class BaseSemanticResultJson : WxJsonResult
    {
        public int res { get; set; }//用于标识用户请求后的状态
        public string query { get; set; }//用户的输入字符串
        public string type { get; set; }//服务的全局类别id
        //public BaseSemantic semantic { get; set; }//语义理解后的结构化标识，各个服务不同
        public string answer { get; set; }//部分类别的结果html5展示，目前不支持
        public string text { get; set; }//特殊回复说明
    }
    public class BaseSemantic
    {
        public string intent { get; set; }
    }

    public class SearchResultJson : BaseSemanticResultJson
    {
        public SearchSemantic semantic { get; set; }
    }
    public class SearchSemantic : BaseSemantic
    {
        public SearchSemanticDetail details { get; set; }
    }
    public class SearchSemanticDetail
    {
        public string keyword { get; set; }
        public string channel { get; set; }
    }
    

    //public class SemanticResultJson : WxJsonResult
    //{
    //    public int res { get; set; }
    //    public string query { get; set; }
    //    public string type { get; set; }
    //    public BaseSemanticInfo semantic { get; set; }
    //}

    //public class BaseSemanticInfo
    //{
    //    public string intent { get; set; }//SEARCH 普通查询、ROUTE 路线查询、PRICE 价格查询、GUIDE 攻略查询
    //    public BaseDetails details { get; set; }
    //}

    //public class BaseDetails
    //{ 

    //}

    //public class Semantic_DateTime
    //{
    //    public string type { get; set; }//类型：“DT_REPEAT”  DT_ REPEAT又细分为两个类别：DT_RORI和DT_RINFER。DT_RORI是字面时间，比如：“每天上午九点”；DT_RINFER是推理时间，比如：“工作日除外”
    //    public string time { get; set; }//24小时制，格式：HH:MM:SS
    //    public string time_ori { get; set; }//time的原始字符串
    //    public string repeat { get; set; }//重复标记：0000000 注：依次代表周日，周六，…，周一；1表示该天要重复，0表示不重复
    //    public string repeat_ori { get; set; }//date的原始字符串
    //}

    ///// <summary>
    ///// 注：LOC_POI和NORMAL_POI的区别：LOC_POI是指地名除了国家、省、市、区县的详细地址；NORMAL_POI是指地图上偏向机构的poi点，比如：饭馆、酒店、大厦等等
    ///// </summary>
    //public class Semantic_Location
    //{
    //    public string type { get; set; }//大类型：“LOC”  LOC又细分为如下类别：LOC_COUNTRY、LOC_PROVINCE、LOC_CITY、LOC_TOWN、LOC_POI、NORMAL_POI。
    //    public string country { get; set; }//国家
    //    public string province { get; set; }//省全称，例如：广东省
    //    public string province_simple { get; set; }//省简称，例如：广东|粤
    //    public string city { get; set; }//市全称，例如：北京市
    //    public string city_simple { get; set; }//市简称，例如：北京
    //    public string town { get; set; }//县区全称，例如：海淀区
    //    public string town_simple { get; set; }//县区简称，例如：海淀
    //    public string poi { get; set; }//poi详细地址
    //    public string loc_ori { get; set; }//原始地名串
    //}

    ///// <summary>
    ///// 说明：begin或end，如果为“-1”表示无上限或者下限，如果为“-2”，表示该字段无信息。
    /////NUM_PRICE：价格相关，例：200元左右
    /////NUM_RADIUS：距离相关，例：200米以内
    /////NUM_DISCOUNT：折扣相关，例：八折
    /////NUM_SEASON：部，季相关，例：第一部
    /////NUM_EPI：集相关，例：第一集
    ///// </summary>
    //public class Semantic_Number
    //{
    //    public string type { get; set; }//大类型：“NUMBER”  NUMBER又细分为如下类别：NUM_PRICE、NUM_PADIUS、NUM_DISCOUNT、NUM_SEASON、NUM_EPI、NUM_CHAPTER。
    //    public string begin { get; set; }//开始
    //    public string end { get; set; }//结束
    //}

    //public class Restaurant : BaseDetails
    //{
    //    public Details_Restaurant details { get; set; }
    //}
    ///// <summary>
    ///// 餐馆服务（restaurant）
    ///// </summary>
    //public class Details_Restaurant
    //{
    //    public Semantic_Location location { get; set; }//地点
    //    public string name { get; set; }//餐馆名称
    //    public string category { get; set; }//餐馆类型/菜系
    //    public string special { get; set; }//菜名
    //    public Semantic_Number price { get; set; }//价格（单位元）
    //    public Semantic_Number radius { get; set; }//距离（单位米）
    //    public int coupon { get; set; }//优惠信息：0无（默认），1优惠券，2团购
    //    public int sort { get; set; }//排序类型：0距离（默认），1点评高优先级，2服务质量高优先级，3环境高优
    //}
    ///// <summary>
    ///// 地图服务（map）
    ///// 注：地点区域是地点位置的修饰描述，比如：“我现在在皇寺广场远东大厦门口”，起点区域是：“皇寺广场”，起点位置是：“远东大厦门口”。
    ///// </summary>
    //public class Map : BaseSemanticInfo
    //{
    //    public Details_Map details { get; set; }
    //}

    //public class Details_Map
    //{
    //    public Semantic_Location start_area { get; set; }//起点区域
    //    public Semantic_Location start_loc { get; set; }//起点位置
    //    public Semantic_Location end_area { get; set; }//终点区域
    //    public Semantic_Location end_loc { get; set; }//终点位置
    //    public string route_type { get; set; }//出行方式：walk（步行）, taxi（打车）, bus（公交）, subway（地铁）, drive（自驾）
    //    public int bus_num { get; set; }//公交车号
    //    public string subway_num { get; set; }//地铁线
    //    public int type { get; set; }//排序类型：0较快捷（默认），1少换乘，2少步行
    //    public string keyword { get; set; }//关键词
    //}
    ///// <summary>
    ///// 周边服务（nearby）
    ///// 注：service字段表示是否是服务类，比如：找家政、租房、招聘等即为服务类；找ATM、羽毛球馆等即为非服务类。
    ///// </summary>
    //public class NearBy : BaseSemanticInfo
    //{
    //    public Details_NearBy details { get; set; }
    //}

    //public class Details_NearBy
    //{
    //    public Semantic_Location location { get; set; }//地点
    //    public string keyword { get; set; }//关键词
    //    public string limit { get; set; }//限定词
    //    public Semantic_Number price { get; set; }//价格（单位元）
    //    public Semantic_Number radius { get; set; }//距离（单位米）
    //    public int service { get; set; }//是否是服务类：0不是（默认），1是
    //    public int coupon { get; set; }//优惠信息：0无（默认），1优惠券，2团购
    //    public int sort { get; set; }//排序类型：0距离（默认），1点评高优先级，2服务质量高优先级，3环境高优先级，4价格高到低，5价格低到高
    //}
    ///// <summary>
    ///// 团购服务（coupon）
    ///// </summary>
    //public class Coupon : BaseSemanticInfo
    //{
    //    public Details_Coupon details { get; set; }
    //}

    //public class Details_Coupon
    //{
    //    public Semantic_Location location { get; set; }//地点
    //    public Semantic_Number price { get; set; }//价格（单位元）
    //    public Semantic_Number radius { get; set; }//距离（单位米）
    //    public string keyword { get; set; }//关键词
    //    public int coupon { get; set; }//优惠信息：0所有（默认），1优惠券，2团购
    //    public int sort { get; set; }//排序类型：0距离（默认），1价格高到低，2价格低到高
    //}
    ///// <summary>
    ///// 酒店服务（hotel）
    ///// </summary>
    //public class Hotel : BaseSemanticInfo
    //{
    //    public Details_Hotel details { get; set; }
    //}

    //public class Details_Hotel
    //{
    //    public Semantic_Location location { get; set; }//地点
    //    public Semantic_DateTime start_date { get; set; }//入住时间
    //    public Semantic_DateTime end_date { get; set; }//离开时间
    //    public string name { get; set; }//酒店名称
    //    public Semantic_Number price { get; set; }//价格（单位元）
    //    public Semantic_Number radius { get; set; }//距离（单位米）
    //    public string level { get; set; }//等级：五星级酒店，四星级酒店，三星级酒店，青年旅社，经济型酒店，公寓式酒店
    //    public int wifi { get; set; }//1（有wifi），0（无wifi）
    //    public string roomtype { get; set; }//房间类型：标准间，单人间，双人间，三人间
    //    public int coupon { get; set; }//优惠信息：0无（默认），1优惠券，2团购
    //    public int sort { get; set; }//排序类型：0距离（默认），1点评高优先级，2服务质量高优先级，3环境高优先级，4价格高到低，5价格低到高
    //}
    ///// <summary>
    ///// 旅游服务（travel）
    ///// </summary>
    //public class Travel : BaseSemanticInfo
    //{
    //    public Details_Travel details { get; set; }
    //}

    //public class Details_Travel
    //{
    //    public Semantic_Location location { get; set; }//旅游目的地
    //    public string spot { get; set; }//景点名称
    //    public Semantic_DateTime datetime { get; set; }//旅游日期
    //    public string tag { get; set; }//旅游类型词
    //    public int category { get; set; }//0默认，1自由行，2跟团游
    //}
    ///// <summary>
    ///// 航班服务（flight）
    ///// </summary>
    //public class Flight : BaseSemanticInfo
    //{
    //    public Details_Flight details { get; set; }
    //}

    //public class Details_Flight
    //{
    //    public string flight_no { get; set; }//航班号
    //    public Semantic_Location start_loc { get; set; }//出发地
    //    public Semantic_Location end_loc { get; set; }//目的地
    //    public Semantic_DateTime start_date { get; set; }//出发日期
    //    public Semantic_DateTime end_date { get; set; }//返回日期
    //    public string airline { get; set; }//航空公司
    //    public string seat { get; set; }//座位级别（默认无限制）：ECONOMY（经济舱）BIZ（商务舱）FIRST（头等舱）
    //    public int sort { get; set; }//排序类型：0排序无要求（默认），1价格升序，2价格降序，3时间升序，4时间降序
    //}
    ///// <summary>
    ///// 火车服务（train）
    ///// </summary>
    //public class Train : BaseSemanticInfo
    //{
    //    public Details_Train details { get; set; }
    //}

    //public class Details_Train
    //{
    //    public Semantic_DateTime start_date { get; set; }//出发日期
    //    public Semantic_DateTime end_date { get; set; }//返回日期
    //    public Semantic_Location start_loc { get; set; }//起点
    //    public Semantic_Location end_loc { get; set; }//终点
    //    public string code { get; set; }//车次代码，比如：T43等
    //    public string seat { get; set; }//座位级别：YZ（硬座），RZ（软座），YW（硬卧），RW（软卧），YD（一等座），ED（二等座），TD（特等座）
    //    public string category { get; set; }//车次类型：G（高铁），D（动车），T（特快），K（快速），Z（直达），L（临时客车），P（普通）
    //    public string type { get; set; }//类型：DC（单程），WF（往返）
    //}
    ///// <summary>
    ///// 上映电影服务（movie）
    ///// </summary>
    //public class Movie : BaseSemanticInfo
    //{
    //    public Details_Movie details { get; set; }
    //}

    //public class Details_Movie
    //{
    //    public string name { get; set; }//电影名
    //    public string actor { get; set; }//主演
    //    public string director { get; set; }//导演
    //    public string tag { get; set; }//类型：动作片，剧情片，…
    //    public string country { get; set; }//地区：美国，大陆，香港，…
    //    public string cinema { get; set; }//电影院
    //    public Semantic_Location location { get; set; }//地点
    //    public Semantic_DateTime datetime { get; set; }//时间
    //    public int coupon { get; set; }//优惠信息：0无（默认），1优惠券，2团购
    //    public int sort { get; set; }//排序类型：0排序无要求（默认），1评价高优先级
    //}
    ///// <summary>
    ///// 音乐服务（music）
    ///// </summary>
    //public class Music : BaseSemanticInfo
    //{
    //    public Details_Music details { get; set; }
    //}

    //public class Details_Music
    //{
    //    public string song { get; set; }//歌曲名
    //    public string singer { get; set; }//歌手
    //    public string album { get; set; }//专辑
    //    public string category { get; set; }//歌曲类型
    //    public string language { get; set; }//语言：中文，英文，韩文，日文，…
    //    public string movie { get; set; }//电影名
    //    public string tv { get; set; }//电视剧名
    //    public string show { get; set; }//节目名
    //    public int sort { get; set; }//排序类型：0排序无要求（默认），1时间升序，2时间降序，3热度高优先级
    //}
    ///// <summary>
    ///// 视频服务（video）
    ///// </summary>
    //public class Video : BaseSemanticInfo
    //{
    //    public Details_Video details { get; set; }
    //}

    //public class Details_Video
    //{
    //    public string name { get; set; }
    //    public string actor { get; set; }
    //    public string director { get; set; }
    //    public string category { get; set; }
    //    public string tag { get; set; }
    //    public string country { get; set; }
    //    public Semantic_Number season { get; set; }
    //    public Semantic_Number episode { get; set; }
    //    public int sort { get; set; }
    //}
    ///// <summary>
    ///// 小说服务（novel）
    ///// </summary>
    //public class Novel : BaseSemanticInfo
    //{
    //    public Details_Novel details { get; set; }
    //}

    //public class Details_Novel
    //{
    //    public string name { get; set; }//小说名
    //    public string author { get; set; }//小说作者
    //    public string category { get; set; }//小说类型
    //    public Semantic_Number chapter { get; set; }//小说章节
    //    public int sort { get; set; }//排序类型：0排序无要求（默认），1热度高优先级，2时间升序，3时间降序
    //}
    ///// <summary>
    ///// 天气服务（weather）
    ///// </summary>
    //public class Weather : BaseSemanticInfo
    //{
    //    public Details_Weather details { get; set; }
    //}

    //public class Details_Weather
    //{
    //    public Semantic_Location location { get; set; }//地点
    //    public Semantic_Location datetime { get; set; }//时间
    //}
    ///// <summary>
    ///// 股票服务（stock）
    ///// 注：1、币种单位同上市所在地；2、如果用户输入的时间是未来的时间，那么结果展示的是当前时间的信息。
    ///// </summary>
    //public class Stock : BaseSemanticInfo
    //{
    //    public Details_Stock details { get; set; }
    //    public List<Stock_Result> result { get; set; }
    //}

    //public class Details_Stock
    //{
    //    public string name { get; set; }//股票名称
    //    public string code { get; set; }//标准股票代码
    //    public string category { get; set; }//市场：sz,sh,hk,us
    //    public Semantic_DateTime datetime { get; set; }//时间
    //}

    //public class Stock_Result
    //{
    //    public string cd { get; set; }//股票代码
    //    public string np { get; set; }//当前价
    //    public string ap { get; set; }//涨幅
    //    public string apn { get; set; }//涨幅比例
    //    public string tp_max { get; set; }//最高价
    //    public string tp_min { get; set; }//最低价
    //    public string dn { get; set; }//成交量(单位：万)
    //    public string de { get; set; }//成交额(单位：亿)
    //    public string pe { get; set; }//市盈率
    //    public string sz { get; set; }//市值(单位：亿)
    //}
    ///// <summary>
    ///// 提醒服务（remind）
    ///// </summary>
    //public class Remind : BaseSemanticInfo
    //{
    //    public Details_Remind details { get; set; }
    //}

    //public class Details_Remind
    //{
    //    public Semantic_DateTime datetime { get; set; }//时间
    //    public string @event { get; set; }//事件
    //    public int remind_type { get; set; }//类别：0提醒；1闹钟  注：提醒有具体事件，闹钟没有具体事件
    //}
    ///// <summary>
    ///// 常用电话服务（telephone）
    ///// </summary>
    //public class Telephone : BaseSemanticInfo
    //{
    //    public Details_Telephone details { get; set; }
    //}

    //public class Details_Telephone
    //{
    //    public string name { get; set; }//名字
    //    public string telephone { get; set; }//电话
    //}
    ///// <summary>
    ///// 菜谱服务（cookbook）
    ///// </summary>
    //public class Cookbook : BaseSemanticInfo
    //{
    //    public Details_Cookbook details { get; set; }
    //}

    //public class Details_Cookbook
    //{
    //    public string name { get; set; }//菜名
    //    public string category { get; set; }//菜系
    //    public string ingredient { get; set; }//食材

    //}
    ///// <summary>
    ///// 百科服务（baike）
    ///// </summary>
    //public class Baike : BaseSemanticInfo
    //{
    //    public Details_Baike details { get; set; }
    //}

    //public class Details_Baike
    //{
    //    public string keyword { get; set; }//百科关键词
    //}
    ///// <summary>
    ///// 资讯服务（news）
    ///// </summary>
    //public class News : BaseSemanticInfo
    //{
    //    public Details_News details { get; set; }
    //}

    //public class Details_News
    //{
    //    public string keyword { get; set; }//关键词
    //    public string category { get; set; }//新闻类别
    //}
    ///// <summary>
    ///// 电视节目预告（tv）
    ///// </summary>
    //public class TV : BaseSemanticInfo
    //{
    //    public Details_TV details { get; set; }
    //}

    //public class Details_TV
    //{
    //    public Semantic_DateTime datetime { get; set; }//播放时间
    //    public string tv_name { get; set; }//电视台名称
    //    public string tv_channel { get; set; }//电视频道名称
    //    public string show_name { get; set; }//节目名称
    //    public string category { get; set; }//节目类型
    //}
    ///// <summary>
    ///// 通用指令（instruction）[beta]
    ///// </summary>
    //public class Instruction : BaseSemanticInfo
    //{
    //    public Details_Instruction details { get; set; }
    //}

    //public class Details_Instruction
    //{
    //    public int number { get; set; }//数字，根据intent有不同的含义，例如：把声音调到20；
    //    public string value { get; set; }//操作值：STANDARD(标准模式)，MUTE(静音模式)，VIBRA(振动模式)，INAIR(飞行模式)，RING(铃声)，WALLPAPER(壁纸)，TIME(时间)，WIFI(无线网络)，BLUETOOTH(蓝牙)，GPS(GPS)，NET(移动网络)，SPACE(存储空间)，INPUT(输入法设置)，LANGUAGE(语言设置)，PERSONAL(个性化设置)，SCREEN(屏幕保护)，FACTORY_SETTINGS (出厂设置)，SI(系统信息)，UPDATE(系统更新)，PLAY(播放)，OPEN_MUSIC(开机音乐)，TIME_ON(定时开机)，TIME_OFF(定时关机)
    //    public string @operator { get; set; }//操作值：OPEN (打开)，CLOSE (关闭)，MIN (最小)，MAX (最大)，UP (变大)，DOWN (变小)
    //}
    ///// <summary>
    ///// 电视指令（tv_instruction）[beta]
    ///// </summary>
    //public class TV_Instruction : BaseSemanticInfo
    //{
    //    public Details_TV_Instruction details { get; set; }
    //}

    //public class Details_TV_Instruction
    //{
    //    public string tv_name { get; set; }//电视台名称
    //    public string tv_channel { get; set; }//电视频道名称
    //    public string category { get; set; }//节目类型
    //    public int number { get; set; }//数字，根据intent有不同的含义，例如：把声音调到20；换到5台。
    //    public string value { get; set; }//操作值：3D，AV(视频)，AV1(视频1)，AV2(视频2)，HDMI，HDMI1，HDMI2，HDMI3，DTV，ATV，YPBPR，DVI，VGA，USB，ANALOG(模拟电视)，DIGITAL(数字电视)，IMAGE(图像设置)，SCREEN(屏幕比例)，SOUND(声音模式) ，IMAGE_MODEL(图像模式)
    //    public string @operator { get; set; }//操作值：OPEN (打开)，CLOSE (关闭)
    //    public string device { get; set; }//设备：U(U盘)，CLOUD(云存储)
    //    public string file_type { get; set; }//文件类型：VIDEO(视频)，TEXT(文本)，APP(安装包)，PIC(照片)，MUSIC(音乐)
    //}
    ///// <summary>
    ///// 车载指令（car_instruction）[beta]
    ///// </summary>
    //public class Car_Instruction : BaseSemanticInfo
    //{
    //    public Details_Car_Instruction details { get; set; }
    //}

    //public class Details_Car_Instruction
    //{
    //    public int number { get; set; }//数字，根据intent有不同的含义，例如：把温度调到20度
    //    public int position { get; set; }//窗户位置：1(司机)，2(副驾驶)，3(司机后面)，4(副驾驶后面)，5(天窗)
    //    public string @operator { get; set; }//操作值：OPEN (打开)，CLOSE (关闭)，MIN (最小)，MAX (最大)，UP (变大)，DOWN (变小)
    //}

    ///// <summary>
    ///// 应用服务（app）
    ///// </summary>
    //public class Semantic_App : BaseSemanticInfo
    //{
    //    public Details_App details { get; set; }
    //}

    //public class Details_App
    //{
    //    public string name { get; set; }//app名称
    //    public string category { get; set; }//app类别
    //    public string sort { get; set; }//排序方式：0（按质量从高到低），1（按时间从新到旧）
    //    public string type { get; set; }//查看的类型：install（已安装），buy（已购买），update（可更新），latest（最近运行的），home（主页）
    //}

    ///// <summary>
    ///// 网址服务（website）
    ///// </summary>
    //public class Semantic_Website : BaseSemanticInfo
    //{
    //    public Details_Website details { get; set; }
    //}

    //public class Details_Website
    //{
    //    public string name { get; set; }//网址名
    //    public string url { get; set; }//url
    //}

    ///// <summary>
    ///// 网页搜索（search））
    ///// </summary>
    //public class Semantic_Search : BaseSemanticInfo
    //{
    //    public Details_Search details { get; set; }
    //}

    //public class Details_Search
    //{
    //    public string keyword { get; set; }//关键词
    //    public string channel { get; set; }//搜索引擎类型：google, baidu, sogou, 360, taobao,jingdong
    //}
}
