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
        }
    ];

    var getData = function (pageIndex, sortQuery, pageSize) {
        $.ajax({
            url: '/SanPhamArea/SanPham/GetDataPublish',
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