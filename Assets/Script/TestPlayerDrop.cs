using System.Collections;
using System.Collections.Generic;
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
    Rigidbody2D rb;

    #region Y 값 제한 로직
    private bool m_IsClamp = false;
    private float m_CurrTime = 0.0f;
    private float m_MaxTime = 1.0f;
    #endregion~Y 값 제한 로직

    public float maxFallSpeed;
    public float gravityValue;
    public float attSpeed;
    public float maxdodgeCooltime;
    float dodgecool;


    public float sideSpeed;

    public State state;


    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;
        dodgecool = 0;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2 (0f,0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (dodgecool > 0)
        {
            dodgecool -= Time.deltaTime;
            if (dodgecool < maxdodgeCooltime - 1f && state == State.Dodge) 
            {
                state = State.Idle;
            }
        }

        if (rb.velocity.y > -maxFallSpeed)
        {
            rb.velocity += new Vector2(0, -gravityValue);
        }

        if (state == State.Idle || state == State.Dodge)
        {
            if (Input.GetKey(KeyCode.A))
            {
                Vector2 temp = rb.velocity;
                temp.x = -sideSpeed;
                rb.velocity = temp;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Vector2 temp = rb.velocity;
                temp.x = sideSpeed;
                rb.velocity = temp;
            }
            else
            {
                Vector2 temp = rb.velocity;
                temp.x = 0f;
                rb.velocity = temp;
            }



            if (Input.GetKey(KeyCode.Space))
            {
                state = State.Att;
                rb.velocity = new Vector2 (0f, -attSpeed);
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (dodgecool <= 0)
                {
                    state = State.Dodge;
                    dodgecool = maxdodgeCooltime;
                }
                

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
        }
        if (collision.CompareTag("Clamp"))
        {
            rb.velocity = Vector2.zero;
        }
    }

}
