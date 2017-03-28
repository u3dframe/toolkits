using UnityEngine;
using System.Collections;
using System.IO;

namespace Core.Kernel{
	/// <summary>
	/// 类名 : 开发模式下读取资源
	/// 作者 : Canyon
	/// 日期 : 2017-03-28 17:06
	/// 功能 : 读取Assets下面的资源
	/// </summary>
	public class ResourcesDevelop {

		// 是否是放到了Resources下面
		static readonly string m_fnResources = "Resources";
		static readonly int m_iLenFnResources = m_fnResources.Length + 1;

		// 处理Assset路径
		static readonly string m_fnAssests = "Assets";
		static readonly int m_iLenFnAssests = m_fnAssests.Length + 1;

		/// <summary>
		/// Load the specified path.
		/// </summary>
		/// <param name="path">相对路径(有无后缀都可以处理)</param>
		static public UnityEngine.Object Load4Develop(string path,string suffix){
			UnityEngine.Object ret = null;

			#if UNITY_EDITOR
			int index = path.LastIndexOf (m_fnResources);
			if(index < 0){
				// 去掉第一个Assets文件夹路径
				index = path.IndexOf(m_fnAssests);
				if (index >= 0) {
					path = path.Substring (index + m_iLenFnAssests);
				}
				path = "Assets/" + path;
				string suffix2 = Path.GetExtension (path);
				if (string.IsNullOrEmpty (suffix2)) {
					path += suffix;
				}
				ret = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
			}
			#endif

			if (ret == null) {
				ret = LoadInResources (path);
			}
			return ret;
		}

		/// <summary>
		/// Loads the in resources.
		/// </summary>
		/// <returns>The in resources.</returns>
		/// <param name="path">路径</param>
		static public UnityEngine.Object LoadInResources(string path){
			// 去掉最后一个Resources文件夹路径
			int index = path.LastIndexOf (m_fnResources);
			if (index >= 0) {
				path = path.Substring (index + m_iLenFnResources);
			}

			// 去掉后缀名
			string suffix = Path.GetExtension (path);
			if (!string.IsNullOrEmpty (suffix)) {
				path = path.Replace (suffix, "");
			}

			return UnityEngine.Resources.Load (path);
		}

		static public UnityEngine.Object Load4Png(string path){
			return Load4Develop (path, ".png");
		}

		static public UnityEngine.Object Load4Prefab(string path){
			return Load4Develop (path, ".prefab");
		}
	}

	public class Resources : ResourcesDevelop {
		
	}
}