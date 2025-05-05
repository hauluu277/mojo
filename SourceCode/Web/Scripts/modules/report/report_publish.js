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
                return toDate(data.NgayCongBo);
            }
        },
        {
            isSort: true,
            nameModel: 'PathFile',
            content: function (data) {
                var html = "";

                if (data.PathFile) {
                    var listFile = data.PathFile.split('?');
                    for (var i = 0; i < listFile.length; i++) {
                        html += `<p><a href="${listFile[i]}">${data.TenBaoCao}</a></p>`;
                    }
                   
                }
                return html;
            }
        },

    ];

    var getData = function (pageIndex, sortQuery, pageSize) {
        $.ajax({
            url: '/BaoCaoArea/BaoCao/GetPublish',
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
function reloadTable() {
    $("#tbl-BaoCao").hinetTable("reload");
}

$(document).ready(function () {
    pagingConfig();
})