using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject powerItemMenu;
    [SerializeField] private GameObject leaderboardMenu;
    [SerializeField] private GameObject verticalLeaderboardData;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TutorialScript tutorialScript;
    [SerializeField] private PowerItemMenuScript powerItemMenuScript;

    public override void Awake()
    {
        base.Awake();
        verticalLeaderboardData = GameData.Instance.playerData.verticalLeaderboardData;
        verticalLeaderboardData.SetActive(false);
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
    }
    public void pauseGame()
    {
        if(leaderboardMenu.activeInHierarchy == false)
        {
            if (playerController.isPowerItemMenuOpen == false)
            {
                if (pauseMenu.activeInHierarchy == false)
                {
                    Time.timeScale = 0;
                    pauseMenu.SetActive(true);
                    playerController.enabled = false;
                }
                else
                {
                    Time.timeScale = 1;
                    pauseMenu.SetActive(false);
                    playerController.enabled = true;
                }
            }
        }
    }
    public void quitGame()
    {
        Application.Quit();
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
        verticalLeaderboardData.SetActive(!verticalLeaderboardData.activeInHierarchy);
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
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
        GameData.Instance.playerData = FindAnyObjectByType<PlayerData>();
        verticalLeaderboardData = GameData.Instance.playerData.verticalLeaderboardData;
        GameData.Instance.Load();
    }
    IEnumerator powerItemMenuFalse(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        playerController.isPowerItemMenuOpen = false;
    }
}
