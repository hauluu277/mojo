function onAjaxSubmitSuccess(result) {
    if (result.Status) {
        notifySuccess(result.Message);
        closeEditModal();
        onEndAjax();
        reloadTable();
    } else {
        notifyError(result.Message);
    }
}


/**
 * cập nhật danh mục
 * @param {*} id
 */
function onReply(id) {
    OpenFormModal('get', '/QLLienHeArea/QLLienHe/FormReply', { id: id });
}

function onDetail(id) {
    OpenFormModal('get', '/QLLienHeArea/QLLienHe/FormDetail', { id: id });
}

/**
 * xóa danh mục
 * @param {*} id
 * @param {*} event
 */
function onDelete(id) {
    if (confirm("Xác nhận xóa thông tin liên hệ ?")) {
        $.ajax({
            type: "post",
            url: "/QLLienHeArea/QLLienHe/Delete",
            data: { id: id },
            success: function (rs) {
                if (rs.Status) {
                    NotifySuccess("Xóa thông tin liên hệ thành công");
                    reloadTable();
                } else {
                    NotifyError("Xóa thông tin liên hệ thất bại");
                }
            }, error: function (er) {
                console.log(er.Message);
            }
        })
    }
}

function pagingConfig() {
    var conf = [];
    conf = [
        {
            tdClass: "center width-30",
            isSort: false,
            nameModel: "",
            isCounter: true,
            content: function (data) {
                return "<input type='checkbox' value='" + data.ItemID + "'/>"
            }
        },
        {
            isSort: true,
            nameModel: 'Email',
            content: function (data) {
                return data.Email
            }
        },
        {
            isSort: true,
            nameModel: 'Subject',
            content: function (data) {
                return data.Subject
            }
        },
        {
            isSort: true,
            nameModel: 'Url',
            content: function (data) {
                return data.Url
            }
        },
        {
            isSort: true,
            nameModel: 'Message',
            content: function (data) {
                return data.Message;
            }
        },
        {
            isSort: true,
            nameModel: 'ThoiGianPhanHoi',
            content: function (data) {
                return data.ThoiGianPhanHoi_text;
            }
        },
        {
            isSort: true,
            nameModel: 'TrangThai',
            content: function (data) {
                return data.TrangThai == 0 ? "<span class='red'>Chưa phản hồi</span>" : "<span class='text-success'>Đã phản hồi</span>";
            }
        },
        {
            isSort: false,
            nameModel: "",
            tdClass: "center",

            content: function (data) {
                var result = "<div class='btn-group gr-nghiphep'>";
                result += '<a href="javascript:onReply(`' + data.RowGuid + '`)" class="btn-edit hvr-radial-out" title="Trả lời"><i class="fa fa-reply fa-lg"></i></a>'
                result += '<a href="javascript:void(0)" onclick="onDetail(`' + data.RowGuid + '`)" class="btn-detail hvr-radial-out" data-id="' + data.RowGuid + '" title="Chi tiết"><i class="fa fa-info"></i></a>';
                //result += '<a href="javascript:void(0)" onclick="onDelete(`' + data.RowGuid + '`)" class="btn-remove hvr-radial-out" data-id="' + data.RowGuid + '" title="Xóa"><i class="fa fa-trash"></i></a>';
                result += "</div>";
                return result;
            }
        },

    ];

    var getData = function (pageIndex, sortQuery, pageSize) {
        $.ajax({
            url: '/QLLienHeArea/QLLienHe/GetData',
            type: 'post',
            cache: false,
            data: {
                "pageIndex": pageIndex,
                "sortQuery": sortQuery,
                "pageSize": pageSize
            },
            success: function (data) {
                $("#tbl-QLLienHe").hinetTable("data", {
                    pageSize: pageSize != -1 ? pageSize : data.Count,
                    pageIndex: pageIndex,
                    pagecount: data.TotalPage,
                    recordCount: data.Count,
                    listItem: data.ListItem,
                });
            },
            error: function (err) {
                CommonJS.alert(xhr.responseText);
            }
        });

    }

    var tableData = $("#tbl-QLLienHe").hinetTable("init", {
        pageSizeList: { size: [20, 50, 100, -1], label: ['20', '50', '100', 'Tất cả'] },
        pagecount: $('#total-page').val(),
        recordCount: $('#total-record').val(),
        getData: getData,
        listItem: groupData,
        config: conf
    });

}
function toDate(date) {
    if (date) {
        var arr = date.split('-');
        if (arr.length > 2) {
            var year = arr[0];
            var month = arr[1];
            var day = arr[2].substring(0, 2);
            return `${day}/${month}/${year}`;
        }
    }
    return "";
}

function reloadTable() {
    $("#tbl-QLLienHe").hinetTable("reload");
}

$(document).ready(function () {
    pagingConfig();
})