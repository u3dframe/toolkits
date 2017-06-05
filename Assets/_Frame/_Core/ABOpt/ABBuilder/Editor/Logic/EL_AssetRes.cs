using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Core.Kernel;

/// <summary>
/// 类名 : 编译 资源
/// 作者 : Canyon
/// 日期 : 2017-04 -21 16:16
/// 功能 : abname = 资源文件路径截取了相对文件夹[m_rootRelative]后,并去掉了后缀名的部分 (小写)
///        abExtension  = 资源文件后缀名(小写)
/// </summary>
public class EL_AssetRes{

	//序列化对象
	SerializedObject m_Object;

	//序列化属性
	SerializedProperty m_Property;

	List<UnityEngine.Object> m_list = null;

	// 平台
	BuildTarget m_buildTarget = BuildTarget.StandaloneWindows;

	// 相对目录(要打包的资源必须在该目录下)
	string m_rootRelative = "Builds";

	// 是否为资源Asset添加bundle_name
	bool m_isAbName = true;

	// 是否为资源Asset添加ab资源后缀名
	bool m_isAbSuffix = false;

	public void DrawView(SerializedObject obj,SerializedProperty field,List<UnityEngine.Object> list){
		this.m_Object = obj;
		this.m_Property = field;
		this.m_list = list;

		if (m_Object == null) {
			return;
		}

		m_Object.Update ();

		EG_GUIHelper.FEG_BeginVArea ();

		EG_GUIHelper.FEG_HeadTitMid ("Build Resources",Color.magenta);
		EG_GUIHelper.FG_Space(10);

		m_buildTarget = (BuildTarget)EditorGUILayout.EnumPopup ("平台 : ",m_buildTarget);
		EG_GUIHelper.FG_Space(10);

		EG_GUIHelper.FEG_BeginH ();
		{
			m_rootRelative = EditorGUILayout.TextField ("相对目录 : ", m_rootRelative);

			GUIStyle style = new GUIStyle ();
			style.normal.textColor = Color.yellow;
			EditorGUILayout.LabelField ("(资源文件夹节点里必须有个[相对目录]，不然不会Build！！！)", style);
		}
		EG_GUIHelper.FEG_EndH ();
		EG_GUIHelper.FG_Space(10);

		EG_GUIHelper.FEG_BeginH();
		{
			EG_GUIHelper.FEG_BeginToggleGroup ("是否bundleName", ref m_isAbName);
			{
				Color defCol = GUI.color;
				GUI.color = Color.cyan;
				EditorGUILayout.LabelField ("不勾选:",string.Format("1.删除文件列表所生成的资源 \n2.清除该列表下所有bundleName"),EG_GUIHelper.ToOptions(0,30));
				GUI.color = defCol;

				EG_GUIHelper.FG_Space(10);

				m_isAbSuffix = EditorGUILayout.ToggleLeft("是否添加assetbundle后缀", m_isAbSuffix);
			}
			EG_GUIHelper.FEG_EndToggleGroup ();
		}
		EG_GUIHelper.FEG_EndH();
		EG_GUIHelper.FG_Space(10);


		//开始检查是否有修改
		EditorGUI.BeginChangeCheck();

		//显示属性
		//第二个参数必须为true，否则无法显示子节点即List内容
		EditorGUILayout.PropertyField(m_Property,new GUIContent("资源文件夹 : "),true);

		//结束检查是否有修改
		if (EditorGUI.EndChangeCheck())
		{
			//提交修改
			m_Object.ApplyModifiedProperties();
		}

		EG_GUIHelper.FG_Space(10);

		if (GUILayout.Button("DoBuildAssetU5"))
		{
			DoMake();
		}
		EG_GUIHelper.FEG_EndV ();

		EG_GUIHelper.FG_Space(10);
	}

	void DoMake(){
		int lens = m_Property.arraySize;
		if (lens <= 0) {
			EditorUtility.DisplayDialog ("Tips", "请选择来源文件夹!!!", "Okey");
			return;
		}

		for (int i = 0; i < lens; i++) {
			MakeOneFolder (m_list[i]);
		}

		// 清空没用的abname
		AssetDatabase.RemoveUnusedAssetBundleNames ();

		EditorReadWriteSupport.m_isEdtiorLoadAsset = true;

		string outputPath = "";
		switch (m_buildTarget) {
		case BuildTarget.Android:
			outputPath = EditorReadWriteSupport.m_androidPath;
			break;
		case BuildTarget.iOS:
			outputPath = EditorReadWriteSupport.m_iosPath;
			break;
		default:
			outputPath = EditorReadWriteSupport.m_windowsPath;
			break;
		}

		if (!m_isAbName) {
			// 递归
			bool recursive = true;
			if (Directory.Exists (outputPath)) {
				Directory.Delete (outputPath, recursive);
			}
		}

		if (!Directory.Exists (outputPath)) {
			Directory.CreateDirectory (outputPath);
		}

		BuildPipeline.BuildAssetBundles (outputPath, BuildAssetBundleOptions.None, m_buildTarget);

		// 可以在这个地方去加载manifest，读取资源，记录关系
	}

	void MakeOneFolder(UnityEngine.Object one){
		if (one == null)
			return;
		
		System.Type typeFolder = typeof(UnityEditor.DefaultAsset);

		System.Type typeOrg = one.GetType ();

		if (typeOrg != typeFolder) {
			EditorUtility.DisplayDialog ("Tips", "来源文件不是文件夹!!!", "Okey");
			return;
		}

		string pathOrg = AssetDatabase.GetAssetPath (one);

		MakeChildsInFolder (pathOrg);
	}

	void MakeChildsInFolder(string dir) {

		EL_Path.Init(dir);

		int indexRelative = 0;
		int lensRelative = m_rootRelative.Length;

		foreach (string f in EL_Path.files) {
			indexRelative = f.IndexOf (m_rootRelative);
			if (indexRelative < 0) {
				// UnityEngine.Debug.LogWarningFormat ("资源路径[{0}]中没有包含[{1}]值", f, m_rootRelative);
				continue;
			}

			// string name = Path.GetFileName(f);
			string ext = Path.GetExtension(f);

//			string guid = AssetDatabase.AssetPathToGUID (f);
//			UnityEngine.Debug.LogFormat ("guid = [{0}],name=[{1}],ext={2},filepath = [{3}]", guid,name,ext,f);

			// 资源名称是截取了相对文件夹[m_rootRelative]后,并去掉了后缀名的部分
			string abName = "";
			if (m_isAbName) {
				abName = f.Substring (indexRelative + lensRelative + 1);
				abName = abName.Split ('.') [0];
			}

			string abSuffix = "";
			if (m_isAbSuffix) {
				abSuffix = ext.Substring (1);
			}
			SetAssetBundleInfo (f, abName,abSuffix);
		}
		AssetDatabase.Refresh();
	}

	void SetAssetBundleInfo(string assetPath,string abName = "",string abSuffix = ""){
		abName = abName.Trim ();
		bool isABName = !string.IsNullOrEmpty (abName);

		AssetImporter assett = AssetImporter.GetAtPath (assetPath);
		if (isABName) {
			// 资源名
			assett.assetBundleName = abName.ToLower ();
		} else {
			assett.assetBundleName = null;
		}

		if (!isABName) {
			return;
		}

		abSuffix = abSuffix.Trim ();

		bool isABSuffix = !string.IsNullOrEmpty (abSuffix);
		if (isABSuffix) {
			// 资源后缀名
			assett.assetBundleVariant = abSuffix.ToLower ();
		} else {
			if (!string.IsNullOrEmpty (assett.assetBundleVariant)) {
				assett.assetBundleVariant = null;
			}
		}
	}
}
