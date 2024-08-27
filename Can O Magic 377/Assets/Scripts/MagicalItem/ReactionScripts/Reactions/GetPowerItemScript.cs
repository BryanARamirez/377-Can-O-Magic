using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/19/2024]
 * [gives power item when magic item reacts]
 */

public class GetPowerItemScript : BaseReactionScript
{
    //what power item is given when reacted
    [SerializeField] private PowerItemEnum _getPowerItem;
    [SerializeField] private GameObject _powerItemSprite;

    /// <summary>
    /// tells the PowerItemData class which magic item to give
    /// </summary>
    /// <param name="otherItem"></param>
    public override void Reaction(GameObject otherItem)
    {
        if (!PowerItemData.Instance.checkAvailable(_getPowerItem))
        {
            PowerItemData.Instance.GainPowerItem(_getPowerItem);
        }

        Vector3 spawnSprite = (gameObject.transform.position + otherItem.transform.position) / 2f;
        Instantiate(_powerItemSprite, spawnSprite, Quaternion.identity);
    }
}
