using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLvl3Controller : MonoBehaviour
{
    [SerializeField]
    Transform rotationCenter;

    [SerializeField]
    float rotationRadius = 70f, angularSpeed = 2f;

    float posX, posY = 0f;
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius;
        transform.position = new Vector3(posX, posY, 0);
        angle = angle + Time.deltaTime * angularSpeed;

        if(angle >= 360f)
        {
            angle = 0f;
        }


    }
}
