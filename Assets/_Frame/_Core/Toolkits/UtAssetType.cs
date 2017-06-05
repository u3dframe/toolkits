using UnityEngine;
using System.Collections;
using System.ComponentModel;

namespace Core.Kernel
{
	/// <summary>
	/// 类名 : ut-unity 资源Asset类型
	/// 作者 : Canyon
	/// 日期 : 2017-05-17 15:03
	/// 功能 : 
	/// </summary>
	public enum UtAssetType
	{
		[Description ("文本")]
		Text,

		[Description ("字节流")]
		Bytes,

		[Description ("文理-图片")]
		Texture,

		[Description ("AB资源")]
		AssetBunlde
	}
}
