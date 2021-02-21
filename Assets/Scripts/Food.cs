using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "DDD")
        {
            ScoreKeeper.DDDFood += 1;
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            ScoreKeeper.KirbyFood += 1;
            Destroy(gameObject);
        }
    }
}
