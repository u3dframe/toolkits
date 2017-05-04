using UnityEngine;
using System.Collections;

/// <summary>
/// 类名 : GameObject对象 生命周期 监听
/// 作者 : Canyon
/// 日期 : 2017-03-21 10:37
/// 功能 : 
/// </summary>
public class GobjLifeListener : MonoBehaviour {
	[System.NonSerialized]
	public string poolName = "";

	public System.Action<GobjLifeListener> callDestroy;

	void OnDestroy(){
		// Debug.Log ("Destroy,poolName = " + poolName+",gobjname = " + gameObject.name);
		if (callDestroy != null) {
			callDestroy (this);
		}
	}
}
