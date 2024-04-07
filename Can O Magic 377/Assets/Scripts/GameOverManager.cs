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
        Debug.Log("Game Over");
        GameData.Instance.UpdateScoreboard();
        GameData.Instance.Save();
        File.Delete(Application.persistentDataPath + "/sceneData.dat");
        GameData.Instance.keyboard.text = "";
        GameData.Instance.playerName = "";
        GameData.Instance.nameEntered = false;
        GameData.Instance.gameIsOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
