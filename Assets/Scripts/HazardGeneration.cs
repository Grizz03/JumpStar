using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardGeneration : MonoBehaviour
{
    public GameObject[] hazards;
    public float timeBetweenHazards;
    private float hazardGenCounter;

    // Start is called before the first frame update
    void Start()
    {
        hazardGenCounter = timeBetweenHazards;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._canMove)
        {

            hazardGenCounter -= Time.deltaTime;

            if (hazardGenCounter <= 0)
            {
                int chosenHazard = Random.Range(0, hazards.Length);

                Instantiate(hazards[chosenHazard], transform.position, Quaternion.Euler(0f, Random.Range(-45f, 45f), 0f));

                hazardGenCounter = Random.Range(timeBetweenHazards * 0.50f, timeBetweenHazards * 1.5f);

            }

        }
    }
}
