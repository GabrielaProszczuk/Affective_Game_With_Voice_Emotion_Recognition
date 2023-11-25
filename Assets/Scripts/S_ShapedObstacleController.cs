using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ShapedObstacleController : MonoBehaviour
{
    public Transform customPivot;
    public bool back = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(back == true)
        {
            transform.RotateAround(customPivot.position, Vector3.back, 20 * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(customPivot.position, Vector3.forward, 20 * Time.deltaTime);
        }
        

    }
}
