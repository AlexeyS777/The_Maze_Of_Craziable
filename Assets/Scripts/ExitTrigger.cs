using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExitTrigger : MonoBehaviour
{
    [SerializeField] public bool automaticExit = true;

    public TextMeshProUGUI levelTitle;
    public LevelMenu menu;
    public int nextScene;
    public bool foneMusicIndicator = false;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //levelTitle.gameObject.SetActive(true);
            StartCoroutine(NextLevel());
        }
    }

    private IEnumerator NextLevel()
    {
        yield return null;
        if (foneMusicIndicator) GameManager.gameManager.StopMusicIndicator = foneMusicIndicator;

        if (automaticExit)
        {
            menu.SaveLevelGold();
            menu.StartNewGame(GameManager.gameManager.IndexOfScene + 1);
        }
        else 
        {
            menu.SaveLevelGold();
            menu.StartNewGame(nextScene);
        }        
    }
}
