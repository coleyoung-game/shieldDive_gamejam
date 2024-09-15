using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSystem : MonoBehaviour
{
    private static MainSystem m_Instance;
    public static MainSystem Instance { get { return m_Instance; } }
    [SerializeField] private SceneLoader m_SceneLoader;

    public SceneLoader SceneLoader { get { return m_SceneLoader; } }
    
    public GameObject Popup;
    public float BGM_Volume;
    public float SFX_Volume;

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }
}
