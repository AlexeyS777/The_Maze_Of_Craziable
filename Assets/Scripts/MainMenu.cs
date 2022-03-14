using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if(UNITY_EDITOR)
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    public AudioClip mainMenuMusic;

    // Start is called before the first frame update
    void Start()
    {
        StartLevel();
    }

    public virtual void StartLevel()
    {
        if (Cursor.visible == false)
        {
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
        }

        GameManager.gameManager.PlayFhoneMusic(mainMenuMusic);
        GameManager.gameManager.StopMusicIndicator = true;
    }
    
    public void StartNewGame(int value)
    {
        if(GameManager.gameManager.IndexOfScene == 1) GameManager.gameManager.ResetGold();
        GameManager.gameManager.IndexOfScene = value;
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        #if (UNITY_EDITOR)
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif

    }
}
