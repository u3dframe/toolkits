using UnityEngine;
using System.Collections.Generic;
using Toolkits;

/// <summary>
/// 类名 : 界面 - 粒子系统工具
/// 作者 : Canyon
/// 日期 : 2016-10-27 14:34
/// 功能 :  
/// </summary>
public class UIExParticleSystem : MonoBehaviour {
    List<ParticleSystem> listAll = new List<ParticleSystem>();
    List<Renderer> listAllRenders = new List<Renderer>();

    Dictionary<int, List<float>> dicDefaultScale = new Dictionary<int, List<float>>();

    int lens = 0;
    float curSize = -1;
    float _curScale = 1;
    bool _curState = false;
    
    // 该粒子的最长时间
    float _maxTime = 1f;

    void Awake()
    {
        listAll.Clear();
        ParticleSystem[] arr = transform.GetComponentsInChildren<ParticleSystem>(true);
        Renderer[] arrRenders = transform.GetComponentsInChildren<Renderer>(true);
        if (arrRenders != null && arrRenders.Length > 0)
        {
            listAllRenders.AddRange(arrRenders);
        }

        if (arr != null && arr.Length > 0)
        {
            listAll.AddRange(arr);
        }

        lens = listAll.Count;
        if (lens <= 0)
        {
            return;
        }

        ParticleSystem ps;
        float curTime = 0f;
        int key;
        List<float> vList;
        for (int i = 0; i < lens; i++)
        {
            ps = listAll[i];
            curTime = ps.startDelay + ps.duration;
            if (curTime > _maxTime)
            {
                _maxTime = curTime;
            }

            key = ps.GetInstanceID();
            vList = new List<float>();
            vList.Add(ps.startSpeed);
            vList.Add(ps.startSize);
            vList.Add(ps.gravityModifier);
            dicDefaultScale.Add(key, vList);
        }
    }

    public void setSize(float size)
    {
        if (lens <= 0 || size < 0 || size == curSize)
        {
            return;
        }
        curSize = size;
        ParticleSystem ps;
        for (int i = 0; i < lens; i++)
        {
            ps = listAll[i];
            ps.startSize = size;
        }
    }

    public void setScale(float _scale)
    {
        if (lens <= 0 || _scale < 0 || _scale == _curScale)
        {
            return;
        }
        _curScale = _scale;
        
        ParticleSystem ps;
        List<float> vList;
        for (int i = 0; i < lens; i++)
        {
            ps = listAll[i];
            ps.Clear();     //例子prewarm 引起的bug, fix by haoc
            vList = dicDefaultScale[ps.GetInstanceID()];
            ps.startSpeed = _scale * (vList[0]);
            ps.startSize = _scale * (vList[1]);
            ps.gravityModifier = _scale * (vList[2]);
        }
    }

    public void changeState(bool _isPause)
    {
        if (lens <= 0 || _curState == _isPause)
        {
            return;
        }
        ParticleSystem ps;
        _curState = _isPause;
        for (int i = 0; i < lens; i++)
        {
            ps = listAll[i];
            if (_curState)
            {
                ps.Pause();
            }
            else
            {
                ps.Play();
            }
        }
    }

    public float curScale
    {
        get
        {
            return _curScale;
        }
    }

    public float maxTime
    {
        get
        {
            return _maxTime + 0.01f;
        }
    }

    public void SetAlpha(float alpha)
    {
        int lensRender = listAllRenders.Count;
        if (lensRender <= 0 || alpha < 0)
        {
            return;
        }
        Renderer curRender;
        Material mat;
        Color col;
        alpha = alpha > 1 ? alpha / 255f : alpha;
        for (int i = 0; i < lensRender; i++)
        {
            curRender = listAllRenders[i];
            mat = RenderEx.GetMaterial(curRender);
            if (mat != null)
            {
                if (mat.HasProperty("_Color"))
                {
                    col = mat.GetColor("_Color");
                    col.a = alpha;
                    mat.SetColor("_Color", col);
                }

                if (mat.HasProperty("_TintColor"))
                {
                    col = mat.GetColor("_TintColor");
                    col.a = alpha;
                    mat.SetColor("_TintColor", col);
                }
            }
        }
    }

#if UNITY_EDITOR
    public bool isUpdateInEditor = false;

    public float scaleInEditor = 1f;

    public bool isPasueInEditor = false;

    void Update()
    {
        if (!isUpdateInEditor)
        {
            return;
        }

        if (curScale != scaleInEditor)
        {
            setScale(scaleInEditor);
        }

        if (_curState != isPasueInEditor)
        {
            changeState(isPasueInEditor);
        }
    }
#endif

    // 添加一个静态方法
    static public UIExParticleSystem Get(GameObject go)
    {
        UIExParticleSystem ret = go.GetComponent<UIExParticleSystem>();
        if (ret == null) ret = go.AddComponent<UIExParticleSystem>();
        return ret;
    }
}
