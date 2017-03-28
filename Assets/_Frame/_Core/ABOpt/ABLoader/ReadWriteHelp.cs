using UnityEngine;
using System.Collections;
using System.IO;

namespace Core.Kernel{
	/// <summary>
	/// 类名 : 开发模式下读支持
	/// 作者 : Canyon
	/// 日期 : 2017-03-28 13:50
	/// 功能 : 编辑模式下运行项目的资源读取路径(针对制定位置)
	/// </summary>
	public class ReadDevelop{
		// 文件夹分割符号
		static public readonly char DSChar = Path.DirectorySeparatorChar;

		// 编辑器下面资源所在跟目录
		static public readonly string m_assets = "Assets";

		// 资源目录
		static public string m_gameRoot = "GameName";
	}

	/// <summary>
	/// 类名 : 读写支持
	/// 作者 : Canyon
	/// 日期 : 2017-03-28 13:50
	/// 功能 : 读取包内包外资源，写(热更新下载的资源进行写入操作)
	/// </summary>
	public class ReadWriteHelp : ReadDevelop {
		
		// 流文件夹
		static public readonly string m_streamingAssets = Application.streamingAssetsPath + DSChar;

		// 自己封装的
		static public readonly string m_streamingAssets2 =
			#if UNITY_EDITOR
				"file://"+Application.dataPath +"/StreamingAssets/";
			#else
				#if UNITY_ANDROID
				"jar:file://" + Application.dataPath + "!/assets/";
				#elif UNITY_IOS
				"file://"+Application.dataPath +"/Raw/";
				#else
				"file://"+Application.dataPath +"/StreamingAssets/";
				#endif
			#endif
		
		// 打包后资源所放的文件名
		static public string m_gameAssetName = "AssetBundles";

		// 打包平台名
		static public readonly string m_platform = 
			#if UNITY_EDITOR
			"Windows";
			#else
				#if UNITY_ANDROID
				"Android";
				#elif UNITY_IOS
				"IOS";
				#else
				"Windows";
				#endif
			#endif
		
		// 游戏包内资源目录
		static string _m_appContentPath = "";
		static public string m_appContentPath{
			get{
				if (string.IsNullOrEmpty (_m_appContentPath)) {
					_m_appContentPath = m_streamingAssets + m_gameAssetName + DSChar + m_platform + DSChar;
				}
				return _m_appContentPath;
			}
		}

		// 解压的资源目录
		static string _m_appUnCompressPath = "";
		static public string m_appUnCompressPath{
			get{
				if (string.IsNullOrEmpty (_m_appUnCompressPath)) {
					string game = m_gameRoot.ToLower ();
					#if UNITY_EDITOR
						#if UNITY_STANDALONE_WIN
						_m_appUnCompressPath =  "c:/" + game + DSChar;
						#else
						int i = Application.dataPath.LastIndexOf('/');
						_m_appUnCompressPath =  Application.dataPath.Substring(0, i + 1) + game + "/";
						#endif
					#else
						#if UNITY_ANDROID || UNITY_IOS
						_m_appUnCompressPath =  Application.persistentDataPath + DSChar + game + DSChar;
						#elif UNITY_STANDALONE
						// 平台(windos,mac)上面可行??? 需要测试
						_m_appUnCompressPath =  Application.persistentDataPath + DSChar + game + DSChar;
						#else
						// 可行???
						_m_appUnCompressPath =  Application.persistentDataPath + DSChar + game + DSChar;
						#endif
					#endif
				}
				return _m_appUnCompressPath;
			}
		}


		// 是否解压(其实就是拷贝包内资源到可读写文件夹下面)
		static public bool m_isUnCompresss = false;

		// 资源目录
		static public string m_dataPath {
			get{
				return m_isUnCompresss ? m_appUnCompressPath : m_appUnCompressPath;
			}
		}
	}
}
