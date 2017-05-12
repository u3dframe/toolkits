using UnityEngine;
using System.Collections;

/// <summary>
/// 光照贴图
/// </summary>
public class LightmapEx : MonoBehaviour
{

	// 光照图列表
	public Texture2D[] lightMaps;
	// 光照图数据
	LightmapData[] _lightmapDatas = null;

	public LightmapData[] lightmapDatas {
		get {
			if (lightMaps == null) {
				return null;
			}
			if (_lightmapDatas == null) {
				_lightmapDatas = new LightmapData[lightMaps.Length];
				for (int i = 0; i < lightMaps.Length; i++) {
					LightmapData ld = new LightmapData ();
					ld.lightmapFar = lightMaps [i];
					_lightmapDatas [i] = ld;
				}
			}
			return _lightmapDatas;
		}
	}

	public void setLightmapping ()
	{
		LightmapSettings.lightmapsMode = LightmapsMode.NonDirectional;
		LightmapData[] data = lightmapDatas;
		if (data != null) {
			LightmapSettings.lightmaps = data;
		} else {
			clearLightmapping ();
		}
	}

	public void clearLightmapping ()
	{
		LightmapSettings.lightmaps = new LightmapData[0];
	}

	void OnDisable ()
	{
		clearLightmapping ();
	}
}
