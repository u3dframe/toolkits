using UnityEngine;
using System.Collections;

public class ImplViewBase : MonoBehaviour,IViewBase {
	private static bool _isFreeze4Click = false;
	public static bool isFreeze4Click {
		get {
			return _isFreeze4Click;
		}
		set { 
			_isFreeze4Click = value; 
		}
	}

	private Transform _trsfSelf;
	public Transform trsfSelf{
		get{
			if (_trsfSelf == null) {
				_trsfSelf = transform;
			}
			return _trsfSelf;
		}
		set{ 
			_trsfSelf = value;
		}
	}

	private GameObject _gobjSelf;
	public GameObject gobjSelf{
		get{
			if (_gobjSelf == null) {
				_gobjSelf = gameObject;
			}
			return _gobjSelf;
		}
		set{ 
			_gobjSelf = value;
		}
	}

	private bool _isCanPrint = false;
	public bool isCanPrint{
		get{ return _isCanPrint;}
		set{ _isCanPrint = value;}
	}

	// 是否可以冻结该对象(一般是单击事件)
	private bool _isCanFreeze = false;
	public bool isCanFreeze{
		get{ return _isCanFreeze;}
		set{ _isCanFreeze = value;}
	}

	// 显隐状态
	public bool isVisibled{ get; set;}

	// 暂停 状态
	public bool isPause{ get; set;}

	// 名字
	private string _nmGobj = "";
	public string nmGobj{
		get{ 
			if (string.IsNullOrEmpty (_nmGobj)) {
				nmGobj = gobjSelf.name;
			}
			return _nmGobj;
		}
		set{ 
			if (_nmGobj != value) {
				gobjSelf.name = name;
			}
			_nmGobj = value;
		}
	}

	#region MonoBehaviour Func
	void Awake(){
		printSelf ("ImplViewBase , Awake");
	}

	// Use this for initialization
	void Start () {
		OnReady ();
		Call4Start ();
	}
	
	// Update is called once per frame
	// void Update () {
	// }

	void OnEnable(){
		OnReady ();
		Call4Show ();
	}

	void OnDisable(){
		Call4Hide ();
	}

	void OnDestroy(){
		Call4Destroy ();
	}
	#endregion


	#region NGUI Camera Notify Func
	void OnPress(bool isDown){
		if (isCanFreeze && isFreeze4Click) {
			return;
		}
		Call4OnPress (isDown);
	}

	void OnClick(){
		if (isCanFreeze && isFreeze4Click) {
			return;
		}
		Call4OnClick ();
	}
	#endregion

	#region IViewBase implementation
	public void DoInit ()
	{
		printSelf ("ImplViewBase , DoInit");
		Call4Init ();
	}

	public void DoShow ()
	{
		printSelf ("ImplViewBase , DoShow");
		isVisibled = true;
		gobjSelf.SetActive (true);

		OnReady ();
	}

	public void DoHide ()
	{
		printSelf ("ImplViewBase , DoHide");
		isVisibled = false;
		gobjSelf.SetActive (false);
	}

	public void DoDestroy ()
	{
		printSelf ("ImplViewBase , DoDestroy");
		GameObject.Destroy (gobjSelf);
	}

	#endregion

	private bool isInit = false;
	void OnReady(){
		if (isInit) {
			return;
		}
		isInit = true;
		DoInit ();
	}

	protected virtual void Call4Init ()
	{
		printSelf ("ImplViewBase , Call4Init");
	}

	protected virtual void Call4Start ()
	{
		printSelf ("ImplViewBase , Call4Start");
	}

	protected virtual void Call4Show ()
	{
		printSelf ("ImplViewBase , Call4Show");
	}

	protected virtual void Call4Hide ()
	{
		printSelf ("ImplViewBase , Call4Hide");
		StopAllCoroutines();
		CancelInvoke();
	}

	protected virtual void Call4Destroy ()
	{
		printSelf ("ImplViewBase , Call4Destroy");
		gobjSelf = null;
		trsfSelf = null;
	}

	protected virtual void Call4OnPress(bool isDown){
		printSelf ("ImplViewBase , Call4OnPress,isDown = " + isDown);
	}

	protected virtual void Call4OnClick(){
		printSelf ("ImplViewBase , Call4OnClick,isFree = " + isFreeze4Click + ",isCan = " + isCanFreeze);
	}

	#region entity implementation

	public void DoReInit<T1, T2> (T1 t1, T2 t2)
	{
		DoClear ();
		DoInit (t1, t2);
	}

	public void DoInit<T1, T2> (T1 t1, T2 t2)
	{
	}

	public void DoReInit ()
	{
		DoClear ();
		DoInit ();
	}

	public void DoUpdate ()
	{
		OnUpdate ();
	}

	public void DoEnd ()
	{
		OnEnd ();
	}

	public void DoClear ()
	{
		OnClear ();
	}
	#endregion

	protected virtual void OnClear(){
	}

	protected virtual void OnEnd()
	{}

	protected virtual void OnUpdate(){
	}


	void SetParent(GameObject parent,bool isLocal = true){
		TransformEx.parentTrsf (gobjSelf, parent, isLocal);
	}

	void SetParent(Transform parent,bool isLocal = true){
		TransformEx.parentTrsf (trsfSelf, parent, isLocal);
	}

	public void AddTo(GameObject parent,bool isLocal = true){
		SetParent (parent, isLocal);
	}

	public void AddTo(Transform parent,bool isLocal = true){
		SetParent (parent, isLocal);
	}

	public void AddChild(GameObject child,bool isLocal = true){
		TransformEx.parentTrsf (child, gobjSelf, isLocal);
	}

	public void AddChild(Transform child,bool isLocal = true){
		TransformEx.parentTrsf (child, trsfSelf, isLocal);
	}

	public Transform Find(string val,Transform trsfParent = null){
		if (trsfParent != null) {
			return trsfParent.Find (val);
		}
		return trsfSelf.Find (val);
	}

	public GameObject FindGobj(string val,Transform trsfParent = null){
		Transform ret = Find (val,trsfParent);
		if (ret != null) {
			return ret.gameObject;
		}
		return null;
	}


	public T GetComponent<T>(GameObject gobj) where T : Component{
		return gobj.GetComponent<T> ();
	}

	public T GetComponent<T>(Transform trsf) where T : Component{
		return trsf.GetComponent<T> ();
	}

	public T AddComponent<T>(GameObject gobj) where T : Component{
		return gobj.AddComponent<T> ();
	}

	public T AddComponent<T>(Transform trsf) where T : Component{
		return AddComponent<T> (trsf.gameObject);
	}

	public T FindComponent<T> (GameObject gobj,bool isNullAdd = false) where T : Component
	{
		T t = GetComponent<T> (gobj);
		if (isNullAdd && t == null) {
			t = AddComponent<T> (gobj);
		}
		return t;
	}

	public T FindComponent<T> (Transform trsf,bool isNullAdd = false) where T : Component
	{
		T t = GetComponent<T> (trsf);
		if (isNullAdd && t == null) {
			t = AddComponent<T> (trsf);
		}
		return t;
	}

	public T FindComponent<T> (string val,Transform trsfParent,bool isNullAdd) where T : Component{
		Transform trsf = Find (val,trsfParent);
		return FindComponent<T> (trsf,isNullAdd);
	}

	public T FindComponent<T> (string val,bool isNullAdd = false) where T : Component
	{
		Transform trsf = Find (val);
		return FindComponent<T> (trsf,isNullAdd);
	}

	protected virtual void SetPos(Vector3 pos,bool isLocal = true){
		if (isLocal) {
			trsfSelf.localPosition = pos;
		} else {
			trsfSelf.position = pos;
		}
	}

	protected virtual void SetScale(float scale){
		SetScale(Vector3.one * scale);
	}

	protected virtual void SetScale(Vector3 pos){
		trsfSelf.localScale = pos;
	}

	public Vector3 GetScale(bool isLocal = true){
		if (isLocal) {
			return trsfSelf.localScale;
		} else {
			return trsfSelf.lossyScale;
		}
	}

	public void setBoxCollider(BoxCollider boxCol,Vector3 center,Vector3 size){
		boxCol.center = center;
		boxCol.size = size;
	}

	protected void printSelf(object obj){
		if (isCanPrint) {
			if (obj == null) {
				obj = "pars obj is null";
			}

			string msg = gobjSelf.name;
			msg = "<color=red>" + msg + "</color>" + "<color=green>"+ ":"  + "</color>" + "<color=blue>"+ obj.ToString () + "</color>";
			print (msg);
		}
	}
}
