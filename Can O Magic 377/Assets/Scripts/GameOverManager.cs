using System.Collections;
using System.Collections.Generic;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}