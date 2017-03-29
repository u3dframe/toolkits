using UnityEngine;
using System.Collections;

namespace Core
{
	/// <summary>
	/// 类名 : 资源管理
	/// 作者 : Canyon
	/// 日期 : 2017-02-10 11:20
	/// 功能 : 
	/// </summary>
	public class AssetManager
	{
		static public void Test(){
			string[] ps1 = UnityEditor.AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName ("sprite", "RegB");
			string[] ps2 = UnityEditor.AssetDatabase.GetAssetPathsFromAssetBundle ("sprite");
			string[] ps3 = UnityEditor.AssetDatabase.GetAllAssetPaths ();
			Object obj =  Kernel.Resources.Load4Png ("Demo/Builds/Resources/Sprite/RegB1.png");
			Debug.Log ("1 =" + (obj == null));

			obj =  Kernel.Resources.Load4Png ("Demo/Builds/Resources/Sprite/RegB3");
			Debug.Log ("2 =" + (obj == null));

			obj =  Kernel.Resources.Load4Prefab ("Demo/Builds/Resources/Fab/Resources/GobjFab.prefab");
			Debug.Log ("3 =" + (obj == null));

			obj =  Kernel.Resources.Load4Prefab ("Demo/Builds/Resources/Fab/Resources/GobjFab");
			Debug.Log ("4 =" + (obj == null));
			Debug.Log ("===============");

			obj =  Kernel.Resources.Load4Png ("Demo/Builds/Sprite/RegB1.png");
			Debug.Log ("5 =" + (obj == null));

			obj =  Kernel.Resources.Load4Png ("Demo/Builds/Sprite/RegB3");
			Debug.Log ("6 =" + (obj == null));

			obj =  Kernel.Resources.Load4Prefab ("Demo/Builds/Fab/GobjFab.prefab");
			Debug.Log ("7 =" + (obj == null));

			obj =  Kernel.Resources.Load4Prefab ("Demo/Builds/Fab/GobjFab");
			Debug.Log ("8 =" + (obj == null));
			Debug.Log ("===============");

			string pathRoot = Core.Kernel.ReadWriteHelp.m_windowsPath;
			AssetBundle ab = LoadAssetBundle(pathRoot+Core.Kernel.ReadWriteHelp.platformWindows);
			Object[] objs = ab.LoadAllAssets ();
			AssetBundleManifest mainfest = ab.LoadAsset<AssetBundleManifest> ("AssetBundleManifest");
			// unity 5以上只有一个Manifest，就是打包得到的资源路径下面和最后一个文件夹名字一样的文件里面
			string abName = "fab";
			if (mainfest != null) {
				string[] abnames = mainfest.GetAllAssetBundles ();
				foreach (var item in abnames) {
					Debug.Log ("abname = " + item);
				}

				string[] relshops = mainfest.GetAllDependencies (abName);
				foreach (var item in relshops) {
					Debug.Log ("shipname = " + item);
				}
			}
		}

	

		static AssetBundle LoadAssetBundle(string path){
			byte[] data = System.IO.File.ReadAllBytes (path);
			return AssetBundle.LoadFromMemory (data);
		}
	}

}