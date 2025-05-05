
function onDeleteLeader(id, obj) {
    var name = $(obj).attr("data-object-name");
    if (confirm("Xác nhận xóa lãnh đạo '" + name + "'?")) {
        $.ajax({
            type: "post",
            url: "/OrganizationArea/Organization/DeleteLeader",
            data: { id: id },
            success: function (rs) {
                if (rs.Status) {
                    NotifySuccess("Xóa lãnh đạo thành công");
                    reloadTableLeader();
                } else {
                    NotifyError("Xóa lãnh đạo thất bại");
                }
            }, error: function (er) {
                console.log(er.Message);
            }
        })
    }
}
function onDeleteDepartment(id, obj) {
    var name = $(obj).attr("data-object-name");
    if (confirm("Xác nhận xóa phòng ban '" + name + "'?")) {
        $.ajax({
            type: "post",
            url: "/OrganizationArea/Organization/DeleteDepartment",
            data: { id: id },
            success: function (rs) {
                if (rs.Status) {
                    NotifySuccess("Xóa phòng ban thành công");
                    reloadTableDepartment();
                } else {
                    NotifyError("Xóa phòng ban thất bại");
                }
            }, error: function (er) {
                console.log(er.Message);
            }
        })
    }
}