using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDownController : MonoBehaviour
{

    bool goDown;
    float deltaY;
    // Start is called before the first frame update
    void Start()
    {
        goDown = false;
        deltaY = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("bottomLeverDown") == 1)
        { 
            
            if(transform.position.y <= 186)
            {
                goDown = false;
            }
            else if (transform.position.y >= 456)
            {
                goDown = true;
            }

            if (goDown == true)
            {
                deltaY = -40 * Time.deltaTime;
                transform.position = transform.position + new Vector3(0, deltaY, 0);
            }
            else if(goDown == false) 
            {
                deltaY = 40 * Time.deltaTime;
                transform.position = transform.position + new Vector3(0, deltaY, 0);
            }
        }
        GameObject[] nextLevelSign;
        nextLevelSign = GameObject.FindGameObjectsWithTag("NextLevelSign1");
        nextLevelSign[0].transform.position = transform.position + new Vector3(40, 25, 0);
    }
}
