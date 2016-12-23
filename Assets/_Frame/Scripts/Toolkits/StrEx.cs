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
	}
}
