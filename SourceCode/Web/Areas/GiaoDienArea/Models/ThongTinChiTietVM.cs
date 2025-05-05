using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Areas.GiaoDienArea.Models
{
    public class ThongTinChiTietVM
    {
        public List<FileOrFoderTree> TreeFolderThem { get; set; }
    }

    public class FileOrFoderTree
    {

        //tên file or foder
        public string name { get; set; }
        // loại file hay folder: trống là file ; Tree.FOLDER là foder
        public string type { get; set; }
        public bool open { get; set; }
        public bool selected { get; set; }
        //đuôi file gồm .exe, css, skin, config, html, js,...
        public string DuoiFile { get; set; }
        //Con
        public List<FileOrFoderTree> children { get; set; }
        public string DuongdanCha { get; set; }
        public string DuongdanFolder { get; set; }

        public string text { get; set; }
    }
}