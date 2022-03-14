using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    private AudioSource foneMusic;
    private int sceneIndex = 1;
    private int health = 3;
    private int gold = 0;
    private bool stopMusic = false;

    public int IndexOfScene { get { return sceneIndex; } set { sceneIndex = value; } }
    public bool StopMusicIndicator { get {return stopMusic; } set {stopMusic = value; } }    
    public int playerHealth { get { return health; } set { health = value; } }

    public bool gamePause { get; set; }

    private void Awake()
    {
        if(gameManager != null)
        {
            Destroy(gameObject);
            return;
        }

        gameManager = this;
        foneMusic = GetComponent<AudioSource>();
        foneMusic.volume = 0;
        DontDestroyOnLoad(gameObject);
    }

    //_______________________________________________________________________________________
    //                              GAME STATS
    //_______________________________________________________________________________________
    public void PlayerGold(int value)
    {
        gold += value;
    }

    public int PlayerGold()
    {
        return gold;
    }

    public void ResetGold()
    {
        gold = 0;
    }

    //_______________________________________________________________________________________
    //                              GAME SETTINGS
    //_______________________________________________________________________________________

    //_______________________________________________________________________________________
    //                              AUDIO
    //_______________________________________________________________________________________
    public void PlayFhoneMusic(AudioClip clip)
    {
        foneMusic.clip = clip;
        foneMusic.Play();
        StartCoroutine(ChangeFoneMusicVolume(true));
    }

    public void StopFoneMusic()
    {
        StartCoroutine(ChangeFoneMusicVolume(false));
    }

    private IEnumerator ChangeFoneMusicVolume(bool direction)
    {
        if (direction)
        {
            while (foneMusic.volume < 1)
            {
                foneMusic.volume += 0.1f;
                yield return new WaitForSeconds(0.2f);
            }
        }
        else
        {
            while (foneMusic.volume > 0)
            {
                foneMusic.volume -= 0.1f;                
                yield return new WaitForSeconds(0.2f);
            }

            foneMusic.Stop();
            foneMusic.clip = null;
        }
    }
}
