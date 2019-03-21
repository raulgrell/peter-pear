using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState
{
    Air,
    Ground,
    Ladder
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpStrength;
    public CardSlot currentSlot;

    private Rigidbody2D rb;
    private bool jumped;
    private PlayerState state;
    private GameObject groundToIgnore;
    private new BoxCollider2D collider;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private bool idle;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        state = PlayerState.Ground;
    }

    public void setState(PlayerState newState)
    {
        state = newState;
    }
    
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && state == PlayerState.Ground)
        {
            jumped = true;
            state = PlayerState.Air;
        }
        
        switch (state)
        {
            case PlayerState.Ground:
                rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
                if(Mathf.Approximately(rb.velocity.x, 0) && idle)
                {
                    anim.SetBool("walking", true);
                    spriteRenderer.flipX = rb.velocity.x < 0;
                    idle = false;
                }
                else if (!idle)
                {
                    anim.SetBool("walking", false);
                    idle = true;
                }
                break;
            case PlayerState.Air:
                if (jumped) {
                    rb.AddForce(new Vector2(moveHorizontal * speed, jumpStrength));
                    jumped = false;
                }
                break;
            case PlayerState.Ladder:
                rb.velocity = new Vector2(0, moveVertical * speed);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Vector3 hit = collision.contacts[0].normal;
            
            if (hit.y > 0)
            {
                jumped = false;
                state = PlayerState.Ground;
            }
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            state = PlayerState.Air;
        }
    }
}
