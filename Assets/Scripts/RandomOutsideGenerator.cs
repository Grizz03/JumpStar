using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomOutsideGenerator : MonoBehaviour
{
    public float timeBetweenObjects;
    private float objectGenCounter;

    public GameObject[] objects;

    public Transform minPoint;
    public Transform maxPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._canMove)
        {

            objectGenCounter -= Time.deltaTime;

            if (objectGenCounter <= 0)
            {
                int chosenObject = Random.Range(0, objects.Length);

                Vector3 genPoint = new Vector3(Random.Range(minPoint.position.x, maxPoint.position.y), transform.position.y, transform.position.z);

                Instantiate(objects[chosenObject], genPoint, Quaternion.Euler(0f, Random.Range(-45f, 45f), 0f));

                objectGenCounter = Random.Range(timeBetweenObjects * 0.50f, timeBetweenObjects * 1.5f);

            }
        }
    }
}
