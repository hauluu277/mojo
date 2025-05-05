using MediaAlbumFeature.Business;
using mojoPortal.Business;
using mojoPortal.Business.WebHelpers;
using mojoPortal.Web;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace mojoPortal.Features.UI.Media
{
    /// <summary>
    /// Summary description for MutilpleUploadImage
    /// </summary>
    public class MutilpleUploadImage : IHttpHandler
    {
        readonly SiteUser siteUser = SiteUtils.GetCurrentSiteUser();
        readonly SiteSettings siteSettings = CacheHelper.GetCurrentSiteSettings();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int cateId = -1;
            int mid = -1;
            int siteId = -1;
            if (context.Request["categoryIMG"] != null)
            {
                try
                {
                    cateId = int.Parse(context.Request["categoryIMG"].ToString());
                }
                catch
                {

                    cateId = -1;
                }

            }
            if (context.Request["mid"] != null)
            {
                try
                {
                    mid = int.Parse(context.Request["mid"].ToString());
                }
                catch
                {

                    mid = -1;
                }

            }
            if (context.Request["siteId"] != null)
            {
                try
                {
                    siteId = int.Parse(context.Request["siteId"].ToString());
                }
                catch
                {

                    siteId = -1;
                }

            }
            //string categoryName = string.Empty;
            //var category = new CoreCategory(cateId);
            //if (category != null)
            //{
            //    categoryName = category.Name;
            //}
            string dirFullPath = HttpContext.Current.Server.MapPath("~/" + System.Configuration.ConfigurationManager.AppSettings["MediaIMG"]);
            if (!Directory.Exists(dirFullPath))
            {
                Directory.CreateDirectory(dirFullPath);
            }
            string dirFullPath_thumb = HttpContext.Current.Server.MapPath("~/" + System.Configuration.ConfigurationManager.AppSettings["MediaIMG_thumb"]);
            if (!Directory.Exists(dirFullPath_thumb))
            {
                Directory.CreateDirectory(dirFullPath_thumb);
            }
            string dirFullPath_thumbSlide = HttpContext.Current.Server.MapPath("~/" + System.Configuration.ConfigurationManager.AppSettings["MediaIMG_thumbSlide"]);
            if (!Directory.Exists(dirFullPath_thumbSlide))
            {
                Directory.CreateDirectory(dirFullPath_thumbSlide);
            }
            //string[] files;
            //int numFiles;
            //files = System.IO.Directory.GetFiles(dirFullPath);
            //numFiles = files.Length;
            //numFiles = numFiles + 1;

            MediaAlbum media = new MediaAlbum();
            string str_image = "";
            string fileNameIMG = string.Empty;
            string fileNameIMG_thumb = string.Empty;
            string fileNameIMGSlide_thumb = string.Empty;
            foreach (string s in context.Request.Files)
            {
                HttpPostedFile file = context.Request.Files[s];
                string fileName = file.FileName;
                string fileExtension = file.ContentType;
                string guid = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                string fileExtension_fm = Path.GetExtension(file.FileName);
                fileNameIMG = dirFullPath + guid + fileExtension_fm;
                fileNameIMG_thumb = dirFullPath_thumb + guid + fileExtension_fm;
                fileNameIMGSlide_thumb = dirFullPath_thumbSlide + guid + fileExtension_fm;
                if (!string.IsNullOrEmpty(fileName))
                {
                    #region lưu thông tin ảnh
                    media = new MediaAlbum();
                    media.TypeData = MediaAlbumFeature.UI.MediaConstant.IMAGE;
                    media.SiteID = siteId;
                    media.IsPublish = true;
                    media.AlbumOrder = 100;
                    media.ModuleID = mid;
                    media.SizeInKB = file.ContentLength;
                    media.FileName = guid + fileExtension_fm;
                    media.FilePath = fileNameIMG;
                    media.GroupMediaID = cateId;
                    //media.Description = categoryName;
                    media.Save();
                    #endregion


                    fileExtension = Path.GetExtension(fileName);
                    str_image = guid + fileExtension;
                    string pathToSave = HttpContext.Current.Server.MapPath("~/" + System.Configuration.ConfigurationManager.AppSettings["MediaIMG"]);
                    System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(file.InputStream);

                    System.Drawing.Image image = System.Drawing.Image.FromStream(file.InputStream);
                    //Save ảnh thường
                    //using (Bitmap bitmap = new Bitmap(bmpPostedImage, MediaAlbumFeature.UI.MediaConstant.ImageMaxWidth_fm, MediaAlbumFeature.UI.MediaConstant.ImageMaxHeight_fm))
                    //{
                    //    bitmap.Save(fileNameIMG, image.RawFormat);
                    //}
                    using (Bitmap bitmap = new Bitmap(bmpPostedImage, bmpPostedImage.Width, bmpPostedImage.Height))
                    {
                        bitmap.Save(fileNameIMG, image.RawFormat);
                    }

                    // Save anh thum_b
                    using (Bitmap bitmap = new Bitmap(bmpPostedImage, MediaAlbumFeature.UI.MediaConstant.ImageMaxThumbnailWidth_fm, MediaAlbumFeature.UI.MediaConstant.ImageMaxThumbnailHeight_fm))
                    {
                        bitmap.Save(fileNameIMG_thumb, image.RawFormat);
                    }

                    // Save anh thum_b Slide
                    using (Bitmap bitmap = new Bitmap(bmpPostedImage, MediaAlbumFeature.UI.MediaConstant.ImageMaxThumbnailSlideWidth, MediaAlbumFeature.UI.MediaConstant.ImageMaxThumbnailSlideHeight))
                    {
                        bitmap.Save(fileNameIMGSlide_thumb, image.RawFormat);
                    }

                    //ResizeMyImage method call
                    //System.Drawing.Image objImage = ResizeMyImage(bmpPostedImage, 480);
                    //objImage.Save(pathToSave, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            context.Response.Write(str_image);
        }
        public static System.Drawing.Image ResizeMyImage(System.Drawing.Image image, int maxHeight)
        {
            var ratio = (double)maxHeight / image.Height;
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}