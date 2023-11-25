using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPosition : MonoBehaviour
{
    // Start is called before the first frame update

    public int x;
    public int y;
    public int xLimit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < xLimit)
        {
            transform.position = new Vector3(x, y, 0);
        }
    }
}
