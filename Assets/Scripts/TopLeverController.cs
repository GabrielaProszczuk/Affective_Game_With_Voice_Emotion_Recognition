using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLeverController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("topLeverDown", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<Animator>().SetBool("isTouchingLever", true);
            PlayerPrefs.SetInt("topLeverDown", 1);
        }
    }
}
