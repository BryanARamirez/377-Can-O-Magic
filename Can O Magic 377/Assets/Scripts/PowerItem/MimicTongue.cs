using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/05/2024]
 * [script for the mimic tongue]
 */

public class MimicTongue : MonoBehaviour
{
    private PlayerController _playerController;

    private bool _hasCopied = false;

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_hasCopied && collision.gameObject.tag == "MagicItem")
        {
            _hasCopied = true;
            GameObject copiedItem = collision.gameObject;
            _playerController.ReplaceCurrentItem(copiedItem, true);
        }
    }
}
