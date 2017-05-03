using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 类名 : 界面常用函数攻击
/// 作者 : Canyon
/// 日期 : 2017-05-03 10:26
/// 功能 : 
/// </summary>
public static class UITools {
	/// <summary>
	/// 设置列表
	/// </summary>
	/// <param name="wrap">Wrap.</param>
	/// <param name="child">Child.</param>
	/// <param name="list">List.</param>
	/// <param name="initCall">Init call.</param>
	static public void reListForChild<T,U>(Transform wrap,GameObject child,IList list,System.Action<T,U> initCall) where T : Component
	{
		int lens = list == null ? 0 : list.Count;
		int childCount = wrap.childCount;
		Transform trsf = null;
		if (lens > childCount) {
			for (int i = 0; i < lens - childCount; i++) {
				trsf = (GameObject.Instantiate<GameObject>(child)).transform;
				trsf.SetParent (wrap,false);

				// trsf.localPosition = Vector3.zero;
				// trsf.localRotation = Quaternion.identity;
				// trsf.localScale = Vector3.one;
			}
		}

		T tval;
		U uval;
		for (int i = 0; i < lens; i++) {
			trsf = wrap.GetChild (i);
			trsf.gameObject.SetActive (true);
			if (initCall != null) {
				if (typeof(T) == typeof(Transform)) {
					tval = trsf as T;
				} else {
					tval = trsf.GetComponent<T> ();
					if (tval == null) {
						tval = trsf.gameObject.AddComponent<T> ();
					}
				}

				if (typeof(U) == typeof(System.Int32)) {
					uval = (U)((object)i);
				} else {
					uval = (U)list[i];
				}

				initCall (tval, uval);
			}
		}

		childCount = wrap.childCount;
		for (int i = lens; i < childCount; i++) {
			wrap.GetChild (i).gameObject.SetActive(false);
		}
	}

	/// <summary>
	/// 设置列表
	/// </summary>
	/// <param name="wrap">Wrap.</param>
	/// <param name="child">Child.</param>
	/// <param name="list">List.</param>
	/// <param name="initCall">Init call.</param>
	static public void reListForChild<T>(Transform wrap,GameObject child,IList list,System.Action<T,int> initCall) where T : Component
	{
		reListForChild<T,int>(wrap, child, list, initCall);
	}

	/// <summary>
	/// 设置列表
	/// </summary>
	/// <param name="wrap">Wrap.</param>
	/// <param name="child">Child.</param>
	/// <param name="list">List.</param>
	/// <param name="initCall">Init call.</param>
	static public void reListForChild(Transform wrap,GameObject child,IList list,System.Action<Transform,int> initCall){
		reListForChild<Transform> (wrap, child, list, initCall);
	}

	/// <summary>
	/// 设置列表
	/// </summary>
	/// <param name="wrap">Wrap.</param>
	/// <param name="child">Child.</param>
	/// <param name="arrs">Arrs.</param>
	/// <param name="initCall">Init call.</param>
	static public void reListForChild(Transform wrap,GameObject child,object[] arrs,System.Action<Transform,int> initCall){
		reListForChild<Transform,int> (wrap, child, arrs, initCall);
	}

	/// <summary>
	/// 设置列表
	/// </summary>
	/// <param name="wrap">Wrap.</param>
	/// <param name="child">Child.</param>
	/// <param name="arrs">Arrs.</param>
	/// <param name="initCall">Init call.</param>
	static public void reListForChild<T,U>(Transform wrap,GameObject child,object[] arrs,System.Action<T,U> initCall) where T : Component{
		ArrayList list = new ArrayList ();
		if (arrs != null && arrs.Length > 0) {
			list.AddRange (arrs);
		}
		reListForChild<T,U>(wrap, child, list, initCall);
	}

	/// <summary>
	/// 销毁子对象
	/// </summary>
	/// <param name="wrap">Wrap.</param>
	static public void DestroyChilds(Transform wrap){
		GameObject gobj = null;
		int lens = wrap.childCount;
		for (int i = 0; i < lens; i++) {
			gobj = wrap.GetChild (i).gameObject;
			GameObject.Destroy (gobj);
		}
		wrap.DetachChildren ();
	}

	/// <summary>
	/// 字符串转为double
	/// </summary>
	/// <returns>The double.</returns>
	/// <param name="val">Value.</param>
	static public double Str2Double(string val){
		double ret = 0;
		double.TryParse (val, out ret);
		return ret;
	}

	/// <summary>
	/// 字符串转为 int
	/// </summary>
	/// <returns>The int.</returns>
	/// <param name="val">Value.</param>
	static public int Str2Int(string val){
		int ret = 0;
		ret = (int)Str2Double (val);
		return ret;
	}
}
