using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("bubbleTouched", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("bubbleTouched") == 1)
        {
            GameObject[] player;
            player = GameObject.FindGameObjectsWithTag("Player");
            transform.position = player[0].transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("bubbleTouched", 1);
           /* PlayerPrefs.SetInt("showLog", 1);*/
        }
    }
}
