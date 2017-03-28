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

		}
	}
}