using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class PlayerController : MonoBehaviour
{

    public Text livesText;
    public Text gatheredText;
    public Text foughtText;

    private Rigidbody2D rb2D;

    private float moveSpeed;
    private float jumpForce;
    private bool isJumping;
    private float moveHorizontal;
    private float moveVertical;

    public int lives;
    public int gathered;
    public int defeated;

    public GameObject GameOverWindow;
    public Button RetryButton;
    public GameObject Fog;

    private Vector3 startingPosition;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        moveSpeed = 4f;
        jumpForce = 100f;
        isJumping = false;
        lives = 5;
        gathered = 0;
        defeated = 0;
        RetryButton.onClick.AddListener(LoadTheSameLevel);
        PlayerPrefs.SetInt("flowersLvl3", 0);

        startingPosition = transform.position;

        PlayerPrefs.SetInt("canDie", 1);
       
        PlayerPrefs.SetInt("moveToStart", 0);
        if(PlayerPrefs.GetInt("AI") == 0)
        {
            foughtText.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == startingPosition)
        {
            PlayerPrefs.SetInt("canDie", 1);
            Debug.Log("can die again");
        }
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
/*        GetComponent<Animator>().SetBool("idle_right", true);
        GetComponent<Animator>().SetBool("idle_left", false);
        GetComponent<Animator>().SetBool("walk_left", false);
        GetComponent<Animator>().SetBool("walk_right", false);*/
        if(lives == 0)
        {
            StartCoroutine(waitBeforeNewTry(1));
        }

        livesText.text = "Lives: " + lives;
        gatheredText.text = "Gathered: " + gathered + "/5";
        if(PlayerPrefs.GetInt("level") == 1 && PlayerPrefs.GetInt("AI") == 1)
        {
            foughtText.text = "Monsters defeated: " + defeated + "/3";
        }
        

        if (PlayerPrefs.GetInt("level") == 3)
        {
            gathered = PlayerPrefs.GetInt("flowersLvl3");
        }

        if(PlayerPrefs.GetInt("moveToStart") == 1)
        {
            lives = lives - 1;
            Debug.Log("hazard 2!");
            PlayerPrefs.SetInt("canDie", 0);
            StartCoroutine(waitBeforeNewTry(0));
            PlayerPrefs.SetInt("monsterDefeated", -1);
            PlayerPrefs.SetInt("moveToStart",0);
        }

    }

    void FixedUpdate()
    {
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb2D.AddForce(new Vector2(moveHorizontal*moveSpeed, 0f), ForceMode2D.Impulse);
            if (moveHorizontal > 0.1f)
            {
                GetComponent<Animator>().SetBool("walk_right", true);
                GetComponent<Animator>().SetBool("idle_right", true);
                GetComponent<Animator>().SetBool("walk_left", false);
                GetComponent<Animator>().SetBool("idle_left", false);
            }
            if (moveHorizontal < -0.1f)
            {
                GetComponent<Animator>().SetBool("walk_left", true);
                GetComponent<Animator>().SetBool("idle_right", false);
                GetComponent<Animator>().SetBool("walk_right", false);
                GetComponent<Animator>().SetBool("idle_left", true);
            }
        }

        if (moveHorizontal >= -0.1f && moveHorizontal <= 0.1f)
        {
            GetComponent<Animator>().SetBool("walk_left", false);
            GetComponent<Animator>().SetBool("walk_right", false);
        }

        if (moveVertical > 0.1f && !isJumping)
        {
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(PlayerPrefs.GetInt("canDie"));
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Bricks" || collision.gameObject.tag == "logdrop1" || collision.gameObject.tag == "logdrop2" || collision.gameObject.tag == "logdrop3" || collision.gameObject.tag == "logdrop4" || collision.gameObject.tag == "logdrop5" || collision.gameObject.tag == "logdrop6" || collision.gameObject.tag.Contains("Hazard"))
        {
            isJumping = false;
        }

        if (( collision.gameObject.tag == "Hazard") && PlayerPrefs.GetInt("canDie") == 1)
        {
            lives = lives - 1;
            Debug.Log("hazard!");
            PlayerPrefs.SetInt("canDie", 0);
            StartCoroutine(waitBeforeNewTry(1));
            
        }

        if (collision.gameObject.tag == "flower")
        {
            if(gathered >= 5) {
                gathered = 5;
            }
            else {
                gathered = gathered + 1;
            }
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "characterToCatch" && PlayerPrefs.GetInt("wizardCatched") == 1)
        {
            if (gathered >= 5)
            {
                gathered = 5;
            }
            else
            {
                gathered = gathered + 1;
            }
            PlayerPrefs.SetInt("wizardCatched", 0);
        }

   /*     if (collision.gameObject.tag == "flowerLVL3" && PlayerPrefs.GetInt("flowerGathered") == 1)
        {
            lives = lives - 1;
            Debug.Log("hazard!");
            PlayerPrefs.SetInt("canDie", 0);
            StartCoroutine(waitBeforeNewTry(1));
        }*/

        if (((collision.gameObject.tag == "monsterLvl1" || collision.gameObject.tag == "EnemyLocal_lvl1") && PlayerPrefs.GetInt("showPlatform") == 0) && PlayerPrefs.GetInt("canDie") == 1)
        {
            if (PlayerPrefs.GetInt("AI") == 0)
            {
                if (PlayerPrefs.GetInt("monsterDefeated") == 1)
                {
                    PlayerPrefs.SetInt("monsterDefeated", -1);   /*0 not deafeated, 1 defeated, -1 no info yet*/
                    defeated = defeated + 1;
                }
                else if (PlayerPrefs.GetInt("monsterDefeated") == 0)
                {
                    lives = lives - 1;
                    Debug.Log("hazard!");
                    PlayerPrefs.SetInt("canDie", 0);
                    StartCoroutine(waitBeforeNewTry(1));
                    PlayerPrefs.SetInt("monsterDefeated", -1);
                }
            }
            else
            {
                if (PlayerPrefs.GetInt("monsterDefeated") == 1)
                {
                    PlayerPrefs.SetInt("monsterDefeated", -1);   /*0 not deafeated, 1 defeated, -1 no info yet*/
                    defeated = defeated + 1;
                }
                else if (PlayerPrefs.GetInt("monsterDefeated") == 0 && PlayerPrefs.GetString("emotionLevel1") != "" && Int32.Parse(PlayerPrefs.GetString("emotionLevel1")) < 50)
                {
                    lives = lives - 1;
                    Debug.Log("hazard 1!");
                    PlayerPrefs.SetInt("canDie", 0);
                    StartCoroutine(waitBeforeNewTry(1));
                    PlayerPrefs.SetInt("monsterDefeated", -1);
                  //  PlayerPrefs.SetString("emotionLevel1", "");
                }
            }
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Bricks")
        {
            isJumping = true;
        }
    }

    IEnumerator waitBeforeNewTry(float waitTime)
    {
        float counter = 0;

        while (counter < waitTime)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        Vector3 velocity = Vector3.zero;

        transform.position = startingPosition;

        if (lives == 0)
        {
            while (counter < 2)
            {
                counter += Time.deltaTime;
                yield return null;
            }
            Debug.Log("zero ");
            GameOverWindow.SetActive(true);
            Fog.SetActive(true);
        }

        PlayerPrefs.SetInt("canDie", 1);

    }

    public void LoadTheSameLevel()
    {
        Debug.Log("retry clicked");
        int level = PlayerPrefs.GetInt("level");
        lives = 5;
        gathered = 0;
        SceneManager.LoadScene("Scenes/Level_" + level);
    }
}
