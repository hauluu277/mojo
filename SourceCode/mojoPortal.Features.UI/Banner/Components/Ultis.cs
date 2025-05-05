using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BannerFeature.UI
{
    public class Constant
    {
        public static int DefaultID = 0;
        public static string Default = "--Chọn định dạng--";
        public static int ImageID = 1;
        public static string Image = "Đăng ảnh";
        public static int FlashID = 2;
        public static string Flash = "Đăng flash";
    }

    public class BannerConstant
    {
        //Không chọn slide
        public static int NoSlide = 0;
        //Hiển thị rộng 100% (Animated Touch)
        public static int FullWidth_AnimatedTouch = 1;
        //Hiển thị kiểu owl đối tác
        public static int OWL_DoiTac = 2;
        //Hiển thị kiểu owl khoa phòng
        public static int OWL_KhoaPhong = 3;
        //Hiển thị kiểu Jssor FullWidth
        public static int FullWidth_Jssor = 4;
    }
}