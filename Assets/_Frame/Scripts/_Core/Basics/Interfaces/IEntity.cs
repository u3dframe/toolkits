using UnityEngine;
using System.Collections;

/// <summary>
/// 类名 : 实体入口对象
/// 作者 : Canyon
/// 日期 : 2016-12-26 12:00
/// 功能 :  包含数据对象，界面操作对象，模型对象，粒子对象等
/// </summary>
public interface IEntity {

    // 初始化
    void DoReInit();
    
    // 初始化
    void DoInit();
    
    // 更新(time update中)
    void DoUpdate();

    // 结束
    void DoEnd();

    // 清除
    void DoClear();
}
