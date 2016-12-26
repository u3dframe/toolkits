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

    DBU3D_Particle m_db_particle = new DBU3D_Particle();

    void Awake()
    {
        m_db_particle.DoReInit(transform);
    }

    void OnDestroy()
    {
        m_db_particle.DoClear();
    }

    public void setSize(float size)
    {
        m_db_particle.SetSize(size);
    }

    public void setScale(float _scale)
    {
        m_db_particle.SetScale(_scale);
    }

    public void changeState(bool _isPause)
    {
        m_db_particle.ChangeState(_isPause);
    }

    public float curScale
    {
        get
        {
            return m_db_particle.curScale;
        }
    }

    public float maxTime
    {
        get
        {
            return m_db_particle.maxTime;
        }
    }

    public void SetAlpha(float alpha)
    {
        m_db_particle.SetAlpha(alpha);
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

        if (m_db_particle.curState != isPasueInEditor)
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
