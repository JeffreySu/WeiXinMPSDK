#region Apache License Version 2.0
/*----------------------------------------------------------------

Copyright 2019 Jeffrey Su & Suzhou Senparc Network Technology Co.,Ltd.

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file
except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the
License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
either express or implied. See the License for the specific language governing permissions
and limitations under the License.

Detail: https://github.com/JeffreySu/WeiXinMPSDK/blob/master/license.md

----------------------------------------------------------------*/
#endregion Apache License Version 2.0

using System;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Helpers;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Test
{
    [TestClass]
    public class RequestMessageFactoryTest
    {
        string xmlText = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
    <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
    <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
    <CreateTime>1357986928</CreateTime>
    <MsgType><![CDATA[text]]></MsgType>
    <Content><![CDATA[TNT2]]></Content>
    <MsgId>5832509444155992350</MsgId>
</xml>
";

        string xmlLocation = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1358061152</CreateTime>
  <MsgType><![CDATA[location]]></MsgType>
  <Location_X>31.285774</Location_X>
  <Location_Y>120.597610</Location_Y>
  <Scale>19</Scale>
  <Label><![CDATA[中国江苏省苏州市沧浪区桐泾南路251号-309号]]></Label>
  <MsgId>5832828233808572154</MsgId>
</xml>";

        private string xmlImage = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1358061658</CreateTime>
  <MsgType><![CDATA[image]]></MsgType>
  <PicUrl><![CDATA[http://mmsns.qpic.cn/mmsns/ZxBXNzgHyUqazGkXUvujSPxexzynJAicf440qkyLibBd1OEO4saJiavLQ/0]]></PicUrl>
  <MsgId>5832830407062023950</MsgId>
</xml>";

        private string xmlVoice = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1361430302</CreateTime>
  <MsgType><![CDATA[voice]]></MsgType>
  <MediaId><![CDATA[X1yfgB2XI-faU6R2jmKz0X1JZmPCxIvM-9ktt4K92BB9577SCi41S-qMl60q5DJo]]></MediaId>
  <Format><![CDATA[amr]]></Format>
  <MsgId>5847298622973403529</MsgId>
</xml>";

        private string xmlVideo = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1361430302</CreateTime>
  <MsgType><![CDATA[video]]></MsgType>
  <MediaId><![CDATA[mediaId]]></MediaId>
  <ThumbMediaId><![CDATA[thumbMediaId]]></ThumbMediaId>
</xml>";

        private string xmlShortVideo = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1357290913</CreateTime>
<MsgType><![CDATA[shortvideo]]></MsgType>
<MediaId><![CDATA[media_id]]></MediaId>
<ThumbMediaId><![CDATA[thumb_media_id]]></ThumbMediaId>
<MsgId>1234567890123456</MsgId>
</xml>";

//        @"<?xml version=""1.0"" encoding=""utf-8""?>
//<xml>
//  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
//  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
//  <CreateTime>1361430302</CreateTime>
//  <MsgType><![CDATA[video]]></MsgType>
//  <Video>
//    <MediaId><![CDATA[mediaId]]></MediaId>
//    <ThumbMediaId><![CDATA[thumbMediaId]]></ThumbMediaId>
//  </Video> 
//</xml>";

        private string xmlLink = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
<ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
<FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
<CreateTime>1351776360</CreateTime>
<MsgType><![CDATA[link]]></MsgType>
<Title><![CDATA[公众平台官网链接]]></Title>
<Description><![CDATA[Senparc.Weixin.MP SDK公众平台官网链接]]></Description>
<Url><![CDATA[https://sdk.weixin.senparc.com]]></Url>
<MsgId>1234567890123456</MsgId>
</xml>";

        private string xmlEvent_Enter = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
    <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
    <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
    <CreateTime>123456789</CreateTime>
    <MsgType><![CDATA[event]]></MsgType>
    <Event><![CDATA[ENTER]]></Event>
</xml>";

        private string xmlEvent_Location = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
    <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
    <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
    <CreateTime>123456789</CreateTime>
    <MsgType><![CDATA[event]]></MsgType>
    <Event><![CDATA[LOCATION]]></Event>
    <Latitude>23.137466</Latitude>
    <Longitude>113.352425</Longitude>
    <Precision>119.385040</Precision>
</xml>";

        private string xmlEvent_Subscribe = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1364447046</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[subscribe]]></Event>
  <EventKey><![CDATA[]]></EventKey>
</xml>";

        private string xmlEvent_Unsubscribe = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1364447020</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[unsubscribe]]></Event>
  <EventKey><![CDATA[]]></EventKey>
</xml>
";

        private string xmlEvent_Click = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1364447020</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[CLICK]]></Event>
  <EventKey><![CDATA[SubClickRoot_Agent]]></EventKey>
</xml>
";

        private string xmlEvent_Scan = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1364447020</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[scan]]></Event>
  <EventKey><![CDATA[SCENE_VALUE]]></EventKey>
  <Ticket><![CDATA[TICKET]]></Ticket>
</xml>
";

        private string xmlEvent_View = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
  <ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
  <FromUserName><![CDATA[olPjZjsXuQPJoV0HlruZkNzKc91E]]></FromUserName>
  <CreateTime>1394805110</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[VIEW]]></Event>
  <EventKey><![CDATA[https://sdk.weixin.senparc.com]]></EventKey>
</xml>
";

        private string xmlEvent_Scancode_Push = @"<?xml version=""1.0"" encoding=""utf-8""?>
<xml>
<ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408090502</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[scancode_push]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<ScanCodeInfo><ScanType><![CDATA[qrcode]]></ScanType>
<ScanResult><![CDATA[1]]></ScanResult>
</ScanCodeInfo>
</xml>";

        private string xmlEvent_Scancode_Waitmsg = @"<xml><ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408090606</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[scancode_waitmsg]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<ScanCodeInfo><ScanType><![CDATA[qrcode]]></ScanType>
<ScanResult><![CDATA[2]]></ScanResult>
</ScanCodeInfo>
</xml>";

        private string xmlEvent_Pic_Sysphoto = @"<xml><ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408090651</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[pic_sysphoto]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<SendPicsInfo><Count>1</Count>
<PicList><item><PicMd5Sum><![CDATA[1b5f7c23b5bf75682a53e7b6d163e185]]></PicMd5Sum>
</item>
</PicList>
</SendPicsInfo>
</xml>";

        private string xmlEvent_Pic_Photo_Or_Album = @"<xml><ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408090816</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[pic_photo_or_album]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<SendPicsInfo><Count>1</Count>
<PicList><item><PicMd5Sum><![CDATA[5a75aaca956d97be686719218f275c6b]]></PicMd5Sum>
</item>
<item><PicMd5Sum><![CDATA[5a75aaca956d97be686719218f275c6b]]></PicMd5Sum>
</item>
</PicList>
</SendPicsInfo>
</xml>";

        private string xmlEvent_Pic_Weixin = @"<xml><ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408090816</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[pic_weixin]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<SendPicsInfo><Count>1</Count>
<PicList><item><PicMd5Sum><![CDATA[5a75aaca956d97be686719218f275c6b]]></PicMd5Sum>
</item>
</PicList>
</SendPicsInfo>
</xml>";

        private string xmlEvent_Location_Select = @"<xml><ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
<FromUserName><![CDATA[oMgHVjngRipVsoxg6TuX3vz6glDg]]></FromUserName>
<CreateTime>1408091189</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[location_select]]></Event>
<EventKey><![CDATA[6]]></EventKey>
<SendLocationInfo><Location_X><![CDATA[23]]></Location_X>
<Location_Y><![CDATA[113]]></Location_Y>
<Scale><![CDATA[15]]></Scale>
<Label><![CDATA[ 广州市海珠区客村艺苑路 106号]]></Label>
<Poiname><![CDATA[]]></Poiname>
</SendLocationInfo>
</xml>";

        private string xmlEvent_MassSendJobFinish = @"<xml>
<ToUserName><![CDATA[gh_a96a4a619366]]></ToUserName>
<FromUserName><![CDATA[oR5Gjjl_eiZoUpGozMo7dbBJ362A]]></FromUserName>
<CreateTime>1394524295</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[MASSSENDJOBFINISH]]></Event>
<MsgID>1988</MsgID>
<Status><![CDATA[sendsuccess]]></Status>
<TotalCount>100</TotalCount>
<FilterCount>80</FilterCount>
<SentCount>75</SentCount>
<ErrorCount>5</ErrorCount>
</xml>";

        private string xmlEvent_Card_Pass_Check = @"<xml> <ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[card_pass_check]]></Event> //不通过为card_not_pass_check
<CardId><![CDATA[cardid]]></CardId>
</xml>";

           private string xmlEvent_Card_Not_Pass_Check=@"<xml> <ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[card_not_pass_check]]></Event> //不通过为card_not_pass_check
<CardId><![CDATA[cardid]]></CardId>
</xml>";

        private string xmlEvent_User_Get_Card = @"<xml> <ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<FriendUserName><![CDATA[FriendUser]]></FriendUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[user_get_card]]></Event>
<CardId><![CDATA[cardid]]></CardId>
<IsGiveByFriend>1</IsGiveByFriend>
<UserCardCode><![CDATA[12312312]]></UserCardCode>
</xml>";

        private string xmlEvent_User_Del_Card = @"<xml> <ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[FromUser]]></FromUserName>
<CreateTime>123456789</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[user_del_card]]></Event>
<CardId><![CDATA[cardid]]></CardId>
<UserCardCode><![CDATA[12312312]]></UserCardCode>
</xml>";

        private string xmlEvent_Kf_Create_Session = @"<xml>
 <ToUserName><![CDATA[touser]]></ToUserName>
 <FromUserName><![CDATA[fromuser]]></FromUserName>
 <CreateTime>1399197672</CreateTime>
 <MsgType><![CDATA[event]]></MsgType>
 <Event><![CDATA[kf_create_session]]></Event>
 <KfAccount><![CDATA[test1@test]]></KfAccount>
 </xml>";

        private string xmlEvent_Kf_Close_Session = @"<xml>
 <ToUserName><![CDATA[touser]]></ToUserName>
 <FromUserName><![CDATA[fromuser]]></FromUserName>
 <CreateTime>1399197672</CreateTime>
 <MsgType><![CDATA[event]]></MsgType>
 <Event><![CDATA[kf_close_session]]></Event>
 <KfAccount><![CDATA[test1@test]]></KfAccount>
 </xml>";

        private string xmlEvent_Kf_Switch_Session = @"<xml>
 <ToUserName><![CDATA[touser]]></ToUserName>
 <FromUserName><![CDATA[fromuser]]></FromUserName>
 <CreateTime>1399197672</CreateTime>
 <MsgType><![CDATA[event]]></MsgType>
 <Event><![CDATA[kf_switch_session]]></Event>
 <FromKfAccount><![CDATA[test1@test]]></FromKfAccount>
 <ToKfAccount><![CDATA[test2@test]]></ToKfAccount>
 </xml>";

        private string xmlEvent_Poi_Check_Notify = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1408622107</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[poi_check_notify]]></Event>
<UniqId><![CDATA[123adb]]></UniqId>
<PoiId><![CDATA[123123]]></PoiId>
<Result><![CDATA[fail]]></Result>
<Msg><![CDATA[xxxxxx]]></Msg>
</xml>";

        private string xmlEvent_WifiConnected = @"<xml> 
<ToUserName><![CDATA[toUser]]></ToUserName> 
<FromUserName><![CDATA[FromUser]]></FromUserName> 
<CreateTime>123456789</CreateTime> 
<MsgType><![CDATA[event]]></MsgType> 
<Event><![CDATA[WifiConnected]]></Event>
<ConnectTime>0</ConnectTime>
<ExpireTime>0</ExpireTime>
<VendorId><![CDATA[3001224419]]></VendorId>
<PlaceId><![CDATA[PlaceId]]></PlaceId>
<DeviceNo><![CDATA[DeviceNo]]></DeviceNo>
</xml>";

        private string xmlEvent_User_Consume_Card = @"<xml> <ToUserName><![CDATA[toUser]]></ToUserName> 
<FromUserName><![CDATA[FromUser]]></FromUserName> 
<CreateTime>123456789</CreateTime> 
<MsgType><![CDATA[event]]></MsgType> 
<Event><![CDATA[user_consume_card]]></Event> 
<CardId><![CDATA[cardid]]></CardId> 
<UserCardCode><![CDATA[12312312]]></UserCardCode>
<ConsumeSource><![CDATA[(FROM_API)]]></ConsumeSource>
</xml>";

        private string xmlEvent_User_Enter_Session_From_Card = @"<xml> <ToUserName><![CDATA[toUser]]></ToUserName> 
<FromUserName><![CDATA[FromUser]]></FromUserName> 
<CreateTime>123456789</CreateTime> 
<MsgType><![CDATA[event]]></MsgType> 
<Event><![CDATA[user_enter_session_from_card]]></Event> 
<CardId><![CDATA[cardid]]></CardId> 
<UserCardCode><![CDATA[12312312]]></UserCardCode>
</xml>";

        private string xmlEvent_User_View_Card = @"<xml> <ToUserName><![CDATA[toUser]]></ToUserName> 
<FromUserName><![CDATA[FromUser]]></FromUserName> 
<CreateTime>123456789</CreateTime> 
<MsgType><![CDATA[event]]></MsgType> 
<Event><![CDATA[user_view_card]]></Event> 
<CardId><![CDATA[cardid]]></CardId> 
<UserCardCode><![CDATA[12312312]]></UserCardCode>
</xml>";

        private string xmlEvent_Merchant_Order = @"<xml>
<ToUserName><![CDATA[weixin_media1]]></ToUserName>
<FromUserName><![CDATA[oDF3iYyVlek46AyTBbMRVV8VZVlI]]></FromUserName>
<CreateTime>1398144192</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[merchant_order]]></Event>
<OrderId><![CDATA[test_order_id]]></OrderId>
<OrderStatus>2</OrderStatus>
<ProductId><![CDATA[test_product_id]]></ProductId>
<SkuInfo><![CDATA[10001:1000012;10002:100021]]></SkuInfo>
</xml>
";

        private string xmlEvent_Submit_Membercard_User_Info = @"<xml>
  <ToUserName> <![CDATA[gh_3fcea188bf78]]></ToUserName>
  <FromUserName><![CDATA[obLatjlaNQKb8FqOvt1M1x1lIBFE]]></FromUserName>
  <CreateTime>1432668700</CreateTime>
  <MsgType><![CDATA[event]]></MsgType>
  <Event><![CDATA[submit_membercard_user_info]]></Event>
  <CardId><![CDATA[pbLatjtZ7v1BG_ZnTjbW85GYc_E8]]></CardId>
  <UserCardCode><![CDATA[018255396048]]></UserCardCode>
  </xml>";

        private string xmlEvent_ShakearoundUserShake = @"<xml>
<ToUserName><![CDATA[toUser]]></ToUserName>
<FromUserName><![CDATA[fromUser]]></FromUserName>
<CreateTime>1433332012</CreateTime>
    <MsgType><![CDATA[event]]></MsgType>
    <Event><![CDATA[ShakearoundUserShake]]></Event>
    <ChosenBeacon>
        <Uuid><![CDATA[uuid]]></Uuid>
        <Major>12345</Major>
        <Minor>54321</Minor>
        <Distance>0.057</Distance>
    </ChosenBeacon>
    <AroundBeacons>
        <AroundBeacon>
            <Uuid><![CDATA[uuid]]></Uuid>
            <Major>12345</Major>
            <Minor>54321</Minor>
            <Distance>166.816</Distance>
        </AroundBeacon>
        <AroundBeacon>
            <Uuid><![CDATA[uuid]]></Uuid>
            <Major>12345</Major>
            <Minor>54321</Minor>
            <Distance>15.013</Distance>
        </AroundBeacon>
    </AroundBeacons>
</xml>";

        private string xmlEvent_Modify_Store_Audit_Info = @"<xml>
<ToUserName><![CDATA[gh_4346ac1514d8]]></ToUserName>
<FromUserName><![CDATA[od1P50M-fNQI5Gcq-trm4a7apsU8]]></FromUserName>
<CreateTime>1488856741</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[modify_store_audit_info]]></Event>
<audit_id>11111</audit_id>
<status>3</status>
<reason><![CDATA[xxx]]></reason>
</xml>";

        private string xmlEvent_Add_Store_Audit_Info = @"<xml>
<ToUserName><![CDATA[gh_4346ac1514d8]]></ToUserName>
<FromUserName><![CDATA[od1P50M-fNQI5Gcq-trm4a7apsU8]]></FromUserName>
<CreateTime>1488856741</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[add_store_audit_info]]></Event>
<audit_id>11111</audit_id>
<status>3</status>
<reason><![CDATA[xxx]]></reason>
<is_upgrade>3</is_upgrade>
<poiid>12344</poiid>
</xml>";
        private string xmlEvent_Create_Map_Poi_Audit_Info = @"<xml>
<ToUserName><![CDATA[gh_4346ac1514d8]]></ToUserName>
<FromUserName><![CDATA[od1P50M-fNQI5Gcq-trm4a7apsU8]]></FromUserName>
<CreateTime>1488856741</CreateTime>
<MsgType><![CDATA[event]]></MsgType>
<Event><![CDATA[create_map_poi_audit_info]]></Event>
<audit_id>11111</audit_id>
<status>1</status>
<map_poi_id><![CDATA[xxx]]></map_poi_id>
<name><![CDATA[xxx]]></name>
<address><![CDATA[xxx]]></address>
<latitude><![CDATA[134]]></latitude>
<longitude><![CDATA[30]]></longitude>
<sh_remark><![CDATA[xxx]]></sh_remark>
</xml>";
        private string xmlEvent_Apply_Merchant_Audit_InfoRequest = @"<xml>
    <ToUserName><![CDATA[gh_4346ac1514d8]]></ToUserName>
    <FromUserName><![CDATA[od1P50M-fNQI5Gcq-trm4a7apsU8]]></FromUserName>
    <CreateTime>1488856741</CreateTime>
    <MsgType><![CDATA[event]]></MsgType>
    <Event><![CDATA[apply_merchant_audit_info]]></Event>
    <audit_id>11111</audit_id>
    <status>3</status>
    <reason><![CDATA[xxx]]></reason>
</xml>";

        [TestMethod]
        public void GetRequestEntityTest()
        {
            var dt = DateTimeHelper.BaseTime.AddTicks(((long)1358061152 + 8 * 60 * 60) * 10000000);

            {
                //Text
                var doc = XDocument.Parse(xmlText);
                var result = RequestMessageFactory.GetRequestEntity(doc);
                Assert.IsInstanceOfType(result, typeof(RequestMessageText));
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual("TNT2", (result as RequestMessageText).Content);
            }

            {
                //Location
                var doc = XDocument.Parse(xmlLocation);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageLocation;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(19, result.Scale);
            }

            {
                //Image
                var doc = XDocument.Parse(xmlImage);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageImage;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual("http://mmsns.qpic.cn/mmsns/ZxBXNzgHyUqazGkXUvujSPxexzynJAicf440qkyLibBd1OEO4saJiavLQ/0", result.PicUrl);
            }

            {
                //Voice
                var doc = XDocument.Parse(xmlVoice);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageVoice;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual("X1yfgB2XI-faU6R2jmKz0X1JZmPCxIvM-9ktt4K92BB9577SCi41S-qMl60q5DJo", result.MediaId);
            }

            {
                //Video
                var doc = XDocument.Parse(xmlVideo);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageVideo;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual("mediaId", result.MediaId);
                Assert.AreEqual("thumbMediaId", result.ThumbMediaId);
            }

            {
                //ShortVideo
                var doc = XDocument.Parse(xmlShortVideo);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageShortVideo;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual("media_id", result.MediaId);
                Assert.AreEqual("thumb_media_id", result.ThumbMediaId);
            }

            {
                //Link
                var doc = XDocument.Parse(xmlLink);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageLink;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual("公众平台官网链接", result.Title);
                Assert.AreEqual("Senparc.Weixin.MP SDK公众平台官网链接", result.Description);
                Assert.AreEqual("https://sdk.weixin.senparc.com", result.Url);
            }

            {
                //Event-ENTRY
                var doc = XDocument.Parse(xmlEvent_Enter);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Enter;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.ENTER, result.Event);
            }

            {
                //Event-Location
                var doc = XDocument.Parse(xmlEvent_Location);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Location;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.LOCATION, result.Event);
                Assert.AreEqual(23.137466, result.Latitude);
                Assert.AreEqual(113.352425, result.Longitude);
                Assert.AreEqual(119.385040, result.Precision);
            }

            {
                //Event-Subscribe
                var doc = XDocument.Parse(xmlEvent_Subscribe);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Subscribe;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.subscribe, result.Event);
            }

            {
                //Event-Unsubscribe
                var doc = XDocument.Parse(xmlEvent_Unsubscribe);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Unsubscribe;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.unsubscribe, result.Event);
                Assert.AreEqual(new DateTime(2013, 3, 28), result.CreateTime.Date);
            }

            {
                //Event-CLICK
                var doc = XDocument.Parse(xmlEvent_Click);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Click;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.CLICK, result.Event);
                Assert.AreEqual(new DateTime(2013, 3, 28), result.CreateTime.Date);

                Assert.AreEqual("SubClickRoot_Agent", result.EventKey);
            }

            {
                //Event-scan
                var doc = XDocument.Parse(xmlEvent_Scan);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Scan;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.scan, result.Event);
                Assert.AreEqual(new DateTime(2013, 3, 28), result.CreateTime.Date);

                Assert.AreEqual("SCENE_VALUE", result.EventKey);
                Assert.AreEqual("TICKET", result.Ticket);
            }

            {
                //Event-VIEW
                var doc = XDocument.Parse(xmlEvent_View);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_View;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.VIEW, result.Event);
                Assert.AreEqual(new DateTime(2014, 3, 14), result.CreateTime.Date);
                Assert.AreEqual("https://sdk.weixin.senparc.com", result.EventKey);
            }
            
            {
                //Event-Scancode_Push
                var doc = XDocument.Parse(xmlEvent_Scancode_Push);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Scancode_Push;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.scancode_push, result.Event);
                Assert.IsNotNull(result.ScanCodeInfo.ScanResult);
            }

            {
                //Event-Scancode_Scancode_Waitmsg
                var doc = XDocument.Parse(xmlEvent_Scancode_Waitmsg);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Scancode_Waitmsg;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.scancode_waitmsg, result.Event);
                Assert.IsNotNull(result.ScanCodeInfo.ScanResult);
            }

            {
                //Event-Scancode_Pic_Sysphoto
                var doc = XDocument.Parse(xmlEvent_Pic_Sysphoto);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Pic_Sysphoto;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.pic_sysphoto, result.Event);
                Assert.IsNotNull(result.SendPicsInfo.PicList);
            }

            {
                //Event-Scancode_Pic_Photo_Or_Album
                var doc = XDocument.Parse(xmlEvent_Pic_Photo_Or_Album);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Pic_Photo_Or_Album;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.pic_photo_or_album, result.Event);
                Assert.IsNotNull(result.SendPicsInfo.PicList);
            }

            {
                //Event-Scancode_Pic_Weixin
                var doc = XDocument.Parse(xmlEvent_Pic_Weixin);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Pic_Weixin;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.pic_weixin, result.Event);
                Assert.IsNotNull(result.SendPicsInfo.PicList);
            }

            {
                //Event-Location_Select
                var doc = XDocument.Parse(xmlEvent_Location_Select);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Location_Select;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.location_select, result.Event);
                Assert.IsNotNull(result.SendLocationInfo.Location_X);
            }

            {
                //Event-MassSendJobFinish
                var doc = XDocument.Parse(xmlEvent_MassSendJobFinish);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_MassSendJobFinish;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_a96a4a619366", result.ToUserName);
                Assert.AreEqual(Event.MASSSENDJOBFINISH, result.Event);
                Assert.AreEqual(result.SentCount, 75);
                Assert.AreEqual(result.ErrorCount, 5);
                Assert.IsNotNull(result.MsgID);
                Assert.AreEqual(1988, result.MsgID);
            }

            {
                //Event-Card_Pass_Check
                var doc = XDocument.Parse(xmlEvent_Card_Pass_Check);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Card_Pass_Check;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual(Event.card_pass_check, result.Event);
                Assert.IsNotNull(result.CardId);
                Assert.AreEqual("cardid", result.CardId);
            }

            {
                //Event-Card_Not_Pass_Check
                var doc = XDocument.Parse(xmlEvent_Card_Not_Pass_Check);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Card_Not_Pass_Check;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual(Event.card_not_pass_check, result.Event);
                Assert.IsNotNull(result.CardId);
                Assert.AreEqual("cardid", result.CardId);
            }

            {
                //Event-User_Get_Card
                var doc = XDocument.Parse(xmlEvent_User_Get_Card);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_User_Get_Card;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual(Event.user_get_card, result.Event);
                Assert.IsNotNull(result.CardId);
                Assert.AreEqual(1, result.IsGiveByFriend);
            }

            {
                //Event-User_Del_Card
                var doc = XDocument.Parse(xmlEvent_User_Del_Card);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_User_Del_Card;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual(Event.user_del_card, result.Event);
                Assert.IsNotNull(result.CardId);
                Assert.AreEqual("12312312", result.UserCardCode);
            }

            {
                //Event-Kf_Create_Session
                var doc = XDocument.Parse(xmlEvent_Kf_Create_Session);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Kf_Create_Session;
                Assert.IsNotNull(result);
                Assert.AreEqual("touser", result.ToUserName);
                Assert.AreEqual(Event.kf_create_session, result.Event);
                Assert.AreEqual("test1@test", result.KfAccount);
            }

            {
                //Event-Kf_Close_Session
                var doc = XDocument.Parse(xmlEvent_Kf_Close_Session);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Kf_Close_Session;
                Assert.IsNotNull(result);
                Assert.AreEqual("touser", result.ToUserName);
                Assert.AreEqual(Event.kf_close_session, result.Event);
                Assert.AreEqual("test1@test", result.KfAccount);
            }

            {
                //Event-Kf_Switch_Session
                var doc = XDocument.Parse(xmlEvent_Kf_Switch_Session);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Kf_Switch_Session;
                Assert.IsNotNull(result);
                Assert.AreEqual("touser", result.ToUserName);
                Assert.AreEqual(Event.kf_switch_session, result.Event);
                Assert.AreEqual("test1@test", result.FromKfAccount);
                Assert.AreEqual("test2@test", result.ToKfAccount);
            }

            {
                //Event-Poi_Check_Notify
                var doc = XDocument.Parse(xmlEvent_Poi_Check_Notify);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Poi_Check_Notify;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual(Event.poi_check_notify, result.Event);
                Assert.AreEqual("123adb", result.UniqId);
                Assert.AreEqual("fail", result.Result);
            }

            {
                //Event-WifiConnected
                var doc = XDocument.Parse(xmlEvent_WifiConnected);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_WifiConnected;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual(Event.WifiConnected, result.Event);
                Assert.AreEqual("PlaceId", result.PlaceId);
                Assert.AreEqual("3001224419", result.VendorId);
            }

            {
                //Event-User_Consume_Card
                var doc = XDocument.Parse(xmlEvent_User_Consume_Card);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_User_Consume_Card;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual(Event.user_consume_card, result.Event);
                Assert.AreEqual("cardid", result.CardId);
            }

            {
                //Event-User_Enter_Session_From_Card
                var doc = XDocument.Parse(xmlEvent_User_Enter_Session_From_Card);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_User_Enter_Session_From_Card;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual(Event.user_enter_session_from_card, result.Event);
                Assert.AreEqual("cardid", result.CardId);
            }

            {
                //Event-User_View_Card
                var doc = XDocument.Parse(xmlEvent_User_View_Card);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_User_View_Card;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual(Event.user_view_card, result.Event);
                Assert.AreEqual("cardid", result.CardId);
            }

            {
                //Event-Merchant_Order
                var doc = XDocument.Parse(xmlEvent_Merchant_Order);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Merchant_Order;
                Assert.IsNotNull(result);
                Assert.AreEqual("weixin_media1", result.ToUserName);
                Assert.AreEqual(Event.merchant_order, result.Event);
                Assert.AreEqual("test_product_id", result.ProductId);
            }

            {
                //Event-Submit_Membercard_User_Info
                var doc = XDocument.Parse(xmlEvent_Submit_Membercard_User_Info);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_Submit_Membercard_User_Info;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_3fcea188bf78", result.ToUserName);
                Assert.AreEqual(Event.submit_membercard_user_info, result.Event);
                Assert.AreEqual("pbLatjtZ7v1BG_ZnTjbW85GYc_E8", result.CardId);
            }

            {
                //Event-ShakearoundUserShake
                var doc = XDocument.Parse(xmlEvent_ShakearoundUserShake);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_ShakearoundUserShake;
                Assert.IsNotNull(result);
                Assert.AreEqual("toUser", result.ToUserName);
                Assert.AreEqual(Event.ShakearoundUserShake, result.Event);
                Assert.AreEqual(12345, result.ChosenBeacon.Major);
                Assert.AreEqual(54321, result.ChosenBeacon.Minor);
                Assert.AreEqual(2, result.AroundBeacons.Count);
                Assert.AreEqual(15.013, result.AroundBeacons.ElementAt(1).Distance);
            }

            {
                //Event-Apply_Merchant_Audit_InfoRequest
                var doc = XDocument.Parse(xmlEvent_Apply_Merchant_Audit_InfoRequest);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_ApplyMerchantAuditInfo;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_4346ac1514d8", result.ToUserName);
                Assert.AreEqual(Event.apply_merchant_audit_info, result.Event);
                Assert.AreEqual(11111, result.audit_id);
                Assert.AreEqual(3, result.status);
                Assert.AreEqual("xxx", result.reason);
            }

            {
                //Event-Apply_CreateMapPoiAuditInfo
                var doc = XDocument.Parse(xmlEvent_Create_Map_Poi_Audit_Info);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_CreateMapPoiAuditInfo;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_4346ac1514d8", result.ToUserName);
                Assert.AreEqual(Event.create_map_poi_audit_info, result.Event);
                Assert.AreEqual(11111, result.audit_id);
                Assert.AreEqual(1, result.status);
                Assert.AreEqual("xxx", result.map_poi_id);
                Assert.AreEqual("xxx", result.name);
                Assert.AreEqual("xxx", result.address);
                Assert.AreEqual(134, result.latitude);
                Assert.AreEqual(30, result.longitude);
                Assert.AreEqual("xxx", result.sh_remark);
            }

            {
                //Event-Apply_AddStoreAuditInfo
                var doc = XDocument.Parse(xmlEvent_Add_Store_Audit_Info);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_AddStoreAuditInfo;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_4346ac1514d8", result.ToUserName);
                Assert.AreEqual(Event.add_store_audit_info, result.Event);
                Assert.AreEqual(11111, result.audit_id);
                Assert.AreEqual(3, result.is_upgrade);
                Assert.AreEqual("12344", result.poiid);
                Assert.AreEqual(3, result.status);
                Assert.AreEqual("xxx", result.reason);
            }

            {
                //Event-Apply_AddStoreAuditInfo
                var doc = XDocument.Parse(xmlEvent_Modify_Store_Audit_Info);
                var result = RequestMessageFactory.GetRequestEntity(doc) as RequestMessageEvent_ModifyStoreAuditInfo;
                Assert.IsNotNull(result);
                Assert.AreEqual("gh_4346ac1514d8", result.ToUserName);
                Assert.AreEqual(Event.modify_store_audit_info, result.Event);
                Assert.AreEqual(11111, result.audit_id);
                Assert.AreEqual(3, result.status);
                Assert.AreEqual("xxx", result.reason);
            }
        }
    }
}
