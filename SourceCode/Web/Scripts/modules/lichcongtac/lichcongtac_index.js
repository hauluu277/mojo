function onAjaxSubmitSuccess(rs) {
        if (rs.Status == true) {
            reloadTable();
            NotifySuccess(rs.message);
            CloseGlobalModal();
        } else {
            NotifyError(rs.message);
        }
}


/**
 * cập nhật danh mục
 * @param {*} id
 */
function onEdit(id) {
    OpenFormModal('get', '/QLLichLamViecArea/QLLichLamViec/Edit', { id: id });
}
function onCreate(id) {
    OpenFormModal('get', '/QLLichLamViecArea/QLLichLamViec/Create',);
}

/**
 * xóa danh mục
 * @param {*} id
 * @param {*} event
 */
function onDelete(id, obj) {
    var name = $(obj).attr("data-object-name");
    if (confirm("Xác nhận xóa lịch công tác?")) {
        $.ajax({
            type: "post",
            url: "/QllichlamviecArea/Qllichlamviec/Delete",
            data: { id: id },
            success: function (rs) {
                if (rs.Status) {
                    NotifySuccess("Xóa lịch công tác thành công");
                    reloadTable();
                } else {
                    NotifyError("Xóa lịch công tác thất bại");
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
            nameModel: 'NgayLamViec',
            content: function (data) {
                return data.ThoiGianLamViec_text
            }
        },
        {
            isSort: true,
            nameModel: 'NoiDung',
            content: function (data) {
                return data.NoiDung
            }
        },
        {
            isSort: true,
            nameModel: 'DiaDiem',
            content: function (data) {
                return data.DiaDiem
            }
        },
        {
            isSort: true,
            nameModel: 'ThanhPhanThamDu',
            content: function (data) {
                if (data.ListThanhPhanThamDu != null)
                {
                    var html = "";
                    for (var i = 0; i < data.ListThanhPhanThamDu.length; i++) {
                        html += `<p>${data.ListThanhPhanThamDu[i]}</p>`;
                    }
                    return html;
                }
                return "";
            }
        },
        {
            isSort: true,
            tdClass: "center",
            nameModel: 'IsPublish',
            content: function (data) {
                return StatusIcon(data.IsPublish);
            }
        },
        {
            isSort: false,
            nameModel: "",
            tdClass: "center",

            content: function (data) {
                var result = "<div class='btn-group gr-nghiphep'>";
                result += '<a href="javascript:onEdit(' + data.ItemID + ')" class="btn-edit hvr-radial-out" data-id="10595" title="Chỉnh sửa"><i class="fa fa-pencil-square fa-lg"></i></a>'
                result += '<a href="javascript:void(0)" onclick="onDelete(' + data.ItemID + ', this)" class="btn-remove hvr-radial-out" data-id="' + data.ItemID + '" data-object-name="Lịch công tác" title="Xóa"><i class="fa fa-trash"></i></a>';
                result += "</div>";
                return result;
            }
        },

    ];

    var getData = function (pageIndex, sortQuery, pageSize) {
        $.ajax({
            url: '/QLLichLamViecArea/QLLichLamViec/GetData',
            type: 'post',
            cache: false,
            data: {
                "indexPage": pageIndex,
                "sortQuery": sortQuery,
                "pageSize": pageSize
            },
            success: function (data) {
                $("#tbl-ThuTuc").hinetTable("data", {
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

    var tableData = $("#tbl-ThuTuc").hinetTable("init", {
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
    $("#tbl-ThuTuc").hinetTable("reload");
}

$(document).ready(function () {
    pagingConfig();
})