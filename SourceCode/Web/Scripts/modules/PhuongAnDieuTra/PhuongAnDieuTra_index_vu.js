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
                return "<input type='checkbox' value='" + data.SiteID + "'/>"
            }
        },
        {
            tdClass: "text-left",
            isSort: true,
            nameModel: 'SiteName',
            content: function (data) {
                var html = data.SiteName;
                if (data.FileDuThao != null) {
                    html += `<p><i class="fa fa-tags" aria-hidden="true"></i> &nbsp; <a class="red" href="${data.FileDuThao}" target="_blank">File dự thảo</a></p>`;
                }
                if (data.IsTongDieuTra) {
                    html += "<p class='red font-bold'>Tổng điều tra</p>";
                }

                return html;
            }
        },
        {
            tdClass: "center",
            isSort: true,
            nameModel: 'LinhVucID',
            content: function (data) {
                return data.LinhVucName;
            }
        },
        {
            tdClass: "center",
            isSort: true,
            nameModel: 'Nam',
            content: function (data) {
                return data.Nam;
            }
        },
        {
            tdClass: "center",
            isSort: false,
            nameModel: 'HanGopY',
            content: function (data) {
                var result = toDateString(data.HanGopY);
                if (data.IsRoleTongHopGopY == true) {
                    result += "<br/>";
                    result += "<p class='btn-group gr-nghiphep'>";
                    result += `<a href="javascript:FormHanGopY(${data.SiteID})" class="btn-edit hvr-radial-out" data-id="10595" title="Cập nhật hạn góp ý">Cập nhật</a>`
                    result += "</p>";
                }
                return result;
            }
        },
        {
            tdClass: "center",
            isSort: false,
            nameModel: 'TongHopYKien',
            content: function (data) {
                var result = "<div class='btn-group gr-nghiphep'>";
                // (${data.TotalGopY} góp ý)
                result += `<a href="javascript:OpenIndexTongHopGopY(${data.SiteID})" class="btn-edit hvr-radial-out" data-id="10595" title="Xem danh sách góp ý"><i class="fa fa-tags fa-lg"></i> Xem (${data.TotalGopY})</a>`
                result += "</div>";
                return result;
            }
        },
        {
            tdClass: "center",
            isSort: false,
            nameModel: 'TotalGopY',
            content: function (data) {
                var result = "<div class='btn-group gr-nghiphep'>";
                //(${data.TotalGopY} góp ý)
                result += `<a href="javascript:OpenIndexTongHopYKien(${data.SiteID})" class="btn-edit hvr-radial-out" data-id="10595" title="Xem danh sách góp ý"><i class="fa fa-tags fa-lg"></i> Xem (${data.TotalGopY})</a>`
                result += "</div>";
                return result;
            }
        },

        {
            tdClass: "center",
            isSort: false,
            nameModel: "",

            content: function (data) {
                var result = "<div class='btn-group gr-nghiphep'>";
                if (data.IsHetHanGopY == false) {
                    if (data.IsLanhDao) {
                        if (data.GopYCapCuc != null) {
                            if (data.GopYCapCuc.TrangThaiCucTongHop == 1) {
                                result += `<a href="javascript:OpenFormDuyet(${data.SiteID},${data.GopYCapCuc.ItemID},true)" class="btn btn-success" data-id="10595" title="Xem, sửa và duyệt"><i class="fa fa-tags fa-lg"></i> Xem, sửa và duyệt</a>`
                            } else {
                                result += `<a href="javascript:DetailGopY(${data.GopYCapCuc.ItemID})" class="btn btn-success" data-id="10595" title="Xem góp ý đã duyệt"><i class="fa fa-tags fa-lg"></i> Xem góp ý đã duyệt</a>`
                            }
                        } else {
                            result += `<a href="javascript:void(0)" class="disabled btn btn-success" data-id="10595" title="Xem, sửa và duyệt"><i class="fa fa-tags fa-lg"></i> Xem, sửa và duyệt</a>`
                        }

                    } else {
                        if (data.PheDuyetCuc != null && data.PheDuyetCuc.ItemID > 0) {
                            result += `<a href="javascript:DetailGopY(${data.PheDuyetCuc.ItemID})" class="btn btn-success" data-id="10595" title="Xem góp ý đã duyệt"><i class="fa fa-tags fa-lg"></i> Xem góp ý đã duyệt</a>`
                        } else {
                            if (data.PhuongAnDieuTra != null && data.PhuongAnDieuTra.TrangThaiCucTongHop == 1) {
                                result += `<a href="javascript:OpenForm(${data.SiteID},${data.PhuongAnDieuTra.ItemID})" class="btn btn-success" data-id="10595" title="Chỉnh sửa"><i class="fa fa-tags fa-lg"></i>  Chỉnh sửa</a>`
                            } else {
                                result += `<a href="javascript:OpenForm(${data.SiteID},0)" class="btn btn-success" data-id="10595" title="Góp ý phương án"><i class="fa fa-tags fa-lg"></i> Góp ý phương án</a>`
                            }

                        }

                    }
                }
                else {
                    result += "<strong class='red'>Đã hết hạn góp ý</strong>";
                }
                result += "</div>";
                return result;
            }
        },
    ];

    var getData = function (pageIndex, sortQuery, pageSize) {
        $.ajax({
            url: '/PhuongAnDieuTraArea/PhuongAnDieuTra/GetData',
            type: 'post',
            cache: false,
            data: {
                "pageIndex": pageIndex,
                "sortQuery": sortQuery,
                "pageSize": pageSize
            },
            success: function (data) {
                $("#tbl-investigate").hinetTable("data", {
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

    var tableData = $("#tbl-investigate").hinetTable("init", {
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



function reloadTablePhuongAn() {
    $("#tbl-investigate").hinetTable("reload");
}

$(document).ready(function () {
    pagingConfig();
})