using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogToReleaseMonstersController : MonoBehaviour
{
    public Transform customPivot;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("bubbleTouched") == 1)
        {
            if(transform.rotation.z < 0.6)
            {
                transform.RotateAround(customPivot.position, Vector3.forward, 20 * Time.deltaTime);
            }
            
        }
    }
}
