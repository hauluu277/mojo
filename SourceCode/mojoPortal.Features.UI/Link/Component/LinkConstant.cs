using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mojoPortal.Features
{
    public static class LinkConstant
    {
        /// <summary>
        /// Sắp xếp theo bảng chữ cái
        /// </summary>
        public const int OrderByAlphabet = 1;
        /// <summary>
        /// Sắp xếp theo thứ tự tăng dần
        /// </summary>
        public const int OrderByAscending = 2;
        /// <summary>
        /// Sắp xếp theo thứ tự giảm dần
        /// </summary>
        public const int OrderByDescending = 3;

        /// <summary>
        /// Hiển thị 1 link liên kết
        /// </summary>
        public const int DisplayOneLink = 1;
        /// <summary>
        /// Hiển thị 3 link liên kết
        /// </summary>
        public const int DisplayThreeLink = 2;
        /// <summary>
        /// Hiển thị 4 link liên kết
        /// </summary>
        public const int DisplayFourLink = 3;
    }
}