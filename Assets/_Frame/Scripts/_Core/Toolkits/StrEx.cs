using System.IO;

/// <summary>
/// 类名 : 字符串操作工具
/// 作者 : Canyon
/// 日期 : 2016-12-23 15:20:00
/// 功能 : 
/// </summary>
namespace Toolkits
{
	public static class StrEx
	{
		// 转换路径文件
		static public string replaceSeparator(string src){
			if(string.IsNullOrEmpty(src)){
				return "";
			}
			return src.Replace("\\","/");
		}

		static public string replace2Underline(string src){
			if(string.IsNullOrEmpty(src)){
				return "";
			}
			src = replaceSeparator (src);
			src = src.Replace ("/", "_");
			src = src.Replace (".", "_");
			return src;
		}

        static public bool isBefore(string dateStr1, string dateStr2)
        {
            if (string.IsNullOrEmpty(dateStr1) || string.IsNullOrEmpty(dateStr2))
                return false;
            int v = dateStr1.CompareTo(dateStr2);
            bool flag = v >= -1;
            return flag;
        }

        static public bool isAfter(string dateStr1, string dateStr2)
        {
            if (string.IsNullOrEmpty(dateStr1) || string.IsNullOrEmpty(dateStr2))
                return false;
            int v = dateStr1.CompareTo(dateStr2);
            bool flag = v >= 1;
            return flag;
        }

        static public bool isSame(string dateStr1, string dateStr2)
        {
            if (string.IsNullOrEmpty(dateStr1) || string.IsNullOrEmpty(dateStr2))
                return false;
            int v = dateStr1.CompareTo(dateStr2);
            bool flag = v == 0;
            return flag;
        }

        static public bool isNotAfter(string dateStr1, string dateStr2)
        {
            if (string.IsNullOrEmpty(dateStr1) || string.IsNullOrEmpty(dateStr2))
                return false;
            int v = dateStr1.CompareTo(dateStr2);
            bool flag = v <= 0;
            return flag;
        }


        static public bool isNotBefore(string dateStr1, string dateStr2)
        {
            if (string.IsNullOrEmpty(dateStr1) || string.IsNullOrEmpty(dateStr2))
                return false;
            int v = dateStr1.CompareTo(dateStr2);
            bool flag = v >= 0;
            return flag;
        }

        static public bool isStr(object objPars)
        {
            if (objPars == null)
                return false;
            return objPars.GetType() == typeof(string);
        }

        static public int getLens(string str)
        {
            if (string.IsNullOrEmpty(str))
                return 0;
            return str.Length;
        }

        static public int getLens4Trim(string str)
        {
            if (string.IsNullOrEmpty(str))
                return 0;
            str = str.Trim();
            return str.Length;
        }

        static public string trimStr(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            return str.Trim();
        }

        static public bool isNullEmpty(string str)
        {
            int lens = getLens4Trim(str);
            return lens <= 0;
        }
    }
}
