using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterNotFollowingController : MonoBehaviour
{
    bool goRight;
    float deltaX;


  /*  public Text emotionText;
    public GameObject emotionPlate;
    public GameObject emotionSlider;
    public Button exitEmotionPlateButton;

    public GameObject monster;*/


    // Start is called before the first frame update
    void Start()
    {
        goRight = true;
        deltaX = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "EnemyLocal_lvl1" && PlayerPrefs.GetInt("showPlatform") == 0)
        {
            if (transform.position.x <= -120)
            {
                goRight = true;
            }
            else if (transform.position.x >= 100)
            {
                goRight = false;
            }

            if (goRight == true)
            {
                deltaX = 40 * Time.deltaTime;
                transform.position = transform.position + new Vector3(deltaX, 0,  0);
            }
            else if (goRight == false)
            {
                deltaX = -40 * Time.deltaTime;
                transform.position = transform.position + new Vector3(deltaX, 0, 0);
            }
        }

/*        prediction = PlayerPrefs.GetString("emotionLevel");
        if (prediction != "")
        {
            value = Int32.Parse(prediction);
            Debug.Log("val: " + value);
            emotionSlider.GetComponent<Slider>().value = value;
        }
        if (PlayerPrefs.GetInt("monsterTouched") == 1)
        {
            emotionSlider.GetComponent<Slider>().value = value;
            if (value >= 70)
            {
                emotionText.text = "Congratulations! Crush the monster!";

            }
            else if (value < 70 && prediction != "")
            {
                emotionText.text = "You lost the fight!" + "\n 'Cast' again!";
                PlayerPrefs.SetInt("monsterDefeated", 0);
                PlayerPrefs.SetInt("moveToStart", 1);
                PlayerPrefs.SetString("emotionLevel", "");
            }
        }*/
    }

   /* void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("monsterTouched", 1);

            // Destroy(monster);

            *//*get Color value from voice and python*//*


            //emotionSlider.GetComponent<Slider>().value = value;


            if (value >= 70)
            {
                Debug.Log("set value: " + value);
                Destroy(monster);
                emotionPlate.SetActive(false);
                PlayerPrefs.SetInt("exitEmotionPlate", 1);
                PlayerPrefs.SetInt("monsterDefeated", 1);
                PlayerPrefs.SetString("emotionLevel", "");
                emotionSlider.GetComponent<Slider>().value = value;
                PlayerPrefs.SetInt("monsterTouched", 0);
                // PlayerPrefs.SetInt("detect", 0);
            }
            else if (value < 70 && prediction != "")
            {
                PlayerPrefs.SetInt("monsterTouched", 1);
            }
        }
    }*/
}
