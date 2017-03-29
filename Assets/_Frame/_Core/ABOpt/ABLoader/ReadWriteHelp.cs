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

		// 平台
		static public readonly string platformAndroid = "Android";
		static public readonly string platformIOS = "IOS";
		static public readonly string platformWindows = "Windows";

		// 编辑器下面资源所在跟目录
		static public readonly string m_assets = "Assets";

		// 解压资源目录根目录
		static public string m_unCompressRoot = "GameName";

		// 流文件根目录 - 包内文件根目录
		static public string m_gameAssetName = "AssetBundles";
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
		
		// 打包平台名
		static string _m_platform = "";
		static public string m_platform{
			get{
				if (string.IsNullOrEmpty (_m_platform)) {
					_m_platform = 
					#if UNITY_ANDROID
					platformAndroid;
					#elif UNITY_IOS
					platformIOS;
					#else
					platformWindows;
					#endif
				}
				return _m_platform;
			}
			set{
				_m_platform = value;
			}
		} 

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
					string unCompressRoot = m_unCompressRoot.ToLower ();
					#if UNITY_EDITOR
						#if UNITY_STANDALONE_WIN
						_m_appUnCompressPath =  "c:/" + unCompressRoot + DSChar;
						#else
						int i = Application.dataPath.LastIndexOf('/');
						_m_appUnCompressPath =  Application.dataPath.Substring(0, i + 1) + unCompressRoot + DSChar;
						#endif
					#else
						#if UNITY_ANDROID || UNITY_IOS
						_m_appUnCompressPath =  Application.persistentDataPath + DSChar + unCompressRoot + DSChar;
						#elif UNITY_STANDALONE
						// 平台(windos,mac)上面可行??? 需要测试
						_m_appUnCompressPath =  Application.persistentDataPath + DSChar + unCompressRoot + DSChar;
						#else
						// 可行???
						_m_appUnCompressPath =  Application.persistentDataPath + DSChar + unCompressRoot + DSChar;
						#endif
					#endif
				}
				return _m_appUnCompressPath;
			}
		}

		// 打包生成的流文件 - windows资源路径
		static public string m_windowsPath{
			get{
				m_platform = platformWindows;
				return m_appContentPath;
			}
		}

		// 打包生成的流文件 - android资源路径
		static public string m_androidPath{
			get{
				m_platform = platformAndroid;
				return m_appContentPath;
			}
		}

		// 打包生成的流文件 - IOS资源路径
		static public string m_iosPath{
			get{
				m_platform = platformIOS;
				return m_appContentPath;
			}
		}
	}
}
