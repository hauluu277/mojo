using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionAnswerFeatures.UI
{
    public static class QuestionAnswerConstant
    {
        #region thứ tự sắp xếp
        public const string MOI_NHAT = "1";
        public const string CU_NHAT = "2";
        public const string XEM_NHIEU_NHAT = "3";
        public const string TRALOI_NHIEUNHAT = "4";
        public const string CAU_TRALOI_MOINHAT = "5";
        public const string CAUHOI_CHUATRALOI = "6";
        #endregion

        #region trạng thái hiển thị câu hỏi và câu trả lời ở màn hình danh sách câu hỏi và trả lời của người dùng
        public const int Question = 0;
        public const int Answer = 1;

        #endregion

        #region giao diện
        public const string HienThiDangList = "HienThiDangList";
        public const string HienThiDangRutGon = "HienThiDangRutGon";
        #endregion
    }
}