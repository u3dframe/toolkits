using System.Collections;
using System;
using System.Text;
using UnityEngine;
using System.Globalization;

/// <summary>
/// 类名 : 时间工具
/// 作者 : Canyon
/// 日期 : long long ago
/// 功能 :  此功能不完全，没有加上时区(参考)
/// </summary>
namespace Toolkits
{
    public class DateEx
    {
        public const string fmt_yyyy_MM_dd_HH_mm_ss = "yyyy-MM-dd HH:mm:ss";
        public const string fmt_MM_dd_HH_mm = "MM-dd HH:mm";
        public const string fmt_yyyy_MM_dd = "yyyy-MM-dd";
        public const string fmt_yyyyMMdd = "yyyyMMdd";
        public const string fmt_yyyyMMddHHmm = "yyyyMMddHHmm";
        public const string fmt_HH_mm_ss = "HH:mm:ss";
        public const long TIME_MILLISECOND = 1;
        public const long TIME_SECOND = 1000 * TIME_MILLISECOND;
        public const long TIME_MINUTE = 60 * TIME_SECOND;
        public const long TIME_HOUR = 60 * TIME_MINUTE;
        public const long TIME_DAY = 24 * TIME_HOUR;
        public const long TIME_WEEK = 7 * TIME_DAY;
        public const long TIME_YEAR = 365 * TIME_DAY;

        public static long now
        {
            get
            {
                return DateTime.Now.ToFileTime();
            }
        }

        public static long nowMS
        {
            get
            {
                return now / 10000;
            }
        }

        public static string format(string fmt)
        {
            return format(DateTime.Now, fmt);
        }

        public static string nowString()
        {
            return format(fmt_yyyy_MM_dd_HH_mm_ss);
        }

        public static string format(DateTime d, string fmt)
        {
            // return d.ToString(fmt);

            string str_fmt = "{0:" + fmt + "}";
            return string.Format(str_fmt, d);
        }

        public static string formatByMs(long ms, string fmt = fmt_yyyy_MM_dd_HH_mm_ss)
        {
            DateTime d1 = new DateTime(1970, 1, 1,0,0,0);
            long us = (ms + d1.Ticks / 10000) * 10000;
            DateTime d = new DateTime(us);
            
            return format(d, fmt);
        }

        static public DateTime parseTo(string dateStr,string pattern)
        {
            if (StrEx.getLens(dateStr) == 0)
            {
                return DateTime.Now;
            }
            if (StrEx.getLens(pattern) == 0)
            {
                pattern = fmt_yyyy_MM_dd;
            }

            dateStr = dateStr.Replace("\"", "");
            dateStr = dateStr.Replace("\\\\", "");

            return DateTime.ParseExact(dateStr, pattern, CultureInfo.InvariantCulture);

            // IFormatProvider culture = new CultureInfo("zh-CN", true);
            // string[] expectedFormats = new string[] { pattern };
            // return DateTime.ParseExact(dateStr, expectedFormats, culture, DateTimeStyles.AllowInnerWhite);
        }

        static DateTime dat0 = new DateTime(1970, 1, 2);

        public static DateTime javaDate(long x)
        {
            long tm = (x + dat0.Ticks / 10000) * 10000;
            return new DateTime(tm);
        }

        // 取得客户端当前时间
        static public long toJavaNTimeLong()
        {
            return toJavaDate(DateTime.Now);
        }

        public static long toJavaDate(DateTime dat)
        {
            DateTime d1 = new DateTime(1970, 1, 1);
            DateTime d2 = dat.ToUniversalTime();
            TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);
            return (long)ts.TotalMilliseconds;

            /*long v = (dat.Ticks - dat0.Ticks) / 10000;
            return v;*/
        }

        //服务器同步时间diffCSTime:表示客服端与服务器端的时间差，isCellMS:表示到秒，毫秒往上收了一秒
        public static long newDateLong(long diffCSTime = 0, bool isCellMS = false)
        {
            long time = diffCSTime + toJavaNTimeLong();
            if (isCellMS)
            {
                double tmT = time / (double)TIME_SECOND;
                tmT = System.Math.Ceiling(tmT);
                time = (long)tmT * TIME_SECOND;
            }
            return time;
        }

        public static long diffTimeWithServer = 0;

        public static long nowServerTime
        {
            get
            {
                return diffTimeWithServer + toJavaNTimeLong();
            }
        }

        // [0]=天，[1]=时，[2]=分，[3]=秒，[4]=毫秒
        static public int[] getTimeArray(long ms)
        {
            long tmpMs = ms;

            int ss = 1000;
            int mi = ss * 60;
            int hh = mi * 60;
            int dd = hh * 24;
            int day = 0, hour = 0, minute = 0, second = 0, milliSecond = 0;

            if (tmpMs > dd)
            {
                day = (int)(tmpMs / dd);
                tmpMs %= dd;
            }

            if (tmpMs > hh)
            {
                hour = (int)(tmpMs / hh);
                tmpMs %= hh;
            }

            if (tmpMs > mi)
            {
                minute = (int)(tmpMs / mi);
                tmpMs %= mi;
            }

            if (tmpMs > ss)
            {
                second = (int)(tmpMs / ss);
                tmpMs %= ss;
            }

            milliSecond = (int)tmpMs;

            return new int[] { day, hour, minute, second, milliSecond };
        }

        public static string toHHMMSS(long ms)
        {
            int[] ss = getTimeArray(ms);
            return ss[1] + ":" + ss[2] + ":" + ss[3];
        }

        // 时间格式化为:HH:mm:ss;
        public static string toStrEn(long ms)
        {
            int[] arr = getTimeArray(ms);
            int hour = arr[0] * 24 + arr[1];
            String strHour = "";
            String strMinute = "";
            String strSecond = "";
            if (hour > 0)
            {
                strHour = hour < 10 ? "0" + hour : "" + hour;
                strHour += ":";
            }
            int minute = arr[2];
            if (minute >= 0)
            {
                strMinute = minute < 10 ? "0" + minute : "" + minute;
                strMinute += ":";
            }
            int second = arr[3];
            if (second >= 0)
            {
                strSecond = second < 10 ? "0" + second : "" + second;
            }
            return strHour + strMinute + strSecond;
        }

        // 时间格式化为:HH时mm分ss秒;
        public static string toStrCn(long ms)
        {
            int[] arr = getTimeArray(ms);
            int hour = arr[0] * 24 + arr[1];
            String strHour = "";
            String strMinute = "";
            String strSecond = "";
            if (hour > 0)
            {
                strHour = hour < 10 ? "0" + hour : "" + hour;
                strHour += "时";
            }
            int minute = arr[2];
            if (minute > 0)
            {
                strMinute = minute < 10 ? "0" + minute : "" + minute;
                strMinute += "分";
            }
            int second = arr[3];
            if (second >= 0)
            {
                strSecond = second < 10 ? "0" + second : "" + second;
                strSecond += "秒";
            }
            return strHour + strMinute + strSecond;
        }
        
        public static string ToTimeStr(long msec)
        {
            // 将毫秒数换算成x天x时x分x秒x毫秒
            int day = 0, hour = 0, minute = 0, second = 0;
            string retstr = "";

            long remainder;
            day = (int)(msec / 86400000);
            //retstr = (day == 0) ? "" : day + ":";

            remainder = msec % 86400000;
            if (remainder != 0)
            {
                hour = (int)remainder / 3600000;
            }
            hour += day * 24;
            retstr += ((retstr.Length > 0 || hour > 0) ? (hour < 10 ? "0" + hour + ":" : hour + ":") : "");

            remainder = remainder % 3600000;
            if (remainder != 0)
            {
                minute = (int)remainder / 60000;
            }
            retstr += ((retstr.Length > 0 || minute > 0) ? (minute < 10 ? "0" + minute + ":" : minute + ":") : "00:");

            second = (int)remainder % 60000;
            second = second / 1000;
            retstr += (second < 10 ? "0" + second + "" : second + "");
            return retstr;
        }

        static public long getLongJavaByHMS(string hms)
        {
            hms = hms.Replace("\\\\", "");
            string yyMMddHHmmss = format(fmt_yyyy_MM_dd) + " " + hms;
            return getLongJavaByYMDHMS(yyMMddHHmmss);
        }

        static public string nowStrYyyyMMdd()
        {
            return format(fmt_yyyyMMdd);
        }

        static public string nxtStrYyyyMMdd()
        {
            DateTime dt = DateTime.Now;
            DateTime nxtDt = dt.AddDays(1);
            return format(nxtDt, fmt_yyyyMMdd);
        }

        static public bool isSameDateStr(String dateStr)
        {
            if (string.IsNullOrEmpty(dateStr))
                return false;
            string nowStr = nowStrYyyyMMdd();
            int v = nowStr.CompareTo(dateStr);
            bool flag = v > -1;
            return flag;
        }
        
        static public string nowStrYyyyMMddHHmm()
        {
            return format(fmt_yyyyMMddHHmm);
        }

        static public string nxtStrYyyyMMddHHmm()
        {
            DateTime dt = DateTime.Now;
            DateTime nxtDt = dt.AddMinutes(1);
            return format(nxtDt, fmt_yyyyMMddHHmm);
        }

        static public bool isBeforeNow4yyMMddHHmm(String dateStr)
        {
            if (string.IsNullOrEmpty(dateStr))
                return false;
            string nowStr = nowStrYyyyMMddHHmm();
            int v = nowStr.CompareTo(dateStr);
            bool flag = v > -1;
            return flag;
        }

        static public bool isBefore(DateTime dt1, DateTime dt2)
        {
            int v = dt1.CompareTo(dt2);
            bool flag = v <= -1;
            return flag;
        }

        static public bool isBeforeNow(DateTime dt1)
        {
            return isBefore(dt1, DateTime.Now);
        }

        static public bool isBeforeNow(string dateStr,string pattern)
        {
            return isBefore(parseTo(dateStr, pattern), DateTime.Now);
        }

        static public bool isAfter(DateTime dt1, DateTime dt2)
        {
            int v = dt1.CompareTo(dt2);
            bool flag = v >= 1;
            return flag;
        }

        static public bool isAfterNow(DateTime dt1)
        {
            return isAfter(dt1, DateTime.Now);
        }

        static public bool isAfterNow(string dateStr, string pattern)
        {
            return isAfter(parseTo(dateStr, pattern), DateTime.Now);
        }

        static public bool isSame(DateTime dt1, DateTime dt2)
        {
            int v = dt1.CompareTo(dt2);
            bool flag = v == 0;
            return flag;
        }

        static public bool isSameNow(DateTime dt1)
        {
            return isSame(dt1, DateTime.Now);
        }

        static public bool isSameNow(string dateStr, string pattern)
        {
            return isSame(parseTo(dateStr, pattern), DateTime.Now);
        }

        static public bool isNotBefore(DateTime dt1, DateTime dt2)
        {
            int v = dt1.CompareTo(dt2);
            bool flag = v >= 0;
            return flag;
        }

        static public bool isNotBeforeNow(DateTime dt1)
        {
            return isNotBefore(dt1, DateTime.Now);
        }

        static public bool isNotBeforeNow(string dateStr, string pattern)
        {
            return isNotBefore(parseTo(dateStr, pattern), DateTime.Now);
        }

        static public bool isNotAfter(DateTime dt1, DateTime dt2)
        {
            int v = dt1.CompareTo(dt2);
            bool flag = v <= 0;
            return flag;
        }

        static public bool isNotAfterNow(DateTime dt1)
        {
            return isNotAfter(dt1, DateTime.Now);
        }

        static public bool isNotAfterNow(string dateStr, string pattern)
        {
            return isNotAfter(parseTo(dateStr, pattern), DateTime.Now);
        }

        static public long getLongJavaByYMDHMS(string yyMMddHHmmss)
        {
            try
            {
                yyMMddHHmmss = yyMMddHHmmss.Replace("\\\\", "");
                DateTime dt = DateTime.Parse(yyMMddHHmmss);
                long jl = toJavaDate(dt);
                return jl + diffTimeWithServer;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        static public int getCurDay()
        {
            return 0;
            // string dd = format(DateTime.Now, "dd");
            // return NumEx.stringToInt(dd);
        }
    }
}