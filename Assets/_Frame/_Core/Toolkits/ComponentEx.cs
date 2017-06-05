using UnityEngine;
using System.Collections;

namespace Core.Kernel
{
	/// <summary>
	/// 类名 : 組件工具扩展 (Component tool extend)
	/// 作者 : Canyon
	/// 日期 : 2017-05-12 23:23
	/// 功能 : 
	/// </summary>
	public class ComponentEx
	{
		/// <summary>
		/// 添加组件
		/// </summary>
		static public T AddComponent<T>(GameObject gobj) where T : Component{
			return gobj.AddComponent<T> ();
		}

		/// <summary>
		/// 添加组件
		/// </summary>
		static public T AddComponent<T>(Transform trsf) where T : Component{
			return AddComponent<T> (trsf.gameObject);
		}

		/// <summary>
		/// 取得组件
		/// </summary>
		static public T GetComponent<T>(GameObject gobj,bool isAdd4Null = false) where T : Component{
			T ret =  gobj.GetComponent<T> ();
			if (isAdd4Null && ret == null) {
				ret = AddComponent<T> (gobj);
			}
			return ret;
		}

		/// <summary>
		/// 取得组件
		/// </summary>
		static public T GetComponent<T>(Transform trsf,bool isAdd4Null = false) where T : Component{
			return GetComponent<T> (trsf.gameObject,isAdd4Null);
		}

		/// <summary>
		/// Gets the components in children.
		/// </summary>
		/// <returns>The components in children.</returns>
		/// <param name="trsf">Trsf.</param>
		/// <param name="includeInactive">If set to <c>true</c> include inactive.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		static public T[] GetComponentsInChildren<T>(Transform trsf,bool includeInactive = true) where T : Component{
			return trsf.GetComponentsInChildren<T>(includeInactive);
		}

		/// <summary>
		/// 找到trsf下面的子对象
		/// </summary>
		/// <param name="trsf">Trsf.</param>
		/// <param name="valChild">Value. [xxx/xx/xx]</param>
		static public Transform Find(Transform trsf,string valChild){
			return trsf.Find (valChild);
		}

		/// <summary>
		/// 找到trsf下面的子对象
		/// </summary>
		static public Transform Find(GameObject gobj,string valChild){
			return Find (gobj.transform,valChild);
		}

		/// <summary>
		/// 找到子对象(trsfParent 不为空就从trsfParent里面查询,否则就从trsf里面查询)
		/// </summary>
		/// <param name="trsf">Trsf.</param>
		/// <param name="valChild">Value child.</param>
		/// <param name="trsfParent">Trsf parent.</param>
		static public Transform Find(Transform trsf, string valChild,Transform trsfParent){
			if (trsfParent != null)
				return Find (trsfParent, valChild);
			return Find (trsf, valChild);
		}

		/// <summary>
		/// 找到trsf下面的子对象
		/// </summary>
		/// <returns>gameObject对象(可能为空)</returns>
		/// <param name="trsf">Trsf.</param>
		/// <param name="valChild">Value.</param>
		static public GameObject FindGobj(Transform trsf,string valChild){
			Transform ret = Find (trsf,valChild);
			if (ret != null) {
				return ret.gameObject;
			}
			return null;
		}

		/// <summary>
		/// 找到trsf下面的子对象
		/// </summary>
		/// <returns>gameObject对象(可能为空)</returns>
		/// <param name="gobj">Trsf.</param>
		/// <param name="valChild">Value.</param>
		static public GameObject FindGobj(GameObject gobj,string valChild){
			return FindGobj (gobj.transform, valChild);
		}

		/// <summary>
		/// 找到组件
		/// </summary>
		static public T FindComponent<T>(GameObject gobj,bool isAdd4Null = false) where T : Component{
			return GetComponent<T> (gobj, isAdd4Null);
		}

		/// <summary>
		/// 找到组件
		/// </summary>
		static public T FindComponent<T>(Transform trsf,bool isAdd4Null = false) where T : Component{
			return GetComponent<T> (trsf.gameObject,isAdd4Null);
		}

		/// <summary>
		/// 找到子对象的组件
		/// </summary>
		static public T FindComponent<T> (Transform trsf,string valChild,bool isAdd4Null = false) where T : Component
		{
			GameObject gobj = FindGobj (trsf, valChild);
			if (gobj == null)
				return null;
			return GetComponent<T> (gobj, isAdd4Null);
		}

		/// <summary>
		/// 找到子对象的组件
		/// </summary>
		static public T FindComponent<T> (GameObject gobj,string valChild,bool isAdd4Null = false) where T : Component
		{
			return FindComponent<T> (gobj.transform, valChild,isAdd4Null);
		}
	}
}