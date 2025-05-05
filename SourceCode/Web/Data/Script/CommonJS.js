var commonjs = {
    //Load js file
    loadJSFile: function (url, callback) {
        var script = document.createElement("script")
        script.type = "text/javascript";

        if (script.readyState) {  //IE
            script.onreadystatechange = function () {
                if (script.readyState == "loaded" ||
                    script.readyState == "complete") {
                    script.onreadystatechange = null;
                    if (callback != null) {
                        callback();
                    }
                }
            };
        } else {  //Others
            script.onload = function () {
                if (callback != null) {
                    callback();
                }
            };
        }
        script.src = url;
        document.getElementsByTagName("head")[0].appendChild(script);
    },
    //Load css file
    loadCssFile: function (filename) {
        var fileref = document.createElement("link")
        fileref.setAttribute("rel", "stylesheet")
        fileref.setAttribute("type", "text/css")
        fileref.setAttribute("href", filename)
        if (typeof fileref != "undefined")
            document.getElementsByTagName("head")[0].appendChild(fileref)
    },
    //Reload session path url Ckfinder
    callLoadCkfinder: function () {
        setInterval(function () {
            $.ajax({
                type: "POST",
                cache: false,
                url: "/Article/PostArticle.aspx/ReloadAuthentionCkfinder",
                dataType: "json",
                contentType: "application/json; charset= utf-8",
                async: true,
                success: function (response) {
                }, error: function (ex) {
                    console.log(ex);
                }
            });

        }, 600000);
    },
    //Show Ckfinder Get Image url
    getUrlImage: function (id) {
        var finder = new CKFinder();
        finder.inPopup = true;
        finder.defaultLanguage = 'vi';
        finder.language = 'vi';
        finder.popupFeatures = "width=900,height=900,menubar=yes,toolbar=no,modal=yes";
        finder.selectMultiple = true;
        finder.startupPath = "Images:/";
        finder.BaseUrl = "/Images/";
        finder.resourceType = 'Images';
        finder.selectActionFunction = function (fileUrl, data, allFiles) {
            $("#" + id).val(fileUrl);
            $("#" + id).text(fileUrl);
            $("#" + id).change();
        };
        finder.popup();
    },

    //Get Url Parameter
    getUrlParameter: function (parameter, defaultValue) {
        var results = new RegExp('[\?&]' + parameter + '=([^&#]*)').exec(window.location.href);
        if (results == null) {
            return defaultValue;
        }
        else {
            return decodeURIComponent(results[1]);
        }
    }
}




















//function createjscssfile(filename, filetype) {
//    if (filetype == "js") { //if filename is a external JavaScript file
//        var fileref = document.createElement('script')
//        fileref.setAttribute("type", "text/javascript")
//        fileref.setAttribute("src", filename)
//    }
//    else if (filetype == "css") { //if filename is an external CSS file
//        var fileref = document.createElement("link")
//        fileref.setAttribute("rel", "stylesheet")
//        fileref.setAttribute("type", "text/css")
//        fileref.setAttribute("href", filename)
//    }
//    return fileref
//}

//function replacejscssfile(oldfilename, newfilename, filetype) {
//    var targetelement = (filetype == "js") ? "script" : (filetype == "css") ? "link" : "none" //determine element type to create nodelist using
//    var targetattr = (filetype == "js") ? "src" : (filetype == "css") ? "href" : "none" //determine corresponding attribute to test for
//    var allsuspects = document.getElementsByTagName(targetelement)
//    for (var i = allsuspects.length; i >= 0; i--) { //search backwards within nodelist for matching elements to remove
//        if (allsuspects[i] && allsuspects[i].getAttribute(targetattr) != null && allsuspects[i].getAttribute(targetattr).indexOf(oldfilename) != -1) {
//            var newelement = createjscssfile(newfilename, filetype)
//            allsuspects[i].parentNode.replaceChild(newelement, allsuspects[i])
//        }
//    }
//}

//replacejscssfile("oldscript.js", "newscript.js", "js") //Replace all occurences of "oldscript.js" with "newscript.js"
//replacejscssfile("oldstyle.css", "newstyle", "css") //Replace all occurences "oldstyle.css" with "newstyle.css"