using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonController : MonoBehaviour
{

    public GameObject Canvas;
    public GameObject Fog;
    public Button button;
    public Color wantedColor;
    private Color originalColor;
    private ColorBlock cb;

    public Text gameText;

    // Start is called before the first frame update
    void Start()
    {
        cb = button.colors;
        originalColor = cb.selectedColor;
        button.onClick.AddListener(TaskOnClick);
        Debug.Log("in next level");

        if(PlayerPrefs.GetInt("AI") == 0)
        {
            if(PlayerPrefs.GetInt("level") == 1)
            {
                gameText.text = "Your mission here is to gather all magical flowers while not getting eaten by monsters! Try to avoid traps and come back to finish platform.";

            }else if(PlayerPrefs.GetInt("level") == 2)
            {
                gameText.text = "Other wizards need your help! A curse has been cast upon them and they are stuck in a sad dreams with no ability to move. Save them all!";
            }
            else
            {
                gameText.text = "Hello there! Try collecting all wildflowers to complete this level. Remeber to avoid toxic acid bubbles that are mixed up with completely safe ones.";
                 
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("level") == 1)
            {
                gameText.text = "Your mission here is to gather all magical flowers while not getting eaten by monsters! Try to avoid traps and come back to finish platform. Remeber! When fighting with monsters use as much anger as possible - command \"Fight!\".";
            }
            else if (PlayerPrefs.GetInt("level") == 2)
            {
                gameText.text = "Other wizards need your help! A curse has been cast upon them and they are stuck in a sad dreams with no ability to move. To find them and save them using your cheerfull 'Save wizard!' command";
            }
            else
            {
                gameText.text = "Hello there! Try collecting all wildflowers to complete this level. Remeber to avoid toxic acid bubbles that are mixed up with completely safe ones. All flowers change their emotions in time - to safely gather them use command 'Take' in the same tone as flower presents, otherwise it will not work!";
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeWhenHover()
    {
        cb.selectedColor = wantedColor;
        button.colors = cb;
    }

    public void changeWhenLeaves()
    {
        cb.selectedColor = originalColor;
        button.colors = cb;
    }

    void TaskOnClick()
    {
        Debug.Log("ahopj");
        Canvas.SetActive(false);
        Fog.SetActive(false);
    }
}