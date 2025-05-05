using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Web.Areas.GiaoDienArea.Models
{
    public class DaTaFileVM
    {
        public string Data { get; set; }
        public string nameFile { get; set; }
        public string duongdan { get; set; }
        public bool isImg { get; set; } = false;
        public string pathFileData { get; set; }

    }
}