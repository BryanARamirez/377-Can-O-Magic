using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [04/19/2024]
 * [script for Nova orb and Earth Orb]
 */

public class NovaEarthReaction : BaseReactionScript
{
    [SerializeField] private GameObject _voidVFX;

    /// <summary>
    /// resets the default for _reactionFor to earth orb
    /// </summary>
    private void Reset()
    {
        _reactionFor = MagicItemEnum.EarthOrb;
    }

    public override void Reaction(GameObject otherItem)
    {
        this.gameObject.GetComponent<MagicalItemScript>().UnSetTsunami();
        Physics.IgnoreCollision(gameObject.GetComponentInChildren<Collider>(), otherItem.GetComponentInChildren<Collider>(), true);
        Instantiate(_voidVFX, transform.position, Quaternion.identity);
    }
}
