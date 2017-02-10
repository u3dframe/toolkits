using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 类名 : 枚举工具
/// 作者 : Canyon
/// 日期 : 2015-11-20 15:20:00
/// 功能 : 
/// </summary>
namespace Toolkits
{
    public class EnumEx
    {

        // 根据当前枚举值，取得该值所对应的枚举Name(就是Key)
        static public string GetKey4Val<T>(object val)
        {
            return Enum.GetName(typeof(T), val);
        }

        // 判断枚举是否存在
        static public bool IsHas<T>(object val)
        {
            return Enum.IsDefined(typeof(T), val);
        }

        //  字符串转换为枚举:该字符串可以是key,也可以是val的ToString();
        static public T Str2Enum<T>(string val)
        {
            return (T)Enum.Parse(typeof(T), val, true);
        }

        // int转为枚举
        static public T Int2Enum<T>(int val)
        {
            return (T)Enum.ToObject(typeof(T), val);
        }

        static public int Length<T>()
        {
            return Enum.GetValues(typeof(T)).Length;
        }
    }
}
