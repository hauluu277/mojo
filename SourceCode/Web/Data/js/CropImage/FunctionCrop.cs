 public static string CropAnhDaiDien(string imgPath, int Width, int Height,int x,int y ,string saveFilePath)
        {
            var fileName = "";
            var mapPath = "";
            if (string.IsNullOrEmpty(saveFilePath))
            {
                mapPath = HostingEnvironment.MapPath("/");
            }
            else
            {
                mapPath = saveFilePath + "/";
            }

            var dt = DateTime.Now;

            var pathReturn = "Uploads/thumbAnhDaiDien/" + string.Format("{0:yyyy}", dt) + "/" + string.Format("{0:MM}", dt) + '/' + string.Format("{0:dd}", dt);
            string dir = mapPath + pathReturn;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            var arrName = Path.GetFileName(imgPath).Split('.');
            var extention = '.' + arrName[arrName.Length - 1];
            var Name_File = string.Join(".", arrName, 0, arrName.Length - 1);
            fileName = Path.GetFileName(imgPath);
            var pathFile = Path.Combine(dir, fileName); //Đường đẫn vật lý của file;

            if (File.Exists(pathFile))
            {

                Name_File += string.Format("{0:ddMMyyyy-hhmmss}", dt);
                fileName = Name_File + extention;

                pathFile = Path.Combine(dir, fileName);
            }

            using (var streamImg = new FileStream(imgPath, FileMode.Open))
            {
                Bitmap sourceImage = new Bitmap(streamImg);
                Rectangle cropRect = new Rectangle(x, y, Width, Height);

                using (Bitmap objBitmap = new Bitmap(cropRect.Width, cropRect.Height))
                {
                    objBitmap.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
                    using (Graphics objGraphics = Graphics.FromImage(objBitmap))
                    {
                        // Set the graphic format for better result cropping   
                        objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                        objGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        //objGraphics.DrawImage(sourceImage, 0, 0, Width, Height);

                        objGraphics.DrawImage(sourceImage, new Rectangle(0 ,0, objBitmap.Width, objBitmap.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);


                        // Save the file path, note we use png format to support png file   
                        objBitmap.Save(pathFile);
                    }
                }
            }

            return pathReturn + "/" + fileName;

        }