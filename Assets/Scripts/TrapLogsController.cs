using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLogsController : MonoBehaviour
{

    public Transform customPivot;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("logDown", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "logdrop" + PlayerPrefs.GetInt("logDown"))
        {
            if (transform.rotation.z > 0)   /*#whikle if want to jump to 90deg*/
            {
                transform.RotateAround(customPivot.position, Vector3.back, 60 * Time.deltaTime);
            }

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Debug.Log("standing!");
            StartCoroutine(wait(1));


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

        if(gameObject.tag == "logdrop1")
        {
            PlayerPrefs.SetInt("logDown", 1);
        }
        if (gameObject.tag == "logdrop2")
        {
            PlayerPrefs.SetInt("logDown", 2);
        }
        if (gameObject.tag == "logdrop3")
        {
            PlayerPrefs.SetInt("logDown", 3);
        }
        if (gameObject.tag == "logdrop4")
        {
            PlayerPrefs.SetInt("logDown", 4);
        }
        if (gameObject.tag == "logdrop5")
        {
            PlayerPrefs.SetInt("logDown", 5);
        }
        if (gameObject.tag == "logdrop6")
        {
            PlayerPrefs.SetInt("logDown", 6);
        }

        /*  if (transform.rotation.z >= 0.7)
          {
              transform.RotateAround(customPivot.position, Vector3.back, 7000 * Time.deltaTime);
          }*/


        counter = 0;

        while (counter < waitTime)
        {
            counter += Time.deltaTime;
            yield return null;
        }

        PlayerPrefs.SetInt("logDown", 0);

      /*  if (transform.rotation.z < 0.7)
        {
            transform.RotateAround(customPivot.position, Vector3.forward, 6000 * Time.deltaTime);
        }*/

    }
}
