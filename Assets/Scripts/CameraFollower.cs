using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System;
using System.Linq;
using UnityEngine.UI;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public Transform showLog;
    public Transform showPlatform;
    public float smoothSpeed = 5000.0F;
    private Vector3 velocity = Vector3.zero;
    public float xLimitMin;
    public float xLimitMax;
    public float yLimitMin;
    public float yLimitMax;


    void Start()
    {

    /*    PlayerPrefs.SetInt("level", 1);
        PlayerPrefs.SetInt("AI", 1);*/

    }


    void LateUpdate()
    {
        if (PlayerPrefs.GetInt("level") == 1)
        {
            if (PlayerPrefs.GetInt("showPlatform") == 1)
            {
                Vector3 positionNow2 = transform.position;
                float desiredPositionY2 = showPlatform.position.y;
                if (desiredPositionY2 >= 265)
                {
                    desiredPositionY2 = 265;
                }
                if (desiredPositionY2 <= -550)
                {
                    desiredPositionY2 = -550;
                }
                Vector3 desiredPosition2 = new Vector3(positionNow2.x, desiredPositionY2, positionNow2.z);
                transform.position = Vector3.SmoothDamp(positionNow2, desiredPosition2, ref velocity, smoothSpeed);
                StartCoroutine(wait(4));
            }
            else
            {
                Vector3 positionNow = transform.position;
                float desiredPositionY = target.position.y;
                if (desiredPositionY >= 256)
                {
                    desiredPositionY = 256;
                }
                if (desiredPositionY <= -570)
                {
                    desiredPositionY = -570;
                }
                Vector3 desiredPosition = new Vector3(positionNow.x, desiredPositionY, positionNow.z);
                transform.position = Vector3.SmoothDamp(positionNow, desiredPosition, ref velocity, smoothSpeed);
            }
        }
        else
        {
            Vector3 positionNow = transform.position;
            float desiredPositionY = target.position.y;
            if (desiredPositionY >= yLimitMax)
            {
                desiredPositionY = yLimitMax;
            }
            if (desiredPositionY <= yLimitMin)
            {
                desiredPositionY = yLimitMin;
            }

            float desiredPositionX = target.position.x;
            if (desiredPositionX >= xLimitMax)
            {
                desiredPositionX = xLimitMax;
            }
            if (desiredPositionX <= xLimitMin)
            {
                desiredPositionX = xLimitMin;
            }

            Vector3 desiredPosition = new Vector3(desiredPositionX, desiredPositionY, positionNow.z);
            transform.position = Vector3.SmoothDamp(positionNow, desiredPosition, ref velocity, smoothSpeed);
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
        /*PlayerPrefs.SetInt("endShowLog", 1);*/
        PlayerPrefs.SetInt("showPlatform", 0);
    }

}
