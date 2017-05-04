using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 类名 : 对象池管理脚本
/// 作者 : Canyon
/// 日期 : 2017-03-20 10:37
/// 功能 : 最好是预先加载处理，否则请在创建对象池后的下1~2帧率后在取得对象
/// </summary>
public class MgrPools : MonoBehaviour {
	private Transform m_PoolRootObject = null;
	private Dictionary<string, GobjPool> m_GobjPools = new Dictionary<string, GobjPool>();

	Transform PoolRootObject {
		get {
			if (m_PoolRootObject == null) {
				var objectPool = new GameObject("ObjectPool");
				objectPool.transform.SetParent(transform,false);
				objectPool.transform.localScale = Vector3.one;
				m_PoolRootObject = objectPool.transform;
			}
			return m_PoolRootObject;
		}
	}

	// 创建对象池
	private GobjPool CreatePool(string resModelName) {
		return CreatePool (resModelName,resModelName);
	}

	// 创建对象池
	private GobjPool CreatePool(string poolName, string prefab) {
		return CreatePool (poolName, 1, 30, prefab);
	}

	// 创建对象池
	private GobjPool CreatePool(string poolName, int initSize, int maxSize, string prefab) {
		var pool = new GobjPool(poolName, prefab, initSize, maxSize, PoolRootObject);
		m_GobjPools[poolName] = pool;
		StartCoroutine (pool.LoadModel ());
		return pool;
	}

	// 创建对象池
	public GobjPool CreatePool(string poolName, GameObject prefab) {
		return CreatePool (poolName, 1, 30, prefab);
	}

	// 创建对象池
	public GobjPool CreatePool(string poolName, int initSize, int maxSize, GameObject prefab) {
		var pool = new GobjPool(poolName, prefab, initSize, maxSize, PoolRootObject);
		m_GobjPools[poolName] = pool;
		return pool;
	}

	// 取得对象池
	public GobjPool GetPool(string poolName) {
		if (m_GobjPools.ContainsKey(poolName)) {
			return m_GobjPools[poolName];
		}
		return null;
	}

	// 取得或者创建对象池
	public GobjPool GetOrNewPool(string resModelName){
		if (string.IsNullOrEmpty (resModelName))
			return null;
		
		GobjPool ret = GetPool (resModelName);
		if (ret == null) {
			ret = CreatePool (resModelName);
		}
		return ret;
	}

	// 取得对象
	public GameObject Get(string poolName) {
		GameObject result = null;
		if (m_GobjPools.ContainsKey(poolName)) {
			GobjPool pool = m_GobjPools[poolName];
			result = pool.NextAvailableObject();
			if (result == null) {
				Debug.LogWarning("No object available in pool. Consider setting fixedSize to false.: " + poolName);
			}
		} else {
			Debug.LogError("Invalid pool name specified: " + poolName);
		}
		return result;
	}

	// 返回对象
	public void Release(string poolName, GameObject go) {
		if (m_GobjPools.ContainsKey(poolName)) {
			GobjPool pool = m_GobjPools[poolName];
			pool.ReturnObjectToPool(poolName, go);
		} else {
			Debug.LogWarning("No pool available with name: " + poolName);
		}
	}

	// 预加载处理
	string[] preArrs = {
        "drop_box",
		"drop_gold",
//		"drop_pickup",
		"drop_quality_blue",
		"drop_quality_green",
		"drop_quality_purple",
	};

	void Start(){
		StartCoroutine (PreInit ());
	}

	IEnumerator PreInit(){

		yield return new WaitForFixedUpdate();

		int lens = preArrs.Length;
		for (int i = 0; i < lens; i++) {
			PreLoad (preArrs[i]);
			yield return 0;
		}
	}

	void PreLoad(string modelRes){
		if (!string.IsNullOrEmpty (modelRes)) {
			GetOrNewPool (modelRes);
		}
	}
}
