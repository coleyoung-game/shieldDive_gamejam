using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chan
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float m_DownSpeed;
        private bool m_IsUp = false;
        private bool m_IsStop = false;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (m_IsStop)
                return;
            m_IsUp = Input.GetKey(KeyCode.Space);
            
            transform.Translate(m_IsUp ? Vector3.up * m_DownSpeed : Vector3.down * m_DownSpeed);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ground"))
            {
                m_IsStop = true;
                audioManager.PlaySFX(audioManager.princessyay);
            }
        }

    }
}
