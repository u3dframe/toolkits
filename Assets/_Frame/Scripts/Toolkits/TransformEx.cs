using UnityEngine;
using System.Collections;

/// <summary>
/// 类名 : 位置Transform 工具
/// 作者 : Canyon
/// 日期 : 2016-12-23 15:20
/// 功能 : 用于封装Tranform相关的处理
/// </summary>
public static class TransformEx{
	
	static public void parentTrsf(GameObject gobj,GameObject gobjParent,bool isLocal = true){
		Transform trsf = gobj.transform;
		Transform trsfParent = null;
		if (gobjParent != null) {
			trsfParent = gobjParent.transform;
		}
		parentTrsf (trsf, trsfParent, isLocal);
	}

	static public void parentTrsf(Transform trsf,Transform trsfParent,bool isLocal = true){
		trsf.parent = trsfParent;
		if (isLocal) {
			trsf.localPosition = Vector3.zero;
			trsf.localEulerAngles = Vector3.zero;
			trsf.localScale = Vector3.one;
		}
	}
}
