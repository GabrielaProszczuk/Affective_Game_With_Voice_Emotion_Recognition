using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ChangeColorsController : MonoBehaviour
{

    public Color[] colors;
    private string[] emotions = new string[] { "sad", "angry", "happy" };
    float timeLeft;
    Color targetColor;
    private string currentEmotion;
    private string finalEmotion;
    public Text emotionText;
    public GameObject emotionPlate;
    public GameObject emotionSlider;
    private bool changeColors;
    public GameObject flower;
    public Button exitEmotionPlateButton;

    /*to be taken from python*/
    private int value = 0;

    private string prediction;

    // Start is called before the first frame update
    void Start()
    {
        changeColors = true;
        exitEmotionPlateButton.onClick.AddListener(exitEmotionPlate);
        PlayerPrefs.SetInt("exitEmotionPlate", 0);
        PlayerPrefs.SetInt("flowerTouched", 0);
        PlayerPrefs.SetString("selectedEmotion", "");
        PlayerPrefs.SetString("emotionLevel3", "");
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("level") == 3)
        {
            if (changeColors == true)
            {
                int randomNumber = UnityEngine.Random.Range(0, 3);
                currentEmotion = emotions[randomNumber];
                if (timeLeft <= Time.deltaTime)
                {
                    GetComponent<SpriteRenderer>().color = targetColor;
                    targetColor = colors[randomNumber];
                    timeLeft = 2.0f;
                }
                else
                {
                    GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, targetColor, Time.deltaTime / timeLeft);
                    timeLeft -= Time.deltaTime;
                }
            }

            if (PlayerPrefs.GetInt("AI") == 1)
            {

                prediction = PlayerPrefs.GetString("emotionLevel3");
                Debug.Log("prediction lvl3: " + prediction);
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
                        Debug.Log("new pred lvl3: " + prediction);
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
                    //  emotionText.text = "To save the wizards use happiness!" + "\nTry your best!";
                    PlayerPrefs.SetInt("cast", 0);
                    emotionSlider.GetComponent<Slider>().value = 0;
                    PlayerPrefs.SetString("emotionLevel3", "");
                }

                
             //   Debug.Log("new pred lvl3: " + prediction);
         /*       if (prediction != "")
                {
                    value = Int32.Parse(prediction);
                    emotionSlider.GetComponent<Slider>().value = value;
                }
                else
                {
                    emotionSlider.GetComponent<Slider>().value = 0;
                }*/
                if (PlayerPrefs.GetInt("flowerTouched") == 1 && prediction != "")
                {

                  //  emotionSlider.GetComponent<Slider>().value = value;
                    if (emotionSlider.GetComponent<Slider>().value >= 50)
                    {
                        Debug.Log("predction lvl3 detected: " + prediction);
                        value = Int32.Parse(prediction);
                        emotionSlider.GetComponent<Slider>().value = value;
                        emotionText.text = "Congratulations! Select the flower!";
                        PlayerPrefs.SetInt("flowerTouched", 0);
                        PlayerPrefs.SetString("selectedEmotion", "");
                        PlayerPrefs.SetInt("resetEmotion", 0);
                    }
                    else if (emotionSlider.GetComponent<Slider>().value < 50 && prediction != "")
                    {
                        emotionText.text = "You have to try harder!" + "\n 'Cast' again!";
                        PlayerPrefs.SetString("emotionLevel3", "");
                        PlayerPrefs.SetInt("flowerTouched", 0);
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
        if (PlayerPrefs.GetInt("level") == 3)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (PlayerPrefs.GetInt("AI") == 1)
                {
                    finalEmotion = currentEmotion;
                    emotionText.text = "Selected emotion: " + finalEmotion + "\nTry your best!";
                    PlayerPrefs.SetInt("flowerTouched", 1);
                    PlayerPrefs.SetString("selectedEmotion", finalEmotion);


                    changeColors = false;

                    if (emotionSlider.GetComponent<Slider>().value >= 50)
                    {
                        // emotionText.text = "Congratulations! Select the flower!";
                        Debug.Log("set value: " + value);
                        Destroy(flower);
                        emotionPlate.SetActive(false);
                        PlayerPrefs.SetInt("exitEmotionPlate", 1);
                        PlayerPrefs.SetInt("flowersLvl3", PlayerPrefs.GetInt("flowersLvl3") + 1);
                        PlayerPrefs.SetString("emotionLevel3", "");
                        value = 0;
                        emotionSlider.GetComponent<Slider>().value = value;
                        PlayerPrefs.SetInt("flowerTouched", 0);
                        PlayerPrefs.SetInt("resetEmotion", 1);
                     //   Debug.Log("in stop recording 2");
                        PlayerPrefs.SetInt("resetPred", 1);
                        PlayerPrefs.SetString("selectedEmotion", "");

                        // PlayerPrefs.SetInt("detect", 0);
                    }
                    else if (value < 50 && prediction != "")
                    {
                        PlayerPrefs.SetInt("flowerTouched", 1);
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("flowersLvl3", PlayerPrefs.GetInt("flowersLvl3") + 1);
                    Destroy(flower);
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
            emotionText.text = "Selected emotion: " + finalEmotion + "\nReach 50% to exit!";
        }
        
    }
}
