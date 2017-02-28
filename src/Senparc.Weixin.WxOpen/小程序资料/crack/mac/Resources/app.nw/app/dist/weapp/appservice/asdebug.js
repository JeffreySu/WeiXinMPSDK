"use strict";
! function() {
    function e(e) {
        var o = JSON.parse(JSON.stringify(e));
        o.to = "backgroundjs", o.comefrom = "webframe", o.command = "COMMAND_FROM_ASJS", o.appid = C, o.appname = R, o.apphash = W, o.webviewID = q, window.postMessage(o, "*")
    }

    function o(e) {
        e.command = "COMMAND_FROM_ASJS", e.appid = C, e.appname = R, e.apphash = W, e.webviewID = q;
        var o = "____sdk____" + JSON.stringify(e),
            n = prompt(o);
        n = JSON.parse(n), delete n.to, a(n)
    }

    function n(e) {
        e.to = "contentscript", e.comefrom = "webframe", e.webviewID = q, window.postMessage(e, "*")
    }

    function t() {
        var e = Math.random();
        return L[e] ? initMappingID() : e
    }

    function r(n, r, a) {
        var i = t();
        L[i] = a;
        var s = /Sync$/.test(n),
            c = {
                sdkName: n,
                args: r,
                callbackID: i
            };
        s ? o(c) : e(c)
    }

    function a(o) {
        var n = o.command;
        delete o.command;
        var t = o.msg || {},
            r = o.ext || {};
        if ("WINDOW_GET_WEBAPP_ERROR" === n) {
            var a = t.fileName,
                i = t.errStr;
            return console.group("%c加载 " + a + " 错误", "color: red; font-size: x-large"), console.error("%c" + i, "color: red; font-size: x-large"), void console.groupEnd()
        }
        if ("MSG_FROM_WEBVIEW" === n || "GET_ASSDK_RES" === n) {
            var s = t.eventName || r.sdkName;
            O && (console.group(new Date + " GetMsg " + s), console.debug(s, t, r), console.groupEnd()), B.push({
                type: "GetMsg",
                eventName: s,
                data: [s, t, r],
                timesmap: new Date
            })
        }
        if ("MSG_FROM_WEBVIEW" === n) {
            var c = t.eventName,
                u = t.type,
                p = t.data || {};
            p.webviewId = t.webviewID, "ON_APPLIFECYCLE_EVENT" === u ? S(c, p) : "ON_MUSIC_EVENT" === u && A(c, p), WeixinJSBridge._subscribe[c] && WeixinJSBridge._subscribe[c](p, p.webviewId)
        } else if ("GET_ASSDK_RES" === n) {
            var d = r.callbackID;
            L[d](t), delete L[d]
        } else if ("GET_APP_DATA" === n) e({
            appData: __wxAppData,
            sdkName: "send_app_data"
        });
        else if ("WRITE_APP_DATA" === n)
            for (var l in t) {
                var g = t[l],
                    f = g.__webviewId__;
                WeixinJSBridge.publish("appDataChange", {
                    data: {
                        data: g
                    }
                }, [f], !0)
            }
    }

    function i(e, o) {
        try {
            for (var n = F.projectConfig, t = n.Network, r = "webscoket" === o ? t.WsRequestDomain : t.RequestDomain, a = 0; a < r.length; a++)
                if (0 === e.indexOf(r[a])) return !0
        } catch (i) {
            return console.error(i), !1
        }
    }

    function s(e, o, n) {
        if (G++, G > I) return G--, n && n({
            errMsg: "request:fail;"
        }), void console.error("%c 最多同时发起 " + I + " 个 wx.request 请求", "color: red; font-size: x-large");
        var t = o.url,
            r = o.header || {};
        if (!i(t)) return G--, n && n({
            errMsg: "request:fail;"
        }), void console.error("%c URL 域名不合法，请在 mp 后台配置后重试", "color: red; font-size: x-large");
        var a, s = new XMLHttpRequest,
            c = o.method || "POST",
            u = (o.complete, F.networkTimeout && F.networkTimeout.request);
        s.open(c, o.url, !0), s.onreadystatechange = function() {
            if (3 == s.readyState, 4 == s.readyState) {
                s.onreadystatechange = null;
                var e = s.status;
                0 == e ? n && n({
                    errMsg: "request:fail"
                }) : n && n({
                    errMsg: "request:ok",
                    data: s.responseText,
                    statusCode: e
                }), G--, a && clearTimeout(a)
            }
        };
        var p = !1;
        for (var d in r)
            if (r.hasOwnProperty(d)) {
                var l = d.toLowerCase();
                p = "content-type" == l || p, "cookie" === l ? s.setRequestHeader("_Cookie", r[d]) : s.setRequestHeader(d, r[d])
            }
            "POST" != c || p || s.setRequestHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8"), s.setRequestHeader("X-Requested-With", "XMLHttpRequest"), "number" == typeof u && (a = setTimeout(function() {
            s.abort("timeout"), o.complete && o.complete(), o.complete = null, G--, n && n({
                errMsg: "request:fail"
            })
        }, u));
        var g = "string" == typeof o.data ? o.data : null;
        try {
            s.send(g)
        } catch (f) {
            G--, n && n({
                errMsg: "request:fail"
            })
        }
    }

    function c(e, o) {
        var n = K[e];
        n && o && n.push(o)
    }

    function u(e, o) {
        var n = K[e],
            t = !0,
            r = !1,
            a = void 0;
        try {
            for (var i, s = n[Symbol.iterator](); !(t = (i = s.next()).done); t = !0) {
                var c = i.value;
                c(o)
            }
        } catch (u) {
            r = !0, a = u
        } finally {
            try {
                !t && s["return"] && s["return"]()
            } finally {
                if (r) throw a
            }
        }
    }

    function p(e, o, n) {
        var t = o.url,
            r = o.header;
        if (!i(t, "webscoket")) return n && n({
            errMsg: "closeSocket:fail"
        }), void console.error("%c URL 域名不合法，请在 mp 后台配置后，重启项目继续测试", "color: red; font-size: x-large");
        j = new WebSocket(t);
        for (var a in r) r.hasOwnProperty(a);
        j.onopen = function() {
            u("open")
        }, j.onmessage = function(e) {
            u("message", {
                data: e.data
            })
        }, j.onclose = function(e) {
            u("close", e)
        }, j.onerror = function(e) {
            u("error", e)
        }, n && n({
            errMsg: "connectSocket:ok"
        })
    }

    function d(e, o, n) {
        j ? (j.close(), j = null, n && n({
            errMsg: "closeSocket:ok"
        })) : n && n({
            errMsg: "closeSocket:fail"
        })
    }

    function l(e, o, n) {
        var t = o.data;
        if (j) try {
            j.send(t), n && n({
                errMsg: "sendSocketMessage:ok"
            })
        } catch (r) {
            n && n({
                errMsg: "sendSocketMessage:fail," + r.message
            })
        } else n && n({
            errMsg: "sendSocketMessage:fail"
        })
    }

    function g(e, o) {
        c("open", o)
    }

    function f(e, o) {
        c("message", o)
    }

    function v(e, o) {
        c("error", o)
    }

    function w(e, o) {
        c("close", o)
    }

    function m(e, o) {
        var n = U[e];
        n && o && n.push(o)
    }

    function S(e, o) {
        var n = U[e],
            t = !0,
            r = !1,
            a = void 0;
        try {
            for (var i, s = n[Symbol.iterator](); !(t = (i = s.next()).done); t = !0) {
                var c = i.value;
                c(o)
            }
        } catch (u) {
            r = !0, a = u
        } finally {
            try {
                !t && s["return"] && s["return"]()
            } finally {
                if (r) throw a
            }
        }
    }

    function h(e, o) {
        U.onAppLaunch || (o && o({}), U.onAppLaunch = !0)
    }

    function b(e, o) {
        m("onAppTerminate", o)
    }

    function _(e, o) {
        m("onAppRoute", o)
    }

    function y(e, o) {
        m("onAppEnterBackground", o)
    }

    function M(e, o) {
        U.onAppShow || (o && o({}), U.onAppShow = !0), m("onAppEnterForeground", o)
    }

    function k(e, o) {
        var n = X[e];
        n && o && n.push(o)
    }

    function A(e, o) {
        var n = X[e],
            t = !0,
            r = !1,
            a = void 0;
        try {
            for (var i, s = n[Symbol.iterator](); !(t = (i = s.next()).done); t = !0) {
                var c = i.value;
                c(o)
            }
        } catch (u) {
            r = !0, a = u
        } finally {
            try {
                !t && s["return"] && s["return"]()
            } finally {
                if (r) throw a
            }
        }
    }

    function D(e, o) {
        k("onMusicPlay", o)
    }

    function E(e, o) {
        k("onMusicPause", o)
    }

    function N(e, o) {
        k("onMusicEnd", o)
    }

    function x(e, o) {
        k("onMusicError", o)
    }

    function T(e, o, n) {
        n && n({
            errMsg: "openAddress:ok",
            userName: "张三",
            addressPostalCode: "510000",
            provinceFirstStageName: "广东省",
            addressCitySecondStageName: "广州市",
            addressCountiesThirdStageName: "天河区",
            addressDetailInfo: "某巷某号",
            nationalCode: "510630"
        })
    }
    window.MutationObserver = window.WebKitMutationObserver = window.File = void 0;
    var I, W = __wxConfig.apphash,
        C = __wxConfig.appid,
        R = __wxConfig.appname,
        O = !1,
        B = [],
        J = navigator.userAgent,
        q = parseInt(J.match(/webview\/(\d*)/)[1]),
        P = [],
        L = {},
        F = Object.assign({
            domain: ["rapheal.sinaapp.com"],
            networkTimeout: {
                request: 3e4,
                connectSocket: 3e4,
                uploadFile: 3e4,
                downloadFile: 3e4
            }
        }, __wxConfig),
        G = 0,
        z = __wxConfig.appserviceConfig.AppserviceMaxDataSize;
    try {
        I = __wxConfig.projectConfig.Setting.MaxRequestConcurrent
    } catch (H) {
        // console.error(H), I = 5
    }
    var V = {
        login: !0,
        authorize: !0,
        operateWXData: !0,
        getStorage: !0,
        setStorage: !0,
        clearStorage: !0,
        getStorageSync: !0,
        setStorageSync: !0,
        clearStorageSync: !0,
        getMusicPlayerState: !0,
        operateMusicPlayer: !0,
        navigateTo: !0,
        redirectTo: !0,
        navigateBack: !0,
        setNavigationBarTitle: !0,
        showNavigationBarLoading: !0,
        hideNavigationBarLoading: !0,
        getLocation: !0,
        openLocation: !0,
        getNetworkType: !0,
        getSystemInfo: !0,
        chooseContact: !0,
        chooseImage: !0,
        chooseVideo: !0,
        saveFile: !0
    };
    window._____sendMsgToNW = e, window.addEventListener("message", function(e) {
        var o = e.data,
            n = o.to;
        if ("appservice" === n) return delete n.appservice, "complete" !== document.readyState ? void P.push(o) : void a(o)
    }), window.WeixinJSBridge = {};
    var j = null,
        K = {
            open: [],
            message: [],
            error: [],
            close: []
        },
        U = {
            onAppLaunch: !1,
            onAppShow: !1,
            onAppTerminate: [],
            onAppRoute: [],
            onAppEnterBackground: [],
            onAppEnterForeground: []
        },
        X = {
            onMusicPlay: [],
            onMusicPause: [],
            onMusicEnd: [],
            onMusicError: []
        };
    WeixinJSBridge._subscribe = {}, WeixinJSBridge.subscribe = function(e, o) {
        O && (console.group(new Date + " WeixinJSBridge subscribe"), console.debug(e, o), console.groupEnd()), B.push({
            type: "subscribe",
            eventName: e,
            data: arguments,
            timesmap: new Date
        }), WeixinJSBridge._subscribe[e] = o
    }, WeixinJSBridge.publish = function(o, n, t, r) {
        if (O && (console.group(new Date + " WeixinJSBridge publish " + o), console.debug(o, n, t, r), console.groupEnd()), n && 0 !== o.indexOf("canvas")) {
            var a = JSON.stringify(n),
                i = a.length;
            if (i > z) return void console.error("%c " + o + " 数据传输长度为 " + i + " 已经超过最大长度 " + z, "color: red; font-size: x-large")
        }
        B.push({
            type: "publish",
            eventName: o,
            data: arguments,
            timesmap: new Date
        }), "appDataChange" !== o && "pageInitData" !== o && "__updateAppData" !== o || r || e({
            appData: __wxAppData,
            sdkName: "send_app_data"
        }), e({
            eventName: o,
            data: n,
            sdkName: "publish",
            webviewIds: t
        })
    }, WeixinJSBridge.invoke = function(e, o, n) {
        return O && (console.group(new Date + " WeixinJSBridge invoke " + e), console.debug(e, o, n), console.groupEnd()), B.push({
            type: "invoke",
            eventName: e,
            data: arguments,
            timesmap: new Date
        }), V[e] ? void r(e, o, function(o) {
            if (o.errMsg.indexOf("ok") > -1 && ("navigateTo" === e || "redirectTo" === e)) {
                var t = o.url || "",
                    r = t.match(/(([^\?]*)(\?([^\/]*))?)$/),
                    a = "",
                    i = {};
                if (r) {
                    a = r[2] || "";
                    for (var s = (r[4] || "").split("&"), c = 0; c < s.length; ++c) {
                        var u = s[c].split("=");
                        2 == u.length && (i[u[0]] = u[1])
                    }
                }
                var p = e;
                S("onAppRoute", {
                    path: a,
                    query: i,
                    openType: p,
                    webviewId: o.webviewId
                })
            }
            n && n(o)
        }) : void("request" == e ? s(e, o, n) : "connectSocket" == e ? p(e, o, n) : "closeSocket" == e ? d(e, o, n) : "sendSocketMessage" == e ? l(e, o, n) : "openAddress" == e && T(e, o, n))
    }, WeixinJSBridge.on = function(e, o) {
        O && (console.group(new Date + " WeixinJSBridge on " + e), console.debug(e, o), console.groupEnd()), B.push({
            type: "on",
            eventName: e,
            data: arguments,
            timesmap: new Date
        }), "onSocketOpen" == e ? g(e, o) : "onSocketError" == e ? v(e, o) : "onSocketMessage" == e ? f(e, o) : "onSocketClose" == e ? w(e, o) : "onAppLaunch" == e ? h(e, o) : "onAppTerminate" == e ? b(e, o) : "onAppRoute" == e ? _(e, o) : "onAppEnterBackground" == e ? y(e, o) : "onAppEnterForeground" == e ? M(e, o) : "onMusicPlay" == e ? D(e, o) : "onMusicPause" == e ? E(e, o) : "onMusicEnd" == e ? N(e, o) : "onMusicError" == e && x(e, o)
    }, n({
        command: "SHAKE_HANDS"
    }), window.addEventListener("load", function() {
        P.forEach(function(e) {
            a(e)
        }), P = []
    }), window.showDebugInfo = function(e, o) {
        var n = B.filter(function(n) {
            var t = !e || (Array.isArray(e) ? e.includes(n.type) : n.type === e),
                r = !o || (Array.isArray(o) ? o.includes(n.eventName) : n.eventName === o);
            if (t && r) return n
        });
        console.group("showDebugInfo"), n.forEach(function(e) {
            console.group(e.timesmap + " WeixinJSBridge " + e.type + " " + e.eventName), console.debug.apply(window, e.data), console.groupEnd()
        }), console.groupEnd(), O = !0
    }, window.closeDebug = function() {
        console.clear(), O = !1
    }, window.showDebugInfoTable = function() {
        console.table(B)
    }, window.openToolsLog = function() {
        e({
            sdkName: "__open-tools-log"
        })
    }, window.openVendor = function() {
        e({
            sdkName: "__open-tools-vendor"
        })
    }, window.help = function() {
        console.table([{
            fun: "showDebugInfo",
            "arg[0]": "type -- String || Array; publish on subscribe invoke GetMsg",
            "arg[1]": "eventName -- String || Array;",
            example: 'showDebugInfo() showDebugInfo("publish") showDebugInfo(["publish", "invoke"], "onAppRoute")',
            openToolsLog: "open tools logs"
        }, {
            fun: "closeDebug"
        }, {
            fun: "showDebugInfoTable"
        }, {
            fun: "openToolsLog"
        }, {
            fun: "openVendor"
        }])
    }
}();
