using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArticleFeature.Business
{
    public class ArticleAttachmentBO
    {
        public int FileID { get; set; }
        public int ModuleID { get; set; }
        public string FileName { get; set; }
        public string ServerFileName { get; set; }
        public int SizeInKB { get; set; }
        public int DownloadCount { get; set; }
        public DateTime LastModified { get; set; }
        public int ID { get; set; }
        public int ItemID { get; set; }

    }
}
