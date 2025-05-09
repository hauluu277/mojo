﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace CommonHelper.Upload
{
    public class UploadResult
    {

        public bool status { get; set; }
        public string message { get; set; }
        public string fullPath { get; set; }
        public string path { get; set; }
        public string filename { get; set; }
        public string extension { get; set; }

        public string pathPdf { get; set; }
        public bool HasPdf { get; set; }

    }
    public class UploadProvider
    {
        public const string ListExtensionCommon = ".doc,.docx,.pdf,.xls,.xlsx,.ppt,.png,.jpg,.jpeg,.rar,.zip";
        public const string ListExtensionCommonImage = ".png,.jpg,.jpeg";
        public const string ListExtensionCommonPDFImage = ".pdf,.png,.jpg,.jpeg";
        public const string ListExtensionCommonDoc = ".doc,.docx";
        public const string ListExtensionCommonWinRar = ".zip";
        public const string ListExtensionCommonDocX = ".docx";
        public const long MaxSizeCommon = 20000000;
        public const long MaxSizeCommonPDF = 131072000;
        /// <summary>
        /// Lưu file
        /// </summary>
        /// <param name="file">FileBase cần lưu</param>
        /// <param name="name">TeenFile Muốn lưu</param>
        /// <param name="folder">Tên folder chứa file trong thư mục upload</param>
        /// <param name="PathSave">Đường dẫn thư mục upload</param>
        /// <returns></returns>
        /// 

        public static UploadResult CheckSaveFile(HttpPostedFileBase file, string extentionList, long? maxSize)
        {
            var result = new UploadResult();
            result.status = true;

            var arrName = file.FileName.Split('.');
            var extention = '.' + arrName[arrName.Length - 1];
            var Name_File = string.Join(".", arrName, 0, arrName.Length - 1);

            #region Check extention có hợp lệ không

            if (!string.IsNullOrEmpty(extentionList))
            {
                var listExtention = extentionList.Split(',');
                if (!listExtention.Contains(extention.ToLower()))
                {
                    result.status = false;
                    result.message = "Định dạng file không được chấp nhận";
                    return result;
                }
            }
            #endregion

            #region CheckSize
            if (maxSize.HasValue && file.ContentLength > maxSize)
            {
                result.status = false;
                result.message = "File vượt quá kích cỡ cho phép";
                return result;
            }
            #endregion

            return result;
        }
        public static UploadResult SaveFile(HttpPostedFileBase file, string name, string extentionList, long? maxSize, string folder, string PathSave)
        {
            var result = new UploadResult();
            var fileName = "";

            var arrName = file.FileName.Split('.');
            result.extension = arrName[arrName.Length - 1];

            var extention = '.' + result.extension;
            var Name_File = string.Join(".", arrName, 0, arrName.Length - 1);

            #region 1.Kiểm tra có ghi đè tên file không
            if (string.IsNullOrEmpty(name))
            {
                fileName = file.FileName;
            }
            else
            {
                fileName = name + extention;
            }
            #endregion

            var dt = DateTime.Now;


            #region Check extention có hợp lệ không

            if (!string.IsNullOrEmpty(extentionList))
            {
                var listExtention = extentionList.Split(',');
                if (!listExtention.Contains(extention.ToLower()))
                {
                    result.status = false;
                    result.message = "Định dạng file không được chấp nhận";
                    return result;
                }
            }
            #endregion

            #region CheckSize
            if (maxSize.HasValue && file.ContentLength > maxSize)
            {
                result.status = false;
                result.message = "File vượt quá kích cỡ cho phép";
                return result;
            }
            #endregion

            #region 2. Kiểm tra có lưu thư mục riêng không. Nếu chưa có thì tạo

            var pathFolder = "";
            if (string.IsNullOrEmpty(folder))
            {
                folder = "Unknow";
            }

            pathFolder = Path.Combine(PathSave, folder);

            //Kiểm tra folder đã tồn tại chưa. Nếu chưa tồn tại rồi thì tạo mới
            if (!Directory.Exists(pathFolder))
            {
                try
                {
                    Directory.CreateDirectory(pathFolder);
                }
                catch
                {
                    result.status = false;
                    result.message = "Không tạo được folder";
                    return result;
                }

            }

            #endregion

            #region 3.Kiểm tra File đã tồn tại chưa? Nếu tồn tại sửa tên

            var pathFile = Path.Combine(pathFolder, fileName); //Đường đẫn vật lý của file;


            if (File.Exists(pathFile))
            {
                Name_File += string.Format("{0:ddMMyyyy-hhmmss}", dt);
                fileName = Name_File + extention;

                pathFile = Path.Combine(pathFolder, fileName);
            }

            #endregion

            #region 4. Lưu file
            try
            {
                file.SaveAs(pathFile);
                var URLFILE = Path.Combine(folder, fileName);
               
                result.status = true;
                result.path = URLFILE;
                result.fullPath = pathFile;
                result.filename = fileName;
            }
            catch (Exception)
            {
                result.status = false;
                result.message = "Không lưu được tài liệu";
                result.path = "";
                result.fullPath = "";
                return result;
            }

            #endregion

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="name"></param>
        /// <param name="extentionList"></param>
        /// <param name="maxSize"></param>
        /// <param name="UserName"></param>
        /// <param name="ItemType"></param>
        /// <returns></returns>
        public static UploadResult SaveFileDateTimeSrc(HttpPostedFileBase file, string name, string extentionList, long? maxSize, string UserName, long? Id, string ItemType, string PathSave = null)
        {
            var result = new UploadResult();
            var fileName = "";
            var dt = DateTime.Now;

            string saveFolder = string.Empty;
            if (string.IsNullOrEmpty(UserName))
            {
                saveFolder = "PublicUpload";
            }
            else
            {
                saveFolder = UserName + '_' + Id;
            }

            string mapPath = string.Empty;
            if (string.IsNullOrEmpty(PathSave))
            {
                mapPath = HostingEnvironment.MapPath("/");
            }
            else
            {
                mapPath = PathSave + "/";
            }


            string dir = mapPath + "Uploads/" + string.Format("{0:yyyy}", dt) + "/" + string.Format("{0:MM}", dt) + '/' + string.Format("{0:dd}", dt) + '/' + saveFolder;
            string dirStore = dir + '/' + ItemType;

            var arrName = file.FileName.Split('.');
            var extention = '.' + arrName[arrName.Length - 1];
            var Name_File = string.Join(".", arrName, 0, arrName.Length - 1);

            #region 1.Kiểm tra có ghi đè tên file không
            if (string.IsNullOrEmpty(name))
            {
                fileName = file.FileName;
            }
            else
            {
                fileName = name + extention;
            }
            #endregion


            #region Check extention có hợp lệ không

            if (!string.IsNullOrEmpty(extentionList))
            {
                var listExtention = extentionList.Split(',');
                if (!listExtention.Contains(extention.ToLower()))
                {
                    result.status = false;
                    result.message = "Định dạng file không được chấp nhận";
                    return result;
                }
            }
            #endregion

            #region CheckSize
            if (maxSize.HasValue && file.ContentLength > maxSize)
            {
                result.status = false;
                result.message = "File vượt quá kích cỡ cho phép";
                return result;
            }
            #endregion

            #region 2. Kiểm tra có lưu thư mục riêng không. Nếu chưa có thì tạo

            var pathFolder = "";
            if (string.IsNullOrEmpty(dirStore))
            {
                dirStore = "Unknow";

            }
            pathFolder = Path.Combine(dir, ItemType);

            //Kiểm tra folder đã tồn tại chưa. Nếu chưa tồn tại rồi thì tạo mới
            if (!Directory.Exists(pathFolder))
            {
                try
                {
                    Directory.CreateDirectory(pathFolder);
                }
                catch (Exception)
                {
                    result.status = false;
                    result.message = "Không tạo được folder";
                    return result;
                }

            }

            #endregion

            #region 3.Kiểm tra File đã tồn tại chưa? Nếu tồn tại sửa tên

            var pathFile = Path.Combine(dirStore, fileName); //Đường đẫn vật lý của file;

            if (File.Exists(pathFile))
            {

                Name_File += string.Format("{0:ddMMyyyy-hhmmss}", dt);
                fileName = Name_File + extention;

                pathFile = Path.Combine(dirStore, fileName);
            }

            #endregion

            #region 4. Lưu file
            try
            {
                file.SaveAs(pathFile);
                var URLFILE = Path.Combine(dirStore, fileName);
                result.status = true;
                result.path = URLFILE.Replace(mapPath, "");
                result.fullPath = pathFile;
                result.filename = fileName;
            }
            catch (Exception)
            {
                result.status = false;
                result.message = "Không lưu được tài liệu";
                result.path = "";
                result.fullPath = "";
                return result;
            }
            #endregion

            return result;
        }


        public static UploadResult CopyFileDateTimeSrc(string oldFIlePath, string UserName, long? Id, string ItemType)
        {
            var result = new UploadResult();
            if (!System.IO.File.Exists(Path.Combine(HostingEnvironment.MapPath("/"), oldFIlePath)))
            {
                result.status = false;
                result.message = "Không copy được tài liệu";
                result.path = "";
                result.fullPath = "";
                return result;
            }

            #region old file
            var fileName = Path.GetFileName(oldFIlePath);

            var arrName = fileName.Split('.');
            var extention = '.' + arrName[arrName.Length - 1];
            var Name_File = string.Join(".", arrName, 0, arrName.Length - 1);
            #endregion

            #region new path
            var dt = DateTime.Now;
            string mapPath = HostingEnvironment.MapPath("/");
            string dir = mapPath + "Uploads/" + string.Format("{0:yyyy}", dt) + "/" + string.Format("{0:MM}", dt) + '/' + string.Format("{0:dd}", dt) + '/' + UserName + '_' + Id;
            string dirStore = dir + '/' + ItemType;
            #endregion

            #region 1. Kiểm tra có lưu thư mục riêng không. Nếu chưa có thì tạo
            var pathFolder = "";
            if (string.IsNullOrEmpty(dirStore))
            {
                dirStore = "Unknow";

            }
            pathFolder = Path.Combine(dir, ItemType);

            //Kiểm tra folder đã tồn tại chưa. Nếu chưa tồn tại rồi thì tạo mới
            if (!Directory.Exists(pathFolder))
            {
                try
                {
                    Directory.CreateDirectory(pathFolder);
                }
                catch (Exception)
                {
                    result.status = false;
                    result.message = "Không tạo được folder";
                    return result;
                }

            }
            #endregion

            #region 2.Kiểm tra File đã tồn tại chưa? Nếu tồn tại sửa tên

            var pathFile = Path.Combine(dirStore, fileName); //Đường đẫn vật lý của file;

            if (File.Exists(pathFile))
            {

                Name_File += string.Format("{0:ddMMyyyy-hhmmss}", dt);
                fileName = Name_File + extention;

                pathFile = Path.Combine(dirStore, fileName);
            }

            #endregion

            #region 3. chuyển file
            try
            {
                System.IO.File.Copy(Path.Combine(HostingEnvironment.MapPath("/"), oldFIlePath), Path.Combine(dirStore, fileName));
                try
                {
                    System.IO.File.Delete(Path.Combine(HostingEnvironment.MapPath("/"), oldFIlePath));
                }
                catch (Exception)
                {
                    result.status = false;
                    result.message = "Không xóa được tài liệu cũ";
                    result.path = "";
                    result.fullPath = "";
                    return result;
                }

                result.status = true;
                result.message = "Thành công";
                var URLFILE = Path.Combine(dirStore, fileName);
                result.path = URLFILE.Replace(mapPath, "");
                result.fullPath = "";
                return result;
            }
            catch (Exception)
            {
                result.status = false;
                result.message = "Không lưu được tài liệu";
                result.path = "";
                result.fullPath = "";
                return result;
            }
            #endregion
        }

        public static UploadResult SaveFileTinBai(string oldFIlePath, string UserName, long? Id, string ItemType)
        {
            var result = new UploadResult();
            if (!System.IO.File.Exists(Path.Combine(HostingEnvironment.MapPath("/"), oldFIlePath)))
            {
                result.status = false;
                result.message = "Không tạo mới được tài liệu";
                result.path = "";
                result.fullPath = "";
                return result;
            }

            #region old file
            var fileName = Path.GetFileName(oldFIlePath);

            var arrName = fileName.Split('.');
            var extention = '.' + arrName[arrName.Length - 1];
            var Name_File = string.Join(".", arrName, 0, arrName.Length - 1);
            #endregion

            #region new path
            var dt = DateTime.Now;
            string mapPath = HostingEnvironment.MapPath("/");
            string dir = mapPath + "Uploads/AnhBaoIn/" + Id;
            string dirStore = dir + '/';
            #endregion

            #region 1. Kiểm tra có lưu thư mục riêng không. Nếu chưa có thì tạo
            var pathFolder = "";
            if (string.IsNullOrEmpty(dirStore))
            {
                dirStore = "Unknow";

            }
            pathFolder = dir;

            //Kiểm tra folder đã tồn tại chưa. Nếu chưa tồn tại rồi thì tạo mới
            if (!Directory.Exists(pathFolder))
            {
                try
                {
                    Directory.CreateDirectory(pathFolder);
                }
                catch (Exception)
                {
                    result.status = false;
                    result.message = "Không tạo được folder";
                    return result;
                }

            }
            #endregion

            #region 2.Kiểm tra File đã tồn tại chưa? Nếu tồn tại sửa tên

            var pathFile = Path.Combine(dirStore, fileName); //Đường đẫn vật lý của file;

            if (File.Exists(pathFile))
            {

                Name_File += string.Format("{0:ddMMyyyy-hhmmss}", dt);
                fileName = Name_File + extention;

                pathFile = Path.Combine(dirStore, fileName);
            }

            #endregion

            #region 3. chuyển file
            try
            {
                System.IO.File.Copy(Path.Combine(HostingEnvironment.MapPath("/"), oldFIlePath), Path.Combine(dirStore, fileName));
                try
                {
                    System.IO.File.Delete(Path.Combine(HostingEnvironment.MapPath("/"), oldFIlePath));
                }
                catch (Exception)
                {
                    result.status = false;
                    result.message = "Không xóa được tài liệu cũ";
                    result.path = "";
                    result.fullPath = "";
                    return result;
                }

                result.status = true;
                result.message = "Thành công";
                var URLFILE = Path.Combine(dirStore, fileName);
                result.path = URLFILE.Replace(mapPath, "");
                result.fullPath = "";
                return result;
            }
            catch (Exception)
            {
                result.status = false;
                result.message = "Không lưu được tài liệu";
                result.path = "";
                result.fullPath = "";
                return result;
            }
            #endregion
        }

        //public static object SaveFile(object documentFile, object p1, string v1, object p2, string v2, string v3)
        //{
        //    throw new NotImplementedException();
        //}

        public static async void DeleteFileAttach(string path)
        {
            try
            {
                System.IO.File.Delete(path);
            }
            catch (Exception)
            {

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="name"></param>
        /// <param name="extentionList"></param>
        /// <param name="maxSize"></param>
        /// <param name="UserName"></param>
        /// <param name="ItemType"></param>
        /// <returns></returns>
        public static UploadResult SaveFileDateTimeSrcTinBai(HttpPostedFileBase file, string name, string extentionList, long? maxSize, string ItemType, string PathSave)
        {
            var result = new UploadResult();
            var fileName = "";
            var dt = DateTime.Now;

            string saveFolder = string.Empty;
            saveFolder = "TempUpload";

            string mapPath = string.Empty;
            if (string.IsNullOrEmpty(PathSave))
            {
                mapPath = HostingEnvironment.MapPath("/");
            }
            else
            {
                mapPath = PathSave + "/";
            }


            string dir = mapPath + "Uploads/" + saveFolder + '/' + ItemType + string.Format("{0:yyyy}", dt) + "/" + string.Format("{0:MM}", dt) + '/' + string.Format("{0:dd}", dt);
            string dirStore = dir;


            //string dir = mapPath + "Uploads/" + string.Format("{0:yyyy}", dt) + "/" + string.Format("{0:MM}", dt) + '/' + string.Format("{0:dd}", dt) + '/' + saveFolder;
            //string dirStore = dir + '/' + ItemType;

            var arrName = file.FileName.Split('.');
            var extention = '.' + arrName[arrName.Length - 1];
            var Name_File = string.Join(".", arrName, 0, arrName.Length - 1);

            #region 1.Kiểm tra có ghi đè tên file không
            if (string.IsNullOrEmpty(name))
            {
                fileName = file.FileName;
            }
            else
            {
                fileName = name + extention;
            }
            #endregion


            #region Check extention có hợp lệ không

            if (!string.IsNullOrEmpty(extentionList))
            {
                var listExtention = extentionList.Split(',');
                if (!listExtention.Contains(extention.ToLower()))
                {
                    result.status = false;
                    result.message = "Định dạng file không được chấp nhận";
                    return result;
                }
            }
            #endregion

            #region CheckSize
            if (maxSize.HasValue && file.ContentLength > maxSize)
            {
                result.status = false;
                result.message = "File vượt quá kích cỡ cho phép";
                return result;
            }
            #endregion

            #region 2. Kiểm tra có lưu thư mục riêng không. Nếu chưa có thì tạo

            var pathFolder = "";
            if (string.IsNullOrEmpty(dirStore))
            {
                dirStore = "Unknow";

            }
            pathFolder = Path.Combine(dir, ItemType);

            //Kiểm tra folder đã tồn tại chưa. Nếu chưa tồn tại rồi thì tạo mới
            if (!Directory.Exists(pathFolder))
            {
                try
                {
                    Directory.CreateDirectory(pathFolder);
                }
                catch (Exception)
                {
                    result.status = false;
                    result.message = "Không tạo được folder";
                    return result;
                }

            }

            #endregion

            #region 3.Kiểm tra File đã tồn tại chưa? Nếu tồn tại sửa tên

            var pathFile = Path.Combine(dirStore, fileName); //Đường đẫn vật lý của file;

            if (File.Exists(pathFile))
            {

                Name_File += string.Format("{0:ddMMyyyy-hhmmss}", dt);
                fileName = Name_File + extention;

                pathFile = Path.Combine(dirStore, fileName);
            }

            #endregion

            #region 4. Lưu file
            try
            {
                file.SaveAs(pathFile);
                var URLFILE = Path.Combine(dirStore, fileName);
                result.status = true;
                result.path = URLFILE.Replace(mapPath, "");
                result.fullPath = pathFile;
                result.filename = fileName;
            }
            catch (Exception)
            {
                result.status = false;
                result.message = "Không lưu được tài liệu";
                result.path = "";
                result.fullPath = "";
                return result;
            }
            #endregion

            return result;
        }

    }
}
