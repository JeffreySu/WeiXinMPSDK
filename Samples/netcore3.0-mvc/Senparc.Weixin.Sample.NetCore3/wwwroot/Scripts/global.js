var noticeareaHeightChanging = false
$(function () {
    loadQQGroups();

    $('.btn-top-menu').hover(function () {
        $(this).find('ul.nav-sub-catalog').show();
    }, function () {
        $(this).find('ul.nav-sub-catalog').hide();
    });

    var noticeareaHeight = $('#noticearea').height();
    var noticeareaHeight_shrink = noticeareaHeight * 2 / 3;
    $('#noticearea').animate({ height: noticeareaHeight_shrink }, 1000);
    $('#noticearea').hover(function () {
        if (noticeareaHeightChanging) {
            return;
        }
        noticeareaHeightChanging = true;
        $('#noticearea').animate({ height: noticeareaHeight }, function () {
            noticeareaHeightChanging = false;
        });
        //$('#noticearea').css('position', 'absolute');
    }, function () {
        if (noticeareaHeightChanging) {
            return;
        }
        noticeareaHeightChanging = true;
        $('#noticearea').animate({ height: noticeareaHeight_shrink }, function () {
            noticeareaHeightChanging = false;
        });
    });
});

$(function () {
});

function loadQQGroups() {
    $.ajax({
        type: "get",
        async: false,
        url: "https://weixin.senparc.com/WeixinSdk/GetSdkQqGroupListJson",
        dataType: "jsonp",
        jsonp: "callbackparam", //服务端用于接收callback调用的function名的参数
        jsonpCallback: "success_jsonpCallback", //callback的function名称
        success: function (json) {
            $('#qqGroups').html(json[0].html);

            $('<li>').addClass('contatc-name').html('&nbsp; ').insertAfter($('#qqGroups li.contatc-img').eq(8));//在第9个元素后追加

            $('#contact-content li.contact-qq').darkTooltip({
                theme: 'light'
            });

            $('ins div:contains(SCF)').each(function (i, item) {
                $(this).html($(this).html().replace('SCF', '<a href="https://github.com/SenparcCoreFramework/SCF" target="_blank" class="qqGroup_tip">SCF<a>'));
            });

            $('ins div:contains(NeuChar)').each(function (i, item) {
                $(this).html($(this).html().replace('NeuChar', '<a href="https://github.com/Senparc/NeuChar" target="_blank" class="qqGroup_tip">NeuChar<a>'));
            });
        },
        error: function () {
            //alert('fail');
        }
    });
}