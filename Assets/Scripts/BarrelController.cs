using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speedX;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = (new Vector2(15 * speedX, 0));
    }
}
