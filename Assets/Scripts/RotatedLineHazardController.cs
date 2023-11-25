using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatedLineHazardController : MonoBehaviour
{
    public Transform customPivot;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("topLeverDown") == 1)
        {
            transform.RotateAround(customPivot.position, Vector3.back, 20 * Time.deltaTime);
        }
    }

}
