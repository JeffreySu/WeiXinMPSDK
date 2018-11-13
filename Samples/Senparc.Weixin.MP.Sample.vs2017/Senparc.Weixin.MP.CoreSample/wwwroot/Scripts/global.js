﻿var noticeareaHeightChanging = false
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
            $('#contact-content li.contact-qq').darkTooltip({
                theme: 'light'
            });
        },
        error: function () {
            //alert('fail');
        }
    });
}