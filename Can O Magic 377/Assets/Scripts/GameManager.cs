using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject pauseMenuV;
    [SerializeField] private GameObject pauseMenuH;
    [SerializeField] private GameObject powerItemMenu;
    [SerializeField] private GameObject leaderboardMenu;
    [SerializeField] private GameObject vLeaderboardData;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TutorialScript tutorialScript;
    [SerializeField] private PowerItemMenuScript powerItemMenuScript;

    [SerializeField] private GameObject verticalUI;
    [SerializeField] private GameObject horizontalUI;
    [SerializeField] private GameObject verticalButtons;
    [SerializeField] private GameObject horizontalButtons;
    [SerializeField] private GameObject SettingsV;
    [SerializeField] private GameObject SettingsH;
    [SerializeField] private GameObject CreditsH;
    [SerializeField] private GameObject CreditsV;

    [SerializeField] private GameObject CombinationMenuH;
    [SerializeField] private GameObject CombinationMenuV;

    [SerializeField] private GameObject tutorialCanvas;

    public GameObject GameOverScreenV;
    public GameObject GameOverScreenH;
    public GameObject EnterNameButtonV;
    public GameObject EnterNameButtonH;
    public TMP_Text NameV;
    public TMP_Text NameH;

    public bool reactionHappened;
    public string reactionOrb1, reactionOrb2;



    public override void Awake()
    {
        base.Awake();
        GameData.Instance.steam = GameObject.FindGameObjectWithTag("Steam");
        GameData.Instance.playerData = FindAnyObjectByType<PlayerData>();
        vLeaderboardData = GameData.Instance.playerData.vLeaderboardData;
        verticalUI = GameData.Instance.playerData.verticalUI;
        horizontalUI = GameData.Instance.playerData.horizontalUI;
        vLeaderboardData.SetActive(false);
        GameOverScreenH.SetActive(false);
        GameOverScreenV.SetActive(false);
    }
    private void Update()
    {
        if (tutorialScript != null)
        {
            if (tutorialScript.tutorialEnded)
            {
                tutorialScript.tutorialEnded = false;
                tutorialCanvas.SetActive(false);
                GameData.Instance.hasDoneTutorial = true;
                Restart();
            }
        }
        switch(GameData.Instance.screenOrientaionID)
        {
            case 0:
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                break;

            case 1:
                Screen.orientation = ScreenOrientation.Portrait;
                break;
            case 2:
                Screen.orientation = ScreenOrientation.LandscapeRight;
                break;
            case 3:
                Screen.orientation = ScreenOrientation.PortraitUpsideDown;
                break;
            default:
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                break;
        }
        switch(Screen.orientation)
        {
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                horizontalUI.SetActive(false);
                horizontalButtons.SetActive(false);
                verticalUI.SetActive(true);
                verticalButtons.SetActive(true);
                break;

            case ScreenOrientation.LandscapeLeft:
            case ScreenOrientation.LandscapeRight:
                verticalUI.SetActive(false);
                verticalButtons.SetActive(false);
                horizontalUI.SetActive(true);
                horizontalButtons.SetActive(true);
                break;

            default: 
                break;
        }
        if(GameData.Instance.gameIsOver)
        {
            if(GameData.Instance.playerData.currentScore > GameData.Instance.highScoreTable[0].highScore)
            {
                GameData.Instance.playerName = GameData.Instance.keyboard.text;
                NameH.text = GameData.Instance.keyboard.text;
                NameV.text = GameData.Instance.keyboard.text;
            }
        }
        
    }
    public void ScreenOrientationChange()
    {

        switch (Screen.orientation)
        {
            case ScreenOrientation.Portrait:
                GameData.Instance.screenOrientaionID = 2;
                SettingsH.SetActive(true);
                SettingsV.SetActive(false);
                break;
            case ScreenOrientation.PortraitUpsideDown:
                GameData.Instance.screenOrientaionID = 0;
                SettingsH.SetActive(true);
                SettingsV.SetActive(false);
                break;
            case ScreenOrientation.LandscapeLeft:
                GameData.Instance.screenOrientaionID = 1;
                SettingsH.SetActive(false);
                SettingsV.SetActive(true);
                break;
            case ScreenOrientation.LandscapeRight:
                GameData.Instance.screenOrientaionID = 3;
                SettingsH.SetActive(false);
                SettingsV.SetActive(true);
                break;

            default:
                break;
        }
        if (GameData.Instance.isSaving == false)
            GameData.Instance.Save();
    }
    public void Credits()
    {
        switch (Screen.orientation)
        {
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                pauseMenuV.SetActive(!pauseMenuV.activeInHierarchy);
                CreditsV.SetActive(!CreditsV.activeInHierarchy);
                break;

            case ScreenOrientation.LandscapeLeft:
            case ScreenOrientation.LandscapeRight:
                pauseMenuH.SetActive(!pauseMenuH.activeInHierarchy);
                CreditsH.SetActive(!CreditsH.activeInHierarchy);
                break;

            default:
                break;
        }
    }
    public void TestGameOver()
    {
        GameData.Instance.gameIsOver = true;
        GameOverManager.Instance.OnGameOver();
    }
    public void Tutorial()
    {
        GameData.Instance.hasDoneTutorial = false;
        GameData.Instance.Save();
        tutorialScript.enabled = true;
        tutorialCanvas.SetActive(true);
        Restart(); 
        tutorialScript.TutorialSetUp();
    }
    public void TutorialSkip()
    {
        GameData.Instance.hasDoneTutorial = true;
        GameData.Instance.Save();
        tutorialScript.enabled = false;
        tutorialScript.tutorialEnded = true;
    }
    public void pauseGame()
    {
        if(leaderboardMenu.activeInHierarchy == false)
        {
            if (playerController.isPowerItemMenuOpen == false)
            {
                switch (Screen.orientation)
                {
                    case ScreenOrientation.Portrait:
                    case ScreenOrientation.PortraitUpsideDown:
                        if (pauseMenuV.activeInHierarchy == false)
                        {
                            Time.timeScale = 0;
                            pauseMenuV.SetActive(true);
                            playerController.enabled = false;
                        }
                        else
                        {
                            Time.timeScale = 1;
                            pauseMenuV.SetActive(false);
                            playerController.enabled = true;
                        }
                        break;

                    case ScreenOrientation.LandscapeLeft:
                    case ScreenOrientation.LandscapeRight:
                        if (pauseMenuH.activeInHierarchy == false)
                        {
                            Time.timeScale = 0;
                            pauseMenuH.SetActive(true);
                            playerController.enabled = false;
                        }
                        else
                        {
                            Time.timeScale = 1;
                            pauseMenuH.SetActive(false);
                            playerController.enabled = true;
                        }
                        break;

                    default:
                        break;
                }
            }
        }
    }
    public void CombinationMenu()
    {
        switch (Screen.orientation)
        {
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                CombinationMenuV.SetActive(!CombinationMenuV.activeInHierarchy);
                //pauseMenuV.SetActive(!pauseMenuV.activeInHierarchy);
                if(CombinationMenuV.activeInHierarchy)
                {
                    Time.timeScale = 0;
                    playerController.enabled = false;
                }
                else
                {
                    Time.timeScale = 1;
                    playerController.enabled = true;
                }
                break;

            case ScreenOrientation.LandscapeLeft:
            case ScreenOrientation.LandscapeRight:
                CombinationMenuH.SetActive(!CombinationMenuH.activeInHierarchy);
                //pauseMenuH.SetActive(!pauseMenuH.activeInHierarchy);
                if (CombinationMenuH.activeInHierarchy)
                {
                    Time.timeScale = 0;
                    playerController.enabled = false;
                }
                else
                {
                    Time.timeScale = 1;
                    playerController.enabled = true;
                }
                break;

            default:
                break;
        }
    }
    public void Settings()
    {
        switch (Screen.orientation)
        {
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                SettingsV.SetActive(!SettingsV.activeInHierarchy);
                pauseMenuV.SetActive(!pauseMenuV.activeInHierarchy);
                break;

            case ScreenOrientation.LandscapeLeft:
            case ScreenOrientation.LandscapeRight:
                SettingsH.SetActive(!SettingsH.activeInHierarchy);
                pauseMenuH.SetActive(!pauseMenuH.activeInHierarchy);
                break;

            default:
                break;
        }
        GameData.Instance.Save();
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void Restart()
    {
        File.Delete(Application.persistentDataPath + "/sceneData.dat");
        File.Delete(Application.persistentDataPath + "/heldOrb.dat");
        GameData.Instance.Load();
        GameData.Instance.spawningStart = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        switch (Screen.orientation)
        {
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                if(pauseMenuV.activeInHierarchy)
                {
                    pauseMenuV.SetActive(!pauseMenuV.activeInHierarchy);
                }
                break;

            case ScreenOrientation.LandscapeLeft:
            case ScreenOrientation.LandscapeRight:
                if (pauseMenuH.activeInHierarchy)
                {
                    pauseMenuH.SetActive(!pauseMenuH.activeInHierarchy);
                }
                break;

            default:
                break;
        }
        this.GetComponent<SoundSettings>().SetAllVolume();
        Time.timeScale = 1.0f;
        PowerItemData.Instance.ResetInventory();
    }
    public void powerItemOpen()
    {
        if(leaderboardMenu.activeInHierarchy == false)
        {
            playerController.isPowerItemMenuOpen = true;
            powerItemMenu.SetActive(true);
        }
    }
    public void powerItemClose()
    {
        if(leaderboardMenu.activeInHierarchy == false)
        {
            powerItemMenu.SetActive(false);
            StartCoroutine(powerItemMenuFalse(1));
        }
    }
    public void LeaderboardOpen()
    {
        leaderboardMenu.SetActive(!leaderboardMenu.activeInHierarchy);
        vLeaderboardData.SetActive(!vLeaderboardData.activeInHierarchy);
        pauseMenuV.SetActive(!pauseMenuV.activeInHierarchy);
    }
    private void OnEnable()
    {
        tutorialScript = FindAnyObjectByType<TutorialScript>();
        playerController = FindAnyObjectByType<PlayerController>();
        powerItemMenuScript = GetComponent<PowerItemMenuScript>();

        SceneManager.sceneLoaded += OnChangeScene;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded += OnChangeScene;
    }
    private void OnChangeScene(Scene scene, LoadSceneMode mode)
    {
        playerController = FindAnyObjectByType<PlayerController>();
        GameData.Instance.steam = GameObject.FindGameObjectWithTag("Steam");
        GameData.Instance.playerData = FindAnyObjectByType<PlayerData>();
        vLeaderboardData = GameData.Instance.playerData.vLeaderboardData;
        verticalUI = GameData.Instance.playerData.verticalUI;
        horizontalUI = GameData.Instance.playerData.horizontalUI;
        GameOverScreenV.SetActive(false);
        GameOverScreenH.SetActive(false);
        GameData.Instance.Load();
        GameData.Instance.gameIsOver = false;
        Time.timeScale = 1.0f;
    }
    IEnumerator powerItemMenuFalse(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        playerController.isPowerItemMenuOpen = false;
    }
}
