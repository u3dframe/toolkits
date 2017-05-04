using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 类名 : Unity3d Gameobje 对象池
/// 作者 : Canyon
/// 日期 : 2017-03-17 09:37
/// 功能 : 
/// </summary>
internal class GameObjectPool
{
	// 最大数量
	private int maxSize;

	// 初始数量
	private int poolSize;

	// 池名
	private string poolName;

	// 根节点
	private Transform poolRoot;

	// 对象模型
	private GameObject poolObjectPrefab;

	// 数据记录
	private Stack<GameObject> availableObjStack = new Stack<GameObject> ();

	// 借出数量
	int borrowNum = 0;

	// 是否有fab对象
	public bool isHasPrefab = false;

	// prefab 名
	string fabName = "";

	public GameObjectPool (string poolName, GameObject poolObjectPrefab, int initCount, int maxSize, Transform pool)
	{
		this.poolName = poolName;
		this.poolSize = initCount;
		this.maxSize = maxSize;
		this.poolRoot = pool;
		this.poolObjectPrefab = poolObjectPrefab;
		isHasPrefab = poolObjectPrefab != null;

		//populate the pool
		InitPool();
	}

	// 初始化对象
	void InitPool(){
		if (isHasPrefab) {
			fabName = poolObjectPrefab.name;
		}
		if (isHasPrefab && availableObjStack.Count < poolSize) {
			int index = availableObjStack.Count;
			for (; index < poolSize; index++) {
				AddObjectToPool (NewObjectInstance ());
			}
		}
	}

	// 记录
	private void AddObjectToPool (GameObject go)
	{
		//add to pool
		lock (availableObjStack) {
			go.SetActive (false);
			if (availableObjStack.Count < this.maxSize) {
				availableObjStack.Push (go);
				go.transform.SetParent (poolRoot, false);

				if (borrowNum > 0)
					borrowNum--;
			} else {
				GameObject.Destroy (go);
			}
		}
	}

	// 创建
	private GameObject NewObjectInstance ()
	{
		return GameObject.Instantiate (poolObjectPrefab) as GameObject;
	}

	// 取得一个对象
	public GameObject NextAvailableObject ()
	{
		lock (availableObjStack) {
			GameObject go = null;
			poolSize = availableObjStack.Count;
			if (poolSize > 0) {
				go = availableObjStack.Pop ();
				borrowNum++;
			} else {
				go = NewObjectInstance ();
				borrowNum++;
			}

			if (go != null) {
				if (!string.IsNullOrEmpty (fabName)) {
					go.name = fabName;
				}
				go.SetActive (true);
			}
			return go;
		}
	}
		
	// 还原
	public void ReturnObjectToPool (string pool, GameObject po)
	{
		if (poolName.Equals (pool)) {
			AddObjectToPool (po);
		} else {
			Debug.LogError (string.Format ("Trying to add object to incorrect pool {0} ", poolName));
		}
	}

	// 设置prefab
	public void SetFab(GameObject gobjFab){
		this.poolObjectPrefab = gobjFab;
		isHasPrefab = poolObjectPrefab != null;
		InitPool();
	}

	// 对象销毁回调处理
	public void Destroy(string pool,GameObject go){
		lock (availableObjStack) {
			if (poolName.Equals (pool) && go != null) {
				bool isHas = availableObjStack.Contains (go);
				if (isHas) {
					List<GameObject> list = new List<GameObject> (availableObjStack.ToArray ());
					list.Remove (go);
					availableObjStack.Clear ();
					int lens = list.Count;
					for (int i = 0; i < lens; i++) {
						availableObjStack.Push (list [i]);
					}
				}

				borrowNum--;
			}
		}
	}

	// 池名
	public string GetPoolName(){
		return this.poolName;
	}

	// 设置最大数量
	public void SetMaxSize(int max){
		this.maxSize = max <= 0 ? 20 : max;
	}

	public override string ToString ()
	{
		return string.Format (
			"poolName = {0},maxSize={1},poolSize = {2},fabName={3},borrowNum={4},availableObjStackSize = {5}",
			this.poolName,
			this.maxSize,
			this.poolSize,
			this.fabName,
			this.borrowNum,
			this.availableObjStack.Count
		);
	}
}

/// <summary>
/// 类名 : 对象池扩展
/// 作者 : Canyon
/// 日期 : 2017-03-17 16:37
/// 功能 : 
/// </summary>
public class GobjPool {

	// 对象池对象
	private GameObjectPool poolGobj;

	// 资源名
	private string refPrefab;

	public GobjPool (string poolName, string refPrefab,Transform pool) : this(poolName,refPrefab,1,20,pool){
	}

	public GobjPool (string poolName, GameObject gobjFab,Transform pool) : this(poolName,gobjFab,1,20,pool){
	}

	public GobjPool (string poolName, string refPrefab, int initCount, int maxSize, Transform pool) : this (poolName, (GameObject)null, initCount, maxSize, pool){
		this.refPrefab = refPrefab;
	}

	public GobjPool (string poolName, GameObject gobjFab, int initCount, int maxSize, Transform pool) {
		poolGobj = new GameObjectPool (poolName, gobjFab, initCount, maxSize, pool);
	}

	// 加载资源
	public IEnumerator LoadModel(){
		if (string.IsNullOrEmpty (refPrefab)) {
			yield break;
		}
//		var loadOp = AssetBundleManager.Instance ().LoadAssetAsync (refPrefab, typeof(GameObject));
//		yield return loadOp;
//		if (loadOp == null) {
//			Debuger.LogError ("资源名:[" + refPrefab + "],没有生成AssetBundle文件");
//			yield break;
//		}
//		GameObject gobjTmp = loadOp.GetAsset<GameObject> ();
//		poolGobj.SetFab (gobjTmp);
//		loadOp = null;
//		gobjTmp = null;
	}

	// 取得对象
	public GameObject NextAvailableObject (){
		GameObject gobj = this.poolGobj.NextAvailableObject ();
		if (gobj != null) {
			GobjLifeListener listener = Get<GobjLifeListener> (gobj);
			listener.callDestroy = Destroy;
			listener.poolName = this.poolGobj.GetPoolName();
		}
		return gobj;
	}

	// 还原
	public void ReturnObjectToPool (string pool, GameObject po){
		this.poolGobj.ReturnObjectToPool (pool, po);
	}

	// 是否拥有模型
	public bool isHasPrefab{
		get{
			return this.poolGobj != null && this.poolGobj.isHasPrefab;
		}
	}

	// 销毁回调
	void Destroy(GobjLifeListener one){
		if (this.poolGobj != null) {
			this.poolGobj.Destroy (one.poolName, one.gameObject);
		}
	}

	// 设置最大存活数量
	public void SetMaxSize(int maxSize){
		if (this.poolGobj != null) {
			this.poolGobj.SetMaxSize (maxSize);
		}
	}

	public override string ToString ()
	{
		return string.Format ("refPrefab = {0}, poolInfo =[{1}]", this.refPrefab,this.poolGobj);
	}

	// 添加组件静态方法
	static public T Get<T>(GameObject gobj) where T : Component{
		T ret = gobj.GetComponent<T> ();
		if (ret == null) {
			ret = gobj.AddComponent<T> ();
		}
		return ret;
	}

	static public T Get<T>(Transform trsf) where T : Component{
		return Get<T> (trsf.gameObject);
	}
}