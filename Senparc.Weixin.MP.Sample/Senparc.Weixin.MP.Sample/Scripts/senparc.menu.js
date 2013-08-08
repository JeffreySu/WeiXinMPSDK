var senparc = {};
var maxSubMenuCount = 5;
senparc.menu = {
    token: '',
    init: function () {
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
            $.getJSON('/Menu/GetMenu?t=' + Math.random(), { token: senparc.menu.token }, function (json) {
                if (json.menu) {
                    var buttons = json.menu.button;
                    console.log(buttons);
                    for (var i = 0; i < buttons.length; i++) {
                        var button = buttons[i];
                        $('#rootButton_' + i).val(button.name);
                        if (button.sub_button.length > 0) {
                            //二级菜单
                            for (var j = 0; j < button.sub_button; j++) {
                                var subButton = button.sub_button[j];
                                var idPrefix = '#button_' + i + '_' + j;
                                $(idPrefix + "_name").val(subButton.name);
                                $(idPrefix + "_type").val(subButton.type);
                                $(idPrefix + "_key").val(subButton.key);
                            }
                        }
                    }
                } else {
                    alert(json.error || '执行过程有错误，请检查！');
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