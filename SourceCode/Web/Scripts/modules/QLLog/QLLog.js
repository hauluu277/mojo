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
 * Cấu hình hiển thị
 * @param {*} id
 */
function onModalConfig() {
    OpenFormModal('get', '/QLLogArea/QLLog/FormConfig');
}

var getData = function (pageIndex, sortQuery, pageSize) {
    $.ajax({
        url: '/QLLogArea/QLLog/GetData',
        type: 'post',
        cache: false,
        data: {
            "pageIndex": pageIndex,
            "sortQuery": sortQuery,
            "pageSize": pageSize
        },
        success: function (data) {
            $("#tbl-QLLog").hinetTable("data", {
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
    $("#tbl-QLLog").hinetTable("reload");
}

$(document).ready(function () {
    pagingConfig();
})