//$('.live-search-list li').each(function () {
//    $(this).attr('data-search-term', $(this).text().toLowerCase());
//});
//xóa các tin bài liên quan đã được chọn
function CreateTag() {
    $("#btnCreateTag").attr("disabled", "disabled");
    $("#btnCreateTag").addClass("disabled");
    var data_insert = { tag: $("#txtTag").val(), description: $("#txtdesTag").val() }
    var result = "";
    $.ajax({
        type: "POST",
        cache: false,
        url: "/Article/EditPost.aspx/SaveTag",
        contentType: "application/json; charset= utf-8",
        dataType: "json",
        async: false,
        data: JSON.stringify(data_insert),
        success: function (data) {
            result = data.d;
        }, complete: function (done) {
            //alert(done);
            //$("#lboxTag").append("<option vallue='" + result + "'>" + $("#txtTag").val() + "</option");
            //$("#fstResults").append("<span class='fstResultItem'>" + $("#txtTag").val() + "</span>");
            NotifySuccess("Thêm mới tag thành công");
            $(".modal-tag").hide();
            $("#modaltag").modal("hide");
            $("#txtTag").val("");
            $("#txtdesTag").val("")
        }

    });
}
$(document).on("change", "#txtTag", function () {
    if ($(this).val() == null || $(this).val().trim() == "") {
        $("#btnCreateTag").attr("disabled", "disabled");
        $("#btnCreateTag").addClass("disabled");
        $("#nullTag").show();
    } else {
        $("#btnCreateTag").removeAttr("disabled");
        $("#btnCreateTag").removeClass("disabled");
        $("#nullTag").hide();
    }
});
function showtag() {
    $("#modaltag").modal("show");
    $(".modal-tag").show();
}
$(document).ready(function () {

    var active = $("#ArticleReferenceAtive").val();
    if (active != null && active.length > 0) {
        var listArticleReferencen = JSON.parse($("#hdfReference").val());
        var listActive = active.split(',');
        var strActive = "";

        for (var i = 0; i < listActive.length; i++) {
            var activeReference = getByValue(listArticleReferencen, listActive[i]);
            if (activeReference != null) {
                strActive += "<tr data-id='" + activeReference.ItemID + "'>";
                strActive += "<td>" + activeReference.Title + "</td>";
                strActive += "<td><span style='cursor: pointer;' onclick='removeRefenreceActive(" + activeReference.ItemID + ")' title='Xóa tin bài liên quan'><i class='fa fa-times' aria-hidden='true'></i></span></td>";
                strActive += "</tr>";
            }
        }
        $("#refenreceActive").show();
        $("#tblRefenreceActive").html(strActive);
    }
});
function getByValue(arr, value) {

    var result = [];

    arr.forEach(function (o) { if (o.ItemID == value) result.push(o); });

    return result ? result[0] : null; // or undefined
}
function getByActive(arr, value) {
    var result = [];

    arr.forEach(function (o) { if (o == value) result.push(o); });

    return result ? result[0] : null; // or undefined
}
$(document).on("click", "#articleReference .item-wrap .item", function () {
    $(this).toggleClass("selected");
    $("#btnApplyRefence").addClass("disabled");
    $("#btnApplyRefence").attr("disabled", "disabled");
    $("#articleReference .item-wrap .selected").each(function () {
        $("#btnApplyRefence").removeClass("disabled");
        $("#btnApplyRefence").removeAttr("disabled", "disabled");
    });
});
$(document).on("change", "#ddlCategoryReference", function () {
    if ($(this).val() > 0) {
        var categoryId = $(this).val();
        $('#articleReference .item-wrap').each(function () {
            if ($(this).attr("data-category") == categoryId) {
                $(this).fadeIn();
            } else {
                $(this).fadeOut();
            }
        });
    } else {
        var searchTitle = xoa_dau($(this).val());
        $('#articleReference .item-wrap').each(function () {

            if ($(this).filter('[data-search-title *= ' + searchTitle + ']').length > 0 || searchTitle.length < 1) {
                $(this).fadeIn();
            } else {
                $(this).fadeOut();
            }

        });
    }
});
var countShowReference = 0;
//Hiển thị popup dialog chọn các tin bài liên quan
function ShowArticleReference() {
    $("#myModal").modal("show");
    $(".modal-lg").show();
    $("#loadingReference").show();
    if (countShowReference > 0) {
        $(".item-wrap .item").removeClass("selected");
        var activeReference = $("#ArticleReferenceAtive").val();
        if (activeReference != null && activeReference.length > 0) {
            var listActive = activeReference.split(',');
            for (var i = 0; i < listActive.length; i++) {
                if (listActive[i] != null) {
                    $(".item-wrap[data-id='" + listActive[i] + "']").find(".item").addClass("selected");
                }
            }
        }
    }
    else {
        var listArticleReferencen = JSON.parse($("#hdfReference").val());
        var active = $("#ArticleReferenceAtive").val();
        if (active != null && active.length > 0) {
            var listActive = active.split(',');

            var append = "";
            for (var i = 0; i < listArticleReferencen.length; i++) {
                var activeReference = getByActive(listActive, listArticleReferencen[i].ItemID);

                append += "<div class='item-wrap' data-category='" + listArticleReferencen[i].CategoryID + "' title='" + listArticleReferencen[i].Title + "' data-id='" + listArticleReferencen[i].ItemID + "' data-search-title='" + listArticleReferencen[i].FTS + "'>";
                if (activeReference != null) {
                    append += "<div class='item selected'>";
                } else {
                    append += "<div class='item'>";
                }
                append += "<div class='referenceTitle' >";
                append += "<span>" + listArticleReferencen[i].Title + "</span>";
                append += "</div>";
                append += "<div class='referenceDate'>";
                append += "<i class='fa fa-clock-o' aria-hidden='true'></i>&nbsp;";
                append += "<span>" + listArticleReferencen[i].StartDate + "</span>";
                append += "</div>";
                append += "<div class='referenceCategory'>";
                append += "<i class='published fa fa-globe xicon-active-true' aria-hidden='true'></i>&nbsp;";
                append += listArticleReferencen[i].CategoryName;
                append += "</div>";
                append += "</div>";
                append += "</div>";
            }
        } else {
            var append = "";
            for (var i = 0; i < listArticleReferencen.length; i++) {
                append += "<div class='item-wrap' data-category='" + listArticleReferencen[i].CategoryID + "' title='" + listArticleReferencen[i].Title + "' data-id='" + listArticleReferencen[i].ItemID + "' data-search-title='" + listArticleReferencen[i].FTS + "'>";
                append += "<div class='item'>";
                append += "<div class='referenceTitle' >";
                append += "<span>" + listArticleReferencen[i].Title + "</span>";
                append += "</div>";
                append += "<div class='referenceDate'>";
                append += "<i class='fa fa-clock-o' aria-hidden='true'></i>&nbsp;";
                append += "<span>" + listArticleReferencen[i].StartDate + "</span>";
                append += "</div>";
                append += "<div class='referenceCategory'>";
                append += "<i class='published fa fa-globe xicon-active-true' aria-hidden='true'></i>&nbsp;";
                append += listArticleReferencen[i].CategoryName;
                append += "</div>";
                append += "</div>";
                append += "</div>";
            }
        }
        $("#articleReference").html(append);
        $("#loadingReference").hide();
        //$.get("/Article/ArticleReference.html", function (data) {
        //    $("#articleReference").html(data);
        //    $("#loadingReference").hide();
        //});
    }
    countShowReference++;
}
function fillterReference() {
    $(".entity-picker-filter").slideToggle();
}

function removeRefenreceActive(id) {
    if (confirm("Xóa tin bài liên quan đã chọn ?")) {
        $("table#tblRefenreceActive tr[data-id='" + id + "']").remove();
        var strActive = "";
        $("table#tblRefenreceActive tr").each(function () {
            strActive += $(this).attr("data-id") + ",";
        });
        //Cập nhật lại các tin bài liên quan được chọn
        $("#ArticleReferenceAtive").val(strActive);
    }
}
//Lưu lại các tin bài đã được chọn
function ApplyProduct() {
    $("#btnApplyRefence").attr("disabled", "disabled");
    $("#btnApplyRefence").addClass("disabled");
    var renferenceAtive = "";
    var strActive = "";

    $("#articleReference .item-wrap .item.selected").each(function () {
        renferenceAtive += $(this).parent().attr("data-id") + ",";
        strActive += "<tr data-id='" + $(this).parent().attr("data-id") + "'>";
        strActive += "<td>" + $(this).find(".referenceTitle").text() + "</td>";
        strActive += "<td><span style='cursor: pointer;' onclick='removeRefenreceActive(" + $(this).parent().attr("data-id") + ")' title='Xóa tin bài liên quan'><i class='fa fa-times' aria-hidden='true'></i></span></td>";
        strActive += "</tr>";
    });
    $("#tblRefenreceActive").html(strActive);
    $("#refenreceActive").show();
    $("#ArticleReferenceAtive").val(renferenceAtive);
    $("#btnApplyRefence").removeClass("disabled");
    $("#btnApplyRefence").removeAttr("disabled", "disabled");
}

function xoa_dau(str) {
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
    str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
    str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
    str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
    str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
    str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
    str = str.replace(/Đ/g, "D");
    return str;
}


//live search
$('#ReferenceTitle').on('keyup', function () {
    //console.log(listArticleReferencen);
    var searchTitle = xoa_dau($(this).val());
    $('#articleReference .item-wrap').each(function () {

        if ($(this).filter('[data-search-title *= ' + searchTitle + ']').length > 0 || searchTitle.length < 1) {
            $(this).fadeIn();
        } else {
            $(this).fadeOut();
        }

    });

});