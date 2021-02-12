using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileL : MonoBehaviour
{
    Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(-30.0f, 0.0f);
    }
    void OnCollisionEnter2D(Collision2D collide)
    {
        if (collide.gameObject.tag == "Box")
        {
            Destroy(collide.gameObject);
        }
        Destroy(gameObject);
    }
}
