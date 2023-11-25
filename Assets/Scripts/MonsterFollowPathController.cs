using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFollowPathController : MonoBehaviour
{
    public Transform leftBoundry;
    public Transform rightBoundry;

    bool goLeft;
    float deltaX;
    float change;

    // Start is called before the first frame update
    void Start()
    {
        if ((transform.position.x - leftBoundry.transform.position.x) < (rightBoundry.transform.position.x - transform.position.x))
        {
            goLeft = false;
        }
        deltaX = 0f;
        change = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 positionNow = transform.position;
        float positionLeftX = leftBoundry.transform.position.x;
        float positionRightX = rightBoundry.transform.position.x;
        float positionLeftY = leftBoundry.transform.position.y;
        float positionRightY = rightBoundry.transform.position.y;

        if (positionLeftY < positionRightY && positionLeftX < positionRightX)
        {
            change = 10f;
        }
        else if (positionLeftY >= positionRightY)
        {
            change = -10f;
        }

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
            transform.position = transform.position + new Vector3(deltaX, 0, 0);
        }
        else if (goLeft == false)
        {
            deltaX = 40 * Time.deltaTime;
            transform.position = transform.position + new Vector3(deltaX, 0, 0);
        }

        transform.position = new Vector3(transform.position.x, (positionLeftY + positionRightY)/2 + change, 0);

        /* transform.position = new Vector3((positionLeftX + positionRightX)/2, (leftBoundry.transform.position.y + rightBoundry.transform.position.y)/2, 0);*/

    }
}
