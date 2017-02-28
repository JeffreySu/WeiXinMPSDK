"use strict";

function init() {
    function e(e) {
        alert(e)
    }
    var t = require("../../lib/react.js"),
        a = (require("../../utils/tools.js"), require("../../cssStr/cssStr.js")),
        r = (require("path"), require("../../utils/newReport.js")),
        i = require("../../config/urlConfig.js"),
        s = require("../../common/request/request.js"),
        c = (require("../../stroes/webviewStores.js"), require("../../common/log/log.js")),
        o = require("glob"),
        n = require("../../config/errcodeConfig.js"),
        p = (require("../../actions/windowActions.js"), require("../../actions/projectActions.js")),
        l = t.createClass({
            displayName: "Createstep",
            getInitialState: function() {
                return {
                    projectpath: "",
                    appid: "",
                    appname: "",
                    error: "",
                    saveBtnDisable: !0,
                    showQuickStart: !1,
                    checked: !0,
                    showLoading: !1
                }
            },
            chooseDir: function() {
                var e = this,
                    t = document.createElement("input");
                t.setAttribute("type", "file"), t.setAttribute("nwdirectory", !0), t.style.display = "none", global.contentDocumentBody.appendChild(t), t.addEventListener("change", function(a) {
                    o("*", {
                        cwd: t.value
                    }, function(a, r) {
                        var i = 0 === r.length;
                        e.setState({
                            projectpath: t.value,
                            showQuickStart: i
                        })
                    }), global.contentDocumentBody.removeChild(t)
                }), t.addEventListener("cancel", function(e) {
                    global.contentDocumentBody.removeChild(t)
                }), t.click()
            },
            editAppid: function(e) {
                var t = e.target,
                    a = t.value;
                this.setState({
                    appid: a
                })
            },
            editAppname: function(e) {
                var t = e.target,
                    a = t.value;
                this.setState({
                    appname: a
                })
            },
            addProject: function() {
                var t = this,
                    a = this.state.projectpath,
                    o = this.state.appid,
                    l = encodeURIComponent(this.state.appname);
                if (!o) return void e("请填写 appid ");
                if (!l) return void e("请填写 项目名称 ");
                if (!a) return void e("请选择 项目目录 ");
                var m = o + "_" + l,
                    d = this.props.projectLists.find(function(e) {
                        return e.projectid === m
                    });
                return d ? void this.setState({
                    projectpath: "",
                    appid: "",
                    appname: "",
                    saveBtnDisable: !0,
                    error: "已存在 " + o + " " + decodeURIComponent(d.appname) + " 项目，请重新输入"
                }) : (this.setState({
                    showLoading: !0
                }), void s({
                    url: i.createWeappURL + "?appid=" + o,
                    needToken: 1
                }, function(i, s, u) {
                    if (i) c.error("createstep.js create  " + i.toString()), t.setState({
                        showLoading: !1
                    }), e(i.toString());
                    else {
                        t.setState({
                            showLoading: !1
                        }), c.info("createstep.js create  " + u);
                        var h = JSON.parse(u),
                            f = h.baseresponse,
                            v = f ? parseInt(f.errcode) : 0;
                        // if (v === n.DEV_App_Not_Band) return e("当前开发者未绑定此 appid ，请到 mp 后台操作后重试"), nw.Shell.openExternal("https://mp.weixin.qq.com/"), void c.error("createstep.js create project error " + v);
                        // if (0 === v) {
                            var b = h.app_head_img ? h.app_head_img + "/0" : "",
                                g = t.state.showQuickStart && t.state.checked;
                            return d = {
                                appid: o,
                                appname: l,
                                projectpath: a,
                                projectid: m,
                                app_head_img: b,
                                is_admin: h.is_admin
                            }, p.add(d, g), r("project_createsuc", o), t.setState({
                                projectpath: "",
                                appid: "",
                                appname: "",
                                error: "",
                                saveBtnDisable: !0,
                                showLoading: !1
                            }), void t.props.goMain(d)
                        // }
                        // var E = u || "系统错误";
                        // e(E)
                    }
                }))
            },
            changeCheckbox: function(e) {
                var t = e.target.checked;
                this.setState({
                    checked: t
                })
            },
            render: function() {
                var e = this.props.show ? {} : a.displayNone,
                    r = this.state.showQuickStart ? {} : a.visibilityHidden,
                    i = this.props.createBack,
                    s = this.state.showLoading ? "create-form-button-primary detail-upload-dialog-button-primary-loading" : "create-form-button-primary";
                return t.createElement("div", {
                    className: "create-step2",
                    style: e
                }, t.createElement("div", {
                    className: "create-toolbar app-drag"
                }, t.createElement("a", {
                    onClick: i,
                    href: "javascript:;",
                    className: "create-toolbar-close app-no-drag"
                }, t.createElement("i", {
                    className: "create-toolbar-back-icon"
                }), t.createElement("span", null, "返回"))), t.createElement("div", {
                    className: "create-body"
                }, t.createElement("div", {
                    className: "create-name"
                }, "新建项目"), t.createElement("div", {
                    className: "create-form"
                }, t.createElement("div", {
                    className: "create-form-item"
                }, t.createElement("label", {
                    htmlFor: "",
                    className: "create-form-label"
                }, "AppID"), t.createElement("div", {
                    className: "create-form-input-box"
                }, t.createElement("input", {
                    value: this.state.appid,
                    onChange: this.editAppid,
                    type: "text",
                    className: "create-form-input"
                }), t.createElement("p", {
                    style: this.state.error ? a.displayNone : {},
                    className: "create-form-tips"
                }, "填写小程序AppID，可在公众平台开发设置页中查看"), t.createElement("p", {
                    style: this.state.error ? {} : a.displayNone,
                    className: "create-form-tips-warn"
                }, this.state.error))), t.createElement("div", {
                    className: "create-form-item"
                }, t.createElement("label", {
                    htmlFor: "",
                    className: "create-form-label"
                }, "项目名称"), t.createElement("div", {
                    className: "create-form-input-box"
                }, t.createElement("input", {
                    value: this.state.appname,
                    onChange: this.editAppname,
                    type: "text",
                    className: "create-form-input"
                }))), t.createElement("div", {
                    className: "create-form-item"
                }, t.createElement("label", {
                    htmlFor: "",
                    className: "create-form-label"
                }, "本地开发目录"), t.createElement("div", {
                    onClick: this.chooseDir,
                    className: "create-form-input-box"
                }, t.createElement("input", {
                    value: this.state.projectpath,
                    disabled: "true",
                    type: "text",
                    className: "create-form-input create-form-input-with-pointer"
                }), t.createElement("p", {
                    className: "create-form-tips"
                })), t.createElement("div", {
                    className: "create-form-extra"
                }, t.createElement("a", {
                    href: "javascript:;",
                    onClick: this.chooseDir,
                    className: "create-form-extra-button"
                }, "选择"))), t.createElement("div", {
                    style: r,
                    className: "create-quick-checkbox"
                }, t.createElement("input", {
                    id: "quick-checkbox",
                    onChange: this.changeCheckbox,
                    checked: this.state.checked,
                    type: "checkbox"
                }), t.createElement("label", {
                    htmlFor: "quick-checkbox"
                }, "在当前目录中创建 quick start 项目"))), t.createElement("div", {
                    className: "create-form-footer"
                }, t.createElement("a", {
                    href: "javascript:;",
                    className: "create-form-button-default",
                    onClick: i
                }, "取消"), t.createElement("a", {
                    onClick: this.addProject,
                    href: "javascript:;",
                    className: s
                }, "添加项目"))))
            }
        });
    _exports = l
}
var _exports;
init(), module.exports = _exports;
