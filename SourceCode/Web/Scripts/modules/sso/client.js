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
    OpenFormModal('get', '/ClientArea/Client/FormClient', { id: id });
}

function onDongBoChuyenMucTin(id) {
    OpenFormModal('get', '/ClientArea/Client/DongBoChuyenMucTin', { id: id })
}

/**
 * xóa danh mục
 * @param {*} id
 * @param {*} event
 */
function onDelete(id, obj) {
    var name = $(obj).attr("data-object-name");
    if (confirm("Xác nhận Xóa ứng dụng '" + name + "'?")) {
        $.ajax({
            type: "post",
            url: "/ClientArea/Client/Delete",
            data: { id: id },
            success: function (rs) {
                if (rs.Status) {
                    NotifySuccess("Xóa ứng dụng thành công");
                    reloadTable();
                } else {
                    NotifyError("Xóa ứng dụng thất bại");
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
            tdClass: "text-left",
            isSort: true,
            nameModel: '',
            content: function (data) {
                var result = `<div class="btn-group">
                               <button data-toggle="dropdown" class="btn btn-primary btn-white dropdown-toggle" aria-expanded="false">Thao tác
                               </button>
                               <ul class="dropdown-menu">`;
                result += `<li><a href='javascript:void(0)' onclick='onDongBoChuyenMucTin(${data.ItemID})' title='Đồng bộ chuyên mục tin'><i class='fa fa-pencil'></i> Đồng bộ chuyên mục</a></li>`
                result += "<li><a href='javascript:void(0)' onclick='onEdit(" + data.ItemID + ")'   title = 'Chỉnh sửa'><i class='fa fa-pencil'> </i> Sửa thông tin</a> </li>";
                result += "<li><a href='javascript:void(0)' onclick='onDelete(" + data.ItemID + ")'  title = 'Xóa'><i class='fa fa-trash' style='color:red'> </i> Xóa</a></li>";
                result += "</ul></div>";
                return result;
            }
        },
        {
            tdClass: "text-left",
            isSort: true,
            nameModel: 'ClientName',
            content: function (data) {
                var html = data.ClientName;
                return html;
            }
        },
        {
            tdClass: "center",
            isSort: true,
            nameModel: 'ClientID',
            content: function (data) {
                return data.ClientID;
            }
        },
        {
            tdClass: "center",
            isSort: true,
            nameModel: 'ClientUrl',
            content: function (data) {
                return data.ClientUrl;
            }
        },
        {
            tdClass: "center",
            isSort: false,
            nameModel: 'CategoryName',
            content: function (data) {
                var result = data.CategoryName;
                return result;
            }
        },
        //{
        //    tdClass: "center",
        //    isSort: false,
        //    nameModel: 'ClientSignIn',
        //    content: function (data) {
        //        var result = data.ClientSignIn;
        //        return result;
        //    }
        //},
        //{
        //    tdClass: "center",
        //    isSort: false,
        //    nameModel: 'ClientSignOut',
        //    content: function (data) {
        //        var result = data.ClientSignOut;
        //        return result;
        //    }
        //},
    ];

    var getData = function (pageIndex, sortQuery, pageSize) {
        $.ajax({
            url: '/ClientArea/Client/GetData',
            type: 'post',
            cache: false,
            data: {
                "pageIndex": pageIndex,
                "sortQuery": sortQuery,
                "pageSize": pageSize
            },
            success: function (data) {
                $("#tbl-client").hinetTable("data", {
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

    var tableData = $("#tbl-client").hinetTable("init", {
        pageSizeList: { size: [20, 50, 100, -1], label: ['20', '50', '100', 'Tất cả'] },
        pagecount: $('#total-page').val(),
        recordCount: $('#total-record').val(),
        getData: getData,
        listItem: groupData,
        config: conf
    });

}

function OpenForm(siteId, id, isPhuongAnCuc) {
    OpenGlobalModal("get", "/PhuongAnDieuTraArea/PhuongAnDieuTra/FormGopY", { siteId: siteId, id: id, isPhuongAnCuc: isPhuongAnCuc }, true);
}
function OpenFormDuyet(siteId, id) {
    OpenGlobalModal("get", "/PhuongAnDieuTraArea/PhuongAnDieuTra/FormDuyetGopY", { siteId: siteId, id: id }, true);
}


function FormHanGopY(siteId) {
    OpenGlobalModal("get", "/PhuongAnDieuTraArea/PhuongAnDieuTra/FormHanGopY", { siteId: siteId }, true);
}


function OpenIndexGopY(siteId) {
    OpenGlobalModal("get", "/PhuongAnDieuTraArea/PhuongAnDieuTra/IndexGopY", { siteId: siteId }, true);
}
function DetailGopY(id) {
    OpenGlobalModal("get", "/PhuongAnDieuTraArea/PhuongAnDieuTra/DetailGopY", { gopYId: id }, true);
}

function OpenIndexTongHopYKien(siteId) {
    OpenGlobalModal("get", "/PhuongAnDieuTraArea/PhuongAnDieuTra/IndexTongHopYKien", { siteId: siteId }, true);
}



function OpenIndexTongHopGopY(siteId) {
    OpenGlobalModal("get", "/PhuongAnDieuTraArea/PhuongAnDieuTra/IndexTongHopGopY", { siteId: siteId }, true);
}



function reloadTable() {
    $("#tbl-client").hinetTable("reload");
}

$(document).ready(function () {
    pagingConfig();
})