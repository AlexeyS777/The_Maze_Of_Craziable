using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelMenu : MainMenu  //INHERITANCE
{
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private GameObject infoScreen;
    [SerializeField] private GameObject[] healthContainer;
    [SerializeField] private Image[] health;

    private int levelGold = 0;
    private int maxHealth;
    private int playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        StartLevel();
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = false;
    }

    public override void StartLevel() // POLYMORPHISM
    {        
        if(GameManager.gameManager.StopMusicIndicator)
        {
            GameManager.gameManager.PlayFhoneMusic(mainMenuMusic);
            GameManager.gameManager.StopMusicIndicator = false;
        }

        goldText.text = $"Gold: {GameManager.gameManager.PlayerGold()}";

        maxHealth = GameManager.gameManager.playerHealth;
        playerHealth = maxHealth;

        if (infoScreen.activeSelf)
        {
            GameManager.gameManager.gamePause = true;
        }
        else
        {
            InicializingHealth();
        }
    }
    
    public void AddGold(int value)
    {
        levelGold += value;
        goldText.text = $"Gold: {GameManager.gameManager.PlayerGold() + levelGold}";
    }
    
    public void SaveLevelGold()
    {
        GameManager.gameManager.PlayerGold(levelGold);
    }

    public void CloseInfoScreen()
    {
        infoScreen.SetActive(false);
        InicializingHealth();
        GameManager.gameManager.gamePause = false;
    }

    public void InicializingHealth()
    {
        for (int i = 0; i < maxHealth; i++)
        {
            healthContainer[i].SetActive(true);
        }
    }

    public void SetPlayerHealth(bool value)
    {
        if (!value && playerHealth > 0)
        {
            Debug.Log("decrise health");
            playerHealth -= 1;
            health[playerHealth].gameObject.SetActive(false);

            if (playerHealth == 0) StartCoroutine(RestartLevel());
        }
        else if(value && playerHealth < maxHealth )
        {            
            health[playerHealth].gameObject.SetActive(true);
            playerHealth += 1;
        }
    }

    private IEnumerator RestartLevel()
    {
        GameManager.gameManager.gamePause = true;
        yield return new WaitForSeconds(2);
        GameManager.gameManager.gamePause = false;
        StartNewGame(GameManager.gameManager.IndexOfScene);
    }
}
