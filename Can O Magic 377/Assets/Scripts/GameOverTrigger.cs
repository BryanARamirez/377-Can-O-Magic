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

    private void Awake()
    {
        _topOfCan = GameObject.FindGameObjectWithTag("TopRight").transform;
    }

    private void Update()
    {
        if (_canGameOver && transform.position.y > _topOfCan.position.y)
        {
            GameOverManager.Instance.OnGameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _canGameOver = true;
    }
}
