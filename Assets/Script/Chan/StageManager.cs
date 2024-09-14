using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

namespace Chan
{

    [Serializable]
    public struct Stage_Background
    {
        public Transform BGHandle;
        public Vector3 StartPos;
        public Vector3 Offset;
        public Vector3 Size;
        public int Count;
    }
    

    public class StageManager : MonoBehaviour
    {
        [SerializeField] private Stage_Background m_BackgroundSettings;
        [SerializeField] private GameObject m_Player;
        [SerializeField] private Monster[] m_Monsters;
        [Tooltip("Min, Max")]
        [SerializeField] private float _SpawnStartYPoint;
        [SerializeField] private int m_SplitCount;
        [SerializeField] private float[] m_Ratio;

        private int m_CurrYPoint = 0;
        private int m_CurrLevel = 0;
        private int m_CreateCount = 1;
        void Start()
        {
            Init();
        }

        // Update is called once per frame
        void Update()
        {
            CreateObstacle();
        }
        private void Init()
        {
            



            float t_LastPos = GenerateBackground(4);
            float t_SpawnPoint = _SpawnStartYPoint;
            float t_SplitCount = m_SplitCount;
            m_Ratio = new float[m_SplitCount];
            m_Ratio[0] = t_SpawnPoint;
            for (int i = 1; i < m_SplitCount; i++) 
            {
                //t_SpawnPoint -= Random.Range(m_SpawnPosRange[0], m_SpawnPosRange[1]);
                //m_Ratio[i] = t_SpawnPoint;
                m_Ratio[i] = Mathf.Lerp(_SpawnStartYPoint, t_LastPos, i / t_SplitCount);
            }
            GameObject t_End = Instantiate(Resources.Load<GameObject>("End"));
            t_End.transform.position = Vector3.up * (t_LastPos+10);

        }
        /// <summary>
        /// 한 쌍이 기본
        /// </summary>
        /// <param name="_Count"></param>
        private float GenerateBackground(int _Count)
        {
            Vector3 t_DeployPos = Vector3.zero;

            GameObject t_BG = Instantiate(Resources.Load<GameObject>("BG_Sky"));
            t_BG.transform.position = m_BackgroundSettings.StartPos;
            t_DeployPos = m_BackgroundSettings.StartPos;

            for (int i = 0; i < 2* m_BackgroundSettings.Count - 1; i++)
            {
                t_BG = Instantiate(Resources.Load<GameObject>("BG_Sky"));
                if(i % 2 == 0)
                {
                    t_BG.GetComponent<SpriteRenderer>().flipX = true;
                }
                t_DeployPos += m_BackgroundSettings.Offset;
                t_BG.transform.position = t_DeployPos;
            }
            return t_DeployPos.y;
        }
        private void CreateObstacle()
        {
            if (m_CurrYPoint >= m_Ratio.Length) return;
            if (m_Player.transform.position.y < m_Ratio[m_CurrYPoint])
            {
                //if(m_CurrYPoint > m_SplitCount / 2)
                //{
                //    m_CreateCount = 2;
                //}

                for (int i = 0; i < m_CreateCount; i++)
                {
                    //GameObject t_Obs = Instantiate(Resources.Load<GameObject>("OBS_A"));
                    Monster t_Obs = Instantiate(m_Monsters[Random.Range(0, m_Monsters.Length)]);
                    //if (m_CurrYPoint > m_SplitCount / 2)
                    //    t_Obs.bounceSpeed = 60;
                    // 상수는 오브젝트의 생성 위치를 카메라에서 포착되지 않기 위해 넣은 값임.
                    Vector3 t_TempVec = Vector3.up * (m_Player.transform.position.y - 13);
                    t_TempVec.x = Random.Range(-3.0f, 3.0f);
                    t_Obs.transform.position = t_TempVec;
                    m_CurrYPoint++;
                }
            }
        }

    }
}
