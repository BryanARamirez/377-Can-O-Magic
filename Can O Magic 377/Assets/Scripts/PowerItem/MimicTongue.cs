using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/14/2024]
 * [script for the mimic tongue]
 */

public class MimicTongue : MonoBehaviour
{
    //needed componenets
    private PlayerController _playerController;
    private MergeVFX _mergeVFX;

    //has the game object been copied
    private bool _hasCopied = false;

    /// <summary>
    /// finds needed components
    /// </summary>
    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        _mergeVFX = GetComponent<MergeVFX>();
    }

    /// <summary>
    /// if it collides with a magic item, it sends to replace the current object with the coppied item
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (!_hasCopied && collision.gameObject.tag == "MagicItem")
        {
            _hasCopied = true;
            _mergeVFX.PlayMergeVFX(transform.position);
            GameObject copiedItem = collision.gameObject;
            _playerController.ReplaceCurrentItem(copiedItem, true);
        }
    }
}
