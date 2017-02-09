using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 类名 : 集合List 工具
/// 作者 : Canyon
/// 日期 : 2016-12-23 15:20
/// 功能 :
/// </summary>
namespace Toolkits
{
	public class ListEx
	{
		static public List<T> GetList<T>(List<T> listOrg)
		{
			List<T> ret = new List<T>();
			GetList (listOrg, ref ret);
			return ret;
		}

		static public List<T> GetList<T>(IList listOrg)
		{
			List<T> ret = new List<T>();
			GetList (listOrg, ref ret);
			return ret;
		}

		static public void GetList<T>(List<T> listOrg,ref List<T> ret)
		{
			if (ret == null) {
				ret = new List<T> ();
			} else {
				ret.Clear ();
			}

			if (listOrg == null)
				return;

			int lens = listOrg.Count;
			if(lens <= 0)
				return;

			object tmp;
			for (int i = 0; i < lens; i++)
			{
				tmp = listOrg[i];
				if(tmp is T)
				{
					ret.Add((T)tmp);
				}
			}
		}

		static public void GetList<T>(IList listOrg,ref List<T> ret)
		{
			if (ret == null) {
				ret = new List<T> ();
			} else {
				ret.Clear ();
			}

			if (listOrg == null)
				return;

			int lens = listOrg.Count;
			if(lens <= 0)
				return;

			object tmp;
			for (int i = 0; i < lens; i++)
			{
				tmp = listOrg[i];
				if(tmp is T)
				{
					ret.Add((T)tmp);
				}
			}
		}
	}
}