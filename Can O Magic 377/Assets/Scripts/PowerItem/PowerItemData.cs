using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/17/2024]
 * [script use to keep track of power items]
 */

public class PowerItemData : Singleton<PowerItemData>
{
    private PlayerController _playerController;
    private Dictionary<PowerItemEnum, bool> _availableItems;

    [SerializeField] private List<PowerItemEnum> _powerItemPrefabListKey;
    [SerializeField] private List<GameObject> _powerItemPrefabListValue;
    private Dictionary<PowerItemEnum, GameObject> _powerItemPrefabs;

    private void OnEnable()
    {
        _playerController = GameObject.FindAnyObjectByType<PlayerController>();

        _availableItems = new Dictionary<PowerItemEnum, bool>();
        _powerItemPrefabs = new Dictionary<PowerItemEnum, GameObject>();
        for (int index = 0; index < _powerItemPrefabListKey.Count; index++)
        {
            _availableItems.Add(_powerItemPrefabListKey[index], false);
            _powerItemPrefabs.Add(_powerItemPrefabListKey[index], _powerItemPrefabListValue[index]);
        }
    }

    /// <summary>
    /// TODO: when called, calls function to replace current item to drop with power item
    /// </summary>
    /// <param name="powerItem"></param>
    public void UsePowerItem(PowerItemEnum powerItem)
    {
        if (_availableItems[powerItem])
        {
            //replace with function to replace current item with power item
            Debug.Log("used " + powerItem);
            _playerController.ReplaceCurrentItem(_powerItemPrefabs[powerItem]);
            _availableItems[powerItem] = false;
        }
        else
        {
            //replace with function to give feedback that player doesn't have item
            Debug.Log("player doesn't have " + powerItem);
        }
    }

    /// <summary>
    /// called to give player power item that they can use
    /// </summary>
    /// <param name="powerItem">power item type they can use</param>
    public void GainPowerItem(PowerItemEnum powerItem)
    {
        _availableItems[powerItem] = true;
    }

    /// <summary>
    /// get if the player has a slime ball
    /// </summary>
    public bool hasSlime
    {
        get { return _availableItems[PowerItemEnum.SlimeBall]; }
    }

    /// <summary>
    /// get if the player has a mimic tongue
    /// </summary>
    public bool hasMimic
    {
        get { return _availableItems[PowerItemEnum.MimicTongue]; }
    }

    /// <summary>
    /// get if the player has a holy aura
    /// </summary>
    public bool hasHoly
    {
        get { return _availableItems[PowerItemEnum.HolyAura]; }
    }
}
