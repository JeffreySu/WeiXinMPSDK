"use strict";

function init() {
    function e(e) {
        var t = 0,
            r = void 0,
            o = void 0,
            i = void 0;
        if (0 === e.length) return t;
        for (r = 0, i = e.length; r < i; r++) o = e.charCodeAt(r), t = (t << 5) - t + o, t |= 0;
        return t > 0 ? t : 0 - t
    }

    function t() {
        localStorage.setItem("projectLists", JSON.stringify(j))
    }

    function r(e, t) {
        if (t) {
            var r = e.projectpath,
                n = o.join(__dirname, "../weapp/quick/");
            s("./**/**", {
                cwd: n
            }, function(e, t) {
                e || t.forEach(function(e) {
                    var t = o.join(n, e),
                        s = o.join(r, e),
                        a = i.lstatSync(t);
                    if (a.isDirectory()) c.sync(s);
                    else {
                        var p = i.readFileSync(t);
                        i.writeFileSync(s, p)
                    }
                })
            })
        }
    }
    var o = require("path"),
        i = require("fs"),
        n = require("../common/log/log.js"),
        s = require("glob"),
        c = require("mkdir-p"),
        a = require("../common/request/request.js"),
        p = require("../config/urlConfig.js"),
        f = require("../config/errcodeConfig.js"),
        u = require("events").EventEmitter,
        j = JSON.parse(localStorage.getItem("projectLists")) || [],
        d = {},
        g = !1;
    j.forEach(function(t) {
        t.hash = e(t.projectid)
    });
    var l = Object.assign({}, u.prototype, {
        getProjectByHash: function(e) {
            return e = parseInt(e), j.find(function(t) {
                return t.hash === e
            })
        },
        getProjectByID: function(e) {
            return j.find(function(t) {
                return t.projectid === e
            })
        },
        getProjectList: function() {
            return n.info("projectStores.js getProjectList " + JSON.stringify(j)), j
        },
        add: function(o, i) {
            o.hash = e(o.projectid), j.unshift(o), r(o, i), t(), n.info("projectStores.js add " + JSON.stringify(o)), this.emit("ADD_PROJECT", j)
        },
        del: function(e) {
            var r = j.findIndex(function(t) {
                return t.projectid === e
            });
            if (r > -1) {
                var o = j[r];
                delete localStorage["projectattr" + o.hash], j.splice(r, 1), t(), n.info("projectStores.js del " + e), this.emit("DEL_PROJECT", j)
            }
        },
        close: function() {
            this.emit("CLOSE_PROJECT")
        },
        restart: function(e) {
            this.emit("RESTART_PROJECT", e)
        },
        getProjectConfig: function(e) {
            return d[e.hash]
        },
        setProjectConfig: function(e, t) {
            if (!g) {
                g = !0;
                var r = "projectattr" + e.hash,
                    o = JSON.parse(localStorage.getItem(r));
                o && (d[e.hash] = o, t());
                var i = p.getWeappAttrURL,
                    s = i + "?appid=" + e.appid + "&_r=" + Math.random();
                console.log(s), n.info("projectStores.js begin get projectAttr " + s), a({
                    url: s,
                    body: JSON.stringify({
                        appid_list: [e.appid]
                    }),
                    method: "post",
                    needToken: 1
                }, function(i, s, c) {
                    if (g = !1, i) return void n.error("projectStores.js end get projectAttr network error: " + JSON.stringify(i));
                    n.info("projectStores.js end get projectAttr " + c);
                    var a = void 0;
                    try {
                        a = JSON.parse(c)
                    } catch (p) {
                        return n.error("projectStores.js end get projectAttr parse body error: " + c + " " + JSON.stringify(i)), void(!o && alert("系统错误 " + c))
                    }
                    var u = a.baseresponse,
                        j = 0; //u ? parseInt(u.errcode) : 0;
                    if (0 === j) {
                        var l = {}; // a.attr_list[0];
                        d[e.hash] = l, localStorage.setItem(r, JSON.stringify(l)), o || t()
                    } else {
                        if (j === f.DEV_App_Not_Band) {
                            alert("当前开发者未绑定此 appid ，请到 mp 后台操作后重试"), nw.Shell.openExternal("https://mp.weixin.qq.com/"), n.error("projectStores.js setProjectConfig error " + j);
                            var h = require("./webviewStores.js");
                            return void h.emit("NOT_LOGIN")
                        }!o && alert("系统错误 " + c)
                    }
                })
            }
        }
    });
    _exports = l
}
var _exports;
init(), module.exports = _exports;
