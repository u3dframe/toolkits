using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Core.Kernel;

/// <summary>
/// 类名 : 编译协议 protobuf --> lua 
/// 作者 : Canyon
/// 日期 : 2017-04-21 15:40
/// 功能 : 将protobuf 文件转为 lua 文件
/// </summary>
public class EL_Protobuf  {
	
	UnityEngine.Object m_objFolderOrg,m_objFolderTo;

	public void DrawView(){
		EG_GUIHelper.FEG_BeginVArea ();

		EG_GUIHelper.FEG_HeadTitMid ("Build Protobuf-lua-gen File",Color.cyan);


		m_objFolderOrg = EditorGUILayout.ObjectField ("Protobuf 源文件夹:", m_objFolderOrg, typeof(UnityEngine.Object), false);
		EG_GUIHelper.FG_Space(10);

		if (GUILayout.Button("DoBuild"))
		{
			DoMake();
		}
		EG_GUIHelper.FEG_EndV ();

		EG_GUIHelper.FG_Space(10);
	}

	void DoMake(){
		if (m_objFolderOrg == null) {
			EditorUtility.DisplayDialog ("Tips", "请选择来源文件夹!!!", "Okey");
			return;
		}

		System.Type typeFolder = typeof(UnityEditor.DefaultAsset);

		System.Type typeOrg = m_objFolderOrg.GetType ();

		if (typeOrg != typeFolder) {
			EditorUtility.DisplayDialog ("Tips", "来源文件不是文件夹,请选择来源文件夹!!!", "Okey");
			return;
		}

		string pathOrg = AssetDatabase.GetAssetPath (m_objFolderOrg);

		BuildProtobufFile (pathOrg);
	}

	static void BuildProtobufFile(string dir) {

		EL_Path.Init(dir);

		string protoc = "d:/protobuf-2.4.1/src/protoc.exe";
		string protoc_gen_dir = "\"d:/protoc-gen-lua/plugin/protoc-gen-lua.bat\"";

		foreach (string f in EL_Path.files) {
			// string name = Path.GetFileName(f);
			string ext = Path.GetExtension(f);
			if (!ext.Equals(".proto")) continue;

			string rename = f.Replace (dir, "");

			if (rename.IndexOf ("/") == 0) {
				rename = rename.Substring (1);
			}


			ProcessStartInfo info = new ProcessStartInfo();
			info.FileName = protoc;
			info.Arguments = " --lua_out=./ --plugin=protoc-gen-lua=" + protoc_gen_dir + " " + rename;
			info.WindowStyle = ProcessWindowStyle.Hidden;
			info.UseShellExecute = true;
			info.WorkingDirectory = dir;
			info.ErrorDialog = true;

			UnityEngine.Debug.Log(info.FileName + " " + info.Arguments);

			Process pro = Process.Start(info);
			pro.WaitForExit();
		}
		AssetDatabase.Refresh();
	}
}
