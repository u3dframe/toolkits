using UnityEditor;
using System.Collections;
using System.IO;
using UnityEngine;

/// <summary>
/// 类名 : 扩展创建自己的脚本
/// 作者 : Canyon
/// 日期 : 2017-05-15 20:21
/// 功能 : 
/// </summary>
public class ScriptTempExtend :  Editor{

	[MenuItem("Assets/Create/Kernel C# Scripts", false, 91)]
	static void CreateKernelScripts()
	{
		ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
			ScriptableObject.CreateInstance<CreateKernelAction>(),
			GetSelectedPath() + "/NewKernelScript.cs", null,
			"Assets/_Core/ABOpt/ABBuilder/Editor/ExtendTemplate/91-C# Kernel Script-NewKernelScript.cs.txt");
	}

	private static string GetSelectedPath()
	{
		//默认路径为Assets
		string selectedPath = "Assets";

		//获取选中的资源
		UnityEngine.Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.Assets);

		//遍历选中的资源以返回路径
		foreach (UnityEngine.Object obj in selection)
		{
			selectedPath = AssetDatabase.GetAssetPath(obj);
			if (!string.IsNullOrEmpty(selectedPath) && File.Exists(selectedPath))
			{
				selectedPath = Path.GetDirectoryName(selectedPath);
				break;
			}
		}

		return selectedPath;
	}
}
