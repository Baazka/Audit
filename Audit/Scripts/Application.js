$(document).ready(function () {
    //$("#mainbody").css("min-height", $(document).height() - 89);
    countdown();
});

var Modal = {
    show: function () {
        $("#progress").show();
    },
    hide: function () {
        $("#progress").hide();
        seconds = sessionTimeOut;
    }
};
const myNotification = window.createNotification();
var Message = {
    success: function (msg) {
        
        myNotification({
            theme: 'success',
            message: msg,
            showDuration:2000
        });

    },
    error: function (msg) {
        myNotification({
            theme:'error',
            message: msg,
            showDuration: 2000
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

var table;
function initDataTable(paging = true) {
    $('.datatable').DataTable({
        paging: paging,
        language: {
            search: "_INPUT_",
            searchPlaceholder: "Хүснэгтээс хайх",
            "infoEmpty": "Мэдээлэл байхгүй байна.",
            "zeroRecords": "Үр дүн хоосон байна.",
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
function initDataTableFeatureDisabled() {
    $('.datatableFeatureDisabled').DataTable({
        "paging": false,
        "ordering": false,
        "info": false,
        "searching": false
    });
}
var datatables = {
    language: {
        search: "_INPUT_",
        searchPlaceholder: "Хүснэгтээс хайх",
        "infoEmpty": "Мэдээлэл байхгүй байна.",
        "zeroRecords": "Үр дүн хоосон байна.",
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
}

//function exportFile(tableid, excelname) {
//    table.destroy();
//    initDataTable(false);

//    var wb = XLSX.utils.table_to_book(document.getElementById(tableid));
//    XLSX.writeFile(wb, excelname + '.xlsx');

//    table.destroy();
//    initDataTable(true);
//}

function countdown() {
    seconds--;
    //$('#ctr').html(seconds);
    if (seconds <= 0) {
        window.location.reload();
    } else {
        setTimeout('countdown()', 1000);
    }
}

var bmbtns;