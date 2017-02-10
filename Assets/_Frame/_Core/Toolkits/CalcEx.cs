using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 类名 : 计算 工具
/// 作者 : Canyon
/// 日期 : 2016-12-23 15:20
/// 功能 : 
/// </summary>
namespace Toolkits
{
    public class CalcEx
    {
        // 百分比
        static public float percent(float v1, float max)
        {
            if (max == 0) return 0;
            return v1 / max;
        }

        static public double percent(double v1, double max)
        {
            if (max == 0) return 0;
            return v1 / max;
        }

        static public int percentInt(double v1, double max)
        {
            if (max == 0) return 0;
            double v = v1 / max;
            return (int)Math.Ceiling((v * 100));
        }

        // 总页数
        static public int pageCount(int count, int pageSize)
        {
            int page = count / pageSize;

            page = count == page * pageSize ? page : page + 1;
            return page;
        }

        // 取某一页的数据
        static public ArrayList getPage(ArrayList datas, int page, int pageSize)
        {
            int count = datas.Count;
            int begin = (int)(page * pageSize);
            int end = (int)(begin + pageSize);
            if (begin > count || begin < 0 || end < 0)
                return new ArrayList();
            end = count < end ? count : end;
            if (end <= begin)
                return new ArrayList();
            int num = end - begin;
            return datas.GetRange(begin, num);
        }

        static public float Distance(GameObject g1, GameObject g2)
        {
            if (g1 == null || g2 == null)
                return 0;
            Vector3 x1 = g1.transform.localPosition;
            Vector3 x2 = g2.transform.localPosition;
            float d = Vector3.Distance(x1, x2);
            return d;
        }

		// 保留小数
		static public double Round2D(float org, int acc)
		{
			double pow = Mathf.Pow(10, acc);
			double temp = org * pow;
			return Mathf.RoundToInt((float)temp) / pow;
		}
    }

	public class DistanceComparer : IComparer<GameObject>
    {
		public int Compare(GameObject o1, GameObject o2)
        {
			float d = CalcEx.Distance(o1, o2);
            return d > 0 ? -1 : 1;
        }
    }
}