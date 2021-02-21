using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDDPath : MonoBehaviour
{
    [SerializeField]
    Transform[] waypoints;
    [SerializeField]
    float moveSpeed = 5f;
    int waypointIndex = 0;
    public static float Down = 4;
    private bool Hammer = true;
    public float Swing = 1;
    Animator anim;
    public ParticleSystem DustEffect;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        transform.position = waypoints[waypointIndex].transform.position;
        Hammer = false;
        Down = 4;
    }

    // Update is called once per frame
    void Update()
    {
        Down -= Time.deltaTime;
        if (Down <= 1)
        {
            Move();
            HammerTime();
            DustEffect.Play();
        }
        if( Down >= 1)
        {
            DustEffect.Stop();
        }
        Swing -= Time.deltaTime;
        if (Swing <= 1)
        {
            Hammer = false;
        }
        Debug.Log(Hammer);
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
        if(transform.position == waypoints[waypointIndex].transform.position)
        {
            waypointIndex += 1;
        }
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
        anim.SetInteger("DDDState", 1);
        DustEffect.Play();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "End")
        {
            Down = 999;
            ScoreKeeper.winner = 1;
            anim.SetInteger("DDDState", 0);
            DustEffect.Stop();
        }
    }
    void OnCollisionEnter2D(Collision2D collide)
    {
        
        if (collide.gameObject.tag == "Box")
        {
            Destroy(collide.gameObject, 1);
            Hammer = true;
            Swing = 2;
        }
    }
    void HammerTime()
    {
        if (Hammer == true)
        {
            anim.SetInteger("DDDState", 2);
        }
        else
        {
            anim.SetInteger("DDDState", 1);
        }
    }
}
