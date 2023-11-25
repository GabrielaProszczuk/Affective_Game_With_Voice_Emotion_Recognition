using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : MonoBehaviour
{
    public Transform sawLeft;
    public Transform sawRight;

    bool goLeft;
    float deltaX;

    // Start is called before the first frame update
    void Start()
    {
        if((transform.position.x - sawLeft.transform.position.x) < ( sawRight.transform.position.x - transform.position.x))
        {
            goLeft = false;
        }
        deltaX = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionNow = transform.position;
        float positionLeftX = sawLeft.transform.position.x + 10f;
        float positionRightX = sawRight.transform.position.x - 10f;


        if (positionNow.x <= positionLeftX)
        {
            goLeft = false;
        }
        else if (positionNow.x >= positionRightX)
        {
            goLeft = true;
        }

        if (goLeft == true)
        {
            deltaX = -40 * Time.deltaTime;
            transform.position = positionNow + new Vector3(deltaX, 0, 0);
        }
        else if (goLeft == false)
        {
            deltaX = 40 * Time.deltaTime;
            transform.position = positionNow + new Vector3(deltaX,0, 0);
        }
    }
}
