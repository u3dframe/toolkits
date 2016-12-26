using System.Collections;

public interface IViewBase : IEntity {
	// 显示
	void DoShow();

	// 隐藏
	void DoHide();

	// 销毁
	void DoDestroy ();
}
