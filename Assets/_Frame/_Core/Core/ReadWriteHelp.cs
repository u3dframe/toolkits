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
		static protected readonly string m_assets = "Assets";
		static protected readonly int m_iLenFnAssests = m_assets.Length + 1;

		// Resources目录下面
		static protected readonly string m_fnResources = "Resources";
		static protected readonly int m_iLenFnResources = m_fnResources.Length + 1;

		// 编辑模式下的资源后缀名
		static public readonly string m_extLowerPng = ".png";
		static public readonly string m_extLowerFab = ".prefab";

		/// <summary>
		/// 编辑器模式下是否通过加载ab资源得到
		/// </summary>
		static public bool m_isEdtiorLoadAsset = false;

		// 解压资源目录根目录
		static public string m_unCompressRoot = "GameName";

		// 流文件下，加压根目录下的资源根目录
		static public string m_gameAssetName = "AssetBundles";

		// 开发模式下，资源放的路径地址
		static public string m_edtAssetPath = "xxx/Builds";

		// 开发模式下，放到Resources目录下的资源地址
		static public string m_edtResPath = "xxx/Resources";
	}

	/// <summary>
	/// 类名 : 读写支持
	/// 作者 : Canyon
	/// 日期 : 2017-03-28 13:50
	/// 功能 : 读取包内包外资源，写(热更新下载的资源进行写入操作)
	/// </summary>
	public class ReadWriteHelp : ReadDevelop {

		// 编辑模式下Assets文件夹路径
		static public readonly string m_dataPath = Application.dataPath + DSChar;

		// 外部可读写的文件夹路径
		static public readonly string m_persistentDataPath = Application.persistentDataPath + DSChar;

		// 流文件夹路径
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

		// 资源相对路径
		static public string m_assetPlatformPath{
			get{
				string ret = string.Format("{0}{1}{2}{3}",m_gameAssetName , DSChar , m_platform , DSChar);
				#if UNITY_EDITOR
				if(!m_isEdtiorLoadAsset)
					ret = string.Format("{0}{1}",m_edtAssetPath , DSChar);
				#endif
				return ret;
			}
		}

		// 编辑模式下资源根目录
		static string _m_appAssetPath = "";
		static public string m_appAssetPath{
			get{
				if (string.IsNullOrEmpty (_m_appAssetPath)) {
					_m_appAssetPath = string.Format("{0}{1}",m_dataPath , m_assetPlatformPath);
					_m_appAssetPath = ReplaceSeparator (_m_appAssetPath);
				}
				return _m_appAssetPath;
			}
		}

		// 游戏包内资源目录
		static string _m_appContentPath = "";
		static public string m_appContentPath{
			get{
				if (string.IsNullOrEmpty (_m_appContentPath)) {
					_m_appContentPath = string.Format("{0}{1}",m_streamingAssets , m_assetPlatformPath);
					_m_appContentPath = ReplaceSeparator (_m_appContentPath);
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
						_m_appUnCompressPath = string.Format("{0}{1}{2}{3}","c:/" , unCompressRoot , DSChar , m_assetPlatformPath);
						#else
						int i = Application.dataPath.LastIndexOf('/');
						_m_appUnCompressPath = Application.dataPath.Substring(0, i + 1) + unCompressRoot + DSChar + m_assetPlatformPath;
						#endif
					#else
						#if UNITY_ANDROID || UNITY_IOS
						_m_appUnCompressPath = string.Format("{0}{1}{2}{3}",m_persistentDataPath , unCompressRoot , DSChar , m_assetPlatformPath);
						#elif UNITY_STANDALONE
						// 平台(windos,mac)上面可行??? 需要测试
						_m_appUnCompressPath = string.Format("{0}{1}{2}{3}",m_persistentDataPath , unCompressRoot , DSChar , m_assetPlatformPath);
						#else
						// 可行???
						_m_appUnCompressPath = string.Format("{0}{1}{2}{3}",m_persistentDataPath , unCompressRoot , DSChar , m_assetPlatformPath);
						#endif
					#endif
					_m_appUnCompressPath = ReplaceSeparator (_m_appUnCompressPath);
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

		/// <summary>
		/// 统一路径格式
		/// </summary>
		/// <returns>The separator.</returns>
		/// <param name="path">Path.</param>
		static public string ReplaceSeparator(string path){
			return path.Replace ('\\', '/');
		}
	}
}
