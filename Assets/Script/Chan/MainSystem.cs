using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSystem : MonoBehaviour
{
    private static MainSystem m_Instance;
    public static MainSystem Instance { get { return m_Instance; } }
    public float BGM_Volume;
    public float SFX_Volume;

    private void Awake()
    {
        m_Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
