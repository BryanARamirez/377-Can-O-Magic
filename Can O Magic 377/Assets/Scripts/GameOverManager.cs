using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/26/2024]
 * [Manages what happens when game over happens]
 */


public class GameOverManager : Singleton<GameOverManager>
{
    /// <summary>
    /// called when a game over happens
    /// right now, just resets the scene
    /// </summary>
    public void OnGameOver()
    {
        switch (Screen.orientation)
        {
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                GameManager.Instance.GameOverScreenH.SetActive(false);
                GameManager.Instance.GameOverScreenV.SetActive(true);
                if (GameData.Instance.playerData.currentScore > GameData.Instance.highScoreTable[0].highScore)
                {
                    GameManager.Instance.EnterNameButtonH.SetActive(false);
                    GameManager.Instance.EnterNameButtonV.SetActive(true);
                }
                else
                {
                    GameManager.Instance.EnterNameButtonH.SetActive(false);
                    GameManager.Instance.EnterNameButtonV.SetActive(false);
                }
                break;

            case ScreenOrientation.LandscapeLeft:
            case ScreenOrientation.LandscapeRight:
                GameManager.Instance.GameOverScreenH.SetActive(true);
                GameManager.Instance.GameOverScreenV.SetActive(false);
                if (GameData.Instance.playerData.currentScore > GameData.Instance.highScoreTable[0].highScore)
                {
                    GameManager.Instance.EnterNameButtonH.SetActive(true);
                    GameManager.Instance.EnterNameButtonV.SetActive(false);
                }
                else
                {
                    GameManager.Instance.EnterNameButtonH.SetActive(false);
                    GameManager.Instance.EnterNameButtonV.SetActive(false);
                }
                break;
            default:
                break;
        }
    }
    public void EnterName()
    {
        GameData.Instance.keyboard = TouchScreenKeyboard.Open(GameData.Instance.playerName);
        GameData.Instance.playerName = GameData.Instance.keyboard.text;
    }
    public void GameOverRestart()
    {
        Debug.Log("Game Over");
        if(GameData.Instance.playerData.currentScore > GameData.Instance.highScoreTable[0].highScore)
        {
            if (string.IsNullOrEmpty(GameData.Instance.playerName) == true)
            {
                GameData.Instance.playerName = "???";
            }
        }
        GameData.Instance.UpdateScoreboard();
        GameData.Instance.Save();
        File.Delete(Application.persistentDataPath + "/sceneData.dat");
        if(GameData.Instance.playerData.currentScore > GameData.Instance.highScoreTable[0].highScore)
        {
            GameData.Instance.keyboard.text = "";
        }
        GameData.Instance.playerName = "";
        GameData.Instance.nameEntered = false;
        GameData.Instance.gameIsOver = false;
        GameManager.Instance.GameOverScreenH.SetActive(false);
        GameManager.Instance.GameOverScreenV.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
