升级记录
----------
v0.3.5 /2013-4-17

添加RequestMessageLink用于接收处理link类型的信息。同时MessageHandler也增加了对应的OnLinkRequest处理方法。

v0.3.4.2 /2013-4-17
修改GoogleMapHelper.GetGoogleStaticMap()方法，将List<Markers> markersList类型改为IList<Markers>。


v0.3.4 /2013-4-8

将IRequestMessageBase及IResponseMessageBase下的MsgType设为只读，这样所有子类的MsgType都会在开发的时候被确定下来，不用初始化之后再重复设置。

v0.3 /2013-4-5

添加MessageHandler处理类，简化二次开发时对信息的处理流程。
