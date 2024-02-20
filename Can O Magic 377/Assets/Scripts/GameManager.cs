using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private PlayerController playerController;

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
}
