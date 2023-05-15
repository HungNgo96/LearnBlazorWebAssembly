using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Shared.Helper
{
    public static class ConvertFunction
    {
        public static DateTime ConvertStringToDatetime(string strInput, string strFormat)
        {
            string outPut = "";
            if (strFormat == "dd/MM/yyyy")
            {
                string[] split = strInput.Split('/');
                outPut = split[1] + "-" + split[0] + "-" + split[2];
            }
            return Convert.ToDateTime(outPut);
        }
    }

    public static class DateTimeExts
    {
        public static TimeSpan GetDelayTime(this TimeSpan startTime)
        {
            var curTime = DateTime.Now.TimeOfDay;
            TimeSpan res;
            if (curTime > startTime)
            {
                var timeSpanPerDay = new TimeSpan(TimeSpan.TicksPerDay);
                res = timeSpanPerDay.Subtract(curTime) + startTime;
            }
            else
            {
                res = startTime.Subtract(curTime);
            }
            return res;
        }
    }

    public static class StringExts
    {
        private static readonly string[] VNSigns =
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

        public static string ToNoneSign(this string text)
        {
            if (text == null)
            {
                return null;
            }

            for (int i = 1; i < VNSigns.Length; i++)
            {
                for (int j = 0; j < VNSigns[i].Length; j++)
                {
                    text = text.Replace(VNSigns[i][j], VNSigns[0][i - 1]);
                }
            }

            return text;
        }

        public static string ToLongDateTimeString(this DateTime value) => $"{value:yyyy}-{value:MM}-{value:dd} {value:HH}:{value:mm}:{value:ss}";

        public static string GetCurrentFullyDateTime(this DateTime entry)
        {
            return entry.ToString("MM/dd/yyyy hh:mm:ss tt", new CultureInfo("en-US"));
        }

        public static string GetCurrentOnlyDate(this DateTime entry)
        {
            return entry.ToString("MM/dd/yyyy");
        }

        public static string RemoveUnicode(this string text)
        {
            for (int i = 33; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 58; i < 65; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 91; i < 97; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 123; i < 127; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            text = text.Replace(" ", "_")
                .Replace("__", "_");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD).ToLower();

            return regex.Replace(strFormD, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string RemoveUnsign(this string text)
        {
            for (int i = 33; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 58; i < 65; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 91; i < 97; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 123; i < 127; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);

            return regex.Replace(strFormD, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static DateTime? ToDateTime(this string strInput)
        {
            if (string.IsNullOrEmpty(strInput) || string.IsNullOrWhiteSpace(strInput)) return null;
            var pattern = new string[] {
                "M/dd/yyyy hh:mm:ss tt", // [1-12]/[1-31]/YYYY [hh:mm:ss] [AM|PM]
                "MM/dd/yyyy hh:mm:ss tt", // [01-12]/[0-31]/YYYY [hh:mm:ss] [AM|PM]
                "M/d/yyyy hh:mm:ss tt", // [1-12]/[1-31]/YYYY [hh:mm:ss] [AM|PM]
                "MM/d/yyyy hh:mm:ss tt", // // [01-12]/[01-31]/YYYY [hh:mm:ss] [AM|PM]
                "M/dd/yyyy HH:mm:ss", // [1-12]/[1-31]/YYYY [0-24:mm:ss]
                "MM/dd/yyyy HH:mm:ss", // [01-12]/[0-31]/YYYY [0-24:mm:ss]
                "M/d/yyyy HH:mm:ss", // [1-12]/[1-31]/YYYY [0-24:mm:ss]
                "MM/d/yyyy HH:mm:ss", // // [01-12]/[01-31]/YYYY [0-24:mm:ss]
                 "M/d/yyyy HH:mm", // [1-12]/[1-31]/YYYY [0-24:mm]
                "MM/d/yyyy HH:mm", // // [01-12]/[01-31]/YYYY [0-24:mm]
                  "M/dd/yyyy HH:mm", // [1-12]/[1-31]/YYYY [0-24:mm]
                "MM/dd/yyyy HH:mm", // // [01-12]/[01-31]/YYYY [0-24:mm]
                "M/dd/yyyy", // [1-12]/[1-31]/YYYY
                "MM/dd/yyyy", // [01-12]/[0-31]/YYYY
                "M/d/yyyy", // [1-12]/[1-31]/YYYY
                "MM/d/yyyy", // // [01-12]/[01-31]/YYYY
            };

            DateTime res = DateTime.Now;
            var missMatch = 0;
            for (int i = 0; i < pattern.Length; i++)
            {
                try
                {
                    res = DateTime.ParseExact(strInput, pattern[i], CultureInfo.InvariantCulture);
                    break;
                }
                catch (Exception)
                {
                    missMatch++;
                }
            }
            if (missMatch == pattern.Length)
            {
                throw new Exception("Cannot to date with this value: " + strInput);
            }
            return res;
        }
    }
}