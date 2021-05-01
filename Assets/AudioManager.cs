using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource menuMusic;
    public AudioSource gameMusic;
    public AudioSource gameOverMusic;

    public AudioSource sfxCoin;
    public AudioSource sfxJump;
    public AudioSource sfxHit;

    public GameObject mutedImage;

    public bool soundMuted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SoundOnOff()
    {
        if (soundMuted)
        {
            mutedImage.SetActive(false);
            soundMuted = false;
        }else
        {
            mutedImage.SetActive(true);
            soundMuted = true;
        }
    }
}
