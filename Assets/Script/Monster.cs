using System;
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
    private Animator m_Anim;
    private SpriteRenderer m_SpriteRenderer;

    [SerializeField] private float[] m_BounceSpeeds;

    private IEnumerator IE_EffectAnimHandle = null;

    [SerializeField] private int m_CurrLevel = 0;
    [SerializeField] private bool m_IsDestroy;

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
        m_Anim = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
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
            m_SpriteRenderer.flipX = moveSpeed > 0;
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
                    Debug.Log($"playerRigidBody2D.velocity : {playerRigidBody2D.velocity}");
                    audioManager.PlaySFX(audioManager.spike);
                }
            }

            else
            {
                if (playerDrop.state == State.Idle || playerDrop.state == State.Att)
                {
                    if (IE_EffectAnimHandle != null)
                        return;
                    StartCoroutine(IE_EffectAnimHandle = IE_EffectAnim());
                }
            }
            

        }

    }
    private IEnumerator IE_EffectAnim()
    {
        bool t_IsAttack = playerDrop.state == State.Att;
        playerDrop.state = State.Hit;
        playerRigidBody2D.velocity = new Vector2(0f, t_IsAttack ? m_BounceSpeeds[m_CurrLevel] * 2 : m_BounceSpeeds[m_CurrLevel]);
        if (gameObject.tag == "Bounce")
        {
            audioManager.PlaySFX(audioManager.trampoline);
        }
        else if (gameObject.tag == "Bomb")
        {
            audioManager.PlaySFX(audioManager.bombblow2);
        }
        m_Anim.SetBool("IsAction", true);
        yield return new WaitUntil(() => m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Action") && m_Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.98f);
        if (m_IsDestroy)
            Destroy(gameObject);
        else
        {
            m_Anim.SetBool("IsAction", false);
            IE_EffectAnimHandle = null;
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
