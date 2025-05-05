using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mojoportal.CoreHelpers.Ultilities
{
    public static class Ultilities
    {
        private static readonly string KeyCrypt = "bao_dai_bieu_nhan_dan_2020";
        public static Random random = new Random((int)DateTime.Now.Ticks);
        private static Encoding encodingUTF8 = Encoding.UTF8;
        private static int keysize = 12;


        public static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public static string RandomStrings(DateTime date, int coso_id)
        {
            string format = "{0}{1}{2}{3}{4}{5}";
            return string.Format(format, date.Year.ToString().Substring(2, 2), date.Month.ToString("00"), date.Day.ToString("00"), date.Hour.ToString("00"), date.Minute.ToString("00"), coso_id.ToString("0000"));
        }

        /// <summary>
        /// Add thời gian cho datetime null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime? AddTime(this DateTime? obj, string addTime)
        {
            if (!string.IsNullOrEmpty(addTime))
            {
                try
                {
                    var date = addTime.Split(':');
                    if (date != null)
                    {

                        if (!string.IsNullOrEmpty(date[0]) && !string.IsNullOrEmpty(date[1]))
                        {
                            int gio = int.Parse(date[0]);
                            var phut = int.Parse(date[1]);

                            if (obj.HasValue)
                            {
                                var newdate = new DateTime(obj.Value.Year, obj.Value.Month, obj.Value.Day, gio, phut, 0);
                                return newdate;
                            }

                        }
                    }
                    return obj;
                }
                catch (Exception)
                {

                    return obj;
                }

            }
            else
            {
                return obj;
            }
        }

        public static DateTime? ToDateTimeFull(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                return DateTime.Parse(obj, System.Globalization.CultureInfo.CurrentCulture);

            }
            return null;
        }

        //
        public static DateTime? ToDate(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                var date = obj.Split('-');
                if (date != null)
                {
                    if (!string.IsNullOrEmpty(date[0]) && !string.IsNullOrEmpty(date[1]))
                    {
                        var datetime = date[0];
                        var ngay = datetime.Split('/');
                        var day = int.Parse(ngay[0]).ToString("00");
                        var month = int.Parse(ngay[1]).ToString("00");
                        var year = int.Parse(ngay[2]).ToString("0000");


                        return DateTime.ParseExact(string.Format("{0}/{1}/{2}", day, month, year), "dd/MM/yyyy", null);
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }
        public static string ToTime(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                var date = obj.Split('-');
                if (date != null)
                {
                    if (!string.IsNullOrEmpty(date[0]) && !string.IsNullOrEmpty(date[1]))
                    {
                        var datetime = date[1];
                        var ngay = datetime.Split(':');
                        var hour = int.Parse(ngay[0]).ToString("00");
                        var minute = int.Parse(ngay[1]).ToString("00");



                        return string.Format("{0}:{1}", hour, minute);
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }
        public static DateTime ToDateTime(this string obj)
        {
            var date = obj.Split('/');
            var day = int.Parse(date[0]).ToString("00");
            var month = int.Parse(date[1]).ToString("00");
            var year = int.Parse(date[2]).ToString("0000");
            return DateTime.ParseExact(string.Format("{0}/{1}/{2}", day, month, year), "dd/MM/yyyy", null);
        }
        public static DateTime ToDateTimeNotNull(this string obj)
        {
            var date = obj.Split('/');
            var day = int.Parse(date[0]).ToString("00");
            var month = int.Parse(date[1]).ToString("00");
            var year = int.Parse(date[2]).ToString("0000");
            return DateTime.ParseExact(string.Format("{0}/{1}/{2}", day, month, year), "dd/MM/yyyy", null);
        }
        public static DateTime? ToDateTimeV2(this string obj)
        {
            //try
            //{
            if (!string.IsNullOrEmpty(obj))
            {
                var date = obj.Split('/');
                if (date != null)
                {
                    if (!string.IsNullOrEmpty(date[0]) && !string.IsNullOrEmpty(date[1]) && !string.IsNullOrEmpty(date[2]))
                    {
                        var day = int.Parse(date[0]).ToString("00");
                        var month = int.Parse(date[1]).ToString("00");
                        var year = int.Parse(date[2]).ToString("0000");
                        return DateTime.ParseExact(string.Format("{0}/{1}/{2}", day, month, year), "dd/MM/yyyy", null);
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
            //}catch(Exception ex){
            //    return null;
            // }
        }
        public static bool ToBoolByOnOff(this string obj)
        {
            if (!string.IsNullOrEmpty(obj) && obj.ToLower().Equals("On".ToLower()))
            {
                return true;
            }
            return false;

        }
        public static DateTime? ToDateTimeFromMonth(this string obj)
        {
            //try
            //{
            if (!string.IsNullOrEmpty(obj))
            {
                var date = obj.Split('/');
                if (date != null)
                {
                    if (!string.IsNullOrEmpty(date[0]) && !string.IsNullOrEmpty(date[1]))
                    {

                        var month = int.Parse(date[0]).ToString("00");
                        var year = int.Parse(date[1]).ToString("0000");
                        return DateTime.ParseExact(string.Format("{0}/{1}/{2}", "01", month, year), "dd/MM/yyyy", null);
                    }
                }
                return null;
            }
            else
            {
                return null;
            }

        }


        public static DateTime? ToDateTimeFromYear(this string obj)
        {
            //try
            //{
            if (!string.IsNullOrEmpty(obj))
            {
                return DateTime.ParseExact(string.Format("{0}/{1}/{2}", "01", "01", obj), "dd/MM/yyyy", null);

            }
            else
            {
                return null;
            }
            //}catch(Exception ex){
            //    return null;
            // }
        }
        public static DateTime? ToEndDay(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                var date = obj.Split('/');
                if (date != null)
                {
                    if (!string.IsNullOrEmpty(date[0]) && !string.IsNullOrEmpty(date[1]) && !string.IsNullOrEmpty(date[2]))
                    {
                        var day = int.Parse(date[0]).ToString("00");
                        var month = int.Parse(date[1]).ToString("00");
                        var year = int.Parse(date[2]).ToString("0000");
                        return DateTime.ParseExact(string.Format("{0}/{1}/{2} 23:59:59", day, month, year), "dd/MM/yyyy HH:mm:ss", null);
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }
        public static DateTime? ToStartDay(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                var date = obj.Split('/');
                if (date != null)
                {
                    if (!string.IsNullOrEmpty(date[0]) && !string.IsNullOrEmpty(date[1]) && !string.IsNullOrEmpty(date[2]))
                    {
                        var day = int.Parse(date[0]).ToString("00");
                        var month = int.Parse(date[1]).ToString("00");
                        var year = int.Parse(date[2]).ToString("0000");
                        return DateTime.ParseExact(string.Format("{0}/{1}/{2} 00:00:00", day, month, year), "dd/MM/yyyy HH:mm:ss", null);
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }
        public static DateTime? ToEndYear(this DateTime? obj)
        {
            if (obj.HasValue)
            {
                var day = DateTime.DaysInMonth(obj.Value.Year, 12).ToString("00");

                return DateTime.ParseExact(string.Format("{0}/{1}/{2} 23:59:59", day, "12", obj.Value.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);

            }
            else
            {
                return null;
            }
        }

        public static DateTime? ToEndMonth(this DateTime? obj)
        {
            if (obj.HasValue)
            {
                var day = DateTime.DaysInMonth(obj.Value.Year, obj.Value.Month).ToString("00");

                return DateTime.ParseExact(string.Format("{0}/{1}/{2} 23:59:59", day, obj.Value.Month.ToString("00"), obj.Value.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);

            }
            else
            {
                return null;
            }
        }
        public static DateTime ToEndMonth(this DateTime obj)
        {

            var day = DateTime.DaysInMonth(obj.Year, obj.Month).ToString("00");

            return DateTime.ParseExact(string.Format("{0}/{1}/{2} 23:59:59", day, obj.Month.ToString("00"), obj.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);


        }
        public static DateTime? ToEndDay(this DateTime? obj)
        {
            if (obj.HasValue)
            {


                return DateTime.ParseExact(string.Format("{0}/{1}/{2} 23:59:59", obj.Value.Day.ToString("00"), obj.Value.Month.ToString("00"), obj.Value.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);

            }
            else
            {
                return null;
            }
        }
        public static DateTime ToEndDay(this DateTime obj)
        {


            return DateTime.ParseExact(string.Format("{0}/{1}/{2} 23:59:59", obj.Day.ToString("00"), obj.Month.ToString("00"), obj.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);

        }
        public static DateTime? ToStartDay(this DateTime? obj)
        {
            if (obj.HasValue)
            {


                return DateTime.ParseExact(string.Format("{0}/{1}/{2} 00:00:00", obj.Value.Day.ToString("00"), obj.Value.Month.ToString("00"), obj.Value.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);

            }
            else
            {
                return null;
            }
        }
        public static DateTime ToStartDay(this DateTime obj)
        {

            return DateTime.ParseExact(string.Format("{0}/{1}/{2} 00:00:00", obj.Day.ToString("00"), obj.Month.ToString("00"), obj.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);

        }
        public static DateTime? ToStartMonth(this DateTime? obj)
        {
            if (obj.HasValue)
            {


                return DateTime.ParseExact(string.Format("{0}/{1}/{2} 00:00:00", "01", obj.Value.Month.ToString("00"), obj.Value.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);

            }
            else
            {
                return null;
            }
        }
        public static DateTime ToStartMonth(this DateTime obj)
        {


            return DateTime.ParseExact(string.Format("{0}/{1}/{2} 00:00:00", "01", obj.Month.ToString("00"), obj.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);

        }
        public static DateTime ToEndYear(this DateTime obj)
        {

            var day = DateTime.DaysInMonth(obj.Year, 12).ToString("00");

            return DateTime.ParseExact(string.Format("{0}/{1}/{2} 23:59:59", day, "12", obj.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);


        }
        public static DateTime? ToStartYear(this DateTime? obj)
        {
            if (obj.HasValue)
            {

                return DateTime.ParseExact(string.Format("{0}/{1}/{2} 00:00:00", "01", "01", obj.Value.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);

            }
            else
            {
                return null;
            }
        }
        public static DateTime ToStartYear(this DateTime obj)
        {

            return DateTime.ParseExact(string.Format("{0}/{1}/{2} 00:00:00", "01", "01", obj.Year.ToString("0000")), "dd/MM/yyyy HH:mm:ss", null);


        }

        public static short? ToShortOrNULL(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                return short.Parse(obj);
            }
            else
            {
                return null;
            }
        }

        public static int? ToIntOrNULL(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                return int.Parse(obj);
            }
            else
            {
                return null;
            }
        }
        public static long? ToLongOrNULL(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                return long.Parse(obj);
            }
            else
            {
                return null;
            }
        }
        public static short ToShortOrZero(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                return short.Parse(obj);
            }
            else
            {
                return 0;
            }
        }

        public static int ToIntOrZero(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                return int.Parse(obj);
            }
            else
            {
                return 0;
            }
        }

        public static bool ToBoolOrFalse(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                return bool.Parse(obj);
            }
            else
            {
                return false;
            }
        }
        public static bool? ToBoolOrNull(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                try
                {
                    return bool.Parse(obj);
                }
                catch
                {

                    return null;
                }

            }
            else
            {
                return null;
            }
        }
        public static long ToLongOrZero(this string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                return long.Parse(obj);
            }
            else
            {
                return 0;
            }
        }

        public static float ToFloatOrZero(this string obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj))
                {
                    return float.Parse(obj);
                }
                else
                {
                    return 0;
                }
            }
            catch
            {

                return 0;
            }

        }

        public static decimal ToDecimalRegex(this string obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj))
                {
                    obj = string.Join(string.Empty, obj.ToCharArray().Where(Char.IsDigit));
                    return decimal.Parse(obj);
                }
                return 0;
            }
            catch
            {

                return 0;
            }
        }

        public static decimal ToDecimalOrZero(this string obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj))
                {
                    return decimal.Parse(obj);
                }
                else
                {
                    return 0;
                }
            }
            catch
            {

                return 0;
            }
        }
        public static List<long> ToListLong(this string obj, char split_key)
        {
            List<long> listLong = new List<long>();
            if (!string.IsNullOrEmpty(obj))
            {
                var list = obj.Split(split_key);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            listLong.Add(long.Parse(item));
                        }
                    }
                }
            }
            return listLong;
        }
        public static List<int> ToListInt(this string obj)
        {
            List<int> listInt = new List<int>();
            if (!string.IsNullOrEmpty(obj))
            {
                var list = obj.Split(',');
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            listInt.Add(int.Parse(item));
                        }
                    }
                }
            }
            return listInt;
        }
        public static List<int> ToListInt(this string obj, char prefix = ',')
        {
            List<int> listInt = new List<int>();
            if (!string.IsNullOrEmpty(obj))
            {
                var list = obj.Split(prefix);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            listInt.Add(int.Parse(item));
                        }
                    }
                }
            }
            return listInt;
        }
        public static List<string> ToListStringLower(this string obj, char split_key)
        {
            List<string> listInt = new List<string>();
            if (!string.IsNullOrEmpty(obj))
            {
                var list = obj.Split(split_key);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            listInt.Add(item);
                        }
                    }
                }
            }
            return listInt;
        }


        public static List<short> ToListShort(this string obj, char split_key)
        {
            List<short> listInt = new List<short>();
            if (!string.IsNullOrEmpty(obj))
            {
                var list = obj.Split(split_key);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            listInt.Add(short.Parse(item));
                        }
                    }
                }
            }
            return listInt;
        }

 


        /// <summary>
        /// Trả ra ngày bắt đầu và ngày kết thúc của tuần theo năm
        /// </summary>
        /// <param name="iWeek">Tuần</param>
        /// <param name="iYear">Năm</param>
        /// <returns></returns>


        static readonly string[] Columns = new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN", "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ", "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN", "BO", "BP", "BQ", "BR", "BS", "BT", "BU", "BV", "BW", "BX", "BY", "BZ", "CA", "CB", "CC", "CD", "CE", "CF", "CG", "CH", "CI", "CJ", "CK", "CL", "CM", "CN", "CO", "CP", "CQ", "CR", "CS", "CT", "CU", "CV", "CW", "CX", "CY", "CZ", "DA", "DB", "DC", "DD", "DE", "DF", "DG", "DH", "DI", "DJ", "DK", "DL", "DM", "DN", "DO", "DP", "DQ", "DR", "DS", "DT", "DU", "DV", "DW", "DX", "DY", "DZ" };
        public static string ToIndexToColumn(this int index)
        {
            if (index <= 0)
                throw new IndexOutOfRangeException("index must be a positive number");

            return Columns[index - 1];
        }

        public static string Truncate(string input = "", int length = 0)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.Length <= length)
                {
                    return input;
                }
                else
                {
                    return input.Substring(0, length) + "...";
                }
            }
            return string.Empty;
        }

        public static string GetSummary(this string input, int length = 0)
        {
            if (!string.IsNullOrEmpty(input))
            {
                if (input.Length <= length)
                {
                    return input;
                }
                else
                {
                    return input.Substring(0, length) + "...";
                }
            }
            return string.Empty;
        }




        private static readonly string[] VietnameseSigns = new string[]

        {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"

        };

        public static string RemoveUnicode(this string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                "đ",
                "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                "í","ì","ỉ","ĩ","ị",
                "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "d",
            "e","e","e","e","e","e","e","e","e","e","e",
            "i","i","i","i","i",
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
            "u","u","u","u","u","u","u","u","u","u","u",
            "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            string otherChars = "!@#$%^&*(),.[]{}";
            for (int i = 0; i < otherChars.Length; i++)
            {
                text = text.Replace(otherChars[i], ' ');
            }
            text = text.Replace("\t", "");
            return text;
        }

        public static string RemoveSign4VietnameseString(string str)
        {

            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi

            for (int i = 1; i < VietnameseSigns.Length; i++)
            {

                for (int j = 0; j < VietnameseSigns[i].Length; j++)

                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);

            }

            return str;

        }


        public static string ToSafeFileName(this string s)
        {


            return s
                .Replace("\\", "")
                .Replace("/", "")
                .Replace("\"", "")
                .Replace("*", "")
                .Replace(":", "")
                .Replace("?", "")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("|", "");
        }



        public static string Convert_StringvalueToHexvalue(string stringvalue, System.Text.Encoding encoding)
        {
            Byte[] stringBytes = encoding.GetBytes(stringvalue);
            StringBuilder sbBytes = new StringBuilder(stringBytes.Length * 2);
            foreach (byte b in stringBytes)
            {
                sbBytes.AppendFormat("{0:X2}", b);
            }
            return sbBytes.ToString();
        }
        public static string Convert_HexvalueToStringvalue(string hexvalue, System.Text.Encoding encoding)
        {
            int CharsLength = hexvalue.Length;
            byte[] bytesarray = new byte[CharsLength / 2];
            for (int i = 0; i < CharsLength; i += 2)
            {
                bytesarray[i / 2] = Convert.ToByte(hexvalue.Substring(i, 2), 16);
            }
            return encoding.GetString(bytesarray);
        }



        public static char cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {

                return ch;
            }

            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);


        }


        public static string Encipher(string input, int key = 12)
        {
            string output = string.Empty;

            foreach (char ch in input)
                output += cipher(ch, key);

            return output;
        }

        public static bool AllowFile(string fileName, string fileExtent)
        {
            if (!string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(fileExtent))
            {
                fileName = fileName.ToLower();
                fileExtent = fileExtent.ToLower();
                var listFileExtent = fileExtent.Split('|');
                if (listFileExtent.Contains(fileName))
                {
                    return true;
                }
            }
            return false;
        }

        public static string LoadTitle(object name, object subName, bool isLoadSubName = true)
        {
            string strName = string.Empty;
            string strSubName = string.Empty;
            if (name != null)
            {
                strName = name.ToString();
            }
            if (subName != null)
            {
                strSubName = subName.ToString();
            }

            if (!string.IsNullOrEmpty(strSubName) && isLoadSubName)
            {
                return strSubName;
            }
            return strName;
        }


    }
}
