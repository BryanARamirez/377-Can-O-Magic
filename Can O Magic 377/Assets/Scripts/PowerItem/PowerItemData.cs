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
    private Dictionary<PowerItemEnum, bool> availableItems;

    private void OnEnable()
    {
        availableItems = new Dictionary<PowerItemEnum, bool>();
        availableItems.Add(PowerItemEnum.SlimeBall, false);
        availableItems.Add(PowerItemEnum.MimicTongue, false);
        availableItems.Add(PowerItemEnum.HolyAura, false);
    }

    /// <summary>
    /// TODO: when called, calls function to replace current item to drop with power item
    /// </summary>
    /// <param name="powerItem"></param>
    public void UsePowerItem(PowerItemEnum powerItem)
    {
        if (availableItems[powerItem])
        {
            //replace with function to replace current item with power item
            Debug.Log("used " + powerItem);
            availableItems[powerItem] = false;
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
        availableItems[powerItem] = true;
    }

    /// <summary>
    /// get if the player has a slime ball
    /// </summary>
    public bool hasSlime
    {
        get { return availableItems[PowerItemEnum.SlimeBall]; }
    }

    /// <summary>
    /// get if the player has a mimic tongue
    /// </summary>
    public bool hasMimic
    {
        get { return availableItems[PowerItemEnum.MimicTongue]; }
    }

    /// <summary>
    /// get if the player has a holy aura
    /// </summary>
    public bool hasHoly
    {
        get { return availableItems[PowerItemEnum.HolyAura]; }
    }
}
