using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float start = 3;

    public Text TimerText;
    float Timer = 0f;

    void Update()
    {
        start -= Time.deltaTime;

        if (start <= 0)
        {
            Timer += Time.deltaTime;

            int seconds = (int)(Timer % 60);
            int minutes = (int)(Timer / 60) % 60;
            int milliseconds = (int)(Timer * 1000f) % 1000;

            string timerString = string.Format("{0:00:}{1:00:}{2:000}", minutes, seconds, milliseconds);
            TimerText.text = timerString;
        }
    }
}
