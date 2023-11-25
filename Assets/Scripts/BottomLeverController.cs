using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomLeverController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("bottomLeverDown", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerPrefs.GetInt("bottomLeverDown") == 0)
        {
            GetComponent<Animator>().SetBool("isTouchingLever", true);
            PlayerPrefs.SetInt("bottomLeverDown", 1);
            StartCoroutine(wait(2));
        }
    }

    IEnumerator wait(float waitTime)
    {
        float counter = 0;

        while (counter < waitTime)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        PlayerPrefs.SetInt("showPlatform", 1);
    }
}
