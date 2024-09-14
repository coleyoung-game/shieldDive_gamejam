using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    [SerializeField] private float m_PoolingTime;


    private static PoolingManager m_Instance;
    private Queue<GameObject> m_Queue = new Queue<GameObject>();

    private float m_CurrTime = 0.0f;
    
    public static PoolingManager Instance { get { return m_Instance; } }


    private void Awake()
    {
        m_Instance = this; 
    }

    private void Update()
    {
        m_CurrTime += Time.deltaTime;
        if (m_CurrTime > m_PoolingTime)
        {
            m_CurrTime = 0.0f;
            while (m_Queue.Count > 0)
            {
                Destroy(m_Queue.Dequeue());
            }
        }

    }

    public void EnqueueObject(GameObject _Obj)
    {
        m_CurrTime = 0.0f;
        //_Obj.transform.parent = transform;
        m_Queue.Enqueue(_Obj);
    }
    

}
