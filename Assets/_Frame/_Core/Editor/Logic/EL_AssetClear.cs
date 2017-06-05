using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Core.Kernel;

/// <summary>
/// 类名 : 清除 资源
/// 作者 : Canyon
/// 日期 : 2017-06-05 12:30
/// 功能 : 
/// </summary>
public class EL_AssetClear{
	List<UnityEngine.Object> m_listOrg = null;

	List<UnityEngine.Object> m_listCheck = null;

	public void DrawView(SerializedObject obj,SerializedProperty field,List<UnityEngine.Object> list,SerializedProperty fieldClear,List<UnityEngine.Object> listCheck){
		this.m_listOrg = list;
		this.m_listCheck = listCheck;

		if (obj == null) {
			return;
		}

		obj.Update ();

		EG_GUIHelper.FEG_BeginVArea ();

		EG_GUIHelper.FEG_HeadTitMid ("Clear Not Use Assets",Color.cyan);
		EG_GUIHelper.FG_Space(10);

		//开始检查是否有修改
		EditorGUI.BeginChangeCheck();

		//显示属性
		//第二个参数必须为true，否则无法显示子节点即List内容
		EditorGUILayout.PropertyField(field,new GUIContent("资源文件夹 : "),true);
		EG_GUIHelper.FG_Space(10);

		EditorGUILayout.PropertyField(fieldClear,new GUIContent("检测清空资源文件夹 : "),true);
		EG_GUIHelper.FG_Space(10);

		//结束检查是否有修改
		if (EditorGUI.EndChangeCheck())
		{
			//提交修改
			obj.ApplyModifiedProperties();
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
		if (m_listOrg.Count <= 0) {
			EditorUtility.DisplayDialog ("Tips", "请选择来源文件夹!!!", "Okey");
			return;
		}

		if (m_listCheck.Count <= 0) {
			EditorUtility.DisplayDialog ("Tips", "请选择需要Check的来源文件夹!!!", "Okey");
			return;
		}

		EL_Path.Clear ();

		int lens = m_listOrg.Count;
		string folderPath = "";
		for (int i = 0; i < lens; i++) {
			folderPath = CheckOneFolder (m_listOrg[i]);
			if (string.IsNullOrEmpty (folderPath))
				continue;
			EL_Path.Append(folderPath);
		}

		// 清空没用的abname
		AssetDatabase.RemoveUnusedAssetBundleNames ();

	}

	/// <summary>
	/// 检测获得元素的路径
	/// </summary>
	string CheckOneFolder(UnityEngine.Object one){
		if (one == null)
			return null;

		System.Type typeFolder = typeof(UnityEditor.DefaultAsset);

		System.Type typeOrg = one.GetType ();

		if (typeOrg != typeFolder) {
			EditorUtility.DisplayDialog ("Tips", "来源文件不是文件夹!!!", "Okey");
			return null;
		}

		return AssetDatabase.GetAssetPath (one);
	}
}
