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
    if (confirm("Xác nhận api '" + name + "'?")) {
        $.ajax({
            type: "post",
            url: "/SettingServiceArea/SettingService/Delete",
            data: { id: id },
            success: function (rs) {
                if (rs.Status) {
                    NotifySuccess("Xóa api thành công");
                    reloadTable();
                } else {
                    NotifyError("Xóa api thất bại");
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
                return "<input type='checkbox' value='" + data.SiteID + "'/>"
            }
        },
        {
            isSort: true,
            nameModel: 'SiteName',
            content: function (data) {
                var html = `<a href="/GlobalModule/Report/Detail.aspx?reportid=${data.SiteID}">${data.SiteName}</a>`;
                if (data.IsTongDieuTra) {
                    html += "<p class='red font-bold'>Tổng điều tra</p>";
                }
                return html;
            }
        },
        {
            isSort: true,
            nameModel: 'LinhVucID',
            content: function (data) {
                return data.LinhVucName;
            }
        },
        {
            isSort: true,
            nameModel: 'TotalService',
            content: function (data) {
                var value = `<a href="javascript:OpenService(${data.SiteID})"> ${data.TotalService} webservice/api</a>`;
                return value;
            }
        },
        {
            isSort: true,
            nameModel: 'CreatedDate',
            content: function (data) {
                return toDate(data.CreatedDate);
            }
        },

        //{
        //    isSort: false,
        //    nameModel: "",
        //    tdClass: "center",

        //    content: function (data) {
        //        var result = "<div class='btn-group gr-nghiphep'>";
        //        result += '<a href="javascript:onEdit(' + data.SiteID + ')" class="btn-edit hvr-radial-out" data-id="10595" title="Chỉnh sửa"><i class="fa fa-pencil-square fa-lg"></i></a>'
        //        result += '<a href="javascript:void(0)" onclick="onDelete(' + data.SiteID + ', this)" class="btn-remove hvr-radial-out" data-id="' + data.SiteID + '" data-object-name="' + data.SiteName + '" title="Xóa"><i class="fa fa-trash"></i></a>';
        //        result += "</div>";
        //        return result;
        //    }
        //},

    ];

    var getData = function (pageIndex, sortQuery, pageSize) {
        $.ajax({
            url: '/SettingServiceArea/SettingService/GetData',
            type: 'post',
            cache: false,
            data: {
                "pageIndex": pageIndex,
                "sortQuery": sortQuery,
                "pageSize": pageSize
            },
            success: function (data) {
                $("#tbl-SettingService").hinetTable("data", {
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

    var tableData = $("#tbl-SettingService").hinetTable("init", {
        pageSizeList: { size: [20, 50, 100, -1], label: ['20', '50', '100', 'Tất cả'] },
        pagecount: $('#total-page').val(),
        recordCount: $('#total-record').val(),
        getData: getData,
        listItem: groupData,
        config: conf
    });

}

function OpenService(siteId) {
    OpenGlobalModal("get", "/SettingServiceArea/SettingService/IndexService", { siteId: siteId }, true);
}


function reloadTable() {
    $("#tbl-SettingService").hinetTable("reload");
}
function detailProduct(id) {
    CallAjaxLoading("get", "/SettingServiceArea/SettingService/DetailSanPham", { id: id }, true, function (rs) {
        OpenGlobalModal(rs);
    })
}
$(document).ready(function () {
    pagingConfig();
})