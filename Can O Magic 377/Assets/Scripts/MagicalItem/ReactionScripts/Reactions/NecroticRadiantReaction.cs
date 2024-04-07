using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/15/2024]
 * [shrinks both gameobjects when collided]
 */

public class NecroticRadiantReaction : BaseReactionScript
{
    [SerializeField] private float _shrinkDivideBy;

    /// <summary>
    /// resets the default for _reactionFor
    /// </summary>
    private void Reset()
    {
        _reactionFor = MagicItemEnum.RadiantOrb;
    }

    /// <summary>
    /// calls the steam's function for them
    /// </summary>
    public override void Reaction(GameObject otherItem)
    {
        transform.localScale = transform.localScale / _shrinkDivideBy;
        otherItem.transform.localScale = otherItem.transform.localScale / _shrinkDivideBy;
    }
}
