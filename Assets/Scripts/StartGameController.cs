using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameController : MonoBehaviour
{
    public Button buttonWithAI;
    public Button buttonNoAI;
    public GameObject loadingPlate;

    // Start is called before the first frame update
    void Start()
    {
        buttonWithAI.onClick.AddListener(runGameWithAI);
        buttonNoAI.onClick.AddListener(runGameNoAI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void runGameWithAI()
    {
        Debug.Log("with AI");
        PlayerPrefs.SetInt("level", 1);
        PlayerPrefs.SetInt("AI", 1);
        loadingPlate.SetActive(true);
        SceneManager.LoadScene("Scenes/Level_1");
    }

    public void runGameNoAI()
    {
        Debug.Log("no AI");
        PlayerPrefs.SetInt("level", 1);
        PlayerPrefs.SetInt("AI", 0);
        loadingPlate.SetActive(true);
        SceneManager.LoadScene("Scenes/Level_1");
    }
}
