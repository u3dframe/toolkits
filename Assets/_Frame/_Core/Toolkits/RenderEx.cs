using UnityEngine;
using System.Collections;

/// <summary>
/// 类名 : 渲染工具
/// 作者 : Canyon
/// 日期 : 2016-10-27 14:34
/// 功能 :  
/// render.material 是render自身的材质,私有财产(改变时,除了自己，其它没什么变化)
/// render.sharedMaterial 共享材质,一般也是来源材质,是共有的(改变时,物理文件也会相应的改变,版本管理里面也会改变)
/// </summary>
namespace Toolkits
{
    public static class RenderEx
    {

        // 取得渲染Render的材质
        static public Material GetMaterial(Renderer render)
        {
            if (render.material == null)
            {
                return render.sharedMaterial;
            }
            return render.material;
        }

        //  取得渲染Render当前自身的材质
        static public Material GetCurMaterial(Renderer render, bool isCur = false)
        {
            Material mat = GetMaterial(render);
            if (isCur)
            {
                if (mat != render.material)
                {
                    if (render.material != null)
                    {
                        GameObject.Destroy(render.material);
                        render.material = null;
                    }

                    if (mat != null)
                    {
                        render.material = new Material(mat);
                    }
                }
            }
            return mat;
        }

        // 设置边缘光RIM(min = max = 1的时候表示没有边缘光)
        static public void ResetRIM(Renderer render, float min = 1.0f, float max = 1.0f)
        {
            Material mat = GetCurMaterial(render);
            if (mat != null)
            {
                if (mat.HasProperty("_RimMin"))
                {
                    mat.SetFloat("_RimMin", min);
                }
                if (mat.HasProperty("_RimMax"))
                {
                    mat.SetFloat("_RimMax", max);
                }
            }
        }

        // 设置边缘光RIM
        static public void ResetRIM(GameObject gobj, float min = 1.0f, float max = 1.0f)
        {
            MeshRenderer[] meshRender = gobj.GetComponentsInChildren<MeshRenderer>(true);
            if (meshRender != null && meshRender.Length > 0)
            {
                int lens = meshRender.Length;
                MeshRenderer render;
                for (int i = 0; i < lens; i++)
                {
                    render = meshRender[i];
                    ResetRIM(render, min, max);
                }
            }

            SkinnedMeshRenderer[] skinRender = gobj.GetComponentsInChildren<SkinnedMeshRenderer>(true);
            if (skinRender != null && skinRender.Length > 0)
            {
                int lens = skinRender.Length;
                SkinnedMeshRenderer render;
                for (int i = 0; i < lens; i++)
                {
                    render = skinRender[i];
                    ResetRIM(render, min, max);
                }
            }
        }
    }
}
