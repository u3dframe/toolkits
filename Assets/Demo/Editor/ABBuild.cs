using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using Core.Kernel;

public class ABBuild : Editor {
	[MenuItem("Window/BuildABWindows")]
	static void BuildWindows(){
		string path = ReadWriteSupport.m_windowsPath;
		if (!Directory.Exists (path)) {
			Directory.CreateDirectory (path);
		}

		BuildPipeline.BuildAssetBundles (path, 0, EditorUserBuildSettings.activeBuildTarget);
		// AssetBundleBuild[] bulid = new AssetBundleBuild[5];
		// Core.AssetManager.Test();
	}
}
