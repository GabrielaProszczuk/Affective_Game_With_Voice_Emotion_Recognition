using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class MonsterController : MonoBehaviour
{

    public Text emotionText;
    public GameObject emotionPlate;
    public GameObject emotionSlider;
    public Button exitEmotionPlateButton;

    public GameObject monster;

    /*to be taken from python*/
    private int value = 0;

    private string prediction;
    

    // Start is called before the first frame update
    void Start()
    {
        exitEmotionPlateButton.onClick.AddListener(exitEmotionPlate);
        PlayerPrefs.SetInt("exitEmotionPlate", 0);
        PlayerPrefs.SetString("emotionLevel1", "");
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("level") == 1)
        {
            if (PlayerPrefs.GetInt("AI") == 1)
            {               
                if (PlayerPrefs.GetInt("cast") == 1)
                {
                    emotionPlate.SetActive(true);
                    emotionText.text = "To fight the monster be angry!" + "\nTry your best!";
                    PlayerPrefs.SetInt("cast", 0);
                    emotionSlider.GetComponent<Slider>().value = 0;

                }
                prediction = PlayerPrefs.GetString("emotionLevel1");
                if (prediction != "")
                {
                    Debug.Log("new pred lvl 1: " + prediction);
                    value = Int32.Parse(prediction);
                    emotionSlider.GetComponent<Slider>().value = value;
                }
                else
                {
                    emotionSlider.GetComponent<Slider>().value = 0;
                }
                if (PlayerPrefs.GetInt("monsterTouched") == 1)
                {
                    emotionSlider.GetComponent<Slider>().value = value;
                    if (value >= 50)
                    {
                        emotionText.text = "Congratulations! Crush the monster!";
                        PlayerPrefs.SetInt("monsterTouched", 0);

                    }
                    else if (value < 50 && prediction != "")
                    {
                        emotionText.text = "You lost the fight!" + "\n 'Cast' again!";
                        PlayerPrefs.SetInt("monsterDefeated", 0);
                        //   PlayerPrefs.SetInt("moveToStart", 1);
                        PlayerPrefs.SetInt("resetPred", 1);
                        //   PlayerPrefs.SetInt("monsterTouched", 0);

                        PlayerPrefs.SetInt("moveToStart", 1);
                    }
                }
            }
        }
       

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerPrefs.GetInt("level") == 1)
        {

            if (collision.gameObject.tag == "Player")
            {
                if (PlayerPrefs.GetInt("AI") == 1)
                {
                    PlayerPrefs.SetInt("monsterTouched", 1);
                    monster.GetComponent<Renderer>().material.color = new Color(5, 2, 2);

                    // Destroy(monster);

                    /*get Color value from voice and python*/


                    //emotionSlider.GetComponent<Slider>().value = value;


                    if (emotionSlider.GetComponent<Slider>().value >= 50)
                    {
                        Debug.Log("set value: " + value);
                        Destroy(monster);
                        PlayerPrefs.SetInt("resetPred", 1);
                        emotionPlate.SetActive(false);
                        PlayerPrefs.SetInt("exitEmotionPlate", 1);
                        PlayerPrefs.SetInt("monsterDefeated", 1);
                     //   PlayerPrefs.SetString("emotionLevel1", "");
                        value = 0;
                        emotionSlider.GetComponent<Slider>().value = value;
                        PlayerPrefs.SetInt("monsterTouched", 0);
                        PlayerPrefs.SetInt("resetEmotion", 1);
                        Debug.Log("in stop recording 2");
                        // PlayerPrefs.SetInt("detect", 0);
                    }
                    else if (value < 50 && prediction != "")
                    {
                        PlayerPrefs.SetInt("monsterTouched", 1);
                        PlayerPrefs.SetInt("monsterDefeated", 0);

                    }
                }
                else
                {
                    PlayerPrefs.SetInt("monsterTouched", 1);
                    PlayerPrefs.SetInt("monsterDefeated", 0);
                }

            }
        }
    }

    public void exitEmotionPlate()
    {
        int canExit = PlayerPrefs.GetInt("exitEmotionPlate");
        Debug.Log(canExit);
        if (canExit == 1)
        {
            emotionPlate.SetActive(false);
            PlayerPrefs.SetInt("exitEmotionPlate", 0);
        }
        else
        {
            emotionText.text = "To fight the monster be angry!" + "\nReach 50% to exit!";
        }

    }

}
