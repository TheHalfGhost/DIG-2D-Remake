/* Written by Fisher Hensley for UCF DIG-4715 Group 10.
   This script is used to control Kirby. */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirbyController : MonoBehaviour
{
    public float speed;
    public float speedMod;
    public float jump;
    public float inhaleDist;
    public float jumpMod;
    public float inputTime;
    public float slideMod;
    public float slideCutoff;
    public static bool projectile;
    public GameObject projectilePrefabR;
    public GameObject projectilePrefabL;
    private int buttonTime;
    private int testValue;
    private float horizontal;
    private float lastInput;
    private float speedApplied;
    private float slide;
    private bool floating;
    private Rigidbody2D rb2D; // Kirby's Rigibody
    private Collider2D kCollider; // Kibry's 2D Collider
    private Collider2D gCollider; // The Tilemap's Collider
    Vector2 lookDirection = new Vector2(1,0);

    void Start() // runs at the start of the program.
    {
        rb2D = GetComponent<Rigidbody2D>();
        kCollider = GetComponent<Collider2D>();
        gCollider = GameObject.FindGameObjectWithTag("Ground").GetComponent<Collider2D>();
        floating = false;
        projectile = false;
        lastInput = 0;
        speedApplied = 1;
    }

    void Update() // Used for input and other tasks.
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A)) // For Reference - http://aidtech-game.com/double-tap-button-unity3d/#.YCRR9WhKiUl
        {
            if (Time.time - lastInput < inputTime)
            {
                speedApplied = speedMod;
            }
            else
            {
                speedApplied = 1;
            }
            lastInput = Time.time;
        }
        horizontal = Input.GetAxis("Horizontal") * speed * speedApplied;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (floating == false) 
            {
                rb2D.velocity = Vector2.up * jump;
            }
            if (floating == true)
            {
                rb2D.velocity = Vector2.up * jump / jumpMod;
            }
        }
        if (kCollider.IsTouching(gCollider) == false)
        {
            floating = true;
        }
        else
        {
            floating = false;
        }
        
        Vector2 move = new Vector2(horizontal, 0);
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (projectile == false)
            {
                RaycastHit2D hit = Physics2D.Raycast(rb2D.position + Vector2.up * 0.2f, lookDirection, inhaleDist, LayerMask.GetMask("Boxes"));
                if (hit.collider != null)
                {
                    hit.collider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    hit.collider.GetComponent<Rigidbody2D>().AddForce(new Vector2(lookDirection.x * -20, 0));
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (projectile == true)
            {
                if (lookDirection.x == 1)
                {
                    GameObject projectileRight = Instantiate(projectilePrefabR, rb2D.position + lookDirection * 2.5f, Quaternion.identity);
                    projectile = false;
                }
                if (lookDirection.x == -1)
                {
                    GameObject projectileLeft = Instantiate(projectilePrefabL, rb2D.position + lookDirection * 2.5f, Quaternion.identity);
                    projectile = false;
                }
            }
        }
        if (Input.GetKey(KeyCode.S) && speedApplied > 1)
        {
            Debug.Log ("Sliding");
            if (move.x > slideCutoff)
            {
                slide = slideMod * lookDirection.x * -1;
            }
        }
        else
        {
            slide = 0;
        }
    }
    void FixedUpdate() // Used for physics calculations.
    {
        rb2D.AddForce(new Vector2(horizontal, 0));
        rb2D.AddForce(new Vector2(slide, 0));
    }
    void OnTriggerEnter2D(Collider2D collision) // Used so that Kirby can trigger objects.
    {
        
    }
    void OnCollisionEnter2D(Collision2D collide) // Used for collisions with Kirby
    {
        if (collide.gameObject.tag == "Box")
        {
            if (Input.GetKey(KeyCode.E))
            {
                projectile = true;
                Destroy(collide.gameObject);
            }
        }
        if (Input.GetKey(KeyCode.S) && speedApplied > 1 && collide.gameObject.tag == "Box")
        {
            Debug.Log ("Sliding w/Deletion");
            Destroy(collide.gameObject);
        }
    }

    // Teacher's Code
    /*  private bool facingRight = true;
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
            void Flip()
        {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   } */
}