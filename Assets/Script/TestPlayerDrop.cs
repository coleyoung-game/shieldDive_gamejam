using System.Collections;
using System.Collections.Generic;
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
    public float maxFallSpeed;
    public float gravityValue;
    public float attSpeed;


    public float sideSpeed;

    public State state;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Idle;

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2 (0f,0f);

    }

    // Update is called once per frame
    void Update()
    {

        if (rb.velocity.y > -maxFallSpeed)
        {
            rb.velocity += new Vector2(0, -gravityValue);
        }

        if (state == State.Idle)
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

}
