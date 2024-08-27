using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [03/11/2024]
 * [causes game over when 2 unstable runes react]
 */

public class UnstableUnstableReaction : BaseReactionScript
{
    /// <summary>
    /// resets default of what to check for
    /// </summary>
    private void Reset()
    {
        _reactionFor = MagicItemEnum.UnstableRune;
    }

    /// <summary>
    /// causes a game over, but will eventually implement it's own animation
    /// </summary>
    /// <param name="otherItem"></param>
    public override void Reaction(GameObject otherItem)
    {
        GameOverManager.Instance.OnGameOver();
    }
}
