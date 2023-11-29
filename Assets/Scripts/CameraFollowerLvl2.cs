using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowerLvl2 : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5000.0F;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
    }

    void LateUpdate()
    {
        Vector3 positionNow = transform.position;
        float desiredPositionY = target.position.y;
        float desiredPositionX = target.position.x;

        Vector3 desiredPosition = new Vector3(desiredPositionX, desiredPositionY, positionNow.z);
        transform.position = Vector3.SmoothDamp(positionNow, desiredPosition, ref velocity, smoothSpeed);
    }

}
