var jsArray = {};
var LstRole = [];

function loadScript(scriptName, callback) {

    if (!jsArray[scriptName]) {
        var promise = jQuery.Deferred();

        // adding the script tag to the head as suggested before
        var body = document.getElementsByTagName('body')[0],
            script = document.createElement('script');
        script.type = 'text/javascript';
        script.src = scriptName;

        // then bind the event to the callback function
        // there are several events for cross browser compatibility
        script.onload = function () {
            promise.resolve();
        };

        // fire the loading
        body.appendChild(script);

        // clear DOM reference
        //body = null;
        //script = null;

        jsArray[scriptName] = promise.promise();
    }

    jsArray[scriptName].then(function () {
        if (typeof callback === 'function')
            callback();
    });
}

///Khởi tạo giá trị quyền
function InitRole(lstRoleSv) {
    LstRole = [];
    LstRole = lstRoleSv;
}

function LinkRole(Url, Name, Role, strClass, title) {

    //Nếu Role - Mã thao tác tồn tại thì xét quyền truy cập
    if (Role && Role.trim().length > 0) {
        //Nếu Không tồn tại user hoặc User không có bất cứ thao tác nào
        if (!LstRole || LstRole.length == 0) {
            return "";
        }
        else {
            //Trường hợp tồn tại thao tác xét Role có nằm trong danh sách thao tác người dùng có không ?
            if (LstRole.indexOf(Role) < 0) {
                return "";
            }
        }
    }

    var strUrl = Url;

    var builder = '<a href="' + strUrl + '" class="' + strClass + '" title="' + title + '">' + Name + '</a>';
    return builder;
}

//Kiểm tra xem người dùng có quyền thao tác không
function HasRole(Role) {
    if (Role && Role.trim().length > 0) {
        //Nếu Không tồn tại user hoặc User không có bất cứ thao tác nào
        if (!LstRole || LstRole.length == 0) {
            return false;
        }
        else {
            //Trường hợp tồn tại thao tác xét Role có nằm trong danh sách thao tác người dùng có không ?
            if (LstRole.indexOf(Role) >= 0) {
                return true;
            }
        }
    }
    return false;
}
function LinkRoleFunc(Url, Name, Role, strClass, lstParam, title) {

    //Nếu Role - Mã thao tác tồn tại thì xét quyền truy cập
    if (Role && Role.trim().length > 0) {
        //Nếu Không tồn tại user hoặc User không có bất cứ thao tác nào
        if (!LstRole || LstRole.length == 0) {
            return "";
        }
        else {
            //Trường hợp tồn tại thao tác xét Role có nằm trong danh sách thao tác người dùng có không ?
            if (LstRole.indexOf(Role) < 0) {
                return "";
            }
        }
    }

    var strUrl = Url;
    strUrl = strUrl + "(";
    if (lstParam != null && lstParam.length > 0) {
        strUrl += lstParam.join(",");
    }

    strUrl += ")";

    var builder = '<a href="javascript:void(0)"  onClick="' + strUrl + '" title="' + title + '" class="' + strClass + '" >' + Name + '</a>';
    return builder;
}

function NotiSuccess(message) {
    //$.smallBox({
    //    title: "Thông báo",
    //    content: message,
    //    color: "#27ae60",
    //    iconSmall: "fa fa-check fa-2x fadeInRight animated",
    //    timeout: 4000
    //});
    notif({
        type: 'success',
        position: 'bottom',
        msg: message,
        timeout: 5000
    });
}
function NotiWarning(message) {
    notif({
        type: 'warning',
        position: 'bottom',
        msg: message,
        timeout: 5000
    });
}
function NotiError(message) {
    //$.smallBox({
    //    title: "Có lỗi",
    //    content: message,
    //    color: "#e67e22",
    //    iconSmall: "fa fa-warning fa-2x fadeInRight animated",
    //    timeout: 4000
    //});
    notif({
        type: 'error',
        position: 'bottom',
        msg: message,
        timeout: 5000
    });
}

function checkRequireWithIDErr(formID, fieldID, errID) {
    var check_err = false;
    if ($("#" + formID + " #" + fieldID).val().trim() == "") {
        $("#" + formID + " #" + errID).html("Bạn phải nhập thông tin này");
        $("#" + formID + " #" + errID).css('display', 'inline');
        check_err = true;
    } else {
        $(this).next().find(".error").css('display', 'none');
    }
    return check_err;
}

function checkRequireElement(formID, fieldID) {
    var check_err = true;
    var item = $("#" + formID + " #" + fieldID);
    var parent = item.parents(" .form-group").first();
    var errText = parent.find(".error");
    if (item.val() == null || item.val().trim() == "") {

        errText.html("Bạn phải nhập thông tin này");
        errText.css('display', 'inline');

        check_err = false;
    } else {
        errText.css('display', 'none');
    }
    return check_err;
}
function checkRequireElementSelect(formID, fieldID) {
    var check_err = true;
    var item = $("#" + formID + " #" + fieldID);
    var parent = item.parents(" .form-group").first();
    var errText = parent.find(".error");
    if (item.val() == null || item.val().length == 0) {

        errText.html("Bạn phải nhập thông tin này");
        errText.css('display', 'inline');

        check_err = false;
    } else {
        errText.css('display', 'none');
    }
    return check_err;
}

function RequireDropDownlist(formID) {
    var check_err = true;
    var item = $("#" + formID + " select.requiredDropDownList");
    item.each(function () {
        var parent = $(this).parents(" .form-group").first();
        var errText = parent.find(".error");
        if ($(this).val() == null || $(this).val().length == 0) {

            errText.html("Bạn phải nhập thông tin này");
            errText.css('display', 'inline');
            check_err = false;
        } else {
            errText.css('display', 'none');
        }
    })

    return check_err;
}

function checkRequireTextArea(form_id) {
    var check_err = true;
    $("#" + form_id + " .requiredTextArea").each(function () {
        var parent = $(this).parents(" .form-group").first();
        var errText = parent.find(".error");
        if ($(this).html() == null || $(this).html().trim() == "") {

            errText.html("Bạn phải nhập thông tin này");
            errText.css('display', 'inline');

            check_err = false;
        } else {
            errText.css('display', 'none');
        }
    });
    return check_err;
}

function ValidateFieldNumber(formID) {
    var valid = true;
    var pattern = /^[0-9]+$/;
    var obj = $("#" + formID + " .validateNumber");
    obj.each(function () {
        var elem = $(this);
        var parent = $(this).parents(" .form-group").first();
        var errText = parent.find(".error");
        if ($(this).val().trim() != "") {
            if (!pattern.test($(this).val())) {
                errText.html("Bạn chỉ được nhập số");
                errText.css("display", "inline");
                valid = false;
                elem.addClass("forcusError");

            } else {
                errText.css("display", "none");
                elem.removeClass("forcusError");
            }
        }
    });
    return valid;
}
function ValidateFieldNumberDec(formID) {
    var valid = true;
    var pattern = /^[0-9]+(\.([0-9]{1,2})?)?$/;
    var obj = $("#" + formID + " .validateNumberDec");
    obj.each(function () {
        var parent = $(this).parents(" .form-group").first();
        var errText = parent.find(".error");
        if ($(this).val().trim() != "") {
            if (!pattern.test($(this).val())) {
                errText.html("Bạn chỉ được nhập số thập phân");
                errText.css("display", "inline");
                valid = false;
            }
            else {
                errText.css("display", "none");
            }

        }
    });
    return valid;
}


function HopLeNgayThang(strTxtdate) {
    var arrText = strTxtdate.split("/");
    var ngay = parseInt(arrText[0]);

    var thang = parseInt(arrText[1]);
    var nam = parseInt(arrText[2]);

    var isValid = true;
    var mess = "";
    var daysInMonth = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

    if ((!(nam % 4) && nam % 100) || !(nam % 400)) {
        daysInMonth[1] = 29;
    }

    if (ngay > 0 && thang > 0 && nam > 0) {
        if (!(thang >= 1 && thang <= 12)) {
            mess = "Tháng phải nằm trong khoảng từ 1 đến 12";
            return mess;

        }

        if (ngay > daysInMonth[thang - 1]) {
            mess = "Tháng " + thang + " chỉ có " + daysInMonth[thang - 1] + " ngày.";
        }
    } else {
        mess = "Ngày, tháng, năm phải lớn hơn 0";
    }
    return mess;
}

function ValidateFieldDate(formID) {
    var valid = true;
    var pattern = /^[0-3][0-9]\/[01][0-9]\/[12][0-9][0-9][0-9]$/;

    var obj = $("#" + formID + " .validateDate");

    obj.each(function () {
        var elem = $(this);
        var parent = $(this).parents(" .form-group");
        var errText = parent.find(".error");
        var textDate = $(this).val();
        if ($(this).val().trim() == "") {
            elem.addClass("forcusError");
            errText.html("Bạn phải nhập thông tin này");
            errText.css("display", "inline");
            valid = false;
        } else {
            if (!pattern.test($(this).val())) {
                elem.addClass("forcusError");
                errText.html("Vui lòng nhập đúng định dạng ngày dd/mm/yyyy");
                errText.css("display", "inline");
                valid = false;
            }
            else {
                var validRang = HopLeNgayThang(textDate);
                if (validRang) {
                    elem.addClass("forcusError");
                    errText.html(validRang);
                    errText.css("display", "inline");
                    valid = false;
                } else {
                    elem.removeClass("forcusError");
                    errText.css("display", "none");
                }
            }
        }
    })
    return valid;
}

function ConvertToDateISO(str) {
    var dateint = parseInt(str.match(/\d+/)[0]);
    return new Date(dateint).toISOString();
}



function PageSetup() {
    $(".pagination>li:first>a").attr("href", "javascript:GetDataPage(1)");
    $(".pagination>li>a").click(function () {
        $(".pagination>li").removeClass("active");
        $(this).parent().addClass("active");
    })
}

function ValidateFieldDateExist(formID) {
    var valid = true;
    var pattern = /^[0-3][0-9]\/[01][0-9]\/[12][0-9][0-9][0-9]$/;

    var obj = $("#" + formID + " .validateDateExist");

    obj.each(function () {
        var parent = $(this).parents(" .form-group").first();
        var errText = parent.find(".error");
        if ($(this).val().trim() != "") {
            if (!pattern.test($(this).val())) {
                errText.html("Vui lòng nhập đúng định dạng ngày dd/mm/yyyy");
                errText.css("display", "inline");
                valid = false;
            }
            else {
                errText.css("display", "none");

            }
        } else {
            errText.css("display", "none");
        }
    })
    return valid;
}


function ValidateFieldEmail(formID) {
    var valid = true;
    var pattern = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    var obj = $("#" + formID + " .validateEmail");
    obj.each(function () {
        var elem = $(this);
        var parent = $(this).parents(" .form-group").first();
        var errText = parent.find(".error");
        if ($(this).val().trim() == "") {
            errText.html("Bạn phải nhập thông tin này");
            errText.css("display", "inline");
            valid = false;
            elem.addClass("forcusError");
        } else {
            if (!pattern.test($(this).val())) {
                errText.html("Vui lòng nhập đúng định dạng email.");
                errText.css("display", "inline");
                valid = false;
                elem.addClass("forcusError");
            }
            else {
                errText.css("display", "none");
                elem.removeClass("forcusError");
            }
        }
    })
    return valid;
}

function AjaxCheckExist(url, para) {
    var result = false;
    $.ajax({
        url: url,
        data: para,
        type: "Post",
        async: false,
        success: function (rs) {
            if (rs == true) {
                result = true;
            }
        }
    });
    return result;
}

function ValidateFieldPhone(formID) {
    var valid = true;
    var pattern = /^0[1-9]{1}[0-9]{8,9}$/;

    var obj = $("#" + formID + " .validatePhone");

    obj.each(function () {
        var parent = $(this).parents(" .form-group").first();
        var errText = parent.find(".error");
        if ($(this).val().trim() == "") {
            errText.html("Bạn phải nhập thông tin này");
            errText.css("display", "inline");
            valid = false;
        } else {
            if (!pattern.test($(this).val())) {
                errText.html("Số điện thoại 0xxxxxxxxx. Độ dài 10 đến 11 chữ số");
                errText.css("display", "inline");
                valid = false;
            }
            else {
                errText.css("display", "none");
            }
        }
    })
    return valid;
}

function RegexPhone(formID, idfield) {
    var valid = true;
    var pattern = /^0[1-9]{1}[0-9]{8,9}$/;

    var obj = $("#" + formID + " #" + idfield);

    var parent = obj.parents(" .form-group").first();
    var errText = parent.find(".error");
    if (obj.val().trim() == "") {
        errText.html("Bạn phải nhập thông tin này");
        errText.css("display", "inline");
        valid = false;

    } else {
        if (!pattern.test(obj.val())) {
            errText.html("Vui lòng nhập đúng định dạng số điện thoại 0xxxxxxxxx. Độ dài 10 đến 11 chữ số");
            errText.css("display", "inline");
            valid = false;

        }
        else {
            errText.css("display", "none");
        }
    }

    return valid;
}


function ValidateFieldPassword(formID) {
    var valid = true;
    var pattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[0-9A-Za-z\d$@#$!%*?&]{8,100}/;

    var obj = $("#" + formID + " .validatePassword");

    obj.each(function () {
        var elem = $(this);
        var parent = $(this).parents(" .form-group").first();
        var errText = parent.find(".error");
        if ($(this).val().trim() == "") {
            elem.addClass("forcusError");
            errText.html("Bạn phải nhập thông tin này");
            errText.css("display", "inline");
            valid = false;
            $(this).addClass("state-error");


        } else {
            if (!pattern.test($(this).val())) {
                errText.html("Tối thiểu gồm 8 ký tự, bao gồm ký tự hoa, ký tự thường và ký tự đặc biệt");
                elem.addClass("forcusError");
                errText.css("display", "inline");
                valid = false;
            }
            else {
                errText.css("display", "none");
                elem.removeClass("forcusError");
            }
        }
    })
    return valid;
}

function ValidateFieldCMND(formID) {
    var valid = true;
    var pattern = /^[0-9]{9,12}$/;

    var obj = $("#" + formID + " .validateCMND");

    obj.each(function () {
        var parent = $(this).parents(" .form-group").first();
        var errText = parent.find(".error");
        if ($(this).val().trim() == "") {
            errText.html("Bạn phải nhập thông tin này");
            errText.css("display", "inline");
            valid = false;

        } else {
            if (!pattern.test($(this).val())) {
                errText.html("Bạn chỉ được nhập số độ dài từ 9 đến 12 chữ số");
                errText.css("display", "inline");
                valid = false;

            }
            else {
                errText.css("display", "none");

            }
        }
    })
    return valid;
}


function requiredField() {
    var check_err = false;
    $(".required").each(function () {
        if ($(this).val().trim() == "") {
            $(this).next().find(".error").html("Bạn phải nhập thông tin này");
            $(this).next().find(".error").css('display', 'inline');
            check_err = true;
        } else {
            $(this).next().find(".error").css('display', 'none');
        }
    });
    return check_err;
}

function IsDate(formID, fieldID) {
    var valid = true;
    var dateStr = $("#" + formID + " #" + fieldID).val();

    var pattern = /^[0-3][0-9]\/[01][0-9]\/2[0-9][0-9][0-9]$/;
    if ($("#" + formID + " #" + fieldID).val().trim() != "") {
        if (pattern.test(dateStr) == false) {

            valid = false;
        }
    } else {
        valid = false;

    }
    return valid;
}


function regexTenDangNhap(str) {

    var Regex = /^[0-9a-zA-Z\_]+$/;
    if (Regex.test(str)) {
        return true;
    } else {
        return false;
    }
}

function regexDate(formID, fieldID, errID) {
    var valid = false;
    var dateStr = $("#" + formID + " #" + fieldID).val();

    var pattern = /^[0-3][0-9]\/[01][0-9]\/2[0-9][0-9][0-9]$/;
    if ($("#" + formID + " #" + fieldID).val().trim() == "") {
        $("#" + formID + " #" + errID).html("Vui lòng nhập thông tin này");
        $("#" + formID + " #" + errID).css('display', 'inline');
        valid = true;
    } else {

        if (pattern.test(dateStr) == false) {
            $("#" + formID + " #" + errID).html("Sai đinh dạng ngày VD: dd/mm/yyyy");
            $("#" + formID + " #" + errID).css('display', 'inline');
            valid = true;
        } else {
            $("#" + formID + " #" + errID).css('display', 'none');

        }
    }
    return valid;
}

Date.prototype.addDays = function (days) {
    var dat = new Date(this.valueOf());
    dat.setDate(dat.getDate() + days);
    return dat;
}
function parseDate(str) {
    var mdy = str.split('/');
    return new Date(mdy[2], mdy[1] - 1, mdy[0]);
}

function parseDateFromMonth(str) {
    var mdy = str.split('/');
    return new Date(mdy[1], mdy[0] - 1, 1);
}
//chuyển date sang dd/MM/yyyy
function DateToText(obj) {
    var mon = '';
    if ((obj.getMonth() + 1) < 10) {
        mon = "0" + (obj.getMonth() + 1);
    } else {
        mon = (obj.getMonth() + 1);
    }
    var day = "";
    if (obj.getDate() < 10) {
        day = '0' + obj.getDate();
    } else {
        day = obj.getDate();
    }
    var date_string = day + "/" + mon + "/" + obj.getFullYear();
    return date_string;
}

//modify by duynn (20/6/2017)
function requiredFieldForFormId(form_id) {
    var check_err = true;
    $("#" + form_id + " .required").each(function () {
        var elem = $(this);
        var parent = $(this).parents(" .form-group").first();
        var errText = parent.find(".error");
        if ($(this).val() == null || $(this).val().length == 0 || $(this).val().toString().trim() == "") {
            elem.addClass("forcusError");
            errText.html("Bạn phải nhập thông tin này");
            errText.css('display', 'inline');
            check_err = false;
        }else {
            elem.removeClass("forcusError");
            errText.css('display', 'none');
        }
    });
    return check_err;
}

function regexEmail(form_id) {
    var pattern = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    var check_err = false;
    $("#" + form_id + " .email").each(function () {
        if ($(this).val().trim() != "") {
            if (pattern.test($(this).val().trim()) == false) {
                $(this).next().find(".error").html("Bạn phải nhập đúng định dạng email");
                $(this).next().find(".error").css('display', 'inline');
                check_err = true;
            } else {
                $(this).next().find(".error").css('display', 'none');
            }
        }
    });
    return check_err;
}

function isURL(str) {
    var urlRegex = '^(?!mailto:)(?:(?:http|https|ftp)://)(?:\\S+(?::\\S*)?@@)?(?:(?:(?:[1-9]\\d?|1\\d\\d|2[01]\\d|22[0-3])(?:\\.(?:1?\\d{1,2}|2[0-4]\\d|25[0-5])){2}(?:\\.(?:[0-9]\\d?|1\\d\\d|2[0-4]\\d|25[0-4]))|(?:(?:[a-z\\u00a1-\\uffff0-9]+-?)*[a-z\\u00a1-\\uffff0-9]+)(?:\\.(?:[a-z\\u00a1-\\uffff0-9]+-?)*[a-z\\u00a1-\\uffff0-9]+)*(?:\\.(?:[a-z\\u00a1-\\uffff]{2,})))|localhost)(?::\\d{2,5})?(?:(/|\\?|#)[^\\s]*)?$';
    var url = new RegExp(urlRegex, 'i');
    return str.length < 2083 && url.test(str);
}

function regexCMT(form_id) {
    var pattern = /^[0-9]{9,12}$/;
    var check_err = false;
    $("#" + form_id + " .regexCMT").each(function () {
        if ($(this).val().trim() != "") {
            if (pattern.test($(this).val().trim()) == false) {
                $(this).next().find(".error").html("Bạn chỉ được nhập số độ dài 9-12");
                $(this).next().find(".error").css('display', 'inline');
                check_err = true;
            } else {
                $(this).next().find(".error").css('display', 'none');
            }
        }
    });
    return check_err;
}

function CheckRangeDate(formID, DateStartID, DateEndID) {
    var valid = true;
    var dateStart = parseDate($("#" + formID + " #" + DateStartID).val());
    var dateEnd = parseDate($("#" + formID + " #" + DateEndID).val());
    if (dateEnd < dateStart) {
        valid = false;
    }
    return valid;
}



function regexNumber(form_id) {
    var pattern = /^[0-9]+$/;
    var check_err = false;
    $("#" + form_id + " .regexNumber").each(function () {
        if ($(this).val().trim() != "") {
            if (pattern.test($(this).val().trim()) == false) {
                $(this).next().find(".error").html("Bạn chỉ được nhập số");
                $(this).next().find(".error").css('display', 'inline');
                check_err = true;
            } else {
                if (parseInt($(this).val(), 10) < 0) {
                    $(this).next().find(".error").html("Thông tin này phải nhập số có giá trị lớn hơn 0");
                    $(this).next().find(".error").css('display', 'inline');
                    check_err = true;
                } else {
                    $(this).next().find(".error").css('display', 'none');
                }
            }
        }
    });
    return check_err;
}

function GetRangeDay(first, second) {
    return (second - first) / (1000 * 60 * 60 * 24);
}


function NotifErr(message) {
    notif({
        type: 'error',
        position: 'bottom',
        msg: message,
        timeout: 5000
    });
}

function StateAction(ObjectName, state, action) {
    if (state != "") {

        var message;

        if (state == "True") {
            switch (action) {
                case '1':
                    message = "Thêm mới " + ObjectName + " thành công";
                    break;
                case '2':
                    message = "Cập nhật " + ObjectName + " thành công";
                    break;
                case '3':
                    message = "Xóa " + ObjectName + " thành công";
                    break;
            }



            notif({
                type: 'success',
                position: 'bottom',
                msg: message,
                timeout: 5000
            });
        } else {

            switch (action) {
                case '1':
                    message = "Thêm mới " + ObjectName + " thất bại";
                    break;
                case '2':
                    message = "Cập nhật " + ObjectName + " thất bại";
                    break;
                case '3':
                    message = "Xóa " + ObjectName + " thất bại";
                    break;
            }
            notif({
                type: 'error',
                position: 'bottom',
                msg: message,
                timeout: 5000
            });
        }
    }
}


function commonNotifySuccess(mes) {
    notif({
        type: 'success',
        position: 'bottom',
        msg: mes,
        timeout: 5000
    });
}

function DeleteConfirm(url, para, objecName) {
    $.confirm({
        'title': 'Xác nhận xóa',
        'message': 'Bạn có chắc chắn muốn xóa ' + objecName + ' này?',
        'buttons': {
            'Đồng ý': {
                'class': 'btn-confirm-yes btn-info',
                'action': function () {
                    $.ajax({
                        url: url,
                        data: para,
                        type: 'Post',
                        dataType: 'json',
                        success: function (result) {
                            if (result.success) {
                                commonNotifySuccess('Đã xóa ' + objecName);
                                setTimeout(function () {
                                    refreshPageClient();
                                }, 500);
                            } else
                                commonNotifyError(result.message);
                        }, error: function (result) {
                            alert(result.responseText);
                        }
                    })
                }
            },
            'Hủy bỏ': {
                'class': 'btn-danger',
                'action': function () { }
            }
        }
    });


}

function refreshPage(url, para, updateID) {
    $.ajax({
        url: url,
        type: 'GET',
        data: para,
        dataType: 'html',
        success: function (result) {
            $("#" + updateID).html(result);
        }, error: function (result) {
            alert(result.responseText);
        }
    });

}
function GetPartial(url, para, updateID) {
    $.ajax({
        url: url,
        type: 'GET',
        data: para,
        dataType: 'html',
        success: function (result) {
            $("#" + updateID).html(result);
        }, error: function (result) {
            alert(result.responseText);
        }
    });
}

function GetPartialThen(url, para, updateID) {
    $.ajax({
        url: url,
        type: 'GET',
        async: false,
        data: para,
        dataType: 'html',
        success: function (result) {
            $("#" + updateID).html(result);
        }, error: function (result) {
            alert(result.responseText);
        }
    });
}
//Lấy url ảnh ckeditor
function updateValue(id, value) {
    var dialog = CKEDITOR.dialog.getCurrent();
    dialog.setValueOf('info', 'txtUrl', value);
}
var listColorChart = [
    '#3366CC',
    '#DC3912',
    '#FF9900',
    '#109618',
    '#990099',
    '#3B3EAC',
    '#0099C6',
    '#DD4477',
    '#66AA00',
    '#B82E2E',
    '#316395',
    '#994499',
    '#22AA99',
    '#AAAA11',
    '#6633CC',
    '#E67300',
    '#8B0707',
    '#329262',
    '#5574A6',
    '#3B3EAC'
];
function getColor(id) {
    if (id < listColorChart.length)
        return listColorChart[id];
    else
        return listColorChart[0];
}

//Page




var convertDate = function (dateObject) {
    var d = new Date(dateObject);
    var day = d.getDate();
    var month = d.getMonth() + 1;
    var year = d.getFullYear();
    if (day < 10) {
        day = "0" + day;
    }
    if (month < 10) {
        month = "0" + month;
    }
    var date = day + "/" + month + "/" + year;

    return date;
};

function DefineCssPageList() {
    $(".pagination>li:first>a").attr("href", "javascript:GetDataPage(1)");
    $(".pagination>li>a").click(function () {
        $(".pagination>li").removeClass("active");
        $(this).parent().addClass("active");
    })
}
function updateValue(id, value) {
    var dialog = CKEDITOR.dialog.getCurrent();
    dialog.setValueOf('info', 'txtUrl', value);
}

function RemoveUnicode(str) {
    str = str.toLowerCase();
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    return str;
}

//Nếu giá trị là null thì để trống
function getValueDisplay(obj) {
    if (obj == null) {
        return "";
    } else {
        return obj;

    }
}

function ToDate(obj) {
    if (obj == null) {
        return "";
    } else {

        if (obj.indexOf('Date') >= 0) {
            var dateint = parseInt(obj.match(/\d+/)[0]);
            obj = new Date(dateint);
        } else {
            obj = new Date(obj);
        }
        var mon = '';
        if ((obj.getMonth() + 1) < 10) {
            mon = "0" + (obj.getMonth() + 1);
        } else {
            mon = (obj.getMonth() + 1);
        }
        var day = "";
        if (obj.getDate() < 10) {
            day = '0' + obj.getDate();
        } else {
            day = obj.getDate();
        }
        var date_string = day + "/" + mon + "/" + obj.getFullYear();
        return date_string;

    }
}


function ToDateTime(obj) {
    if (obj == null) {
        return "";
    } else {

        if (obj.indexOf('Date') >= 0) {
            var dateint = parseInt(obj.match(/\d+/)[0]);
            obj = new Date(dateint);
        } else {
            obj = new Date(obj);
        }
        var mon = '';
        if ((obj.getMonth() + 1) < 10) {
            mon = "0" + (obj.getMonth() + 1);
        } else {
            mon = (obj.getMonth() + 1);
        }
        var day = "";
        if (obj.getDate() < 10) {
            day = '0' + obj.getDate();
        } else {
            day = obj.getDate();
        }

        var hour = obj.getHours();
        if (hour < 10) {
            hour = "0" + hour;
        }
        var minute = obj.getMinutes()
        if (minute < 10) {
            minute = "0" + minute;
        }
        var date_string = day + "/" + mon + "/" + obj.getFullYear() + " " + hour + ":" + minute;
        return date_string;

    }
}

function ShowMessageError(dom, message) {
    dom.html(message);
    dom.css("display", "inline");

}

//Hàm kiểm tra tồn tại chỉ 1 input trong mảng inputID có dữ liệu 
//Trả về true
//arrInputID : Mảng id input
function CheckExistOnlyOne(idform, arrInputID) {
    var isResult = false;
    //biến kết quả trả về
    var isExist = false;
    //biến xác định đã tồn tại input nào có dữ liệu chưa
    for (var i = 0; i < arrInputID.length; i++) {
        //Nếu như input có dữ liệu và chưa tồn tại input nào có dữ liệu
        if ($("#" + idform + " #" + arrInputID[i]).val().trim().length > 0) {

            if (isExist == false) {
                isExist = true;
                isResult = true;
            } else {
                //trường hợp này đã tồn tại input có dữ liệu và input kiểm tra cũng có dữ liệu
                //Là sai trả về luôn
                isResult = false;
                return isResult;
            }
        }

    }

    if (isExist == false) {
        return false;

    }
    return isResult;
}

//Hiển thị thông báo lỗi của input
function ShowMessageElementInput(idElement, message) {
    var element = $("#" + idElement).parents(".form-group").find(".error");

    ShowMessageError(element, message);

    $("#" + idElement).addClass("forcusError");
}

function HideMessageError(dom) {
    dom.html("");
    dom.css("display", "none");
}
//Ẩn thông báo lỗi của input
function HideMessageElementInput(idElement) {
    var element = $("#" + idElement).parents(".form-group").find(".error");
    HideMessageError(element);
    $("#" + idElement).removeClass("forcusError");

}
function failureAjax() {
    NotiError("Thao tác không thể thực hiện");
}

//function checkForm(idform) {
//    var err = 0;
//    err += requiredFieldForFormId(idform) ? 0 : 1;
//    err += ValidateFieldDate(idform) ? 0 : 1;
//    err += ValidateFieldPassword(idform) ? 0 : 1;
//    err += ValidateFieldEmail(idform) ? 0 : 1;
//    err += ValidateFieldPhone(idform) ? 0 : 1;
//    err += checkRequireTextArea(idform) ? 0 : 1;
//    err += RequireDropDownlist(idform) ? 0 : 1;
//    err += ValidateFieldCMND(idform) ? 0 : 1;
//    err += ValidateFieldNumber(idform) ? 0 : 1;
//    err += ValidateFieldNumberDec(idform) ? 0 : 1;
//    //err += htmlFieldForFormId(idform) ? 0 : 1;
//    //err += SpecialLetterCheck(idform) ? 0 : 1;
//    //err += EnglishLetterCheck(idform) ? 0 : 1;
//    if (err) {
//        return false;
//    } else {
//        return true;
//    }
//}

function AjaxCall(url, type, data, callback, callbackError) {
    var isfunction = callback && typeof (callback) == "function";
    if (!isfunction) {
        callback = function () {
            console.log("Chưa cài đặt sự kiện thành công");
            HideLoading();
        }
    }
    var isfunction = callbackError && typeof (callbackError) == "function";
    if (!isfunction) {
        callbackError = function () {
            NotiError("Thao tác không thể thực hiện");
            HideLoading();
        }
    }
    $.ajax({
        url: url,
        type: type,
        data: data,
        success: callback,
        error: callbackError
    })

}

//error form element

function ErrorMessage(selector, message) {
    var err = $(selector).parents(".form-group").find(".error");
    ShowMessageError(err, message);
}


$(".txt-picker").datepicker({
    dateFormat: 'dd/mm/yy',
    changeMonth: true,
    changeYear: true, yearRange: "-50:+20",
    showWeek: false, weekHeader: "Tuần",
    language: 'vi',
    prevText: '<i class="fa fa-chevron-left"></i>',
    nextText: '<i class="fa fa-chevron-right"></i>',
    onSelect: function (date) {

    }
});

function WaitingLoad_Start() {
    waitingDialog.show('Đang xử lý...', {

        // if the option is set to boolean false, it will hide the header and "message" will be set in a paragraph above the progress bar.
        // When headerText is a not-empty string, "message" becomes a content above the progress bar and headerText string will be set as a text inside the H3;
        headerText: '',

        // this will generate a heading corresponding to the size number
        headerSize: 3,

        // extra class(es) for the header tag
        headerClass: '',

        // bootstrap postfix for dialog size, e.g. "sm", "m"
        dialogSize: 'sm',

        // bootstrap postfix for progress bar type, e.g. "success", "warning";
        progressType: 'success',

        // determines the tag of the content element
        contentElement: 'p',

        // extra class(es) for the content tag
        contentClass: 'content'

    });

}
function WaitingLoad_End() {
    waitingDialog.hide()
}

function FixPostFileAjax(idForm, fucntionSuccess) {
    $("form#" + idForm).submit(function () {
        var selectorForm = "#" + idForm;
        var dataString;
        event.preventDefault();
        var contentType1 = false;
        var action = $(selectorForm).attr("action");
        if ($(selectorForm).attr("enctype") == "multipart/form-data") {
            //this only works in some browsers.
            //purpose? to submit files over ajax. because screw iframes.
            //also, we need to call .get(0) on the jQuery element to turn it into a regular DOM element so that FormData can use it.
            dataString = new FormData($(selectorForm).get(0));
            contentType1 = false;
            processData = false;
        } else {
            // regular form, do your own thing if you need it
        }
        $.ajax({
            type: "POST",
            url: action,
            data: dataString,
            dataType: "json", //change to your own, else read my note above on enabling the JsonValueProviderFactory in MVC
            contentType: false,
            processData: false,
            success: fucntionSuccess,
            error: failureAjax,
        });
        return false;

    });
}
function htmlFieldForFormId(form_id) {
    var check_err = true;
    $("#" + form_id + " .html-element").each(function () {
        var elem = $(this);
        var parent = $(this).parents(" .form-group").first();
        var errText = parent.find(".error");
        if (!$(errText).is(":visible")) {
            if (/<[a-z][\s\S]*>/i.test($(this).val())) {
                elem.addClass("forcusError");
                errText.html("Bạn không được nhập ký tự dạng html");
                errText.css('display', 'inline');
                check_err = false;
            } else {
                elem.removeClass("forcusError");
                errText.css('display', 'none');
            }
        }

    });
    return check_err;
}
function checkHtmlInput(idform) {
    var err = 0;
    err += htmlFieldForFormId(idform) ? 0 : 1;
    if (err) {
        return false;
    } else {
        return true;
    }
}
function checkSpecialChar(value) {
    return /[~!@#$%^&\*\(\)]*/i.test(value);
}
function SpecialLetterCheck(formId) {
    var check_err = true;
    $("#" + formId + " .special-letter").each(function () {
        var elem = $(this);
        var parent = $(this).parents(" .form-group").first();
        var errText = parent.find(".error");
        if (!$(errText).is(":visible")) {
            if (/[~`!#$%\^&*+=\-\[\]\\';,/{}|\\":<>\?]/g.test($(this).val())) {
                elem.addClass("forcusError");
                errText.html("Bạn không được nhập các ký tự đặc biệt");
                errText.css('display', 'inline');
                check_err = false;
            } else {
                elem.removeClass("forcusError");
                errText.css('display', 'none');
            }
        }
    });
    return check_err;
}
function EnglishLetterCheck(formId) {
    var check_err = true;
    $("#" + formId + " .english-letter").each(function () {
        var elem = $(this);
        var parent = $(this).parents(" .form-group").first();
        var errText = parent.find(".error");
        if (!$(errText).is(":visible")) {
            var letters = /^[0-9a-zA-Z ]+$/;
            if (!$(this).val().match(letters)) {
                elem.addClass("forcusError");
                errText.html("Bạn chỉ được nhập chữ, số");
                errText.css('display', 'inline');
                check_err = false;
            } else {
                elem.removeClass("forcusError");
                errText.css('display', 'none');
            }
        }
    });
    return check_err;
}
function PasswordRegex(value) {
    return /^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$/.test(value);
}

Number.prototype.formatNumberToMoney = function (n, x, s, c) {
    var re = '\\d(?=(\\d{' + (x || 3) + '})+' + (n > 0 ? '\\D' : '$') + ')',
        num = this.toFixed(Math.max(0, ~~n));

    return (c ? num.replace('.', c) : num).replace(new RegExp(re, 'g'), '$&' + (s || ','));
};
String.prototype.formatMoneyToNumber = function () {
    return +this.replace(/,/g, "");
}
String.prototype.formatLongText = function (LENGTH = 70) {
    return this.length > LENGTH ? this.substring(0, LENGTH) + "..." : this;
}
String.prototype.formatDateStringToNumber = function () {
    const strDateArr = this.split("/");
    let newStrDate = strDateArr[1] + "/" + strDateArr[0] + "/" + strDateArr[2];

    return +(new Date(newStrDate));
}