using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadLobby : MonoBehaviour
{
    AsyncOperation asyncOperation;
    public Image loadBar;
    public TextMeshProUGUI barText;
    private static int sceneID = 1;         //ENCAPSULATION
    private static bool fmVolumeDirection = false;

    public static int SceneID{ set {sceneID = value; } }
    //public static bool FoneMusicVolume { set { fmVolumeDirection = value; } }

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        Screen.orientation = ScreenOrientation.LandscapeLeft; 
        StartCoroutine(LoadSceneCor());
    }
    private IEnumerator LoadSceneCor()
    {
        yield return new WaitForSeconds(1f);

        if (GameManager.gameManager != null)
        {
            if(GameManager.gameManager.StopMusicIndicator)
            {
                GameManager.gameManager.StopFoneMusic();
                yield return new WaitForSeconds(2f);
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
            
            sceneID = GameManager.gameManager.IndexOfScene;
        }

        asyncOperation = SceneManager.LoadSceneAsync(sceneID); 

        while (!asyncOperation.isDone)
        {
            float progress = asyncOperation.progress / 0.9f;
            loadBar.fillAmount = progress;
            barText.text = "Loading ... " + string.Format("{0:0}%",progress * 100f);
            yield return 0;
        } 
    }    
}
