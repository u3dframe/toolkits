using UnityEngine;
using System.Collections;

/// <summary>
/// 类名 : 实体入口对象
/// 作者 : Canyon
/// 日期 : 2016-12-26 12:00
/// 功能 : 3个参数的初始化
/// </summary>
public interface IEntity3 : IEntity {
    void DoReInit<T1, T2,T3>(T1 t1, T2 kt2,T3 t3);
    
    void DoInit<T1, T2, T3>(T1 t1, T2 kt2, T3 t3);
}
