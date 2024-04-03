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

    /// <summary>
    /// tells the PowerItemData class which magic item to give
    /// </summary>
    /// <param name="otherItem"></param>
    public override void Reaction(GameObject otherItem)
    {
        PowerItemData.Instance.GainPowerItem(_getPowerItem);
        if (!PowerItemData.Instance.checkAvailable(_getPowerItem))
        {
            Destroy(gameObject);
        }
    }
}
