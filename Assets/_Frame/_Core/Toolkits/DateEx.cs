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
		public const string fmt_yyyy_MM_dd_HH_mm = "yyyy-MM-dd HH:mm";
        public const string fmt_MM_dd_HH_mm = "MM-dd HH:mm";
        public const string fmt_yyyy_MM_dd = "yyyy-MM-dd";
        public const string fmt_yyyyMMdd = "yyyyMMdd";
        public const string fmt_yyyyMMddHHmm = "yyyyMMddHHmm";
		public const string fmt_yyyyMMddHHmmss = "yyyyMMddHHmmss";
        public const string fmt_HH_mm_ss = "HH:mm:ss";
		public const int TIME_MILLISECOND = 1;
		public const int TIME_SECOND = 1000 * TIME_MILLISECOND;
		public const int TIME_MINUTE = 60 * TIME_SECOND;
		public const int TIME_HOUR = 60 * TIME_MINUTE;
		public const int TIME_DAY = 24 * TIME_HOUR;
        public const long TIME_WEEK = 7 * TIME_DAY;
		public const long TIME_YEAR = 365 * (long)TIME_DAY;

		/// <summary>
		/// 默认开始时间是1970,1,1
		/// </summary>
		static public DateTime m_defDate = new DateTime(1970, 1, 1);

		/// <summary>
		/// 当前系统时间 本地时间
		/// </summary>
		static public DateTime nowDate{
			get{
				return DateTime.Now;
			}
		}

		/// <summary>
		/// 当前系统时间 本地时间 - utc
		/// </summary>
		static public DateTime nowDateUtc{
			get{
				return DateTime.UtcNow;
			}
		}

		/// <summary>
		/// 当前系统时间 (单位:100纳秒 = 0.1毫秒)
		/// </summary>
        public static long nowUtcTicks
        {
            get
            {
				return nowDateUtc.Ticks;
            }
        }

		/// <summary>
		/// 当前系统时间 (单位:100纳秒 = 0.1毫秒)
		/// </summary>
		public static long nowUtcFileTime
		{
			get
			{
				return nowDateUtc.Ticks;
			}
		}

		/// <summary>
		/// 当前系统时间 (单位:毫秒)
		/// </summary>
		public static long nowMSUtcTicks
        {
            get
            {
                return nowUtcTicks / 10000;
            }
        }

		/// <summary>
		/// 时间格式化
		/// </summary>
		public static string Format(DateTime d, string fmt)
		{
			// return d.ToString(fmt);
			string str_fmt = "{0:" + fmt + "}";
			return string.Format(str_fmt, d);
		}

		public static string FormatByMs(long ms, string fmt = fmt_yyyy_MM_dd_HH_mm_ss)
		{
			DateTime d = m_defDate.AddMilliseconds (ms);
			return Format(d, fmt);
		}

        static public DateTime ParseToUtc(string dateStr,string pattern)
        {
			DateTime ret = nowDateUtc;
            if (StrEx.getLens(dateStr) == 0)
            {
				return ret;
            }
            if (StrEx.getLens(pattern) == 0)
            {
                pattern = fmt_yyyy_MM_dd;
            }

            dateStr = dateStr.Replace("\"", "");
            dateStr = dateStr.Replace("\\\\", "");

			ret = DateTime.ParseExact(dateStr, pattern, CultureInfo.InvariantCulture);
			ret = ret.ToUniversalTime ();

			return ret;
            // IFormatProvider culture = new CultureInfo("zh-CN", true);
            // string[] expectedFormats = new string[] { pattern };
            // return DateTime.ParseExact(dateStr, expectedFormats, culture, DateTimeStyles.AllowInnerWhite);
        }

        public static long ToDiffMS(DateTime src)
        {
            DateTime d2 = src.ToUniversalTime(); // 转为utc格式
			TimeSpan ts = new TimeSpan(d2.Ticks - m_defDate.Ticks);
            return (long)ts.TotalMilliseconds;
        }

		/// <summary>
		/// 本地时间  +  差值时间 = 时间long值(单位毫秒)
		/// diffMS = 差值时间
		/// isSecond = 是否收缩到秒(往上收)
		/// </summary>
        public static long ToTimeVal(long diffMS = 0, bool isSecond = false)
        {
			long time = diffMS + ToDiffMS(nowDateUtc);
            if (isSecond)
            {
                double tmT = time / (double)TIME_SECOND;
                tmT = System.Math.Ceiling(tmT);
                time = (long)tmT * TIME_SECOND;
            }
            return time;
        }

		/// <summary>
		/// 判断两时间是否相同
		/// </summary>
		static public bool IsSame(DateTime dt1, DateTime dt2)
		{
			int v = dt1.CompareTo(dt2);
			bool flag = v == 0;
			return flag;
		}

		/// <summary>
		/// 判断 dt1 是否在 dt2 之前
		/// </summary>
		static public bool IsBefore(DateTime dt1, DateTime dt2)
		{
			int v = dt1.CompareTo(dt2);
			bool flag = v <= -1;
			return flag;
		}

		/// <summary>
		/// 判断 dt1 是否在 dt2 之后
		/// </summary>
		static public bool IsAfter(DateTime dt1, DateTime dt2)
		{
			int v = dt1.CompareTo(dt2);
			bool flag = v >= 1;
			return flag;
		}

		/// <summary>
		/// 判断 dt1 是否 不在 dt2 之前
		/// </summary>
		static public bool IsNotBefore(DateTime dt1, DateTime dt2)
		{
			int v = dt1.CompareTo(dt2);
			bool flag = v >= 0;
			return flag;
		}

		/// <summary>
		/// 判断 dt1 是否 不在 dt2 之后
		/// </summary>
		static public bool IsNotAfter(DateTime dt1, DateTime dt2)
		{
			int v = dt1.CompareTo(dt2);
			bool flag = v <= 0;
			return flag;
		}

		/// <summary>
		/// 服务器 与 本地时间差(与Tick时间时间是1970,1,1)
		/// </summary>
		public static long diffMSTimeWithServer = 0;

		public static void InitDiffServerTimeMS(long serverTimeMS){
			diffMSTimeWithServer = serverTimeMS - ToDiffMS(nowDateUtc);
		}

		public static void InitDiffServerTime(long serverTimeSecond){
			InitDiffServerTimeMS (serverTimeSecond * TIME_SECOND);
		}
		
		// yyyy-MM-dd HH:mm:ss
		public static void InitDiffServerTime(string y_m_d_h_m_s){
			DateTime svDate = ParseToUtc(y_m_d_h_m_s,fmt_yyyy_MM_dd_HH_mm_ss);
			TimeSpan ts = svDate - nowDateUtc;
			diffMSTimeWithServer = (int) ts.TotalMilliseconds;
		}

        public static long nowMSServerTime
        {
            get
            {
				return ToTimeVal (diffMSTimeWithServer);
            }
        }

		public static DateTime nowDateUtcServer{
			get{
				return nowDateUtc.AddMilliseconds(diffMSTimeWithServer);
			}
		}

		public static DateTime nextDateUtcServer{
			get{
				return nowDateUtcServer.AddDays (1);
			}
		}

		public static string Format(string fmt)
		{
			return Format(nowDateUtcServer, fmt);
		}

		public static string nowString
		{
			get{
				return Format (fmt_yyyy_MM_dd_HH_mm_ss);
			}
		}

		static public string nowStrYyyyMMdd
		{
			get{
				return Format (fmt_yyyyMMdd);
			}
		}

		static public string nextStrYyyyMMdd
		{
			get{
				return Format (nextDateUtcServer, fmt_yyyyMMdd);
			}
		}

		static public string nowStrYyyyMMddHHmm
		{
			get{
				return Format (fmt_yyyyMMddHHmm);
			}
		}

		static public int curDay
		{
			get{
				string dd = Format ("dd");
				// return NumEx.stringToInt(dd);
				return int.Parse (dd);
			}
		}

		static string strFmtTimeEN3 = "{0:00}:{1:00}:{2:00}";
		static string strFmtTimeEN2 = "{0:00}:{1:00}";
		static string strFmtTimeCN3 = "{0:00}时{1:00}分{2:00}秒";
		static string strFmtTimeCN2 = "{0:00}分{1:00}秒";

        // [0]=天，[1]=时，[2]=分，[3]=秒，[4]=毫秒
        static public int[] ToTimeArray(long diffMS)
        {
            long tmpMs = diffMS;

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

		/// <summary>
		/// 时间差转为字符串
		/// 默认hh:mm:ss，或者只保留2个[hh:mm],[mm:ss]
		/// </summary>
		public static string ToStrDiffTime(long ms,bool isShowAll,bool isCN = false){
			int[] arrs = ToTimeArray(ms);
			long val1 = arrs [1] + arrs[0] * TIME_DAY;
			string _fmt = isCN ? strFmtTimeCN3 : strFmtTimeEN3;

			if (isShowAll) {
				return string.Format (_fmt, val1, arrs [2], arrs [3]);
			}

			long val2 = 0;
			if (val1 > 0) {
				val2 = arrs [2];
			} else {
				val1 = arrs [2];
				val2 = arrs [3];
			}

			_fmt = isCN ? strFmtTimeCN2 : strFmtTimeEN2;
			return string.Format (_fmt, val1, val2);
		}

        public static string ToHHMMSS(long ms)
        {
			return ToStrDiffTime (ms, true);
        }

        public static string ToStrEn(long ms)
        {
			return ToStrDiffTime (ms, false);
        }

        public static string ToStrCn(long ms)
        {
			return ToStrDiffTime (ms, false,true);
        }
           
        static public bool IsBeforeNow4yyMMddHHmm(String yyyyMMddHHmm)
        {
            if (string.IsNullOrEmpty(yyyyMMddHHmm))
                return false;
			int v = nowStrYyyyMMddHHmm.CompareTo(yyyyMMddHHmm);
            bool flag = v > -1;
            return flag;
        }

        static public bool IsBeforeNow(DateTime dt1)
        {
			return IsBefore(dt1, nowDateUtcServer);
        }

        static public bool IsBeforeNow(string dateStr,string pattern)
        {
            return IsBefore(ParseToUtc(dateStr, pattern), nowDateUtcServer);
        }

        static public bool IsAfterNow(DateTime dt1)
        {
            return IsAfter(dt1, nowDateUtcServer);
        }

        static public bool IsAfterNow(string dateStr, string pattern)
        {
            return IsAfter(ParseToUtc(dateStr, pattern), nowDateUtcServer);
        }

        static public bool IsSameNow(DateTime dt1)
        {
            return IsSame(dt1, nowDateUtcServer);
        }

        static public bool IsSameNow(string dateStr, string pattern)
        {
            return IsSame(ParseToUtc(dateStr, pattern), nowDateUtcServer);
        }

        static public bool IsNotBeforeNow(DateTime dt1)
        {
            return IsNotBefore(dt1, nowDateUtcServer);
        }

        static public bool IsNotBeforeNow(string dateStr, string pattern)
        {
            return IsNotBefore(ParseToUtc(dateStr, pattern), nowDateUtcServer);
        }

        static public bool IsNotAfterNow(DateTime dt1)
        {
            return IsNotAfter(dt1, nowDateUtcServer);
        }

        static public bool IsNotAfterNow(string dateStr, string pattern)
        {
            return IsNotAfter(ParseToUtc(dateStr, pattern), nowDateUtcServer);
        }

        static public long ToSvTimeByY_MDHMS(string yyMMddHHmmss)
        {
            try
            {
                yyMMddHHmmss = yyMMddHHmmss.Replace("\\\\", "");
                // DateTime dt = DateTime.Parse(yyMMddHHmmss);
				DateTime dt = ParseToUtc(yyMMddHHmmss,fmt_yyyy_MM_dd_HH_mm_ss);
                long jl = ToDiffMS(dt);
                return jl + diffMSTimeWithServer;
            }
            catch (Exception)
            {

                return 0;
            }
        }

		static public long ToSvTimeByHMS(string hms)
		{
			hms = hms.Replace("\\\\", "");
			string yyMMddHHmmss = Format(fmt_yyyy_MM_dd) + " " + hms;
			return ToSvTimeByY_MDHMS(yyMMddHHmmss);
		}
    }
}