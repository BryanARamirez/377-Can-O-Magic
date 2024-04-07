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

    [SerializeField] private GameObject _magicItemModel;
    private Material _magicItemMaterial;
    [Range(0, 1)]
    [SerializeField] private float _darkenPercentage = .3f;

    private void Awake()
    {
        _magicItemMaterial = _magicItemModel.GetComponent<Renderer>().material;
    }

    /// <summary>
    /// get the magic item's type of magic item
    /// </summary>
    public MagicItemEnum magicItemName
    {
        get { return _itemData.magicItemName; }
    }

    public void Reacted()
    {
        if (!_hasReacted)
        {
            _hasReacted = true;
            _magicItemMaterial.color = new Color(_magicItemMaterial.color.r * (1 - _darkenPercentage), _magicItemMaterial.color.g * (1 - _darkenPercentage), _magicItemMaterial.color.b * (1 - _darkenPercentage), _magicItemMaterial.color.a);
        }
    }

    public void SetMimic()
    {
        _isMimic = true;
    }

    public void SetDrop()
    {
        _hasDropped = true;
    }

    public int GetPoints()
    {
        if (!_isMimic)
        {
            return _itemData.points;
        }
        else
        {
            return _itemData.points/2;
        }
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
