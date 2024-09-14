using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Monster : MonoBehaviour
{
    GameObject player;
    TestPlayerDrop playerDrop;
    Rigidbody2D playerRigidBody2D;
    BoxCollider2D m_BoxCollider2D;

    [SerializeField] private float[] m_BounceSpeeds;

    private int m_CurrLevel = 0;


    public float moveSpeed;
    //public float bounceSpeed;
    public bool isMonster;

    AudioManager audioManager;
    

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerDrop = player.GetComponent<TestPlayerDrop>();
        playerRigidBody2D = player.GetComponent<Rigidbody2D>();
        m_BoxCollider2D = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isMonster)
        {
            gameObject.transform.position += new Vector3(moveSpeed,0,0);
        }
        if (gameObject.transform.position.x > GameSceneManager.Instance.WorldWidth - m_BoxCollider2D.size.x / 2 || gameObject.transform.position.x < -GameSceneManager.Instance.WorldWidth - m_BoxCollider2D.size.x / 2)
        {
            moveSpeed *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            if (isMonster)
            {
                if (playerDrop.state == State.Att)
                {
                    Destroy(gameObject);
                    playerDrop.state = State.Idle;
                    playerRigidBody2D.velocity = new Vector2(0f, 1f);
                    audioManager.PlaySFX(audioManager.attacksound);
                }
                else if (playerDrop.state == State.Idle)
                {
                    playerDrop.state = State.Hit;
                    playerRigidBody2D.velocity = new Vector2(0f, m_BounceSpeeds[m_CurrLevel]);
                    audioManager.PlaySFX(audioManager.trampoline);
                }
            }

            else
            {
                if (playerDrop.state == State.Idle || playerDrop.state == State.Att)
                {
                    playerDrop.state = State.Hit;
                    playerRigidBody2D.velocity = new Vector2(0f, m_BounceSpeeds[m_CurrLevel]);
                    audioManager.PlaySFX(audioManager.trampoline);
                }
            }
            

        }

    }

    public void LevelUp()
    {
        if (m_CurrLevel >= m_BounceSpeeds.Length - 1)
            return;
        m_CurrLevel++;
    }

    public int HasLevelCount() { return m_BounceSpeeds.Length; }
}
