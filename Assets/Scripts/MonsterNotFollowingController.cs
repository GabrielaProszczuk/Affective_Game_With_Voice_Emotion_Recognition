using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterNotFollowingController : MonoBehaviour
{
    bool goRight;
    float deltaX;



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


    }

}
