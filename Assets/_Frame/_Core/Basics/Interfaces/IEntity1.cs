using UnityEngine;
using System.Collections;

/// <summary>
/// 类名 : 实体入口对象
/// 作者 : Canyon
/// 日期 : 2016-12-26 12:00
/// 功能 : 1个参数的初始化
/// </summary>
public interface IEntity1 : IEntity {
    void DoReInit<T>(T t1);
    
    void DoInit<T>(T t1);
}
