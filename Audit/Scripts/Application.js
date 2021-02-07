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
        //buttons: [{
        //    extend: 'excelHtml5',
        //    className: 'btn btn-sm btn-custom',
        //    charset: 'UTF-8',
        //    text: 'Excel'
        //}],
        buttons: [
            {
                extend: 'excel',
                customize: function (xlsx) {
                    //Apply styles, Center alignment of text and making it bold.
                    var sSh = xlsx.xl['styles.xml'];
                    var lastXfIndex = $('cellXfs xf', sSh).length - 1;

                    var n1 = '<numFmt formatCode="##0.0000%" numFmtId="300"/>';
                    var s2 = '<xf numFmtId="0" fontId="2" fillId="0" borderId="0" applyFont="1" applyFill="0" applyBorder="0" xfId="0" applyAlignment="1">' +
                        '<alignment horizontal="center"/></xf>';

                    sSh.childNodes[0].childNodes[0].innerHTML += n1;
                    sSh.childNodes[0].childNodes[5].innerHTML += s2;

                    var greyBoldCentered = lastXfIndex + 1;

                    //Merge cells as per the table's colspan
                    var sheet = xlsx.xl.worksheets['sheet1.xml'];
                    var dt = $('#tblReport').DataTable();
                    var frColSpan = $(dt.table().header()).find('th:nth-child(1)').prop('colspan');
                    var srColSpan = $(dt.table().header()).find('th:nth-child(2)').prop('colspan');
                    var columnToStart = 2;

                    var mergeCells = $('mergeCells', sheet);
                    mergeCells[0].appendChild(_createNode(sheet, 'mergeCell', {
                        attr: {
                            ref: 'A1:' + toColumnName(frColSpan) + '1'
                        }
                    }));

                    mergeCells.attr('count', mergeCells.attr('count') + 1);

                    var columnToStart = 2;

                    while (columnToStart <= frColSpan) {
                        mergeCells[0].appendChild(_createNode(sheet, 'mergeCell', {
                            attr: {
                                ref: toColumnName(columnToStart) + '2:' + toColumnName((columnToStart - 1) + srColSpan) + '2'
                            }
                        }));
                        columnToStart = columnToStart + srColSpan;
                        mergeCells.attr('count', mergeCells.attr('count') + 1);
                    }

                    //Text alignment to center and apply bold
                    $('row:nth-child(1) c:nth-child(1)', sheet).attr('s', greyBoldCentered);
                    for (i = 0; i < frColSpan; i++) {
                        $('row:nth-child(2) c:nth-child(' + i + ')', sheet).attr('s', greyBoldCentered);
                    }

                    function _createNode(doc, nodeName, opts) {
                        var tempNode = doc.createElement(nodeName);
                        if (opts) {
                            if (opts.attr) {
                                $(tempNode).attr(opts.attr);
                            }
                            if (opts.children) {
                                $.each(opts.children, function (key, value) {
                                    tempNode.appendChild(value);
                                });
                            }
                            if (opts.text !== null && opts.text !== undefined) {
                                tempNode.appendChild(doc.createTextNode(opts.text));
                            }
                        }
                        return tempNode;
                    }

                    //Function to fetch the cell name
                    function toColumnName(num) {
                        for (var ret = '', a = 1, b = 26; (num -= a) >= 0; a = b, b *= 26) {
                            ret = String.fromCharCode(parseInt((num % b) / a) + 65) + ret;
                        }
                        return ret;
                    }
                }
            }
        ],
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