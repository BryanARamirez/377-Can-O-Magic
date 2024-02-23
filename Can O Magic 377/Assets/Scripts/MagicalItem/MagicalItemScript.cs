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

    [SerializeField] private bool _hasDropped = false;

    //has it already reacted
    [SerializeField] private bool _hasReacted = false;

    [SerializeField] private bool _isMimic = false;

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

    public void SetMimic()
    {
        _isMimic = true;
    }

    public void SetDrop()
    {
        _hasDropped = true;
    }

    public void GetPoints()
    {

    }

    /// <summary>
    /// get and set whether or not the magic item has reacted
    /// </summary>
    public bool hasReacted
    {
        get { return _hasReacted; }
    }

    public bool isMimic
    {
        get { return _isMimic; }
    }

    public bool hasDropped
    {
        get { return _hasDropped; }
    }
}
