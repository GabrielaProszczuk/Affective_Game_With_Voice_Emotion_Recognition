using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleLvl3Controller : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > 450)
        {
            transform.position = startingPosition;
        }

        if(transform.position.y > -883 && transform.position.y < 470)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
        }

        if (transform.position.y >= 310)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.position = startingPosition;
            if(gameObject.tag == "Hazard")
            {
                Debug.Log("toxic bubble");
            }
        }
    }
}
