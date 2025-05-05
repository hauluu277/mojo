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
            nameModel: 'AnhNoiBat',
            content: function (data) {
                return "<img src='" + data.AnhNoiBat+"' style='width:250px;height:150px'/>";
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

    ];

    var getData = function (pageIndex, sortQuery, pageSize) {
        $.ajax({
            url: '/DeTaiArea/DeTai/GetDataPublish',
            type: 'post',
            cache: false,
            data: {
                "pageIndex": pageIndex,
                "sortQuery": sortQuery,
                "pageSize": pageSize,
                "SiteID": $("#SiteID").val()
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
    CallAjaxLoading("get", "/DeTaiArea/DeTai/DetailPublish", { id: id }, true, function (rs) {
        OpenGlobalModal(rs);
    })
}
$(document).ready(function () {
    pagingConfig();
})