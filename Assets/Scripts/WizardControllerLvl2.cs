using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WizardControllerLvl2 : MonoBehaviour
{
    public Text emotionText;
    public GameObject emotionPlate;
    public GameObject emotionSlider;
    public Button exitEmotionPlateButton;

    public GameObject wizard;

    /*to be taken from python*/
    private int value = 0;

    private string prediction;
    private int tmp_pred;

    // Start is called before the first frame update
    void Start()
    {
        exitEmotionPlateButton.onClick.AddListener(exitEmotionPlate);
        PlayerPrefs.SetInt("exitEmotionPlate", 0);
        PlayerPrefs.SetInt("wizardTouchedFirstTime", 1);
      //  PlayerPrefs.SetInt("cleanValue", 1);
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerPrefs.GetInt("level") == 2)
        {
            if (PlayerPrefs.GetInt("AI") == 1)
            {
                /*    if (PlayerPrefs.GetInt("clean") == 1)
                    {

                        Debug.Log("should be clear");
                        emotionSlider.GetComponent<Slider>().value = 0;
                        value = 0;
                    }*/

                prediction = PlayerPrefs.GetString("emotionLevel2");

                if (PlayerPrefs.GetInt("resetEmotion") == 1)
                {
                    Debug.Log("in reset");
                    emotionSlider.GetComponent<Slider>().value = 0;
                    value = 0;
                }
                else
                {
                    if (prediction != "")
                    {
                        Debug.Log("new pred lvl2: " + prediction);
                        value = Int32.Parse(prediction);
                        emotionSlider.GetComponent<Slider>().value = value;
                    }
                    else
                    {
                        value = 0;
                        emotionSlider.GetComponent<Slider>().value = 0;
                    }

                }

                if (PlayerPrefs.GetInt("cast") == 1)
                {
                    emotionPlate.SetActive(true);
                    emotionText.text = "To save the wizards use happiness!" + "\nTry your best!";
                    PlayerPrefs.SetInt("cast", 0);
                    
                    // PlayerPrefs.SetInt("cleanValue", 1);
                }


                /*  prediction = PlayerPrefs.GetString("emotionLevel2");*/



                //  emotionSlider.GetComponent<Slider>().value = tmp_pred;
                if (PlayerPrefs.GetInt("wizardTouched") == 1)
                {
                    if (value >= 50)
                    {
                        emotionText.text = "Congratulations! Save the wizard!";
                        PlayerPrefs.SetInt("wizardTouched", 0);
                        PlayerPrefs.SetInt("wizardCatched", 1);
                        PlayerPrefs.SetInt("resetEmotion", 0);

                    }
                    else if (emotionSlider.GetComponent<Slider>().value < 50 && prediction != "")
                    {
                        emotionText.text = "You have to try harder!" + "\n 'Cast' again!";
                        PlayerPrefs.SetInt("wizardCatched", 0);
                        PlayerPrefs.SetString("emotionLevel2", "");
                        PlayerPrefs.SetInt("wizardTouched", 0);

                    }
                }
            }
            else
            {
                emotionSlider.GetComponent<Slider>().value = 0;
            }
            
        }
       
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerPrefs.GetInt("level") == 2)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (PlayerPrefs.GetInt("AI") == 1)
                {
                    PlayerPrefs.SetInt("wizardTouched", 1);
                    PlayerPrefs.SetInt("resetEmotion", 1);
                    wizard.GetComponent<Renderer>().material.color = new Color(5, 2, 2);

                    if (emotionSlider.GetComponent<Slider>().value >= 50)
                    {
                        Destroy(wizard);
                        emotionPlate.SetActive(false);
                        PlayerPrefs.SetInt("exitEmotionPlate", 1);
                        PlayerPrefs.SetInt("wizardCatched", 1);
                        PlayerPrefs.SetString("emotionLevel2", "");
                        value = 0;
                        emotionSlider.GetComponent<Slider>().value = value;
                        PlayerPrefs.SetInt("resetPred", 1);
                        PlayerPrefs.SetInt("wizardTouched", 0);
                    }
                    else if (emotionSlider.GetComponent<Slider>().value < 50 && prediction != "")
                    {
                        PlayerPrefs.SetInt("wizardTouched", 1);


                    }             
                }
                else
                {
                    PlayerPrefs.SetInt("wizardCatched", 1);
                    Destroy(wizard);
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
            value = 0;
            emotionSlider.GetComponent<Slider>().value = value;
            PlayerPrefs.SetInt("exitEmotionPlate", 0);
        }
        else
        {
            emotionText.text = "Be happy to attract the wizard!" + "\nReach 50% to exit!";
        }

    }
}
