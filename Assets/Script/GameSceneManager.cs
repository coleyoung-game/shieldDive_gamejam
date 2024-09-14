using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    private static GameSceneManager m_Instance;

    private float m_WorldHeight;
    private float m_WorldWidth;

    public static GameSceneManager Instance { get { return m_Instance; } }
    public float WorldHeight { get { return m_WorldHeight; } }
    public float WorldWidth{ get { return m_WorldWidth; } }

    private void Awake()
    {
        m_Instance = this;
        Init();
    }


    private void Init()
    {
        float t_Aspect = (float)Screen.width / Screen.height;
        m_WorldHeight = Camera.main.orthographicSize;// * 2;
        m_WorldWidth = m_WorldHeight * t_Aspect;
        Debug.Log($"m_WorldWidth : {m_WorldWidth}, m_WorldHeight : {m_WorldHeight}");
    }
}
