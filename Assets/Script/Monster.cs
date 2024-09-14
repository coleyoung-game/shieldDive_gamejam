using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Monster : MonoBehaviour
{
    GameObject player;
    TestPlayerDrop playerDrop;
    Rigidbody2D playerRigidBody2D;

    public float moveSpeed;
    public float bounceSpeed;
    public bool isMonster;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerDrop = player.GetComponent<TestPlayerDrop>();
        playerRigidBody2D = player.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isMonster)
        {
            gameObject.transform.position += new Vector3(moveSpeed,0,0);
        }
        if (gameObject.transform.position.x > 4.5f || gameObject.transform.position.x < -4.5f)
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
                }
                else if (playerDrop.state == State.Idle)
                {
                    playerDrop.state = State.Hit;
                    playerRigidBody2D.velocity = new Vector2(0f, bounceSpeed);
                }
            }

            else
            {
                playerDrop.state = State.Hit;
                playerRigidBody2D.velocity = new Vector2(0f, bounceSpeed);
            }
            

        }

    }
}
