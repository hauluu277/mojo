//Validate dữ liệu trước khi submit form
//element: phần tử cần validate
//element_error: phần tử hiển thị khi validate rỗng
//regex: biểu thức validate


//element_error_regex: phần tử hiển thị khi validate biểu thức
function validate_element(element, element_error, nameRegex, element_error_regex, isNumber) {
    //regex = /[0-9 -()+]+$/;
    //alert(element.attr("id"));
    if (typeof element != undefined) {
        if (element.val().trim().length <= 0) {
            element_error.show();
            element.focus();
            return 0;
        }
        else {
            if (isNumber == "1") {
                if (parseInt(element.val().trim()) <= 0) {
                    element_error.show();
                    element.focus();
                    return 0;
                }
                else {
                    element_error.hide();
                }
            } else {
                element_error.hide();
            }
        }
        return 1;
    }
    return 1;
}
function validate_element_regex(element, element_error, useRegex, nameRegex, element_error_regex, isNumber) {
    var validate_ele = validate_element(element, element_error, nameRegex, element_error_regex, isNumber);
    if (validate_ele == 1) {
        if (useRegex == "1") {
            var regex = new RegExp(nameRegex);
            if (!regex.test(element.val().trim())) {
                element_error_regex.show();
                element.focus();
                return 0;
            }
            else {
                element_error_regex.hide();
            }
        }
    }
    else {
        return 0;
    }
    return 1;
}
function validate_lenght(element, min, max, element_error) {
    if (element.attr("id") == "CMND") {
        if (min == 9 && max == 12) {
            if (element.val().trim().length != min && element.val().trim().length != max) {
                element_error.show();
                element.focus();
                return 0;
            }
            else {
                element_error.hide();
            }
        }
    }
    else {
        if (min > 0) {
            if (element.val().trim().length < min) {
                element_error.show();
                element.focus();
                return 0;
            }
            else {
                element_error.hide();
            }
        }
        else {
            element_error.hide();
        }
        if (max > 0) {
            if (element.val().trim().length > max) {
                element_error.show();
                element.focus();
                return 0;
            }
            else {
                element_error.hide();
            }
        }
        else {
            element_error.hide();
        }
    }
    return 1;
}
function validate_lenght_regex(element, min, max, element_error, useRegex, nameRegex, element_error_regex) {
    element_error_regex.hide(); element_error.show();
    var val_lenght = validate_lenght(element, min, max, element_error);
    if (val_lenght == 1) {
        if (useRegex == "1") {
            var regex = new RegExp(nameRegex);
            if (!regex.test(element.val().trim())) {
                element_error_regex.show();
                element.focus();
                return 0;
            }
            else {
                element_error_regex.hide();
            }
        }
    }
    else {
        return 0;
    }
    return 1;
}
function checkDateTime(date) {
    // regular expression to match required date format
    re = /^(\d{1,2})\/(\d{1,2})\/(\d{4})$/;
    if (date != '') {
        if (regs = date.match(re)) {
            // day value between 1 and 31
            if (regs[1] < 1 || regs[1] > 31) {
                return false;
            }
            // month value between 1 and 12
            if (regs[2] < 1 || regs[2] > 12) {
                return false;
            }
            if ((regs[1] > 29 && regs[2] == 2)) {
                return false;
            }
            // year value between 1895 and now
            if (regs[3] < 1895) {
                return false;
            }
            //try {
            //    var date1 = $.datepicker.parseDate("dd-mm-yy",date);
            //    //$("#SEARCH_BATDAU_ERROR").hide();
            //}
            //catch (err) {
            //   // $("#SEARCH_BATDAU_ERROR").show();
            //    return false;
            //}
        } else {
            return false;
        }
    }
    else {
        return false;
    }
    return true;
}
function checkMonthYear(date) {
    // regular expression to match required date format
    re = /^(\d{1,2})\-(\d{4})$/;
    if (date != '') {
        if (regs = date.match(re)) {
            // month value between 1 and 12
            if (regs[1] < 1 || regs[1] > 12) {
                return false;
            }
            // year value between 1895 and 2081
            if (regs[2] < 1951 && regs[2] > 2081) {
                return false;
            }
        } else {
            return false;
        }
    }
    else {
        return false;
    }
    return true;
}
function validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}
function validatePhone(phone) {
    var re = /^[0-9]{10,11}$/;
    return re.test(phone);
}
function validateCMND(cmnd) {
    var re = /^[0-9]{9,12}$/;
    if (!re.test(cmnd)) {
        return false;
    }
    if (cmnd.length == 9 || cmnd.length == 12) {
        return true;
    }
    return false;
}
function removehtml(Html_string) {
    var isHTML = RegExp.prototype.test.bind(/(<([^>]+)>)/i);
    return isHTML(Html_string);
}
function deletehtml(Html_string) {
    return Html_string.replace(/(<([^>]+)>)/ig, "").trim();
}
function checkfile() {
    var so_file = $("#SO_FILE_CURRENT").val();
    var lst_so_file = so_file.split(',');
    var extension = $("#EXTENSION_HIDDEN").val();
    var lst_extension = extension.split(',');
    var file_size = $("#MAXSIZE_HIDDEN").val();
    var file_error = true;
    outloop:
    for (var i = 0; i < lst_so_file.length - 1; i++) {
        if (document.getElementById('filebase_' + lst_so_file[i]) != null) {
            if ($('#filebase_' + lst_so_file[i]).val() != "") {
                var fsize = $('#filebase_' + lst_so_file[i])[0].files[0].size;
                var ftype = $('#filebase_' + lst_so_file[i])[0].files[0].type;
                var file = $('#filebase_' + lst_so_file[i]).val();
                var extension = file.replace(/^.*\./, '');
                innerloop:
                for (var j = 0; j < lst_extension.length; j++) {
                    if (extension == lst_extension[j].split('.').pop() && fsize <= file_size) {
                        $("#file_null_" + lst_so_file[i]).hide();
                        file_error = true;
                        break innerloop;
                    } else {
                        $("#file_null_" + lst_so_file[i]).show();
                        //alert("ok");
                        file_error = false;
                    }
                }
            }
        }
    }
    if (file_error == false) {
        return false;
    } else {
        return true;
    }
}
function getDateFromString(dateStr) {
    if (dateStr !== null && dateStr.trim() !== '') {
        var from = dateStr.trim().split("/");
        return new Date(from[2], from[1] - 1, from[0]);
    }
    return null;
}
function HtmlValidate(value) {
    return /<[a-z][\s\S]*>/i.test(value);
}


function ToBool(value) {
    if (value != null && value.length > 0) {
        if (value == "true" || value == "True") {
            return true;
        }
    }

    return false;
}
function ToLimitText(value, limit) {
    if (value == null || value == "") {
        return value;
    }
    var valueLength = value.length;
    if (valueLength <= limit) {
        return value;
    }
    if (valueLength > limit) {
        var subtext = value.substr(0, limit);
        return subtext + "...";
    }
}

function YouTubeGetID(url) {
    var ID = '';
    url = url.replace(/(>|<)/gi, '').split(/(vi\/|v=|\/v\/|youtu\.be\/|\/embed\/)/);
    if (url[2] !== undefined) {
        ID = url[2].split(/[^0-9a-z_\-]/i);
        ID = ID[0];
    }
    else {
        ID = url;
    }
    return ID;
}

function isFullscreen() {

    //if ($.browser.opera) {

    //    var fs = $('<div class="fullscreen"></div>');
    //    $('body').append(fs);

    //    var check = fs.css('display') == 'block';
    //    fs.remove();

    //    return check;
    //}

    var st = screen.top || screen.availTop || window.screenTop;

    if (st != window.screenY) {

        return false;
    }

    return window.fullScreen == true || screen.height - document.documentElement.clientHeight <= 30;
}
function ToStatusText(obj) {
    if (obj) {
        return "<span class='text-success font-bold'>Đã được kích hoạt</span>";
    } else {
        return "<span class='text-danger font-bold'>Không kích hoạt</span>";
    }

}

function ToResult(obj) {
    if (obj) {
        return "<span class='text-success font-bold'>Đồng ý</span>";
    } else {
        return "<span class='red font-bold'>Không đồng ý</span>";
    }
}


function ToStatus(obj) {
    if (obj) {
        return '<span class="text-success"><i class="fa fa-check-circle fa-lg" aria-hidden="true"></i></span>';
    } else {
        return '<span class="text-danger"><i class="fa fa-ban fa-lg" aria-hidden="true"></i></span>';
    }
}

String.prototype.format = function () {
    var formatted = this;
    for (var arg in arguments) {
        formatted = formatted.replace("{" + arg + "}", arguments[arg]);
    }
    return formatted;
};

String.formatAll = function (string) {
    var args = Array.prototype.slice.call(arguments, 1, arguments.length);
    return string.replace(/{(\d+)}/g, function (match, number) {
        return typeof args[number] != "undefined" ? args[number] : match;
    });
};

$(".answer").click(function () {
    $(".answer").removeClass("actived");
    $(this).addClass("actived");
});

//$(window).bind('load', function () {
//    $('img').each(function () {
//        if ((typeof this.naturalWidth != "undefined" &&
//            this.naturalWidth == 0)
//            || this.readyState == 'uninitialized') {
//            $(this).attr('src', '/img/no-image3.png');
//        }
//    });
//});

function ReplaceImage() {
    $('img').each(function () {
        if ((typeof this.naturalWidth != "undefined" &&
            this.naturalWidth == 0)
            || this.readyState == 'uninitialized') {
            $(this).attr('src', '/img/no-image3.png');
        }
    });
}

//function SetupFormError(formId) {
//    $("#" + formId + " .has-err").each(function () {
//        var item = $(this);
//        $(item).parent().append("<span class='text-error'>Dữ liệu không được để trống</span>");
//    });
//}
//function FormInValid(formId) {
//    var result = true;
//    $("#" + formId + " .require").each(function () {
//        var value = $(this).val();
//        var item = $(this);
//        var parent = $(item).parent();
//        if (value == null || value == "") {
//            $(parent).find(".text-error").next().show();
//            result = false;
//        } else {
//            $(parent).find(".text-error").next().hide();
//        }
//    });
//    return result;
//}

function formatMoney(num, currency = "") {
    if (num) {
        return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,') + currency;
    }
    return num;
}


function toSubString(obj, to) {
    if (obj == null || to == null) {
        return '';
    }
    if (obj.length <= to) {
        return obj;
    }
    if (obj.length > to) {
        return obj.substring(0, to) + " ...";
    }

}

function formatDateForServer(obj) {
    if (obj == null || obj == '') {
        return obj;
    }
    var date = obj.split("/");
    if (date.length == 3) {
        var day = date[0];
        var month = date[1];
        var year = date[2];
        return month + "/" + day + "/" + year;
    }
    return '';
}

function ShowLoading() {
    $("#loading").show();
}
function HideLoading() {
    $("#loading").hide();
}

/**
 * Điều hướng về trang liền trước
 * @param {string} defaultLink Đường dẫn mặc định
 */
function BackAction(defaultLink = "/Dashboard/Index") {
    if (!!document.referrer || window.history.length > 2) {
        window.location = document.referrer;
    }
    else {
        window.location.href = defaultLink;
    }
}
/**
 * Kiểm tra id truyền lên server có phải là kiểu số hay không, 
 * và trả ra thông báo cho người dùng
 * @param {any} id id định truyền lên server
 */
function IsNotNumber(id) {
    if (typeof id !== "number" || isNaN(id) || Number.isNaN(id)) {
        NotiError("Vui lòng không chỉnh sửa mã nguồn trang");
        return true;
    }
    return false;
}
///**
// * Convert number to money
// * @param {any} value
// * @param {any} tofixed
// * @param {any} d
// * @param {any} t
// */
//function formatMoney(value, tofixed, d, t) {
//    if (value) {
//        var tofixed = isNaN(tofixed = Math.abs(tofixed)) ? 0 : tofixed,
//            d = d == undefined ? "." : d,
//            t = t == undefined ? "," : t,
//            s = value < 0 ? "-" : "",
//            i = String(parseInt(value = Math.abs(Number(value) || 0).toFixed(tofixed))),
//            j = (j = i.length) > 3 ? j % 3 : 0;

//        return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (tofixed ? d + Math.abs(value - i).toFixed(tofixed).slice(2) : "");
//    }
//};
/**
 * Sinh ra mảng năm, tương tự phương thức range của Python
 * @param {number} start Năm bắt đầu
 * @param {number} end Năm kết thúc
 */
function yearRange(start, end, isReverse = false) {
    let length = end - start + 1;
    const result = new Array(length);
    if (!isReverse) {
        while (length--) {
            result[length] = end--;
        }
    }
    else {
        while (length--) {
            result[length] = start++;
        }
    }
    return result;
}
/**
 * Sinh ra option chọn năm theo khoảng số nguyên
 * @param {number} startRange Khoảng bắt đầu
 * @param {number} endRange Khoảng kết thúc
 */
function yearOptionGenerate(startRange = 0, endRange = 0) {
    let currentYear = new Date().getFullYear();
    let listYear = yearRange(currentYear - startRange, currentYear + endRange, true);
    let result = "";

    if (listYear.length > 0) {
        for (let year of listYear) {
            result += `<option value="${year}">${year}</option>`
        }
    }

    return result;
}

function ToResultText(obj) {
    if (obj) {
        return "Đồng ý";
    } else {
        return "Không đồng ý";
    }
    return "";
}
/**
 * Sinh ra option chọn năm theo khoảng năm
 * @param {number} endYear Năm kết thúc
 * @param {number} startYear Năm bắt đầu, mặc định là 2000
 * @param {boolean} isReverse Có sắp xếp từ dưới lên không, mặc định là true
 */
function yearOptionGenerateWithYearInput(endYear, startYear = 2000, isReverse = true) {
    let listYear = yearRange(startYear, encodeURI, isReverse),
        result = "";

    if (listYear.length > 0) {
        for (let year of listYear) {
            result += `<option value="${year}">${year}</option>`;
        }
    }

    return result;
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


function NotifySuccess(message) {
    notif({
        type: 'success',
        position: 'bottom',
        msg: message,
    });

    //try {
    //    notif({
    //        type: 'success',
    //        msg: message,
    //        position: 'right',
    //        timeout: 1000,
    //        clickable: true,
    //        color: "white",
    //    });
    //} catch (err) {
    //    console.log(err);
    //}
}
function NotifyError(message) {
    notif({
        type: 'error',
        position: 'bottom',
        msg: message,
    });
    //try {
    //    notif({
    //        type: 'error',
    //        msg: message,
    //        position: 'bottom',
    //        timeout: 5000,
    //        clickable: true
    //    });
    //} catch (err) {
    //    console.log(err);
    //}
}
function NotifyWarning(message) {
    notif({
        type: 'warning',
        position: 'bottom',
        msg: message,
    });

    //try {
    //    notif({
    //        type: 'warning',
    //        msg: message,
    //        position: 'right',
    //        timeout: 100,
    //        clickable: true
    //    });
    //} catch (err) {
    //    console.log(err);
    //}
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
(function () {
    $(window).scroll(function () {
        if ($(window).scrollTop() >= 200) {
            $('.paneLeftOut').addClass('fixed-tops');
            $('.paneRightOut').addClass('fixed-tops');

        } else {
            $('.paneLeftOut').removeClass('fixed-tops');
            $('.paneRightOut').removeClass('fixed-tops');
        }
    });
    function scrolltop() {
        $('html,body').animate({
            scrollTop: 0
        }, 'fast');
    }
    if ($('.video-box-list').length > 0) {
        $('.video-box-list').slick({
            slidesToShow: 2,
            slidesToScroll: 1,
            autoplay: false,
            autoplaySpeed: 2000,
        });
    }
})();


function DoCheck(control, check) {
    for (var i = 0; i < control.length; i++) {
        if (control[i].type.toLowerCase() == "checkbox" && control[i].id.indexOf('chk') != -1) {
            control[i].checked = check;
        }
    }
};
function DoCheckAll(obj) {
    var control = document.getElementsByTagName("input");
    DoCheck(control, obj.checked);
};

function CheckItem(obj) {
    var control = document.getElementsByTagName("input");
    var controlChkAll;
    var allControl = 0;
    var checkedControl = 0;
    for (var i = 0; i < control.length; i++) {
        if (control[i].type.toLowerCase() == "checkbox" && control[i].id.indexOf('chk') != -1) {
            allControl++;
            if (control[i].checked == true) {
                checkedControl++;
            }
        }
        if (control[i].type.toLowerCase() == "checkbox" && control[i].id.indexOf('checkAll') != -1) {
            controlChkAll = control[i];
        }
    }
    if (allControl == checkedControl) {
        controlChkAll.checked = true;
    }
    else {
        controlChkAll.checked = false;
    }
}

function CallAjaxLoading(type, url, parameter, loading, callback) {
    $.ajax({
        beforeSend: function () {
            if (loading) {
                $("#loading").show();
            }
        },
        complete: function () {
            if (loading) {
                $("#loading").hide();
            }
        },
        type: type,
        url: url,
        data: parameter,
        cache: false,
        contentType: "application/json; charset=utf-8",
        //dataType: "json",
        async: true,
        success: function (res) {
            callback(res);
        }, error: function (err) {
            console.log("Đã có lỗi xảy ra");
            console.log(err);
            console.log(err.responseText);
        }

    });
}
function CallAjaxFix(type, url, parameter, callback) {
    $.ajax({
        type: type,
        url: url,
        data: parameter,
        cache: false,
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (res) {
            callback(res);
        }, error: function (err) {
            console.log("Đã có lỗi xảy ra");
            console.log(err.responseText);
        }

    });
}

function CallAjax(type, url, parameter, callback) {
    $.ajax({
        type: type,
        url: url,
        data: parameter,
        cache: false,
        async: true,
        contentType: "application/json; charset=utf-8",
        //dataType: "json",
        success: function (res) {
            callback(res);
        }, error: function (err) {
            console.log("Đã có lỗi xảy ra");
            console.log(err.responseText);
        }

    });
}


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

function removeJsonObject(arr, func) {
    for (var i = 0; i < arr.length; i++) {
        if (func.call(arr[i])) {
            arr.splice(i, 1);
            return arr;
        }
    }
}

function hasJsonObject(arr, func) {
    for (var i = 0; i < arr.length; i++) {
        if (func.call(arr[i])) {
            return true;
        }
    }
    return false;
}

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

function toTrim(obj) {
    if (obj != null && obj.length > 0) {
        return obj.trim();
    }
    return obj;
}

/*In nội dung bài viết*/
function PrintSubject(titlePage) {
    $(".gotop").hide();
    $(".feedback").hide();
    $(".fblikebutton").hide();
    $("h2.moduletitle").hide();
    $(".pnOtherArticle").hide();
    $(".otherpanel").hide();
    $(".print").hide();
    var htmlContent = $('.articlecenter-rightnav div').html();
    var obj;
    obj = document.getElementById('tableforprint');
    myWindow = window.open('_blank', 'Preview', 'resizable = 1 ,scrollbars =1 , menubar = 1 ,status=1,left = 0 ,top = 0 ');
    myWindow.document.write("<html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'>");
    myWindow.document.write("<head>");
    myWindow.document.write("<title>" + titlePage + "</title><style type='text/css'>.Title{ font-family: 'Times New Roman'; font-size: 14pt; font-weight: bold; margin-top: 0 }</style>");
    myWindow.document.write("<link rel='stylesheet' type='text/css' href='/Data/Sites/1/skins/art42-blue/style.css' />");
    //myWindow.document.write("<link rel='stylesheet' type='text/css' href='/CMS/css/ISE_SP.css' media='print' />");
    myWindow.document.write("</head>");
    myWindow.document.write("<body style='width:700px;margin: 0px auto 0;background-color: #FFF;' >");

    myWindow.document.write("<div align='right' style='width:700px;margin-left:auto;margin-right:auto'><a herf='' onclick='javascript:print();return false;'><img style='border-width:0px;cursor: pointer;' src='/Data/Sites/1/skins/art42-blue/images/printer32x32.png' alt='In trang'/></a></div>");
    myWindow.document.write("<div style='float:left;width:100%;font-size:25px;'><center>" + $(".siteheading").html() + "</center></div>");
    //siteheading
    myWindow.document.write("<div style='float:left;width:100%'>" + document.URL + "</div>");
    myWindow.document.write("<div align='left' style='width:100%;text-align:left;margin-left:auto;margin-right:auto'>" + htmlContent + "</div><br/>");
    myWindow.document.write("<div style='float:left;width:100%'>" + document.URL + "</div>");
    myWindow.document.write("");
    myWindow.document.write("</body>");
    myWindow.document.write("</html>");
    myWindow.document.close();
    $(".gotop").show();
    $(".feedback").show();
    $(".fblikebutton").show();
    $("h2.moduletitle").show();
    $(".pnOtherArticle").show();
    $(".otherpanel").show();
    $(".print").show();
    return false;
}
function CallAjaxLoading(type, url, parameter, loading, callback) {
    $.ajax({
        beforeSend: function () {
            if (loading) {
                $("#loading").show();
            }
        },
        complete: function () {
            if (loading) {
                $("#loading").hide();
            }
        },
        type: type,
        url: url,
        data: parameter,
        cache: false,
        contentType: "application/json; charset=utf-8",
        //dataType: "json",
        async: true,
        success: function (res) {
            callback(res);
        }, error: function (err) {
            console.log("Đã có lỗi xảy ra");
            console.log(err);
            console.log(err.responseText);
        }

    });
}
function CallAjaxFix(type, url, parameter, callback) {
    $.ajax({
        type: type,
        url: url,
        data: parameter,
        cache: false,
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (res) {
            callback(res);
        }, error: function (err) {
            console.log("Đã có lỗi xảy ra");
            console.log(err.responseText);
        }

    });
}

function CallAjax(type, url, parameter, callback) {
    $.ajax({
        type: type,
        url: url,
        data: parameter,
        cache: false,
        async: true,
        contentType: "application/json; charset=utf-8",
        //dataType: "json",
        success: function (res) {
            callback(res);
        }, error: function (err) {
            console.log("Đã có lỗi xảy ra");
            console.log(err.responseText);
        }
    });
}

function OpenFormModal(type, url, parameter) {
    $.ajax({
        beforeSend: function () {
            if (loading) {
                $("#loading").show();
            }
        },
        complete: function () {
            if (loading) {
                $("#loading").hide();
            }
        },
        type: type,
        url: url,
        data: parameter,
        cache: false,
        contentType: "application/json; charset=utf-8",
        //dataType: "json",
        async: true,
        success: function (rs) {
            $("#globalModal").html(rs);
            $("#globalModal").modal("show");
        }, error: function (err) {
            console.log("Đã có lỗi xảy ra");
            console.log(err);
            console.log(err.responseText);
        }

    });
}
function AjaxError() {
    NotifyError("Không thể thực hiện thao thác này");
}

function CloseGlobalModal() {
    $("#globalModal").modal("hide");
    $("#globalModal").html('');
}
function OpenGlobalModal(rs) {
    $("#globalModal").modal("show");
    $("#globalModal").html(rs);


    function OpenGlobalModal2(type, url, parameter, loading) {
        $.ajax({
            beforeSend: function () {
                if (loading) {
                    $("#loading").show();
                }
            },
            complete: function () {
                if (loading) {
                    $("#loading").hide();
                }
            },
            type: type,
            url: url,
            data: parameter,
            cache: false,
            contentType: "application/json; charset=utf-8",
            //dataType: "json",
            async: true,
            success: function (res) {
                console.log(res);
                $("#globalModal").html(res);
                $("#globalModal").modal("show");
            }, error: function (err) {
                console.log("Đã có lỗi xảy ra");
                console.log(err);
                console.log(err.responseText);
            }
        });
    }
}





function StatusIcon(status) {
    if (status == 1 || status == true) {
        return '<i class="fa fa-check text-success" aria-hidden="true"></i>';
    } else {
        return '<i class="fa fa-ban text-danger" aria-hidden="true"></i>';
    }
}

$(document).ajaxStart(function () {
    $("#loading").show();
})

$(document).ajaxComplete(function () {
    $("#loading").hide();
})

function LoadCkeditor(id) {
    CKEDITOR.replace(id, {
        toolbar: [
            { name: 'document', items: ['Source', '-', 'Save', 'NewPage', 'ExportPdf', 'Preview', 'Print', '-', 'Templates'] },
            { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
            { name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', '-', 'Scayt'] },
            { name: 'forms', items: ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'] },
            '/',
            { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'CopyFormatting', 'RemoveFormat'] },
            { name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl', 'Language'] },
            { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
            { name: 'insert', items: ['Image', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'] },
            '/',
            { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
            { name: 'colors', items: ['TextColor', 'BGColor'] },
            { name: 'tools', items: ['Maximize', 'ShowBlocks'] },
        ], filebrowserImageBrowseUrl: '/ClientScript/ckfinder/ckfinder.html?type=Images',
        filebrowserUploadUrl: '/ClientScript/ckfinder/core/connector/aspx/connector.aspx',
        filebrowserBrowseUrl: '/ClientScript/ckfinder/ckfinder.html',
        filebrowserFlashBrowseUrl: '/ClientScript/ckfinder/ckfinder.html?type=Flash',
        allowedContent: true
    });


}


function LoadCkeditorFull(id) {
    CKEDITOR.replace(id, {
        toolbar: [
            { name: 'document', groups: ['mode', 'document', 'doctools'], items: ['Source', '-', 'Save', 'NewPage', 'Preview', 'Print', '-', 'Templates'] },
            { name: 'clipboard', groups: ['clipboard', 'undo'], items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
            { name: 'editing', groups: ['find', 'selection', 'spellchecker'], items: ['Find', 'Replace', '-', 'Scayt'] },
            '/',
            { name: 'basicstyles', groups: ['basicstyles', 'cleanup'], items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },
            { name: 'paragraph', groups: ['list', 'blocks', 'align', 'bidi'], items: ['NumberedList', 'BulletedList', '-', 'Blockquote'] },
            { name: 'links', items: ['Link', 'Unlink'] },
            { name: 'insert', items: ['Image', 'Flash', 'Table', 'Iframe'] },
            { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
            { name: 'colors', items: ['TextColor', 'BGColor'] },
            '/',
            { name: 'forms', items: ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'] },
        ],
        filebrowserImageBrowseUrl: '/ClientScript/ckfinder/ckfinder.html?type=Images',
        filebrowserUploadUrl: '/ClientScript/ckfinder/core/connector/aspx/connector.aspx',
        filebrowserBrowseUrl: '/ClientScript/ckfinder/ckfinder.html',
        filebrowserFlashBrowseUrl: '/ClientScript/ckfinder/ckfinder.html?type=Flash',
        allowedContent: true
    });


}

function LoadCkeditorSimple(id) {
    var editor = CKEDITOR.replace(id, {
        toolbar: [
            { name: 'document', groups: ['mode', 'document', 'doctools'], items: ['Source', '-', 'Save', 'NewPage', 'Preview'] },
            { name: 'clipboard', groups: ['clipboard', 'undo'], items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
            { name: 'editing', groups: ['find', 'selection', 'spellchecker'], items: ['Find', 'Replace', '-', 'Scayt'] },
            { name: 'insert', items: ['Image', 'Flash', 'Table', 'Iframe'] },
        ],
        filebrowserImageBrowseUrl: '/ClientScript/ckfinder/ckfinder.html?type=Images',
        filebrowserUploadUrl: '/ClientScript/ckfinder/core/connector/aspx/connector.aspx',
        filebrowserBrowseUrl: '/ClientScript/ckfinder/ckfinder.html',
        filebrowserFlashBrowseUrl: '/ClientScript/ckfinder/ckfinder.html?type=Flash',
    });
    //CKFinder.setupCKEditor(editor, { basePath: '/ClientScript/ckfinder/ckfinder.html?Type=Images', rememberLastFolder: false });
}

function GetValueCkeditor(id) {
    var cc = id;
    var value = CKEDITOR.instances[id].getData();
    $("#" + id).val(value);
}