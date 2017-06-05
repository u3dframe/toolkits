using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// 类名 : unity 清空工程无用的区域
/// 作者 : Canyon
/// 日期 : 2017-06-05 12:30
/// 功能 : 
/// </summary>
public class ClearWindowEditor : EditorWindow {

	static bool isOpenWindowView = false;

	static protected ClearWindowEditor vwWindow = null;

	// 窗体宽高
	static public float width = 600;
	static public float height = 335;

	[MenuItem("Tools/ClearNotUseAsset")]
	static void AddWindow()
	{
		if (isOpenWindowView || vwWindow != null)
			return;

		try
		{
			isOpenWindowView = true;
			float x = 460;
			float y = 200;
			Rect rect = new Rect(x, y, width, height);

			// 大小不能拉伸
			// vwWindow = GetWindowWithRect<EDW_Skill>(rect, true, "SkillEditor");

			// 窗口，只能单独当成一个窗口,大小可以拉伸
			//vwWindow = GetWindow<EDW_Skill>(true,"SkillEditor");

			// 这个合并到其他窗口中去,大小可以拉伸
			vwWindow = GetWindow<ClearWindowEditor>("ClearWindowEditor");

			vwWindow.position = rect;
		}
		catch (System.Exception)
		{
			OnClearSWindow();
			throw;
		}
	}

	static void OnClearSWindow()
	{
		isOpenWindowView = false;
		vwWindow = null;
	}

	void OnEnable(){
		m_Object = new SerializedObject (this);
		m_Property = m_Object.FindProperty ("_assetList");

		m_PropertyCheck = m_Object.FindProperty ("_checkList");
	}

	void OnDestroy()
	{
		OnClearSWindow();
	}

	// 在给定检视面板每秒10帧更新
	void OnInspectorUpdate()
	{
		Repaint();
	}

	//序列化对象
	SerializedObject m_Object;

	//序列化属性
	SerializedProperty m_Property;

	//序列化属性
	SerializedProperty m_PropertyCheck;

	/// <summary>
	/// 工程用到的
	/// </summary>
	[SerializeField]
	protected List<UnityEngine.Object> _assetList = new List<UnityEngine.Object>();

	/// <summary>
	/// 需要清除的文件
	/// </summary>
	public List<UnityEngine.Object> _checkList = new List<UnityEngine.Object>();

	// 编译 资源
	EL_AssetClear clearRes = new EL_AssetClear();

	void OnGUI(){
		EG_GUIHelper.FEG_BeginV ();
		{
			EG_GUIHelper.FEG_BeginH ();
			{
				GUIStyle style = EditorStyles.label;
				style.alignment = TextAnchor.MiddleCenter;
				string txtDecs = "类名 : 资源清除工具\n"
					+ "作者 : Canyon\n"
					+ "日期 : 2017-06-05 12:30\n"
					+ "描述 : 暂无\n";
				GUILayout.Label(txtDecs, style);
				style.alignment = TextAnchor.MiddleLeft;
			}
			EG_GUIHelper.FEG_EndH ();

			EG_GUIHelper.FG_Space(10);

			clearRes.DrawView(m_Object,m_Property,_assetList,m_PropertyCheck,_checkList);
		}
		EG_GUIHelper.FEG_EndV ();
	}
}
