using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelSign : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject Fog;
    public Button NextLevelButton;

    // Start is called before the first frame update
    void Start()
    {
        NextLevelButton.onClick.AddListener(LoadNewLevel);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            /*            if (collision.gameObject.GetComponent<PlayerController>().gathered == 5)
                        {
                            Canvas.SetActive(true);
                            Fog.SetActive(true);
                        }*/

            Canvas.SetActive(true);
            Fog.SetActive(true);
        }

        if(PlayerPrefs.GetInt("level") == 3)
        {
            Debug.Log("finale!");
        }
    }

    private void LoadNewLevel()
    {
        int level = PlayerPrefs.GetInt("level");
        level = level + 1;
        PlayerPrefs.SetInt("level", level);
        SceneManager.LoadScene("Scenes/Level_" + level);
        PlayerPrefs.SetInt("newLevelLoaded", 1);
    }
}
