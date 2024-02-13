using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/12/2024]
 * [WIP, test script to test multiple reactions]
 */

public class WaterBlueReaction : BaseReactionScript
{
    private void Reset()
    {
        _reactionFor = MagicItemEnum.BlueRune;
    }

    public override void Reaction()
    {
        Debug.Log("tsunami");
    }
}
