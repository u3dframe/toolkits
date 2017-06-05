using UnityEngine;
using System.Collections;
using UnityEditor.ProjectWindowCallback;
using System.IO;
using UnityEditor;

/// <summary>
/// 类名 : 定义一个创建资源的 Kernel 类并实现其Kernel方法
/// 作者 : Canyon
/// 日期 : 2017-05-15 20:45
/// 功能 : 
/// </summary>
public class CreateKernelAction : EndNameEditAction {
	public override void Action (int instanceId, string pathName, string resourceFile)
	{
		//创建资源
		Object obj = CreateAssetFormTemplate(pathName, resourceFile);
		//高亮显示该资源
		ProjectWindowUtil.ShowCreatedAsset(obj);
	}

	internal static Object CreateAssetFormTemplate(string pathName, string resourceFile)
	{

		//获取要创建资源的绝对路径
		string fullName = Path.GetFullPath(pathName);
		//读取本地模版文件
		StreamReader reader = new StreamReader(resourceFile);
		string content = reader.ReadToEnd();
		reader.Close();

		//获取资源的文件名
		string fileName = Path.GetFileNameWithoutExtension(pathName);

		//替换默认的文件名
		content = content.Replace("#SCRIPTNAME#", fileName);

		// 替换时间
		content = content.Replace("#datatime#",string.Format("{0:yyyy-MM-dd HH:mm}",System.DateTime.Now));

		//写入新文件
		StreamWriter writer = new StreamWriter(fullName, false, System.Text.Encoding.UTF8);
		writer.Write(content);
		writer.Close();

		//刷新本地资源
		AssetDatabase.ImportAsset(pathName);
		AssetDatabase.Refresh();

		return AssetDatabase.LoadAssetAtPath(pathName, typeof(Object));
	}
}
