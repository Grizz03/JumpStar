using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public float deathScreenDelay;
    public string mainMenuName;
    public GameObject notEnoughCoinsScreen;
    public PlayerMovement thePlayer;
    public GameObject pauseScreen;
    public GameObject[] models;
    public GameObject defaultChar;


    // Start is called before the first frame update
    void Start()
    {
        pauseScreen.SetActive(false);
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

        //loads model for character
        for (int i = 0; i < models.Length; i++)
        {
            if (models[i].name == PlayerPrefs.GetString("SelectedChar"))
            {
                GameObject clone = Instantiate(models[i], thePlayer.modelHolder.position, thePlayer.modelHolder.rotation);

                clone.transform.parent = thePlayer.modelHolder;
                Destroy(clone.GetComponent<Rigidbody>());

                defaultChar.SetActive(false);
            }
        }
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

        // deathScreen.SetActive(true);
        deathScreenCoins.text = coinsCollected + " Coins!";
        deathScreenDistance.text = Mathf.Floor(distanceCovered) + "m!";

        StartCoroutine("ShowDeathScreen");
    }

    public IEnumerator ShowDeathScreen()
    {
        yield return new WaitForSeconds(deathScreenDelay);
        deathScreen.SetActive(true);
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

    public void ContinueGame()
    {
        if (coinsCollected >= 100)
        {

            coinsCollected -= 100;

            canMove = true;
            _canMove = true;
            deathScreen.SetActive(false);
            coinsText.text = "Coins: " + coinsCollected;
            thePlayer.ResetPlayer();
        }
        else
        {
            notEnoughCoinsScreen.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GetCoins()
    {

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuName);
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void CloseNotEnoughCoins()
    {
        notEnoughCoinsScreen.SetActive(false);
    }

    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PausedGame()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
