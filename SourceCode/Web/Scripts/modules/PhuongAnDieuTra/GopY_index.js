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


function pagingConfigGopY() {
    var isLanhDao = $("#IsLanhDao").val();
    var conf = [];
    conf = [
        {
            tdClass: "text-left width-30",
            isSort: false,
            nameModel: "",
            isCounter: true,
            content: function (data) {
                return "<input type='checkbox' value='" + data.SiteID + "'/>"
            }
        },
        {
            isSort: true,
            nameModel: 'HoTen',
            tdClass: "text-left",
            content: function (data) {
                var html = data.HoTen;
                if (data.ChucVu != null && data.ChucVu != '') {
                    html += "<p class='text-left'>(" + data.ChucVu + ")</p>";
                }
                return html;
            }
        },
        {
            isSort: true,
            nameModel: 'AD_TenDonViCS',
            content: function (data) {
                return data.TenDonVi;
            }
        },
        {
            isSort: true,
            nameModel: 'AD_TenPhongBan',
            content: function (data) {
                return data.TenPhongBan;
            }
        },
        {
            isSort: true,
            nameModel: 'CreatedDate',
            content: function (data) {
                return toDate(data.CreatedDate);
            }
        },
        {
            isSort: true,
            nameModel: 'CreatedDate',
            content: function (data) {
                if (data.IsNhatTriVoiDuThao) {
                    return "Nhất trí với dự thảo";
                }
                return "<span class='red'>Có ý kiến</span>";
            }
        },
        {
            isSort: false,
            nameModel: "",
            tdClass: "center",

            content: function (data) {
                var result = "<div class='btn-group gr-nghiphep'>";
                //1: Chờ phê duyệt
                //2: Đã phê duyệt
                if (data.AllowPheDuyet && data.TrangThai == 1) {
                    result += `<a href="javascript:PheDuyetGopY(${data.ItemID})" class="btn-edit hvr-radial-out" data-id="10595" title="Phê duyệt góp ý phương án"><i class="fa fa-share fa-lg"></i> Phê duyệt</a>`
                    result += "<br/>";
                    result += "<br/>";
                }
                result += `<a href="javascript:DetailGopY(${data.ItemID})" class="btn-edit hvr-radial-out" data-id="10595" title="Xem thông tin góp ý phương án"><i class="fa fa-exclamation-circle fa-lg"></i> Xem góp ý</a>`
                result += "<br/>";
                result += "<br/>";
                if (data.TrangThai == 2) {
                    result += "<span>(Đã duyệt)</span>";
                } else {
                    result += "<span class='red'>(Chờ duyệt)</span>";
                }

                result += "</div>";

                result += "</div>";
                return result;
            }
        },

    ];

    var getData = function (pageIndex, sortQuery, pageSize) {
        $.ajax({
            url: '/PhuongAnDieuTraArea/PhuongAnDieuTra/GetDataGopY',
            type: 'post',
            cache: false,
            data: {
                "pageIndex": pageIndex,
                "sortQuery": sortQuery,
                "pageSize": pageSize
            },
            success: function (data) {
                $("#tbl-gopy").hinetTable("data", {
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

    var tableData = $("#tbl-gopy").hinetTable("init", {
        pageSizeList: { size: [20, 50, 100, -1], label: ['20', '50', '100', 'Tất cả'] },
        pagecount: $('#total-page-gopy').val(),
        recordCount: $('#total-record-gopy').val(),
        getData: getData,
        listItem: groupData,
        config: conf
    });

}


function PheDuyetGopY(id) {
    if (confirm("Xác nhận phê duyệt góp ý phương án điều tra")) {
        $.ajax({
            url: '/PhuongAnDieuTraArea/PhuongAnDieuTra/SaveGopYPhuongAn',
            type: 'post',
            cache: false,
            data: {
                id: id
            },
            success: function (rs) {
                if (rs.Status) {
                    NotifySuccess("Phê duyệt góp ý phương án điều tra thành công");
                    reloadTable();
                } else {
                    NotifyError("Không thể thực hiện thao tác này!");
                }
            },
            error: function (err) {
                CommonJS.alert(xhr.responseText);
            }
        });
    }
}




function DetailGopY(id) {
    OpenGlobalModal2("get", "/PhuongAnDieuTraArea/PhuongAnDieuTra/DetailGopY", { gopYId: id }, true);
}


function OpenService(siteId) {
    OpenGlobalModal("get", "/PhuongAnDieuTraArea/PhuongAnDieuTra/IndexService", { siteId: siteId }, true);
}


function reloadTable() {
    $("#tbl-gopy").hinetTable("reload");
}

$(document).ready(function () {
    pagingConfigGopY();
})