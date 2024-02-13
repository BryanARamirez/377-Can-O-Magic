using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/12/2024]
 * [calls to activate steam when called to react]
 */

public class WaterFireReaction : BaseReactionScript
{
    //game object for steam
    private GameObject _steam;

    /// <summary>
    /// resets the default for _reactionFor
    /// </summary>
    private void Reset()
    {
        _reactionFor = MagicItemEnum.FireOrb;
    }

    /// <summary>
    /// get steam game object
    /// </summary>
    private void Awake()
    {
        _steam = GameObject.FindWithTag("Steam");
    }

    /// <summary>
    /// calls the steam's function for them
    /// </summary>
    public override void Reaction()
    {
        Debug.Log("steam");
    }
}
