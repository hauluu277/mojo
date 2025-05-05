/// <reference path="../../../ckfinder/ckfinder.js" />
CKEDITOR.plugins.add('selectmultiple', {
    lang: 'vi',
    icons: 'selectmultiple',
    init: function (editor) {
        editor.addCommand('selectmultiple', {
            exec: function (editor) {
                var finder = new CKFinder();
                finder.inPopup = true;
                finder.defaultLanguage = 'vi';
                finder.language = 'vi';
                finder.popupFeatures = "width=900,height=900,menubar=yes,toolbar=no,modal=yes";
                finder.selectMultiple = true;
                //finder.startupPath = "Images:/";
                //finder.BaseUrl = "/Images/";
                finder.resourceType = 'Images';
                finder.selectActionFunction = function (fileUrl, data, allFiles) {
                    var urlFiles = "";
                    $(allFiles).each(function (index, element) {
                        urlFiles += '<p><img src="' + element.url + '"/></p>';
                    });
                    editor.insertHtml(urlFiles);
                };
                finder.popup();
                // window.open(editor.config.filebrowserImageBrowseLinkUrl,
                // 'ckfinder_multiple', 'status=0, toolbar=0, location=0, menubar=0, ' +
                // 'directories=0, resizable=1, scrollbars=0, width=800, height=600'
                // );
            }

        });
        editor.ui.addButton('Selectmultiple', {
            label: 'Chọn nhiều ảnh',
            command: 'selectmultiple',
            toolbar: 'insert'
        });
    }
});