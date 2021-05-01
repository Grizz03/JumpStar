using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject mainScreen;
    public GameObject switchingScreen;

    public string levelToLoad;

    public Transform theCamera;
    public Transform charSwitchHolder;

    private Vector3 camTargetPos;

    public float cameraSpeed;

    public GameObject[] theChars;
    public int currentCharacter;

    public GameObject switchPlayButton;
    public GameObject switchUnlockButton;
    public GameObject switchGetCoinsButton;
    public int currentCoins;

    public GameObject charLockedImage;

    public Text coinsText;

    // Start is called before the first frame update
    void Start()
    {
        camTargetPos = theCamera.position;

        if (!PlayerPrefs.HasKey(theChars[0].name))
        {
            PlayerPrefs.SetInt(theChars[0].name, 1);
        }


        if (PlayerPrefs.HasKey("coinsCollected"))
        {
            currentCoins = PlayerPrefs.GetInt("coinsCollected");
        }
        else
        {
            PlayerPrefs.SetInt("CoinsCollected", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        theCamera.position = Vector3.Lerp(theCamera.position, camTargetPos, cameraSpeed * Time.deltaTime);

        coinsText.text = "Coins: " + currentCoins;

#if UNITY_EDITOR

        if(Input.GetKeyDown(KeyCode.L))
        {
            for (int i = 1; i < theChars.Length; i++)
            {
                PlayerPrefs.SetInt(theChars[i].name, 0);
            }
        }

#endif
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void ChooseChar()
    {
        mainScreen.SetActive(false);
        switchingScreen.SetActive(true);

        camTargetPos = theCamera.position + new Vector3(0f, charSwitchHolder.position.y, 0f);

        UnlockedCheck();
    }

    public void moveLeft()
    {
        if (currentCharacter > 0)
        {
            camTargetPos += new Vector3(2f, 0f, 0);
            currentCharacter--;

            UnlockedCheck();
        }
    }

    public void MoveRight()
    {
        if (currentCharacter < theChars.Length - 1)
        {
            camTargetPos -= new Vector3(2f, 0f, 0);
            currentCharacter++;

            UnlockedCheck();
        }
    }

    public void UnlockedCheck()
    {
        if (PlayerPrefs.HasKey(theChars[currentCharacter].name))
        {
            if (PlayerPrefs.GetInt(theChars[currentCharacter].name) == 0)
            {
                switchPlayButton.SetActive(false);

                charLockedImage.SetActive(true);

                if (currentCoins < 500)
                {
                    switchGetCoinsButton.SetActive(true);
                    switchUnlockButton.SetActive(false);

                }
                else
                {
                    switchUnlockButton.SetActive(true);
                    switchGetCoinsButton.SetActive(false);
                }
            }
            else
            {
                switchPlayButton.SetActive(true);

                charLockedImage.SetActive(false);

                switchGetCoinsButton.SetActive(false);
                switchUnlockButton.SetActive(false);
            }
        }
        else
        {
            PlayerPrefs.SetInt(theChars[currentCharacter].name, 0);

            UnlockedCheck();
        }
    }

    public void UnlockChar()
    {
        currentCoins -= 500;

        PlayerPrefs.SetInt(theChars[currentCharacter].name, 1);
        PlayerPrefs.SetInt("CoinsCollected", currentCoins);

        UnlockedCheck();
    }

    public void SelectCharacter()
    {
        PlayerPrefs.SetString("SelectedChar", theChars[currentCharacter].name);

        PlayGame();
    }

}
