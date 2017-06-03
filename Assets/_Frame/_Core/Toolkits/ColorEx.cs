using UnityEngine;
using System.Collections;

namespace Toolkits
{
	/// <summary>
	/// 类名 : 颜色 工具
	/// 作者 : Canyon
	/// 日期 : 2016-12-23 15:20
	/// 功能 :
	/// </summary>
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
			return ToColor (255, 255, 255, 160);
		}

		static public Color GetGrayColor2 ()
		{
			return ToColor (104, 104, 104, 100);
		}

		static public Color ToColor (float red, float green, float blue, float alpha){
			if (red > 1)
				red = red / 255f;
			
			if (green > 1)
				green = green / 255f;
			
			if (blue > 1)
				blue = blue / 255f;

			if (alpha > 1)
				alpha = alpha / 255f;

			return new Color (red, green, blue, alpha);
		}

		static public Color ToColor (float r, float g, float b){
			return ToColor (r, g, b, 1);
		}

		static public Color ToColor (int r, int g, int b, int a)
		{
			return ToColor((float)r,(float)g,(float)b,(float)a);
		}

		static public Color ToColor (int r, int g, int b)
		{
			return ToColor (r, g, b, 255);
		}

		static public Color ToColor (byte r, byte g, byte b, byte a)
		{
			return new Color32 (r, g, b, a);
		}

		static public Color ToColor (byte r, byte g, byte b)
		{
			return ToColor(r, g, b, (byte)255);
		}

		static public Color ToColor2 (int r, int g, int b, int a){
			return ToColor ((byte)r, (byte)g, (byte)b, (byte)a);
		}

		static public Color ToColor2 (int r, int g, int b){
			return ToColor ((byte)r, (byte)g, (byte)b);
		}

		static public Color ToColor(Color32 col){
			return (Color)col;
		}

		// parse 解析
		static public Color32 ToColor32(Color col){
			return (Color32)col;
		}

		/// <summary>
		/// 16进制转为Color颜色,#101010
		/// </summary>
		/// <returns>The to color.</returns>
		/// <param name="hex">Hex.[可以为(#101010)，也可以无#号即为(1a1d1f)]</param>
		static public Color HexToColor(string hex){
			int indexBeg = 0;
			int lens = 2;

			if (hex.IndexOf ("#") == 0) {
				indexBeg = 1;
			}

			byte r = byte.Parse (hex.Substring (indexBeg,lens), System.Globalization.NumberStyles.HexNumber);
			indexBeg += lens;

			byte g = byte.Parse (hex.Substring (indexBeg,lens), System.Globalization.NumberStyles.HexNumber);
			indexBeg += lens;

			byte b = byte.Parse (hex.Substring (indexBeg,lens), System.Globalization.NumberStyles.HexNumber);
			return ToColor (r, g, b);
		}
	}
}