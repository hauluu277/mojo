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
function onEdit(id) {
    OpenFormModal('get', '/BaoCaoArea/BaoCao/FormBaoCao', { id: id });
}

/**
 * xóa danh mục
 * @param {*} id
 * @param {*} event
 */
function onDelete(id, obj) {
    var name = $(obj).attr("data-object-name");
    if (confirm("Xác nhận xóa công khai ngân sách '" + name + "'?")) {
        $.ajax({
            type: "post",
            url: "/BaoCaoArea/BaoCao/Delete",
            data: { id: id },
            success: function (rs) {
                if (rs.Status) {
                    NotifySuccess("Xóa công khai ngân sách thành công");
                    reloadTable();
                } else {
                    NotifyError("Xóa công khai ngân sách thất bại");
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
            nameModel: 'TenBaoCao',
            content: function (data) {
                return data.TenBaoCao
            }
        },
        {
            isSort: true,
            nameModel: 'NamChuKyBaoCao',
            content: function (data) {
                return data.NamChuKyBaoCao;
            }
        },

        {
            isSort: true,
            nameModel: 'BieuMau',
            content: function (data) {
                return data.BieuMau;
            }
        },
        {
            isSort: true,
            nameModel: 'SoQuyetDinhCongBo',
            content: function (data) {
                return data.SoQuyetDinhCongBo;
            }
        },
        {
            isSort: true,
            nameModel: 'NgayCongBo',
            content: function (data) {
                return data.NgayCongBoString;
            }
        },
        {
            isSort: true,
            nameModel: 'PathFile',
            content: function (data) {
                return '<a href="' + data.PathFile + '">' + data.TenBaoCao + '</a>';
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
                result += '<a href="javascript:void(0)" onclick="onDelete(' + data.ItemID + ', this)" class="btn-remove hvr-radial-out" data-id="' + data.ItemID + '" data-object-name="' + data.TenBaoCao + '" title="Xóa"><i class="fa fa-trash"></i></a>';
                result += "</div>";
                return result;
            }
        },

    ];

    var getData = function (pageIndex, sortQuery, pageSize) {
        $.ajax({
            url: '/BaoCaoArea/BaoCao/GetData',
            type: 'post',
            cache: false,
            data: {
                "pageIndex": pageIndex,
                "sortQuery": sortQuery,
                "pageSize": pageSize
            },
            success: function (data) {
                $("#tbl-BaoCao").hinetTable("data", {
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

    var tableData = $("#tbl-BaoCao").hinetTable("init", {
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
    $("#tbl-BaoCao").hinetTable("reload");
}

$(document).ready(function () {
    pagingConfig();
})