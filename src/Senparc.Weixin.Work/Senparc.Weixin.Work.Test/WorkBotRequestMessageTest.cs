using Senparc.Weixin.Work.Entities;
using Senparc.CO2NET.Helpers.Serializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Helpers;


namespace Senparc.Weixin.Work.Test
{
    [TestClass]
    public class WorkBotRequestMessageTest
    {
        /// <summary>
        /// 测试文本消息
        /// </summary>
        [TestMethod]
        public void GetJSONRequestEntityTextTest()
        {
            string json = """
{
    "msgid": "CAIQ16HMjQYY\/NGagIOAgAMgq4KM0AI=",
    "aibotid": "AIBOTID",
    "chatid": "CHATID",
    "chattype": "group",
    "from": {
        "userid": "USERID"
    },
    "msgtype": "text",
    "text": {
        "content": "@RobotA hello robot"
    }
}
""";
        var jsonObject = json.GetObject<BotRequestMessageText>();
        Assert.IsNotNull(jsonObject);
        Assert.AreEqual("CAIQ16HMjQYY/NGagIOAgAMgq4KM0AI=", jsonObject.msgid);
        Assert.AreEqual("AIBOTID", jsonObject.aibotid);
        Assert.AreEqual("CHATID", jsonObject.chatid);
        Assert.AreEqual("group", jsonObject.chattype);
        Assert.AreEqual("USERID", jsonObject.from.userid);
        Assert.AreEqual("text", jsonObject.msgtype);
        Assert.AreEqual("@RobotA hello robot", jsonObject.text.content);
        }

        /// <summary>
        /// 测试图片消息
        /// </summary>
        [TestMethod]
        public void GetJSONRequestEntityImageTest()
        {
            string json = """
            {
    "msgid": "CAIQz7/MjQYY/NGagIOAgAMgl8jK/gI=",
    "aibotid": "AIBOTID",
    "chatid": "CHATID",
    "chattype": "group",
    "from": {
        "userid": "USERID"
    },
    "msgtype": "image",
    "image": {
        "url": "https://ww-aibot-img-1258476243.cos.ap-guangzhou.myqcloud.com/BHoPdA3/cdnimg_e2e243ba85a2d05ef201392de7291a.png?sign=q-sign-algorithm%3Dsha1%26q-ak%3DAKIDbBpaJvdLBvpnibcYcfyPuaO5f9U1UoGo%26q-sign-time%3D1733467811%3B1733468111%26q-key-time%3D1733467811%3B1733468111%26q-header-list%3Dhost%26q-url-param-list%3D%26q-signature%3D0f7b6576943685f82870bc8db306dbf09dfe0fd6"
    }
}
""";
        var jsonObject = json.GetObject<BotRequestMessageImage>();
        Assert.IsNotNull(jsonObject);
        Assert.AreEqual("CAIQz7/MjQYY/NGagIOAgAMgl8jK/gI=", jsonObject.msgid);
        Assert.AreEqual("AIBOTID", jsonObject.aibotid);
        Assert.AreEqual("CHATID", jsonObject.chatid);
        Assert.AreEqual("group", jsonObject.chattype);
        Assert.AreEqual("USERID", jsonObject.from.userid);
        Assert.AreEqual("image", jsonObject.msgtype);
        Assert.AreEqual("https://ww-aibot-img-1258476243.cos.ap-guangzhou.myqcloud.com/BHoPdA3/cdnimg_e2e243ba85a2d05ef201392de7291a.png?sign=q-sign-algorithm%3Dsha1%26q-ak%3DAKIDbBpaJvdLBvpnibcYcfyPuaO5f9U1UoGo%26q-sign-time%3D1733467811%3B1733468111%26q-key-time%3D1733467811%3B1733468111%26q-header-list%3Dhost%26q-url-param-list%3D%26q-signature%3D0f7b6576943685f82870bc8db306dbf09dfe0fd6", jsonObject.image.url);
        }

        /// <summary>
        /// 测试混合消息
        /// </summary>
        [TestMethod]
        public void GetJSONRequestEntityMixedTest()
        {
            string json = """
{
    "msgid": "CAIQrcjMjQYY/NGagIOAgAMg6PDc/w0=",
    "aibotid": "AIBOTID",
    "chatid": "CHATID",
    "chattype": "group",
    "from": {
        "userid": "USERID"
    },
    "msgtype": "mixed",
    "mixed": {
        "msg_item": [
            {
                "msgtype": "text",
                "text": {
                    "content": "@机器人 这是今日的测试情况"
                }
            },
            {
                "msgtype": "image",
                "image": {
                    "url": "https://ww-aibot-img-1258476243.cos.ap-guangzhou.myqcloud.com/BHoPdA3/cdnimg_e2e243ba85a2d05ef201392de7291a.png?sign=q-sign-algorithm%3Dsha1%26q-ak%3DAKIDbBpaJvdLBvpnibcYcfyPuaO5f9U1UoGo%26q-sign-time%3D1733467811%3B1733468111%26q-key-time%3D1733467811%3B1733468111%26q-header-list%3Dhost%26q-url-param-list%3D%26q-signature%3D0f7b6576943685f82870bc8db306dbf09dfe0fd6"
                }
            }
        ]
    }
}
""";
        var jsonObject = json.GetObject<BotRequestMessageMixed>();
        Assert.IsNotNull(jsonObject);
        Assert.AreEqual("CAIQrcjMjQYY/NGagIOAgAMg6PDc/w0=", jsonObject.msgid);
        Assert.AreEqual("AIBOTID", jsonObject.aibotid);
        Assert.AreEqual("CHATID", jsonObject.chatid);
        Assert.AreEqual("group", jsonObject.chattype);
        Assert.AreEqual("USERID", jsonObject.from.userid);
        Assert.AreEqual("mixed", jsonObject.msgtype);
        Assert.AreEqual("text", jsonObject.mixed.msg_item[0].msgtype);
        Assert.AreEqual("@机器人 这是今日的测试情况", jsonObject.mixed.msg_item[0].text.content);
        Assert.AreEqual("image", jsonObject.mixed.msg_item[1].msgtype);
        Assert.AreEqual("https://ww-aibot-img-1258476243.cos.ap-guangzhou.myqcloud.com/BHoPdA3/cdnimg_e2e243ba85a2d05ef201392de7291a.png?sign=q-sign-algorithm%3Dsha1%26q-ak%3DAKIDbBpaJvdLBvpnibcYcfyPuaO5f9U1UoGo%26q-sign-time%3D1733467811%3B1733468111%26q-key-time%3D1733467811%3B1733468111%26q-header-list%3Dhost%26q-url-param-list%3D%26q-signature%3D0f7b6576943685f82870bc8db306dbf09dfe0fd6", jsonObject.mixed.msg_item[1].image.url);
        }

        /// <summary>
        /// 测试卡片消息
        /// </summary>
        [TestMethod]
        public void GetJSONRequestEntityStreamTest()
        {
            string json = """
            {
    "msgid": "CAIQz7/MjQYY/NGagIOAgAMgl8jK/gI=",
    "aibotid": "AIBOTID",
    "chatid": "CHATID",
    "chattype": "group",
    "from": {
        "userid": "USERID"
    },
    "msgtype": "stream",
    "stream": {
        "id": "STREAMID"
    }
}
""";
        var jsonObject = json.GetObject<BotRequestMessageStream>();
        Assert.IsNotNull(jsonObject);
        Assert.AreEqual("CAIQz7/MjQYY/NGagIOAgAMgl8jK/gI=", jsonObject.msgid);
        Assert.AreEqual("AIBOTID", jsonObject.aibotid);
        Assert.AreEqual("CHATID", jsonObject.chatid);
        Assert.AreEqual("group", jsonObject.chattype);
        Assert.AreEqual("USERID", jsonObject.from.userid);
        Assert.AreEqual("stream", jsonObject.msgtype);
        Assert.AreEqual("STREAMID", jsonObject.stream.id);
        }
    }

    [TestClass]
    public class BotRequestEventTest
    {
        [TestMethod]
        public void EnterChatEventTest()
        {
            string json = """
            {
    "msgid": "CAIQ16HMjQYY\/NGagIOAgAMgq4KM0AI=",
	"create_time":1700000000,
    "aibotid": "AIBOTID",
    "from": {
		"corpid": "wpxxxx",
        "userid": "USERID"
    },
    "msgtype": "event",
    "event": {
        "eventtype": "enter_chat"
    }
}
""";
        var jsonObject = json.GetObject<BotRequestMessageEvent_Enter>();
        Assert.IsNotNull(jsonObject);
        Assert.AreEqual("CAIQ16HMjQYY/NGagIOAgAMgq4KM0AI=", jsonObject.msgid);
        Assert.AreEqual(1700000000, jsonObject.create_time);
        Assert.AreEqual("AIBOTID", jsonObject.aibotid);
        Assert.AreEqual("wpxxxx", jsonObject.from.corpid);
        Assert.AreEqual("USERID", jsonObject.from.userid);
        Assert.AreEqual("event", jsonObject.msgtype);
        Assert.AreEqual("enter_chat", jsonObject.@event.eventtype);
        }

        [TestMethod]
        public void TemplateCardEventTest()
        {
            string json = """
            {
    "msgid": "CAIQ16HMjQYY\/NGagIOAgAMgq4KM0AI=",
    "create_time": 1700000000,
    "aibotid": "AIBOTID",
    "from": {
        "corpid": "CORPID",
        "userid": "USERID"
    },
    "chatid": "CHATID",
    "chattype": "group",
    "msgtype": "event",
    "event": {
        "eventtype": "template_card_event",
        "template_card_event": {
            "card_type": "vote_interaction",
            "event_key": "button_replace_text",
            "taskid": "fBmjTL7ErRCQSNA6GZKMlcFiWX1shOvg",
            "selected_items": {
                "selected_item": [
                    {
                        "question_key": "button_selection_key1",
                        "option_ids": {
                            "option_id": [
                                "button_selection_id1"
                            ]
                        }
                    }
                ]
            }
        }
    }
}
""";
        var jsonObject = json.GetObject<BotRequestMessageEvent_TemplateCardEvent>();
        Assert.IsNotNull(jsonObject);
        Assert.AreEqual("CAIQ16HMjQYY/NGagIOAgAMgq4KM0AI=", jsonObject.msgid);
        Assert.AreEqual(1700000000, jsonObject.create_time);
        Assert.AreEqual("AIBOTID", jsonObject.aibotid);
        Assert.AreEqual("CORPID", jsonObject.from.corpid);
        Assert.AreEqual("USERID", jsonObject.from.userid);
        Assert.AreEqual("CHATID", jsonObject.chatid);
        Assert.AreEqual("group", jsonObject.chattype);
        Assert.AreEqual("event", jsonObject.msgtype);
        Assert.AreEqual("template_card_event", jsonObject.@event.eventtype);
        Assert.AreEqual("vote_interaction", jsonObject.@event.template_card_event.card_type);
        Assert.AreEqual("button_replace_text", jsonObject.@event.template_card_event.event_key);
        Assert.AreEqual("fBmjTL7ErRCQSNA6GZKMlcFiWX1shOvg", jsonObject.@event.template_card_event.taskid);
        Assert.AreEqual("button_selection_key1", jsonObject.@event.template_card_event.selected_items.selected_item[0].question_key);
        Assert.AreEqual("button_selection_id1", jsonObject.@event.template_card_event.selected_items.selected_item[0].option_ids.option_id[0]);
        }
    }
}