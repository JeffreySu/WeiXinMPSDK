var senparc = {};
senparc.menu = {
    token: '',
    init: function () {
        $('#buttonDetails').hide();
        $('#menuEditor').hide();

        $(':input[id^=button_]').click(function () {
            $('#buttonDetails').show();
            var keyId = 'menu_button' + $(this).attr('data-j') + '_sub_button' + $(this).attr('data-i') + '_key';
            var txtDetailsKey = $('#buttonDetails_key');
            var txtButtonKey = $('#' + keyId);
            txtDetailsKey.val(txtButtonKey.val());
            txtDetailsKey.unbind('blur').blur(function () {
                txtButtonKey.val($(this).val());
            });
        });

        $('#menuLogin_Submit').click(function () {
            $.getJSON('/Menu/GetToken', { appId: $('#menuLogin_AppId').val(), appSecret: $('#menuLogin_AppSecret').val() },
                function (json) {
                    if (json.access_token) {
                        senparc.menu.setToken(json.access_token);
                    } else {
                        alert(json.error || '执行过程有错误，请检查！');
                    }
                });
        });

        $('#menuLogin_SubmitOldToken').click(function() {
            senparc.menu.setToken($('#menuLogin_OldToken').val());
        });
    },
    setToken:function (token) {
        senparc.menu.token = token;
        $('#tokenStr').html(token);
        $('#menuEditor').show();
        $('#menuLogin').hide();
    }
};