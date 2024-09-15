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
        private bool m_IsVibe = false;

        private void Start()
        {
            Init();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                StartCoroutine(IE_CameraShake(0.05f, 7, 0.2f));
            }
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            if (m_IsVibe)
                return;
            m_CalcCameraPos = Vector3.up * (m_PlayerPos.position.y + m_CameraPos.y > 0 ? 0:m_PlayerPos.position.y + m_CameraPos.y);
            if (m_CalcCameraPos.y < -341.05F)
            {
                m_CalcCameraPos = new Vector3(0, -341.05f, -1);
            }
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

        public void CameraShake(float _IterTime, int _Count, float _Power)
        {
            StartCoroutine(IE_CameraShake( _IterTime, _Count, _Power));
        }

        private IEnumerator IE_CameraShake(float _IterTime, int _Count, float _Power)
        {
            m_IsVibe = true;
            int t_CurrCount = 0;
            while (t_CurrCount <=_Count)
            {
                yield return new WaitForSeconds(_IterTime);
                t_CurrCount++;
                m_CalcCameraPos = Vector3.up * (m_PlayerPos.position.y + m_CameraPos.y > 0 ? 0 : m_PlayerPos.position.y + m_CameraPos.y);
                m_CalcCameraPos.z = -1;
                transform.position = new Vector3(Random.Range(-_Power, _Power), m_CalcCameraPos.y + Random.Range(-_Power, _Power), -1);
            }
            m_IsVibe = false;
        }
    }
}
