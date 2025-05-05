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

function configControls() {
    $('#btn--add-new').on('click', function () {
        onOpenEditModal('/DMDANHMUCDATAArea/DMDANHMUCDATA/Edit', { groupId: $('#hidden--nhom-danhmuc-id').val() }, 'post');
    });
}

/**
 * cập nhật danh mục
 * @param {*} id
 */
function onEdit(id) {
    onOpenEditModal('/DMDANHMUCDATAArea/DMDANHMUCDATA/Edit', { groupId: $('#hidden--nhom-danhmuc-id').val(), id: id }, 'post');
}

/**
 * xóa danh mục
 * @param {*} id
 * @param {*} event
 */
function onDelete(id, event) {
    var objectName = $(event).data('object-name');
    onConfirmDelete('/DMDANHMUCDATAArea/DMDANHMUCDATA/Delete', { id: id }, objectName, reloadTable);
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
                return "<input type='checkbox' value='" + data.ID + "'/>"
            }
        },

        {
            isSort: true,
            nameModel: 'TEXT',
            content: function (data) {
                var value = data.TEXT;
            
                return value;
            }
        },
        {
            isSort: true,
            nameModel: 'CODE',
            content: function (data) {
                return data.CODE
            }
        },
        {
            isSort: true,
            nameModel: 'DATA',
            tdClass: 'center',
            content: function (data) {
                return data.DATA;
            }
        },
        {
            isSort: true,
            nameModel: 'THUTU_HIENTHI',
            content: function (data) {
                if (data.THUTU_HIENTHI) {
                    return data.THUTU_HIENTHI;
                } else {
                    return "0";
                }
            }
        },
        {
            isSort: true,
            nameModel: 'GHICHU',
            content: function (data) {
                return (data.GHICHU == "null" ? "" : data.GHICHU)
            }
        },
        {
            isSort: false,
            nameModel: "",
            tdClass: "center",

            content: function (data) {
                var result = "<div class='btn-group gr-nghiphep'>";
                result += generateEditButton(data.ID);
                result += generateRemoveButton(data.ID, data.TEXT);
                if (isDanhMucNghiPhep) {
                    result += "<a class='btn btn-primary' style='color:white' title='Cấu hình' href='javascript:void(0)' onclick='onCauHinhNghiPhep(" + data.ID + ")'>";
                    result += "<i class='fa fa-cog fa-lg'></i>&nbsp;Vai trò && phép nghỉ";
                    result += "</a>";


                    result += "<a class='btn btn-primary' style='color:white' title='Cấu hình chữ ký số' href='javascript:void(0)' onclick='onCauHinhChuKySo(" + data.ID + ")'>";
                    result += "<i class='fa fa-cog fa-lg'></i>&nbsp;Chữ ký số";
                    result += "</a>";
                }

                if (isDanhMucCongXa) {
                    result += "<a class='btn btn-primary' style='color:white' title='Cấu hình chữ ký số' href='javascript:void(0)' onclick='onCauHinhPhieuCongXa(" + data.ID + ")'>";
                    result += "<i class='fa fa-cog fa-lg'></i>&nbsp;Chữ ký số";
                    result += "</a>";
                }

                if (isDanhMucBienBanTongHop) {
                    result += "<a class='btn btn-primary' style='color:white' title='Cấu hình chữ ký số' href='javascript:void(0)' onclick='onCauHinhFileBienBan(" + data.ID + ")'>";
                    result += "<i class='fa fa-cog fa-lg'></i>&nbsp;File biên bản";
                    result += "</a>";
                }
                result += "</div>";
                return result;
            }
        },

    ];

    var getData = function (pageIndex, sortQuery, pageSize) {
        $.ajax({
            url: '/DMDANHMUCDATAArea/DMDANHMUCDATA/GetData',
            type: 'post',
            cache: false,
            data: {
                "id": $('#hidden--nhom-danhmuc-id').val(),
                "pageIndex": pageIndex,
                "sortQuery": sortQuery,
                "pageSize": pageSize
            },
            success: function (data) {
                $("#tbl-dmdanhmucdata").hinetTable("data", {
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

    var tableData = $("#tbl-dmdanhmucdata").hinetTable("init", {
        pageSizeList: { size: [20, 50, 100, -1], label: ['20', '50', '100', 'Tất cả'] },
        pagecount: $('#total-page').val(),
        recordCount: $('#total-record').val(),
        getData: getData,
        listItem: groupData,
        config: conf
    });

}