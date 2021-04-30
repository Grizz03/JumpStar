using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGeneration : MonoBehaviour
{
    // public GameObject pathObject;

    public GameObject pathObject;

    public Transform thresholdPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z < thresholdPoint.position.z)
        {
            //copy pathObject if true
            // Instantiate(pathObject, transform.position, transform.rotation);
            // transform.position += new Vector3(0f, 0f, 3.2f);

            //Random path generation
    
            Instantiate(pathObject, transform.position, transform.rotation);
            transform.position += new Vector3(0f, 0f, 3.2f);
        }
    }
}
