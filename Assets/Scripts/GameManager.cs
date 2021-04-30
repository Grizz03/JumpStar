using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("coinsCollected"))
        {
            coinsCollected = PlayerPrefs.GetInt("coinsCollected");
        }
        increaseSpeedCounter = timeToIncreaseSpeed;
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
        }

        //increase speed over time
        if (canMove)
        {
            increaseSpeedCounter -= Time.deltaTime;
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
        }
    }
}
