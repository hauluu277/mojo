using System;
using System.IO;
using System.Net;
using System.Text;
using System.Configuration;
using System.Globalization;

namespace Utilities
{
    public static class FeatureUtilities
    {
        public static string ConvertToUnsign(string s)
        {
            string stFormD = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            foreach (char t in stFormD)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(t);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(t);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }

        public static string ConvertToScope(string s)
        {
            if (s.Contains(":"))
            {
                s = s.Replace(":", "");
            }
            if (s.Contains("%"))
            {
                s = s.Replace("%", "");
            }
            if (s.Contains("^"))
            {
                s = s.Replace("^", "");
            }
            if (s.Contains("*"))
            {
                s = s.Replace("*", "");
            }
            if (s.Contains("&"))
            {
                s = s.Replace("&", "");
            }
            if (s.Contains("#"))
            {
                s = s.Replace("#", "");
            }
            if (s.Contains("@"))
            {
                s = s.Replace("@", "");
            }
            if (s.Contains("!"))
            {
                s = s.Replace("!", "");
            }
            if (s.Contains("~"))
            {
                s = s.Replace("~", "");
            }
            if (s.Contains("$"))
            {
                s = s.Replace("$", "");
            }
            if (s.Contains("'"))
            {
                s = s.Replace("'", "");
            }
            if (s.Contains("\""))
            {
                s = s.Replace("\"", "");
            }
            return s.Replace(" ", "-");
        }

        public static string ConvertToPascalCase(string str)
        {
            //if nothing is proivided throw a null argument exception
            if (str == null) throw new ArgumentNullException("str", @"Null text cannot be converted!");

            if (str.Length == 0) return str;

            //split the provided string into an array of words
            string[] words = str.Split(' ');

            //loop through each word in the array
            for (int i = 0; i < words.Length; i++)
            {
                //if the current word is greater than 1 character long
                if (words[i].Length > 0)
                {
                    //grab the current word
                    string word = words[i];

                    //convert the first letter in the word to uppercase
                    char firstLetter = char.ToUpper(word[0]);

                    //concantenate the uppercase letter to the rest of the word
                    words[i] = firstLetter + word.Substring(1);
                }
            }

            //return the converted text
            return string.Join(" ", words);
        }

        public static bool IsValidFileSize(Double fileSize, string webConfigKey)
        {
            bool flagResult = fileSize <= Int32.Parse(ConfigurationManager.AppSettings[webConfigKey]);
            return flagResult;
        }

        public static bool IsValidFileExtension(string fileExtension, string webConfigKey)
        {
            bool flagResult = ConfigurationManager.AppSettings[webConfigKey].IndexOf(fileExtension.ToLower()) >= 0;
            return flagResult;
        }

        //public static void AddConfirmButton(WebControl control, string strConfirm)
        //{
        //        control.Attributes.Add("onclick", "javascript:return " +
        //                                          "confirm('" + strConfirm + "')");
        //}

        public static string StripHtml(string html, bool allowHarmlessTags)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;
            if (allowHarmlessTags)
                return System.Text.RegularExpressions.Regex.Replace(html, "", string.Empty);
            return System.Text.RegularExpressions.Regex.Replace(html, "<[^>]*>", string.Empty);
        }

        public static void ResizeImage(ref int width, ref int height, int resizeWidth, int resizeHeight, int imageWidth, int imageHeight)
        {
            if (imageHeight > resizeHeight)
            {
                width = (imageWidth * resizeHeight) / imageHeight;
                height = resizeHeight;

                if (width > resizeWidth)
                {
                    height = (imageHeight * resizeWidth) / imageWidth;
                    width = resizeWidth;
                }
            }
            else if (imageWidth > resizeWidth)
            {
                height = (imageHeight * resizeWidth) / imageWidth;
                width = resizeWidth;

                if (height > resizeHeight)
                {
                    width = (imageWidth * resizeHeight) / imageHeight;
                    height = resizeHeight;
                }
            }
        }

        public static string FormatInfo(string description, int number)
        {
            return FormatInfo(description, null, number);
        }

        public static string FormatInfo(string description)
        {
            return FormatInfo(description, null, 150);
        }

        public static string FormatInfo(string description, string detail)
        {
            return FormatInfo(description, detail, 150);
        }


        public static string FormatInfo(string description, string detail, int number)
        {
            string result;
            if (description.Length > 0)
            {
                result = StripHtml(description, false);
                if (result.Length > number)
                    result = StripHtml(description, false).Substring(0, number);
            }
            else
            {
                result = StripHtml(description, false);
                if (result.Length > number)
                    result = StripHtml(description, false).Substring(0, number);
            }
            return result;
        }

        public static string CutString(string content, int maxLength, string suffix)
        {
            string pureContent = StripHtml(content, false);
            if (pureContent.Length > 150)
            {
                pureContent = content.Substring(0, maxLength) + suffix;
            }
            return pureContent;
        }

        //Edit by KhoaVV on 24/10/2010 - Resize
        public static void ResizeRef(ref int width, ref int height, int minW, int minH)
        {
            if (width < minW && height < minH)
            {
                return;
            }
            if (width / (float)minW > height / (float)minH)//Chon width lam ti le
            {
                height = (int)(minW * (float)height / width);
                width = minW;
            }
            else
            {
                width = (int)(minH * (float)width / height);
                height = minH;
            }
        }

        public static string FormatPrice(string price, CultureInfo currencyCulture)
        {
            decimal result;
            decimal.TryParse(price, out result);
            return result.ToString("c", currencyCulture).Replace("₫", "VNĐ").Replace(",00", "");
        }

        public static string GetRealImageUrl(string url)
        {
            return url.Insert(url.LastIndexOf('.'), "_t");
        }

        public static string RemoveTwoColorModuleTitleText(string text)
        {
            return text.Replace("<span class='second left'>", string.Empty).Replace("<span class='second right'>", string.Empty).Replace("<span class='first left'>", string.Empty).Replace("<span class='first right'>", string.Empty).Replace("</span>", " ");
        }

        public static string ConvertStringArrayToString(string[] array)
        {
            //
            // Concatenate all the elements into a StringBuilder.
            //
            StringBuilder builder = new StringBuilder();
            foreach (string value in array)
            {
                builder.Append(value);
                builder.Append(',');
            }
            return builder.ToString();
        }

        public static string GetImageFile(string typeFile = "")
        {
            string result = string.Empty;
            switch (typeFile)
            {
                case ".doc":
                    result = "doc.png";
                    break;
                case ".docx":
                    result = "docx.png";
                    break;
                case ".pdf":
                    result = "pdf.png";
                    break;
                case ".xls":
                    result = "xls.png";
                    break;
                case "xlsx":
                    result = "xlsx.png";
                    break;
                case ".zip":
                    result = "zip.png";
                    break;
                case ".rar":
                    result = "rar.png";
                    break;
                case "pptx":
                    result = "pptx.png";
                    break;
                case "txt":
                    result = "txt.png";
                    break;
                default:
                    result = "files.png";
                    break;
            }


            return result;
        }
    }
}
