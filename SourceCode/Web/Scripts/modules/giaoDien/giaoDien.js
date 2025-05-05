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
    OpenFormModal('get', '/GiaoDienArea/GiaoDien/FormGiaoDien', { id: id });
}
/**
 * cập nhật danh mục
 * @param {*} id
 */
function onDetail(id) {
    OpenFormModal('get', '/GiaoDienArea/GiaoDien/DetailGiaoDien', { id: id });
}


/**
 * xóa danh mục
 * @param {*} id
 * @param {*} event
 */
function onDelete(id, obj) {
    var name = $(obj).attr("data-object-name");
    if (confirm("Xác nhận xóa giao diện '" + name + "'?")) {
        $.ajax({
            type: "post",
            url: "/GiaoDienArea/GiaoDien/Delete",
            data: { id: id },
            success: function (rs) {
                if (rs.Status) {
                    NotifySuccess("Xóa giao diện thành công");
                    reloadTable();
                } else {
                    NotifyError("Xóa giao diện thất bại");
                }
            }, error: function (er) {
                console.log(er.Message);
            }
        })
    }
}


function ChonGiaoDien(maGiaoDien) {
    if (confirm(`Xác nhận chọn giao diện ${maGiaoDien} làm giao diện mặc định của trang?`)) {
        $.ajax({
            type: "post",
            url: "/GiaoDienArea/GiaoDien/SaveChonGiaoDien",
            data: { maGiaoDien: maGiaoDien },
            success: function (rs) {
                if (rs.Status) {
                    NotifySuccess("Chọn giao diện thành công");
                    //reloadTable();
                    window.location.href = window.location.href;
                } else {
                    NotifyError("Chọn giao diện thất bại");
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
            nameModel: 'TenGiaoDien',
            content: function (data) {
                var html = `<a rel="previewitem" title="Xem trước giao diện (${data.MaGiaoDien})" href="/default.aspx?skin=${data.MaGiaoDien}"> ${data.TenGiaoDien}</a>`;
                if (data.IsActiveGiaoDien) {
                    html += `<p class="red">(Đang sử dụng)</p>`;
                }
                return html;
            }
        },
        {
            isSort: true,
            nameModel: 'MaGiaoDien',
            content: function (data) {
                return data.MaGiaoDien
            }
        },
        {
            isSort: true,
            nameModel: 'DuongDanZipTaiLen',
            content: function (data) {
                return `<a href="/${data.DuongDanZipTaiLen}" download="/${data.DuongDanZipTaiLen}">` + data.DuongDanZipTaiLen + `</a>`

            }
        },
        //{
        //    isSort: true,
        //    nameModel: 'IdLinhVuc',
        //    content: function (data) {
        //        return data.TenLinhVuc;
        //    }
        //},
        //{
        //    isSort: true,
        //    tdClass: "center",
        //    nameModel: 'IsPublish',
        //    content: function (data) {
        //        return StatusIcon(data.IsPublish);
        //    }
        //},
        {
            isSort: false,
            nameModel: "",
            tdClass: "center",

            content: function (data) {
                var result = "<div class='btn-group gr-nghiphep'>";
                result += '<a href="javascript:onDetail(' + data.ItemID + ')" class="btn-edit hvr-radial-out" data-id="10595" title="Thông tin giao diện"><i class="fa fa-info-circle fa-lg"></i></a>'
                result += '<a href="javascript:onEdit(' + data.ItemID + ')" class="btn-edit hvr-radial-out" data-id="10595" title="Chỉnh sửa"><i class="fa fa-pencil-square fa-lg"></i></a>'
                result += '<a href="javascript:void(0)" onclick="onDelete(' + data.ItemID + ', this)" class="btn-remove hvr-radial-out" data-id="' + data.ItemID + '" data-object-name="' + data.TenGiaoDien + '" title="Xóa"><i class="fa fa-trash"></i></a>';
                result += "</div>";
                result += `<p style="margin-top:20px"><button type="button" onclick="ChonGiaoDien('${data.MaGiaoDien}')">Chọn giao diện</button></p>`;
                return result;
            }
        },

    ];

    var getData = function (pageIndex, sortQuery, pageSize) {
        $.ajax({
            url: '/GiaoDienArea/GiaoDien/GetData',
            type: 'post',
            cache: false,
            data: {
                "pageIndex": pageIndex,
                "sortQuery": sortQuery,
                "pageSize": pageSize
            },
            success: function (data) {
                $("#tbl-GiaoDien").hinetTable("data", {
                    pageSize: pageSize != -1 ? pageSize : data.Count,
                    pageIndex: pageIndex,
                    pagecount: data.TotalPage,
                    recordCount: data.Count,
                    listItem: data.ListItem,
                    showCheckBox: false,
                });
            },
            error: function (err) {
                CommonJS.alert(xhr.responseText);
            }
        });

    }

    var tableData = $("#tbl-GiaoDien").hinetTable("init", {
        pageSizeList: { size: [20, 50, 100, -1], label: ['20', '50', '100', 'Tất cả'] },
        pagecount: $('#total-page').val(),
        recordCount: $('#total-record').val(),
        getData: getData,
        listItem: groupData,
        config: conf,
        showCheckBox: false,
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
    $("#tbl-GiaoDien").hinetTable("reload");
}

$(document).ready(function () {
    pagingConfig();
    var colorBoxConfig = {
        width: '95%', height: '98%', iframe: true
    };
    $("a[rel='previewitem']").colorbox(colorBoxConfig);
    $("a[rel='previewitem']").click(function (e) {
        e.preventDefault();
    });
})