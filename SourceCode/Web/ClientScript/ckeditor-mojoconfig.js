CKEDITOR.editorConfig = function (config) {
    config.entities = true;
    config.scayt_autoStartup = true;
    config.disableNativeSpellChecker = true;
    config.justifyClasses = ['text-left', 'text-center', 'text-right', 'text-justify'];
    config.indentClasses = ['text-indent-1', 'text-indent-2', 'text-indent-3'];
    //config.setLang = ('audio', 'en');
    config.extraPlugins = 'widgetselection,button,toolbar,notification,clipboard,lineutils,widget,html5audio,oembed,selectmultiple';
    config.oembed_maxWidth = '560';
    config.oembed_maxHeight = '315';
    config.allowedContent = true;
	config.htmlEncodeOutput = false;
	config.entities = false;
    //config.fontSize_sizes = 'X-Small/font-xsmall;Small/font-small;Normal/font-normal;Large/font-large;X-Large/font-xlarge';
    //config.fontSize_style = {
    //    element: 'span',
    //    attributes: {
    //        'class': '#(size)'
    //    },
    //    overrides: [{
    //        element: 'font',
    //        attributes: {
    //            'size': null
    //        }
    //    }]
    //};

    config.format_tags = 'p;h1;h2;h3;h4';
    config.format_h1 = {
        element: 'h3'
    };
    config.format_h2 = {
        element: 'h4'
    };
    config.format_h3 = {
        element: 'h5'
    };
    config.format_h4 = {
        element: 'h6'
    };

    config.toolbar_CKFull =
        [
            ['Source', '-', 'Save', 'NewPage', 'Preview', '-', 'Templates'],
            ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print'],
            ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
            ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'],
            '/', ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
            ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote'],
            ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
            ['Link', 'Unlink', 'Anchor'],
            ['Image', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak'],
            '/', ['Styles', 'Format', 'Font', 'FontSize'],
            ['TextColor', 'BGColor'],
            ['Maximize', 'ShowBlocks', '-', 'About']
        ];


    config.toolbar_Full =
        [
            ['Source', 'Maximize'],
            ['SelectAll', 'RemoveFormat', 'Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print'],
            ['Undo', 'Redo', '-', 'Find', 'Replace', 'Bold', 'Italic', 'Underline', '-', 'Strike', 'Superscript'],
            '/', ['Blockquote', 'Format', 'Styles', 'FontSize'],
            ['NumberedList', 'BulletedList'],
            ['Link', 'Unlink', 'Anchor'],
            ['Image', 'oembed', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar']

        ];

    config.toolbar_Newsletter =
        [
            ['Source'],
            ['SelectAll', 'RemoveFormat', 'Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print'],
            ['Undo', 'Redo', '-', 'Find', 'Replace'],
            '/', ['Blockquote', 'Bold', 'Italic', 'Underline', 'Strike', 'Superscript'],
            ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent'],
            ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
            ['Link', 'Unlink', 'Anchor'],
            ['Image', 'Table', 'HorizontalRule', 'SpecialChar'],
            '/', ['Format', 'Font', 'FontSize'],
            ['TextColor', 'BGColor'],
            ['Maximize', 'Preview']

        ];

    config.toolbar_FullWithTemplates =
        [
            ['Source', '-', 'Save', 'NewPage', 'Preview', '-', 'Templates'],
            ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print'],
            ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'CopyFormatting', 'RemoveFormat'],
            ['Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField'],
            '/', ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
            ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote'],
            ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', 'BidiLtr', 'BidiRtl'],
            ['Link', 'Unlink', 'Anchor'],
            ['Selectmultiple', 'Image', 'Html5audio', 'oembed', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'],
            '/', ['Styles', 'Format', 'Font', 'FontSize'],
            ['TextColor', 'BGColor'],
            ['Maximize', 'ShowBlocks', '-', 'About']
            //['Source', 'Maximize', 'ShowBlocks'],
            //['SelectAll', 'RemoveFormat', 'Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Print'],
            //['Undo', 'Redo'],
            //['Find', 'Replace'],
            //['Bold', 'Italic', 'Underline', 'Strike', 'Superscript'],
            //'/', ['Blockquote', 'Format', 'Styles', 'FontSize'],
            //['NumberedList', 'BulletedList', 'Outdent', 'Indent'],
            //['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
            //['Link', 'Unlink', 'Anchor'],
            //['Templates', 'Image', 'oembed', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar']

        ];

    config.toolbar_Forum =
        [
            ['Cut', 'Copy', 'PasteText', '-'],
            ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
            ['Blockquote', 'Bold', 'Italic', 'Underline'],
            ['NumberedList', 'BulletedList'],
            ['Link', 'Unlink'],
            ['SpecialChar', 'Smiley']
        ];

    config.toolbar_ForumWithImages =
        [
            ['Cut', 'Copy', 'PasteText', '-'],
            ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
            ['Blockquote', 'Bold', 'Italic', 'Underline', 'Image'],
            ['NumberedList', 'BulletedList'],
            ['Link', 'Unlink'],
            ['SpecialChar', 'Smiley']
        ];

    config.toolbar_SimpleWithSource =
        [
            ['Source', 'Cut', 'Copy', 'PasteText'],
            ['Blockquote', 'Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink', 'Smiley']
        ];

    config.toolbar_AnonymousUser =
        [
            ['Cut', 'Copy', 'PasteText'],
            ['Blockquote', 'Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink', 'Smiley']
        ];

    config.toolbar_CustomOne =
        [
            ['Cut', 'Copy', 'PasteText'],
            ['Blockquote', 'Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink', 'Smiley']
        ];

    config.toolbar_Custom2 =
        [
            ['Cut', 'Copy', 'PasteText'],
            ['Blockquote', 'Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink', 'Smiley']
        ];
};
