using System.Collections;

/// <summary>
/// 类名 : 视图对象接口
/// 作者 : Canyon
/// 日期 : 2016-12-26 12:00
/// 功能 :  
/// </summary>
public interface IViewBase : IEntity2 {
	// 显示
	void DoShow();

	// 隐藏
	void DoHide();

	// 销毁
	void DoDestroy ();
}
