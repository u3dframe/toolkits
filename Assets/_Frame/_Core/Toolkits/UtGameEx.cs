using UnityEngine;
using System.Collections;

namespace Core.Kernel
{
	/// <summary>
	/// 类名 : Ut-Unity 游戏扩展方法
	/// 作者 : Canyon
	/// 日期 : 2016-12-23 15:20
	/// 功能 : 用于处理Tranform，GameObject,Component等对象的方法
	/// </summary>
	public class UtGameEx : ComponentEx
	{

		/// <summary>
		/// 设置父节点
		/// </summary>
		static public void parentTrsf (GameObject gobj, GameObject gobjParent, bool isLocal = true)
		{
			Transform trsf = gobj.transform;
			Transform trsfParent = null;
			if (gobjParent != null) {
				trsfParent = gobjParent.transform;
			}
			parentTrsf (trsf, trsfParent, isLocal);
		}

		/// <summary>
		/// 设置父节点
		/// </summary>
		static public void parentTrsf (Transform trsf, Transform trsfParent, bool isLocal = true)
		{
			bool isWorldPosStays = !isLocal;
			trsf.SetParent (trsfParent, isWorldPosStays);
//		if (isLocal) {
//			trsf.localPosition = Vector3.zero;
//			trsf.localEulerAngles = Vector3.zero;
//			trsf.localScale = Vector3.one;
//		}
		}
	}
}