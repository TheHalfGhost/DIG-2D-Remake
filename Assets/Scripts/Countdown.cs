using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public Text Counttext;
    public float Down = 4;
    public GameObject kirby;

    // Start is called before the first frame update
    void Start()
    {
        kirby.GetComponent<KirbyController>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Down -= Time.deltaTime;

        if(Down <= 4)
        {
            Counttext.text = "3";
        }
        if(Down <= 3)
        {
            Counttext.text = "2";
        }
        if (Down <= 2)
        {
            Counttext.text = "1";
        }
        if(Down <= 1)
        {
            Counttext.text = "GO!";
            kirby.GetComponent<KirbyController>().enabled = true;
        }
        if(Down <= 0)
        {
            Destroy(gameObject);
        }
    }
}