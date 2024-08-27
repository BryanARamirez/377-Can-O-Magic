using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [04/15/2024]
 * [script use to keep track of power items]
 */

public class PowerItemData : Singleton<PowerItemData>
{
    private PlayerController _playerController;
    private Dictionary<PowerItemEnum, bool> _availableItems;

    [SerializeField] private List<PowerItemEnum> _powerItemPrefabListKey;
    [SerializeField] private List<GameObject> _powerItemPrefabListValue;
    private Dictionary<PowerItemEnum, GameObject> _powerItemPrefabs;

    /// <summary>
    /// sets up needed variables and components
    /// </summary>
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
            if (_playerController == null)
            {
                _playerController = GameObject.FindAnyObjectByType<PlayerController>();
            }
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
        Debug.Log("player got " + powerItem);
        _availableItems[powerItem] = true;
    }

    /// <summary>
    /// checks if the passed power item is available
    /// </summary>
    /// <param name="powerItem">Item that wants to be checked</param>
    /// <returns>is powerItem available</returns>
    public bool checkAvailable(PowerItemEnum powerItem)
    {
        return _availableItems[powerItem];
    }

    /// <summary>
    /// function to get the number of power items available
    /// </summary>
    /// <returns>int of power items available</returns>
    public int NumberOfPowerItems()
    {
        int num = 0;
        foreach (KeyValuePair<PowerItemEnum, bool> powerItem in _availableItems)
        {
            if (powerItem.Value)
            {
                num++;
            }
        }

        return num;
    }

    /// <summary>
    /// resets available power items
    /// </summary>
    public void ResetInventory()
    {
        List<PowerItemEnum> keys = new List<PowerItemEnum>(_availableItems.Keys);

        foreach (PowerItemEnum key  in keys)
        {
            _availableItems[key] = false;
        }
    }
}
