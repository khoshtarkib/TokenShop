using System;
using System.Globalization;

namespace TokenShop.Common
{
    public class PersianDate
    {
        public static string ConvertEnglishToPersianDate(DateTime? dt)
        {
            if (dt == null)
                return "";

            PersianCalendar Pdate = new PersianCalendar();
            return Pdate.GetYear(dt.Value).ToString("0000") + "/" + Pdate.GetMonth(dt.Value).ToString("00") + "/" + Pdate.GetDayOfMonth(dt.Value).ToString("00");
        }
        public static DateTime ConvertPersianDateToEnglishDate(string input)
        {
            input = input.Replace("&lt;br/&gt;", string.Empty);
            input = input.Replace("-", "/");
            input = input.Replace("۰", "0");
            input = input.Replace("۱", "1");
            input = input.Replace("۲", "2");
            input = input.Replace("۳", "3");
            input = input.Replace("۴", "4");
            input = input.Replace("۵", "5");
            input = input.Replace("۶", "6");
            input = input.Replace("۷", "7");
            input = input.Replace("۸", "8");
            input = input.Replace("۹", "9");
            PersianCalendar Pdate = new PersianCalendar();
            string[] splitArray = new string[3];
            splitArray = input.Split('/');
            int roz, mah, sal;
            sal = int.Parse(splitArray[0]);
            mah = int.Parse(splitArray[1]);
            roz = int.Parse(splitArray[2]);
            return (Pdate.ToDateTime(sal, mah, roz, 0, 0, 0, 0));
        }
        public static string GetToday()
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(DateTime.Now).ToString("0000") + "/" + pc.GetMonth(DateTime.Now).ToString("00") + "/" + pc.GetDayOfMonth(DateTime.Now).ToString("00");
        }
    }
}