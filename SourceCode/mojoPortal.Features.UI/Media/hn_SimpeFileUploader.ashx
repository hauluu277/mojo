using System;
using System.Web;
using System.IO;
using System.Drawing;
using MediaAlbumFeature.Business;
namespace MediaAlbumFeature.UI
{
    public class hn_SimpeFileUploader : mojoPortal.Web.UI.BaseContentUploadHandler, IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            mojoPortal.Business.SiteUser siteUser = mojoPortal.Web.SiteUtils.GetCurrentSiteUser();
            mojoPortal.Business.SiteSettings siteSettings = mojoPortal.Business.WebHelpers.CacheHelper.GetCurrentSiteSettings();
            context.Response.ContentType = "text/plain";
            int cateId = -1;
            if (context.Request.QueryString["categoryIMG"] != null)
            {
                try
                {
                    int.Parse(context.Request.QueryString["categoryIMG"]);
                }
                catch
                {

                    cateId = 1;
                }

            }

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
            string[] files;
            int numFiles;
            files = System.IO.Directory.GetFiles(dirFullPath);
            numFiles = files.Length;
            numFiles = numFiles + 1;

            MediaAlbum media = new MediaAlbum();
            string str_image = "";
            string fileNameIMG = string.Empty;
            string fileNameIMG_thumb = string.Empty;
            foreach (string s in context.Request.Files)
            {
                HttpPostedFile file = context.Request.Files[s];
                string fileName = file.FileName;
                string fileExtension = file.ContentType;
                string guid = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                fileNameIMG = dirFullPath + guid + fileExtension;
                fileNameIMG_thumb = dirFullPath_thumb + guid + fileNameIMG_thumb;
                if (!string.IsNullOrEmpty(fileName))
                {
                    #region lưu thông tin ảnh
                    media.TypeData = MediaAlbumFeature.UI.MediaConstant.IMAGE;
                    media.SiteID = siteSettings.SiteId;
                    media.IsPublish = true;
                    media.AlbumOrder = 100;
                    media.FileName = guid + fileExtension;
                    media.FilePath = fileNameIMG;
                    media.GroupMediaID = cateId;
                    media.Save();
                    #endregion


                    fileExtension = Path.GetExtension(fileName);
                    str_image = guid + fileExtension;
                    string pathToSave = HttpContext.Current.Server.MapPath("~/" + System.Configuration.ConfigurationManager.AppSettings["MediaIMG"]);
                    System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(file.InputStream);
                    //Save ảnh thường
                    using (Bitmap bitmap = new Bitmap(bmpPostedImage, MediaAlbumFeature.UI.MediaConstant.ImageMaxWidth_fm, MediaAlbumFeature.UI.MediaConstant.ImageMaxHeight_fm))
                    {
                        bitmap.Save(fileNameIMG, bmpPostedImage.RawFormat);
                    }

                    // Save anh thum_b
                    using (Bitmap bitmap = new Bitmap(bmpPostedImage, MediaAlbumFeature.UI.MediaConstant.ImageMaxThumbnailWidth_fm, MediaAlbumFeature.UI.MediaConstant.ImageMaxThumbnailHeight_fm))
                    {
                        bitmap.Save(fileNameIMG_thumb, bmpPostedImage.RawFormat);
                    }

                    //ResizeMyImage method call
                    System.Drawing.Image objImage = ResizeMyImage(bmpPostedImage, 480);
                    objImage.Save(pathToSave, System.Drawing.Imaging.ImageFormat.Jpeg);
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