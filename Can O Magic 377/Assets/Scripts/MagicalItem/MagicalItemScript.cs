using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/17/2024]
 * [Manages all the scripts for magic items and holds data for all magic items]
 */

public class MagicalItemScript : MonoBehaviour
{
    //what is magic item
    [SerializeField] private MagicItemData _itemData;

    //has it already reacted
    [SerializeField] private bool _hasReacted = false;

    /// <summary>
    /// get the magic item's type of magic item
    /// </summary>
    public MagicItemEnum magicItemName
    {
        get { return _itemData.magicItemName; }
    }

    public void Reacted()
    {
        _hasReacted = true;
    }

    /// <summary>
    /// get and set whether or not the magic item has reacted
    /// </summary>
    public bool hasReacted
    {
        get { return _hasReacted; }
    }
}
