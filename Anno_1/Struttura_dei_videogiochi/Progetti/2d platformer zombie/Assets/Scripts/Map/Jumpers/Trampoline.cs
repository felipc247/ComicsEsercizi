using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jump_force;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        { 
            // zeroing velocity to avoid accidental multiple collsion
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector2(rb.velocity.x, rb.velocity.y + jump_force), ForceMode2D.Impulse);
            
        }

    }
}
