using UnityEngine;
using System.Collections;
using UnityEditor;

public class ABBuild : Editor {
	[MenuItem("Window/BuildAB")]
	static void Build(){		
		 BuildPipeline.BuildAssetBundles (Application.dataPath + "/Build", 0, EditorUserBuildSettings.activeBuildTarget);
		// AssetBundleBuild[] bulid = new AssetBundleBuild[5];
		Core.AssetManager.Test();
	}
}
