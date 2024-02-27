using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TutorialScript tutorialScript;
    private GameData gameData;

    public override void Awake()
    {
        base.Awake();
        gameData = GetComponent<GameData>();
        if(gameData.hasDoneTutorial)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            tutorialScript = FindAnyObjectByType<TutorialScript>();
        }
        playerController = FindAnyObjectByType<PlayerController>();
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
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        playerController.enabled = false;
    }
    public void unpauseGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        playerController.enabled = true;
    }
    public void quitGame()
    {
        Application.Quit();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnChangeScene;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded += OnChangeScene;
    }
    private void OnChangeScene(Scene scene, LoadSceneMode mode)
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }
}
