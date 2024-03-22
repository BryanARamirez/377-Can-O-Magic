using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/26/2024]
 * [Placed in Magic items to detect game over]
 */

public class GameOverTrigger : MonoBehaviour
{
    private bool _canGameOver = false;
    private Transform _topOfCan;

    /// <summary>
    /// get the transform for the top of the can
    /// </summary>
    private void Awake()
    {
        _topOfCan = GameObject.FindGameObjectWithTag("TopRight").transform;
    }

    /// <summary>
    /// check if it goes over the can
    /// </summary>
    private void Update()
    {
        if (_canGameOver && transform.position.y > _topOfCan.position.y)
        {
            GameData.Instance.gameIsOver = true;
            GameData.Instance.keyboard = TouchScreenKeyboard.Open(GameData.Instance.playerName);
        }
        if (string.IsNullOrEmpty(GameData.Instance.playerName) == false && GameData.Instance.keyboard.active == false)
        {
            GameData.Instance.nameEntered = true;
        }
        if (GameData.Instance.nameEntered)
        {
            GameOverManager.Instance.OnGameOver();
        }
        //GameData.Instance.playerName = GameData.Instance.keyboard.text;
    }

    /// <summary>
    /// once it collides with something, turn on bool to check height
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        _canGameOver = true;
    }
}
