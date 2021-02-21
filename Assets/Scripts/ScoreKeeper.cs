using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    public static int DDDFood = 0;
    public static int KirbyFood = 0;
    public static int winner = 0;
    public static int Level = 0;

    public Text ScoreDDD;
    public Text ScoreKriby;
    public Text Decide;
    public GameObject kirby;

    public GameObject Retrybutton;
    public GameObject Nextlevelbutton;
    public GameObject Winbutton;
    public GameObject MMbutton;
    public GameObject Exitbutton;


    // Start is called before the first frame update
    void Start()
    {
        Retrybutton.SetActive(false);
        Nextlevelbutton.SetActive(false);
        Winbutton.SetActive(false);
        MMbutton.SetActive(false);
        Exitbutton.SetActive(false);
        KirbyFood = 0;
        DDDFood = 0;
        winner = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreDDD.text = "Dedede: " + DDDFood;
        ScoreKriby.text = "Kirby: " + KirbyFood;
        if (winner == 1)
        {
            if (DDDFood > KirbyFood)
            {
                Decide.text = "Winner is Dedede!";
                Retrybutton.SetActive(true);
                MMbutton.SetActive(true);
                Exitbutton.SetActive(true);
                kirby.GetComponent<KirbyController>().enabled = false;
            }
            if (KirbyFood > DDDFood)
            {
                if (Level == 2)
                {
                    Decide.text = "Winner is Kirby!";
                    Winbutton.SetActive(true);
                }
                else
                {
                    Decide.text = "Winner is Kirby!";
                    Nextlevelbutton.SetActive(true);
                }
            }
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Next()
    {
        SceneManager.LoadScene("Level 2");
        Level = 2;
    }
    public void Win()
    {
        SceneManager.LoadScene("WinScreen");
    }
    public void Quitgame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
