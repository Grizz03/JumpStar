using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool canMove;
    static public bool _canMove;
    private bool gameStarted;
    public float worldSpeed;
    static public float _worldSpeed;
    public int coinsCollected;
    private bool coinHitThisFrame;
    //speeding up
    public float timeToIncreaseSpeed;
    private float increaseSpeedCounter;
    public float speedMultiplier;
    private float targetSpeedMultiplier;
    public float acceleration;
    public float speedIncreaseAmount;
    private float accelerationStore;
    private float worldSpeedStore;
    public GameObject tapMessage;
    public Text coinsText;
    public Text distanceText;
    private float distanceCovered;

    public GameObject deathScreen;
    public Text deathScreenCoins;
    public Text deathScreenDistance;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("coinsCollected"))
        {
            coinsCollected = PlayerPrefs.GetInt("coinsCollected");
        }
        increaseSpeedCounter = timeToIncreaseSpeed;

        targetSpeedMultiplier = speedMultiplier;
        worldSpeedStore = worldSpeed;
        accelerationStore = acceleration;

        coinsText.text = "Coins: " + coinsCollected;
        distanceText.text = distanceCovered + "m";
    }

    // Update is called once per frame
    void Update()
    {
        _canMove = canMove;
        _worldSpeed = worldSpeed;

        if (!gameStarted && Input.GetMouseButtonDown(0))
        {
            canMove = true;
            _canMove = true;

            gameStarted = true;

            tapMessage.SetActive(false);
        }

        //increase speed over time
        if (canMove)
        {
            increaseSpeedCounter -= Time.deltaTime;
            if (increaseSpeedCounter <= 0)
            {
                increaseSpeedCounter = timeToIncreaseSpeed;

                //worldSpeed = worldSpeed * speedMultiplier;
                targetSpeedMultiplier = targetSpeedMultiplier * speedIncreaseAmount;

                timeToIncreaseSpeed = timeToIncreaseSpeed * 0.97f;
            }
            acceleration = accelerationStore * speedMultiplier;
            speedMultiplier = Mathf.MoveTowards(speedMultiplier, targetSpeedMultiplier, acceleration * Time.deltaTime);
            worldSpeed = worldSpeedStore * speedMultiplier;

            //updating ui
            distanceCovered += Time.deltaTime * worldSpeed;
            distanceText.text = Mathf.Floor(distanceCovered) + "m";
        }


        coinHitThisFrame = false;
    }

    public void HitHazard()
    {
        canMove = false;
        _canMove = false;

        PlayerPrefs.SetInt("coinsCollected", coinsCollected);
    }

    public void AddCoin()
    {
        if (!coinHitThisFrame)
        {
            coinsCollected++;
            coinHitThisFrame = true;

            coinsText.text = "Coins: " + coinsCollected;
        }
    }
}
