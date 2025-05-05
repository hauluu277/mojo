using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Web;

namespace mojoPortal.Features.UI.Article
{
    public class ShowImage : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                var a = context.Request.Form["file"];
                string folder = System.Configuration.ConfigurationManager.AppSettings["ArticleImagesFolder"];
                String pathToApplicationsFolder
                = HttpContext.Current.Server.MapPath(
                "~/" + ConfigurationManager.AppSettings["ArticleImagesFolder"]);
                if (!Directory.Exists(pathToApplicationsFolder))
                {
                    Directory.CreateDirectory(pathToApplicationsFolder);
                }

                string path = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["ArticleImagesFolder"]);
                //string path540 = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["ArticleImagesFolder540"]);
                //string path503 = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["ArticleImagesFolder503"]);
                //string path297 = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["ArticleImagesFolder297"]);
                //string path359 = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["ArticleImagesFolder359"]);
                //string path158 = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["ArticleImagesFolder158"]);
                //if (!Directory.Exists(path540))
                //{
                //    Directory.CreateDirectory(path540);
                //}
                //if (!Directory.Exists(path503))
                //{
                //    Directory.CreateDirectory(path503);
                //}
                //if (!Directory.Exists(path297))
                //{
                //    Directory.CreateDirectory(path297);
                //}
                //if (!Directory.Exists(path359))
                //{
                //    Directory.CreateDirectory(path359);
                //}
                //if (!Directory.Exists(path158))
                //{
                //    Directory.CreateDirectory(path158);
                //}

                string dirFullPath = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["ArticleImagesFolder"]);
                string[] files;
                int numFiles;
                files = System.IO.Directory.GetFiles(dirFullPath);
                numFiles = files.Length;
                numFiles = numFiles + 1;
                string str_image = "";
                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = context.Request.Files[s];
                    string fileName = file.FileName;
                    string fileThumb = string.Empty;
                    string fileExtension = file.ContentType;
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        fileExtension = Path.GetExtension(fileName);
                        //str_image = folder + numFiles.ToString() + fileExtension;
                        Guid guid = Guid.NewGuid();
                        str_image = guid + fileExtension;
                        string pathToSave_100 = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["ArticleImagesFolder"]) + str_image;
                        file.SaveAs(pathToSave_100);

                        //System.Drawing.Image image = System.Drawing.Image.FromFile(pathToSave_100);

                        //#region lưu ảnh với định dạng (540 * 302)
                        //fileThumb = path540 + str_image;
                        //using (Bitmap bitmap = new Bitmap(image, int.Parse(System.Configuration.ConfigurationManager.AppSettings["ArticleImageThumbnailWidth540"]), int.Parse(System.Configuration.ConfigurationManager.AppSettings["ArticleImageThumbnailHeight302"])))
                        //{
                        //    bitmap.Save(fileThumb, image.RawFormat);
                        //    bitmap.Dispose();
                        //}
                        //#endregion

                        //#region lưu ảnh với định dạng (503 * 245)
                        //fileThumb = path503 + str_image;
                        //using (Bitmap bitmap = new Bitmap(image, int.Parse(System.Configuration.ConfigurationManager.AppSettings["ArticleImageThumbnailWidth503"]), int.Parse(System.Configuration.ConfigurationManager.AppSettings["ArticleImageThumbnailHeight245"])))
                        //{
                        //    bitmap.Save(fileThumb, image.RawFormat);
                        //    bitmap.Dispose();
                        //}
                        //#endregion

                        //#region lưu ảnh với định dạng (297 * 197)
                        //fileThumb = path297 + str_image;
                        //using (Bitmap bitmap = new Bitmap(image, int.Parse(System.Configuration.ConfigurationManager.AppSettings["ArticleImageThumbnailWidth297"]), int.Parse(System.Configuration.ConfigurationManager.AppSettings["ArticleImageThumbnailHeight197"])))
                        //{
                        //    bitmap.Save(fileThumb, image.RawFormat);
                        //    bitmap.Dispose();
                        //}
                        //#endregion

                        //#region lưu ảnh với định dạng (359 * 200)
                        //fileThumb = path359 + str_image;
                        //using (Bitmap bitmap = new Bitmap(image, int.Parse(System.Configuration.ConfigurationManager.AppSettings["ArticleImageThumbnailWidth359"]), int.Parse(System.Configuration.ConfigurationManager.AppSettings["ArticleImageThumbnailHeight200"])))
                        //{
                        //    bitmap.Save(fileThumb, image.RawFormat);
                        //    bitmap.Dispose();
                        //}
                        //#endregion

                        //#region lưu ảnh với định dạng (158 * 91)
                        //fileThumb = path158 + str_image;
                        //using (Bitmap bitmap = new Bitmap(image, int.Parse(System.Configuration.ConfigurationManager.AppSettings["ArticleImageThumbnailWidth158"]), int.Parse(System.Configuration.ConfigurationManager.AppSettings["ArticleImageThumbnailHeight91"])))
                        //{
                        //    bitmap.Save(fileThumb, image.RawFormat);
                        //    bitmap.Dispose();
                        //}
                        //#endregion
                        //image.Dispose();
                    }


                }
                //var httpPostedFile = HttpContext.Current.Request.Files["Avatar"];

                // Validate the uploaded image(optional)

                // Get the complete file path
                //string imageName = Path.Combine(HttpContext.Current.Server.MapPath(folder), file.FileName);
                //string url = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["ArticleImagesFolder"] + httpPostedFile.FileName;
                // Save the uploaded file to "UploadedFiles" folder
                //file.SaveAs(imageName);
                context.Response.Write(str_image);

            }
            catch (Exception ac)
            {

            }
            //  database record update logic here  ()


        }
        #endregion
    }
}
