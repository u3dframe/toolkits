using UnityEngine;
using System.Collections;

/// <summary>
/// 类名 : 实体入口对象
/// 作者 : Canyon
/// 日期 : 2016-12-26 12:00
/// 功能 : 2个参数的初始化
/// </summary>
public interface IEntity2 : IEntity {
    
    void DoReInit<T1,T2>(T1 t1,T2 kt2);
    
    void DoInit<T1, T2>(T1 t1, T2 kt2);
}
