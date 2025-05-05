var pager;
var actives = [];


    String.prototype.formatAll = function () {
        var args;
        args = arguments;
        if (args.length === 1 && args[0] !== null && typeof args[0] === 'object') {
            args = args[0];
        }
        return this.replace(/{([^}]*)}/g, function (match, key) {
            return (typeof args[key] !== "undefined" ? args[key] : match);
        });
    };

String.prototype.format = function () {
    var formatted = this;
    for (var arg in arguments) {
        formatted = formatted.replace("{" + arg + "}", arguments[arg]);
    }
    return formatted;
};

function getURLParameters(paramName) {
    paramName = paramName.toLowerCase();
    var sURL = window.document.URL.toString().toLowerCase();
    if (sURL.indexOf("?") > 0) {
        var arrParams = sURL.split("?");
        var arrURLParams = arrParams[1].split("&");
        var arrParamNames = new Array(arrURLParams.length);
        var arrParamValues = new Array(arrURLParams.length);

        var i = 0;
        for (i = 0; i < arrURLParams.length; i++) {
            var sParam = arrURLParams[i].split("=");
            arrParamNames[i] = sParam[0];
            if (sParam[1] != "")
                arrParamValues[i] = unescape(sParam[1]);
            else
                arrParamValues[i] = "No Value";
        }

        for (i = 0; i < arrURLParams.length; i++) {
            if (arrParamNames[i] == paramName) {
                //alert("Parameter:" + arrParamValues[i]);
                return arrParamValues[i];
            }
        }
        return "No Parameters Found";
    }
}

$(document).ready(function () {
    if (!String.prototype.format) {
        String.prototype.format = function () {
            var args;
            args = arguments;
            if (args.length === 1 && args[0] !== null && typeof args[0] === 'object') {
                args = args[0];
            }
            return this.replace(/{([^}]*)}/g, function (match, key) {
                return (typeof args[key] !== "undefined" ? args[key] : match);
            });
        };
    }


    var url_string = window.location.href;


    var itemId = getURLParameters("itemid");



    var parameter = {
        itemId: itemId
    }
    //CallAjax("post", "/Article/PostArticle.aspx/GetArticleReference", JSON.stringify(parameter), function (data) {
    
    //    var result = data.d;
    //    if (result) {
    //        for (var i = 0; i < result.length; i++) {
    //            actives.push({ id: result[i].ItemID, title: result[i].Title });
    //        }
    //    }
    //    console.log("GetArticleReference");
    //    console.log(actives);
    //});

});


$(document).on("click", "#articleReference .item-wrap .item", function () {

    var current = $(this);
    var id = current.parent().attr("data-id");
    var title = current.parent().attr("title");

    if (current.hasClass("selected")) {
        actives = removeJsonObject(actives, function () {
            return this.id == id;
        });
    } else {
        actives.push({ id: id, title: title });
    }
    current.toggleClass("selected");
    LoadActive();
});

function LoadActive() {
    console.log("GetArticleReference");
    console.log(actives);
    var append = "<ul>";
    if (actives) {
        for (var i = 0; i < actives.length; i++) {
            append += "<li data-id='" + actives[i].id + "'>";
            append += "<span onclick='removeActive(" + actives[i].id + ")'>×</span>";
            append += actives[i].title;
            append += "</li>";
        }
    } else {
        append += "<li>Chưa có tin bài được chọn</li>";
    }
    append += "</ul>";
    $("#reference_Active").html(append);
}


function LoadReference(data) {
    var clone = $("#tempReference").val();
    var append = "";
    $.each(data.d.ListItem, function (index, element) {
        var selected = "";
        if (hasJsonObject(actives, function () { return this.id == element.ItemID })) {
            selected = "selected";
        }
        append += clone.formatAll({ title: element.Title, id: element.ItemID, active: selected, startDate: element.StartDate, categoryName: element.CategoryName });
    });

    $("#articleReference").html(append);
}

function setup_pagination(data) {
    if (data.d.TotalPage > 1) {
        if (pager) {
            pager.reload({
                totalPage: data.d.TotalPage,
            });
        } else {
            pager = new Pagination({
                parent: '#pagination',
                totalPage: data.d.TotalPage,
                align: 'left',
                prevText: '❮  ',
                nextText: '  ❯'
            });
        }

        pager.on('afterChange', function (args) {
            var param = {
                pageIndex: args.currentPage,
                pageSize: 9,
                category: parseInt($("#ddlCategoryReference").val()),
                keyword: $("#ReferenceTitle").val(),
                typeSearch:0
            }
            CallAjaxLoading("post", "/Article/PostArticle.aspx/GetPageReferenceBO", JSON.stringify(param), true, LoadReference);
        });
    } else {
        $('#pagination').html('');
    }
}

function ReferenSearch() {
    var param = {
        pageIndex: 1,
        pageSize: 9,
        category: parseInt($("#ddlCategoryReference").val()),
        keyword: $("#ReferenceTitle").val(),
        typeSearch: 0
    };
    CallAjaxLoading("post", "/Article/PostArticle.aspx/GetPageReferenceBO", JSON.stringify(param), true, function (data) {
        LoadReference(data);
        setup_pagination(data);
    });
}

//Hiển thị popup dialog chọn các tin bài liên quan
function ShowArticleReference() {
    $("#myModal").modal("show");
    if (!$("#myModal").hasClass("actived")) {
        $("#myModal").addClass("actived");
        $(".modal-lg").show();
        var param = {
            pageIndex: 1,
            pageSize: 9,
            category: 0,
            keyword: '',
            typeSearch: 0
        };
        CallAjaxLoading("post", "/Article/PostArticle.aspx/GetPageReferenceBO", JSON.stringify(param), true, function (data) {
            LoadReference(data);
            setup_pagination(data);
        });
    }
}

function removeActive(id) {
    if (confirm("Xác nhận xóa")) {
        $("#articleReference .item-wrap[data-id='" + id + "'] .item").toggleClass("selected");
        $("#reference_Active ul li[data-id='" + id + "']").remove();
    }
}


function removeRefenreceActive(id) {
    if (confirm("Xóa tin bài liên quan đã chọn ?")) {
        $("table#tblRefenreceActive tr[data-id='" + id + "']").remove();
        var strActive = "";
        $("table#tblRefenreceActive tr").each(function () {
            strActive += $(this).attr("data-id") + ",";
        });

        actives = removeJsonObject(actives, function () {
            return this.id == id;
        });
        LoadActive();
        //Cập nhật lại các tin bài liên quan được chọn
        $("#ArticleReferenceAtive").val(strActive);
    }
}
//Lưu lại các tin bài đã được chọn
function ApplyProduct() {
    console.log("ApplyProduct");
    console.log(actives);
    var renferenceAtive = "";
    var strActive = "";
    var activeString = [];


    if (actives) {
        $.each(actives, function (index, element) {
            activeString.push(element.id);
            strActive += "<tr data-id='" + element.id + "'>";
            strActive += "<td>" + element.title + "</td>";
            strActive += "<td><span class='poiter' onclick='removeRefenreceActive(" + element.id + ")' title='Xóa tin bài liên quan'><i class='fa fa-times' aria-hidden='true'></i></span></td>";
            strActive += "<td>";
        });
    }

    $("#tblRefenreceActive").html(strActive);
    $("#refenreceActive").show();
    $("#ArticleReferenceAtive").val(activeString.join(","));
    $("#myModal").modal("hide");
    $(".modal-tag").hide();
}


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
