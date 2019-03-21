using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleController : MonoBehaviour
{
    private Vector2 origPosition;
    private SpriteRenderer sprite;
    private ParticleSystem particles;
    
    internal Rigidbody2D body;
    internal Collider2D box;
    internal bool thrown;

    private void Start()
    {
        origPosition = transform.localPosition;    
        particles = GetComponent<ParticleSystem>();
        sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            sprite.enabled = false;
            body.velocity = Vector2.zero;
            particles.Play();
            thrown = false;
        }
    }
    
    private void OnDisable()
    {
        gameObject.transform.localPosition = origPosition;
        sprite.enabled = true;
        thrown = true;
    }
}
