using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public enum State
{
    Idle,
    Hit,
    Att,
    Dodge
}

public class TestPlayerDrop : MonoBehaviour
{
    private Animator m_Animator;
    private SpriteRenderer m_SpriteRenderer;
    Rigidbody2D rb;
    BoxCollider2D Boxcollider;

    private IEnumerator IE_OnAttackHandle = null;
    private IEnumerator IE_OnDodgeHandle = null;

    private Color m_DodgeAlpha = new Color(1, 1, 1, 0.5f);
   

    #region Y 값 제한 로직
    private bool m_IsClamp = false;
    private float m_CurrTime = 0.0f;
    private float m_MaxTime = 1.0f;
    #endregion~Y 값 제한 로직

    public float maxFallSpeed;
    public float gravityValue;
    public float attSpeed;
    public float maxdodgeCooltime;
    public float dodgeLength;
    float dodgecool;


    public float sideSpeed;

    public State state;


    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        dodgecool = 0;
        if (m_Animator == null)
            m_Animator = GetComponent<Animator>();
        if(m_SpriteRenderer == null)
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2 (0f,0f);

        Boxcollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dodgecool > -2f)
        {
            dodgecool -= Time.deltaTime;
            if (dodgecool < maxdodgeCooltime - dodgeLength && state == State.Dodge) 
            {
                state = State.Idle;
                m_SpriteRenderer.color = Color.white;
            }
        }
        //maxFallSpeed *= 1.001f;
        DownSpeedUp();
        if (rb.velocity.y > -maxFallSpeed)
        {
            rb.velocity += new Vector2(0, -gravityValue);
        }

        if (state == State.Idle || state == State.Dodge)
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (gameObject.transform.position.x > - (GameSceneManager.Instance.WorldWidth - Boxcollider.size.x/2))
                {
                    m_SpriteRenderer.flipX = false;
                    Vector2 temp = rb.velocity;
                    temp.x = -sideSpeed;
                    rb.velocity = temp;
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (gameObject.transform.position.x < (GameSceneManager.Instance.WorldWidth - Boxcollider.size.x / 2))
                {
                    m_SpriteRenderer.flipX = true;
                    Vector2 temp = rb.velocity;
                    temp.x = sideSpeed;
                    rb.velocity = temp;
                }
            }
            else
            {
                Vector2 temp = rb.velocity;
                temp.x = 0f;
                rb.velocity = temp;
            }


            /// TODO: getkeydown으로 바꿔야될 것 같음. 확인 필요
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnAttack();
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) && IE_OnAttackHandle == null)
            {
                OnDodge();
            }
        }

        else if (state == State.Hit) 
        {
            if (rb.velocity.y < 0f)
            {
                state = State.Idle;
                rb.velocity = new Vector2(0f, 0f);
            }
        }
    }
    private void FixedUpdate()
    {
        if(m_IsClamp)
        {
            m_CurrTime += Time.deltaTime;
            if(m_CurrTime > m_MaxTime)
            {
                m_CurrTime = 0;
                m_IsClamp = false;
            }
        }
        if (transform.position.y > 9 && !m_IsClamp)
        {
            m_IsClamp = true;
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            rb.simulated = false;
            GameObject princess = GameObject.FindWithTag("Princess");
            princess.GetComponent<Animator>().SetTrigger("clap");
        }
        if (collision.CompareTag("Clamp"))
        {
            rb.velocity = Vector2.zero;
        }
    }
    private float m_Offset = -100;
    private void DownSpeedUp()
    {
        if (transform.position.y < m_Offset)
        {
            Debug.Log("DownSpeedUp!");
            m_Offset += -100;
            maxFallSpeed *= 1.1f;
        }
    }

    private void OnAttack()
    {
        if (IE_OnAttackHandle != null)
            return;
        StartCoroutine(IE_OnAttackHandle = IE_OnAttack());
    }

    private IEnumerator IE_OnAttack()
    {
        Vector2 t_PrevVelocity = rb.velocity;
        m_Animator.SetTrigger("IsAttack");
        rb.velocity = Vector2.zero;
        rb.velocity = new Vector2(0f, attSpeed/2);
        yield return new WaitUntil(() => m_SpriteRenderer.sprite.name == "Attack_3");
        state = State.Att;
        rb.velocity = new Vector2(0f, -attSpeed);
        yield return new WaitUntil(() => m_Animator.GetCurrentAnimatorStateInfo(0).IsName("c_Attack") && m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.98f);
        yield return new WaitForSeconds(0.2f);
        state = State.Idle;
        rb.velocity = t_PrevVelocity;
        IE_OnAttackHandle = null;
    }

    private void OnDodge()
    {
        if (IE_OnDodgeHandle != null)
            return;
        state = State.Dodge;
        dodgecool = maxdodgeCooltime;
        m_Animator.SetTrigger("IsDodge");
        m_SpriteRenderer.color = m_DodgeAlpha;
        //StartCoroutine(IE_OnDodgeHandle = IE_OnAttack());
    }

    //private IEnumerator IE_OnDodge()
    //{
    //    state = State.Dodge;
    //    dodgecool = maxdodgeCooltime;
    //    m_Animator.SetTrigger("IsDodge");
    //}

}
