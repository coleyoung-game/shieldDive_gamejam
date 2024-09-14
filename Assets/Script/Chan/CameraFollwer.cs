using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chan
{
    public class CameraFollwer : MonoBehaviour
    {
        [SerializeField] private Transform m_PlayerPos;
        [SerializeField] private Vector3 m_CameraPos;
        private Vector3 m_CalcCameraPos;
        private float m_Aspect;
        private float m_WorldHeight;
        private float m_WorldWidth;

        private void Start()
        {
            Init();
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            m_CalcCameraPos = Vector3.up * (m_PlayerPos.position.y + m_CameraPos.y > 0 ? 0:m_PlayerPos.position.y + m_CameraPos.y);
            m_CalcCameraPos.z = -1;
            transform.position = m_CalcCameraPos;
        }

        private void Init()
        {
            m_Aspect = (float)Screen.width / Screen.height;
            m_WorldHeight = GetComponent<Camera>().orthographicSize;// * 2;
            m_WorldWidth = m_WorldHeight * m_Aspect;
            Debug.Log($"m_WorldWidth : {m_WorldWidth}, m_WorldHeight : {m_WorldHeight}");
        }
    }
}
