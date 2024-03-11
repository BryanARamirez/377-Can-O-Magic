using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/17/2024]
 * [script for Nova orb and Earth Orb]
 */

public class NovaEarthReaction : BaseReactionScript
{
    /// <summary>
    /// resets the default for _reactionFor to earth orb
    /// </summary>
    private void Reset()
    {
        _reactionFor = MagicItemEnum.EarthOrb;
    }

    public override void Reaction(GameObject otherItem)
    {
        Physics.IgnoreCollision(gameObject.GetComponentInChildren<Collider>(), otherItem.GetComponentInChildren<Collider>(), true);
    }
}
