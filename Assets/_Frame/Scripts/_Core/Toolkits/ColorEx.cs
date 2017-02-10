using UnityEngine;
using System.Collections;

/// <summary>
/// 类名 : 颜色 工具
/// 作者 : Canyon
/// 日期 : 2016-12-23 15:20
/// 功能 :
/// </summary>
namespace Toolkits
{
	public class ColorEx
	{
		/// <summary>
		/// Set the jet color (based on the Jet color map) ( http://www.metastine.com/?p=7 )
		/// val should be normalized between 0 and 1
		/// </summary>
		static public Color GetJetColor (float val)
		{
			float fourValue = 4.0f * val;
			float red = Mathf.Min (fourValue - 1.5f, -fourValue + 4.5f);
			float green = Mathf.Min (fourValue - 0.5f, -fourValue + 3.5f);
			float blue = Mathf.Min (fourValue + 0.5f, -fourValue + 2.5f);
			Color newColor = new Color ();
			newColor.r = Mathf.Clamp01 (red);                
			newColor.g = Mathf.Clamp01 (green);
			newColor.b = Mathf.Clamp01 (blue);
			newColor.a = 1;
			return newColor;
		}

		static public Color GetGrayColor ()
		{
			Color r2 = GetColor (255, 255, 255, 160);
			return r2;
		}

		static public Color GetGrayColor2 ()
		{
			Color r2 = GetColor (104, 104, 104, 100);
			return r2;
		}

		static public Color GetColor (int r, int g, int b, int a)
		{
			float rf = r / 255f;
			float gf = g / 255f;
			float bf = b / 255f;
			float af = a / 255f;
			Color r2 = new Color (rf, gf, bf, af);
			return r2;
		}

		static public Color GetColor (int r, int g, int b)
		{
			return GetColor (r, g, b, 255);
		}
	}
}