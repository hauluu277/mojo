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
    OpenFormModal('get', '/SanPhamArea/SanPham/FormSanPham', { id: id });
}

/**
 * xóa danh mục
 * @param {*} id
 * @param {*} event
 */
function onDelete(id, obj) {
    var name = $(obj).attr("data-object-name");
    if (confirm("Xác nhận xóa sản phẩm '" + name + "'?")) {
        $.ajax({
            type: "post",
            url: "/SanPhamArea/SanPham/Delete",
            data: { id: id },
            success: function (rs) {
                if (rs.Status) {
                    NotifySuccess("Xóa sản phẩm thành công");
                    reloadTable();
                } else {
                    NotifyError("Xóa sản phẩm thất bại");
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
            nameModel: 'MaSanPham',
            content: function (data) {
                var value = data.MaSanPham;

                return value;
            }
        },
        {
            isSort: true,
            nameModel: 'TenSanPham',
            content: function (data) {
                return `<a href='javascript:detailProduct(` + data.ItemID + `)'>${data.TenSanPham}</a>`;
            }
        },
        {
            isSort: true,
            nameModel: 'LinhVucSanPham',
            content: function (data) {
                return data.TenLinhVuc;
            }
        },

        {
            isSort: true,
            nameModel: 'DiaChiLienHe',
            content: function (data) {
                return data.DiaChiLienHe;
            }
        },
        {
            isSort: true,
            nameModel: 'SoDienThoai',
            content: function (data) {
                return data.SoDienThoai;
            }
        },
        {
            isSort: true,
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
                result += '<a href="javascript:void(0)" onclick="onDelete(' + data.ItemID + ', this)" class="btn-remove hvr-radial-out" data-id="' + data.ItemID + '" data-object-name="' + data.TenSanPham + '" title="Xóa"><i class="fa fa-trash"></i></a>';
                result += "</div>";
                return result;
            }
        },

    ];

    var getData = function (pageIndex, sortQuery, pageSize) {
        $.ajax({
            url: '/SanPhamArea/SanPham/GetData',
            type: 'post',
            cache: false,
            data: {
                "pageIndex": pageIndex,
                "sortQuery": sortQuery,
                "pageSize": pageSize
            },
            success: function (data) {
                $("#tbl-sanpham").hinetTable("data", {
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

    var tableData = $("#tbl-sanpham").hinetTable("init", {
        pageSizeList: { size: [20, 50, 100, -1], label: ['20', '50', '100', 'Tất cả'] },
        pagecount: $('#total-page').val(),
        recordCount: $('#total-record').val(),
        getData: getData,
        listItem: groupData,
        config: conf
    });

}
function reloadTable() {
    $("#tbl-sanpham").hinetTable("reload");
}
function detailProduct(id) {
    CallAjaxLoading("get", "/SanPhamArea/SanPham/DetailSanPham", { id: id }, true, function (rs) {
        OpenGlobalModal(rs);
    })
}
$(document).ready(function () {
    pagingConfig();
})