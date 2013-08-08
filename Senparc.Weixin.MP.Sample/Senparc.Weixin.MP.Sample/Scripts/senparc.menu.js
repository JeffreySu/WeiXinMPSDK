var senparc = {};
var maxSubMenuCount = 5;
var menuState;
senparc.menu = {
    token: '',
    init: function () {
        menuState = $('#menuState');

        $('#buttonDetails').hide();
        $('#menuEditor').hide();

        $(':input[id^=menu_button]').click(function () {
            $('#buttonDetails').show();
            var keyId = $(this).attr('data-root')
                            ? ('menu_button' + $(this).attr('data-root') + '_key')
                            : ('menu_button' + $(this).attr('data-j') + '_sub_button' + $(this).attr('data-i') + '_key');
            var txtDetailsKey = $('#buttonDetails_key');
            var txtButtonKey = $('#' + keyId);
            txtDetailsKey.val(txtButtonKey.val());
            txtDetailsKey.unbind('blur').blur(function () {
                txtButtonKey.val($(this).val());
            });
        });

        $('#menuLogin_Submit').click(function () {
            $.getJSON('/Menu/GetToken?t=' + Math.random(), { appId: $('#menuLogin_AppId').val(), appSecret: $('#menuLogin_AppSecret').val() },
                function (json) {
                    if (json.access_token) {
                        senparc.menu.setToken(json.access_token);
                    } else {
                        alert(json.error || '执行过程有错误，请检查！');
                    }
                });
        });

        $('#menuLogin_SubmitOldToken').click(function () {
            senparc.menu.setToken($('#menuLogin_OldToken').val());
        });

        $('#btnGetMenu').click(function () {
            menuState.html('获取菜单中');
            $.getJSON('/Menu/GetMenu?t=' + Math.random(), { token: senparc.menu.token }, function (json) {
                if (json.menu) {
                    var buttons = json.menu.button;
                    //此处i与j和页面中反转
                    for (var i = 0; i < buttons.length; i++) {
                        var button = buttons[i];
                        $('#menu_button' + i + '_name').val(button.name);
                        $('#menu_button' + i + '_key').val(button.key);

                        if (button.sub_button.length > 0) {
                            //二级菜单
                            for (var j = 0; j < button.sub_button.length; j++) {
                                var subButton = button.sub_button[j];
                                var idPrefix = '#menu_button' + i + '_sub_button' + j;
                                $(idPrefix + "_name").val(subButton.name);
                                $(idPrefix + "_type").val(subButton.type);
                                $(idPrefix + "_key").val(subButton.key);
                            }
                        } else {
                            //底部菜单
                            //...
                        }
                    }
                    menuState.html('已完成');
                } else {
                    menuState.html(json.error || '执行过程有错误，请检查！');
                }
            });
        });
    },
    setToken: function (token) {
        senparc.menu.token = token;
        $('#tokenStr').html(token);
        $('#menuEditor').show();
        $('#menuLogin').hide();
    }
};