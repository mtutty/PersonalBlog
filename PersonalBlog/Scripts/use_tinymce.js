function use_tinymce() {
    $('textarea').tinymce({
        // Location of TinyMCE script
        script_url: '/Scripts/tinymce/tiny_mce.js',

        // General options
        theme: "advanced",
        plugins: "autolink,lists,style,table,save,advhr,advimage,advlink,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,contextmenu,paste,fullscreen,nonbreaking,advlist,netFileBrowser",

        // Theme options
        theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,styleselect,formatselect,fontselect,fontsizeselect",
        theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,netFileBrowser,cleanup,help,code,|,forecolor,backcolor",
        theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media",
        theme_advanced_buttons4: "styleprops,|,del,ins,attribs,fullscreen",
        theme_advanced_toolbar_location: "top",
        theme_advanced_toolbar_align: "left",
        theme_advanced_statusbar_location: "bottom",
        theme_advanced_resizing: true,

        // Example content CSS (should be your site CSS)
        content_css: "/Content/blog.css",

        // Drop lists for link/image/media/template dialogs
        template_external_list_url: "lists/template_list.js",
        external_link_list_url: "lists/link_list.js",
        external_image_list_url: "lists/image_list.js",
        media_external_list_url: "lists/media_list.js",

    });
}