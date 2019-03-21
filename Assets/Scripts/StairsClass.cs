using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsClass : MonoBehaviour
{
    private BoxCollider2D ground;
    
    private void Climb(Collider2D playerCollider)
    {
        float moveVertical = Input.GetAxisRaw("Vertical");
        float yVelocity = playerCollider.gameObject.GetComponent<Rigidbody2D>().velocity.y;

        if (!Mathf.Approximately(moveVertical, 0))
        {
            playerCollider.gameObject.GetComponent<PlayerController>().setState(PlayerState.Ladder);
        }
        else if (Mathf.Approximately(yVelocity, 0))
        {
            playerCollider.gameObject.GetComponent<PlayerController>().setState(PlayerState.Ground);
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Animator>().SetBool("climb", true);
            Climb(other);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

            ground = collision.gameObject.GetComponent<BoxCollider2D>();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Animator>().SetBool("climb", false);
            other.gameObject.GetComponent<PlayerController>().setState(PlayerState.Ground);
        }
    }
}
