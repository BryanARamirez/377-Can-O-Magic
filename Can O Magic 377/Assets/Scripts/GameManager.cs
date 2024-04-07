using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    }
    private void Update()
    {
        if (tutorialScript != null)
        {
            if (tutorialScript.tutorialEnded)
            {
                SceneManager.LoadScene(1);
            }
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
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void Restart()
    {
        File.Delete(Application.persistentDataPath + "/sceneData.dat");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        switch (Screen.orientation)
        {
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                pauseMenuV.SetActive(!pauseMenuV.activeInHierarchy);
                break;

            case ScreenOrientation.LandscapeLeft:
            case ScreenOrientation.LandscapeRight:
                pauseMenuH.SetActive(!pauseMenuH.activeInHierarchy);
                break;

            default:
                break;
        }
        Time.timeScale = 1.0f;
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
        if (GameData.Instance.hasDoneTutorial)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            tutorialScript = FindAnyObjectByType<TutorialScript>();
        }
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
        GameData.Instance.Load();
    }
    IEnumerator powerItemMenuFalse(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        playerController.isPowerItemMenuOpen = false;
    }
}
