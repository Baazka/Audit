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

function initDataTable() {
    $('.datatable').DataTable({
        "scrollX": true,
        dom: 'Bfrtip',
        buttons: [{
            extend: 'excelHtml5',
            className: 'btn btn-sm btn-custom',
            charset: 'UTF-8',
            text: 'Excel'
        }],
        language: {
            search: "_INPUT_",
            searchPlaceholder: "Хүснэгтээс хайх",
            "infoEmpty": "Мэдээлэл байхгүй байна.",
            "zeroRecords": "Хайлтын үр дүн байхгүй байна.",
            "decimal": ".",
            "thousands": ",",
            //"search": "Хүснэгтээс хайх",
            "lengthMenu": "Хуудаст <b>_MENU_</b> бичлэг",
            "infoFiltered": "(Нийт _MAX_ мэдээллээс хайлт хийв)",
            "info": "Хуудас: <b>_PAGE_</b>/<b>_PAGES_</b>  Нийт тоо: <b>_START_</b>-<b>_END_</b>/<b>_TOTAL_</b>",
            "paginate": {
                "first": "Эхнийх",
                "last": "Сүүлийх",
                "next": "Дараах",
                "previous": "Өмнөх"
            },
            "aria": {
                "sortAscending": ": өсөхөөр эрэмбэлэх",
                "sortDescending": ": буурхаар эрэмбэлэх"
            },
            "loadingRecords": "Түр хүлээнэ үү ...",
            "processing": "Боловсруулж байна ..."
        }
    });
}