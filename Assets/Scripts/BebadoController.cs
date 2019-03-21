using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BebadoController : MonoBehaviour
{
    public BottleController bottle;
    public float range = 4;

    private Rigidbody2D rb;
    private Vector2 velocity;
    private float origPositionX;
    private bool turned;
    private GameObject target;
    private BoxCollider2D box;

    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        origPositionX = transform.position.x;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player");
        velocity = new Vector2(10, rb.velocity.y);
    }

    void Update()
    {
        rb.velocity = velocity;

        if (transform.position.x > origPositionX + range && !turned)
        {
            turned = true;
            velocity *= -1;
        }

        if ((transform.position.x < origPositionX - range) && turned)
        {
            velocity *= -1;
            turned = false;
        }

        var distanceVector = transform.position - target.transform.position;

        if (Mathf.Abs(distanceVector.x) < 35 && Mathf.Abs(distanceVector.y) < 1 && !bottle.thrown)
        {
            Physics2D.IgnoreCollision(box, bottle.box);
            bottle.gameObject.SetActive(true);
            bottle.body.AddForce(new Vector2(-distanceVector.x * 50, 500));
            bottle.thrown = true;
        }
    }
}