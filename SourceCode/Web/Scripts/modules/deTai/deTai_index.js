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
    var siteId = $("#SiteID").val();
    OpenFormModal('get', '/DeTaiArea/DeTai/FormDeTai', { id: id, SiteID: siteId });
}

/**
 * xóa danh mục
 * @param {*} id
 * @param {*} event
 */
function onDelete(id, obj) {
    var name = $(obj).attr("data-object-name");
    if (confirm("Xác nhận xóa đề tài '" + name + "'?")) {
        $.ajax({
            type: "post",
            url: "/DeTaiArea/DeTai/Delete",
            data: { id: id },
            success: function (rs) {
                if (rs.Status) {
                    NotifySuccess("Xóa đề tài thành công");
                    reloadTable();
                } else {
                    NotifyError("Xóa đề tài thất bại");
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
            nameModel: 'MaDeTai',
            content: function (data) {
                var value = data.MaDeTai;

                return value;
            }
        },
        {
            isSort: true,
            nameModel: 'TenDeTai',
            content: function (data) {
                return `<a href='javascript:detailDeTai(` + data.ItemID + `)'>${data.TenDeTai}</a>`;
            }
        },
        {
            isSort: true,
            nameModel: 'ChuNhiemDeTai',
            content: function (data) {
                return data.ChuNhiemDeTai;
            }
        },

        {
            isSort: true,
            nameModel: 'ThuocChuongTrinh',
            content: function (data) {
                return data.ThuocChuongTrinhName;
            }
        },
        {
            isSort: true,
            nameModel: 'ThoiGianThucHien',
            content: function (data) {
                return data.ThoiGianThucHien;
            }
        },
        {
            isSort: true,
            nameModel: 'TongKinhPhi',
            content: function (data) {
                return data.TongKinhPhi;
            }
        },
        {
            isSort: true,
            nameModel: 'XepLoai',
            content: function (data) {
                return data.XepLoaiName;
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
                result += '<a href="javascript:void(0)" onclick="onDelete(' + data.ItemID + ', this)" class="btn-remove hvr-radial-out" data-id="' + data.ItemID + '" data-object-name="' + data.TenDeTai + '" title="Xóa"><i class="fa fa-trash"></i></a>';
                result += "</div>";
                return result;
            }
        },

    ];

    var getData = function (pageIndex, sortQuery, pageSize) {
        $.ajax({
            url: '/DeTaiArea/DeTai/GetData',
            type: 'post',
            cache: false,
            data: {
                "pageIndex": pageIndex,
                "sortQuery": sortQuery,
                "pageSize": pageSize,
                "siteID": $("#SiteID").val()
            },
            success: function (data) {
                $("#tbl-DeTai").hinetTable("data", {
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

    var tableData = $("#tbl-DeTai").hinetTable("init", {
        pageSizeList: { size: [20, 50, 100, -1], label: ['20', '50', '100', 'Tất cả'] },
        pagecount: $('#total-page').val(),
        recordCount: $('#total-record').val(),
        getData: getData,
        listItem: groupData,
        config: conf
    });

}
function reloadTable() {
    $("#tbl-DeTai").hinetTable("reload");
}
function detailDeTai(id) {
    CallAjaxLoading("get", "/DeTaiArea/DeTai/DetailDeTai", { id: id }, true, function (rs) {
        OpenGlobalModal(rs);
    })
}
$(document).ready(function () {
    pagingConfig();
})