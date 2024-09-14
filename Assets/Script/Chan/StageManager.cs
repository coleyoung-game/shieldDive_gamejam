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
        [SerializeField] private Monster m_dragon;
        [SerializeField] private Transform m_MonsterHadle;
        [Tooltip("Min, Max")]
        [SerializeField] private float _SpawnStartYPoint;
        [SerializeField] private int m_SplitCount;
        [SerializeField] private float[] m_Ratio;

        private int m_CurrYPoint = 0;
        private int m_CurrLevel = 0;
        private int m_CreateCount = 1;

        public float LastYValue;

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
            float t_LastPos = GenerateBackground();
            LastYValue = t_LastPos;
            float t_SpawnPoint = _SpawnStartYPoint;
            float t_SplitCount = m_SplitCount;
            m_Ratio = new float[m_SplitCount];
            m_Ratio[0] = t_SpawnPoint;
            for (int i = 1; i < m_SplitCount; i++) 
            {
                m_Ratio[i] = Mathf.Lerp(_SpawnStartYPoint, t_LastPos + 20,i / t_SplitCount);
            }
            GameObject t_End = Instantiate(Resources.Load<GameObject>("End"));
            t_End.transform.position = Vector3.up * (t_LastPos+10);

        }

        private float easeInQuint(float _Number)
        {
            return 1 - Mathf.Pow(_Number, 4);
        }
        private float easeOutQuart(float _Number)
        {
            return 1 - Mathf.Pow(1 - _Number, 4);
        }

        private float GenerateBackground()
        {
            //Vector3 t_DeployPos = Vector3.zero;
            //
            //GameObject t_BG = Instantiate(Resources.Load<GameObject>("BG_Sky"));
            //t_BG.transform.position = m_BackgroundSettings.StartPos;
            //t_DeployPos = m_BackgroundSettings.StartPos;
            //
            //for (int i = 0; i < 2* m_BackgroundSettings.Count - 1; i++)
            //{
            //    t_BG = Instantiate(Resources.Load<GameObject>("BG_Sky"));
            //    if(i % 2 == 0)
            //    {
            //        t_BG.GetComponent<SpriteRenderer>().flipX = true;
            //    }
            //    t_DeployPos += m_BackgroundSettings.Offset;
            //    t_BG.transform.position = t_DeployPos;
            //}
            //return t_DeployPos.y;
            return -357;
        }
        private void CreateObstacle()
        {
            if (m_CurrYPoint >= m_Ratio.Length) return;
            if (m_Player.transform.position.y < m_Ratio[m_CurrYPoint])
            {
                if (m_CurrYPoint >= m_SplitCount / 2)
                    m_CreateCount = 2;

                for (int i = 0; i < m_CreateCount; i++)
                {
                    //GameObject t_Obs = Instantiate(Resources.Load<GameObject>("OBS_A"));
                    Monster t_Obs = Instantiate(m_Monsters[Random.Range(0, m_Monsters.Length)], m_MonsterHadle);
                    t_Obs.Init();
                    // SplitCount(몬스터 스폰 빈도) / Monster Maxlevel
                    if (m_CurrYPoint % (m_SplitCount / t_Obs.HasLevelCount()) == 0)
                    {
                        //Debug.Log("LEVELUP!");
                        t_Obs.LevelUp();
                    }
                    if (m_CurrYPoint >= m_SplitCount / 2)
                        m_CreateCount = 2;
                    Vector3 t_TempVec = Vector3.up * (m_Player.transform.position.y - 13);
                    
                    if(m_CreateCount > 1)
                    {
                        if(i%2==0)
                            t_TempVec.x = Random.Range(-t_Obs.WidthClamp, -0.5f);
                        else
                            t_TempVec.x = Random.Range(0.5f, t_Obs.WidthClamp);
                    }
                    else
                        t_TempVec.x = Random.Range(-t_Obs.WidthClamp, t_Obs.WidthClamp);



                    t_Obs.transform.position = t_TempVec;
                    m_CurrYPoint++;
                    if (m_CurrYPoint == 30)
                    {
                        Monster t_Drg = Instantiate(m_dragon, m_MonsterHadle);
                        t_Drg.Init();
                    }
                }
            }
        }

    }
}
