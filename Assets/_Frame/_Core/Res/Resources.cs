using UnityEngine;
using System.Collections;

namespace Core
{
	/// <summary>
	/// 类名 : 资源加载
	/// 作者 : Canyon
	/// 日期 : 2017-02-10 11:20
	/// 功能 : 加载Resource文件下面的资源
	/// </summary>
	public static class Resources
	{
		/// <summary>
		/// path 是Resources文件下面的相对路径
		/// </summary>
		/// <param name="path">
		/// 例如 音效资源(A.mp3)在Resources文件夹下面的Audio里面
		/// 相对工程路径:Assets\ ...(不清楚父节点) \ Resouces\Audio\A.mp3
		/// 此时Path = Audio\A(不需要后缀)
		/// </param>
		static public UnityEngine.Object Load(string path){
			return UnityEngine.Resources.Load (path);
		}
	}
}