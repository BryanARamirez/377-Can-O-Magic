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
    private bool hitSomethingOnce;

    [SerializeField] private GameObject _magicItemModel;
    public Material[] _magicItemMaterial = new Material[2];
    [Range(0, 1)]
    [SerializeField] private float _darkenPercentage = .3f;
    private bool _hasShrunk = false;
    private bool _canTsunami = true;

    private void Awake()
    {
        _magicItemMaterial = _magicItemModel.GetComponent<Renderer>().materials;
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

            foreach (Material mat in _magicItemMaterial)
            {
                mat.SetColor("_EmissionColor", Color.black);
                mat.color = new Color(mat.color.r * (1 - _darkenPercentage), mat.color.g * (1 - _darkenPercentage), mat.color.b * (1 - _darkenPercentage), mat.color.a);
            }
            //_magicItemMaterial.color = new Color(_magicItemMaterial.color.r * (1 - _darkenPercentage), _magicItemMaterial.color.g * (1 - _darkenPercentage), _magicItemMaterial.color.b * (1 - _darkenPercentage), _magicItemMaterial.color.a);
        }
    }

    public void SetMimic()
    {
        _isMimic = true;
    }
    public void SetShrunk()
    {
        _hasShrunk = true;
    }
    public void UnSetTsunami()
    {
        _canTsunami = false;
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
    public bool isShrunk
    {
        get { return _hasShrunk; }
    }
    public bool isTsunamiable
    {
        get { return _canTsunami; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(hitSomethingOnce == false)
        {
            if(GameData.Instance.isSaving == false)
            {
                GameData.Instance.SaveScene();
            }
            hitSomethingOnce = true;
        }
    }
}
