$(document).ready(function () {
    $("#mainbody").css("min-height", $(document).height() - 89);
});

var Modal = {
    show: function () {
        $("#progress").show();
    },
    hide: function () {
        $("#progress").hide();
    }
};
const myNotification = window.createNotification();
var Message = {
    success: function (msg) {
        
        myNotification({
            theme: 'success',
            message: msg
        });

    },
    error: function (msg) {
        myNotification({
            theme:'error',
            message: msg
        });
    },
    close: function () {
        $("#alert_msg").alert('close');
    }
};
function errorResponse(xhr, status, error) {
    switch (xhr.status) {
        case 0:
            Message.error("Та системд дахин нэвтэрнэ үү!");
            setTimeout(function () { location.reload() }, 3000);
            break;
        case 403:
            Message.error("Та энэ үйлдлийг хийх эрхгүй байна!");
            break;
        case 401:
            Message.error("Та системд дахин нэвтэрнэ үү!");
            setTimeout(function () { location.reload() }, 3000);
            break;
        case 405:
            Message.error("Тайлан түр засвартай байна!");
            break;
        case 999:
            Message.error("Системийн алдаа гарлаа.");
            break;
        default:
            Message.error(error);
    }
};