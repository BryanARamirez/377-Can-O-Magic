using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/17/2024]
 * [script used for testing, do not use]
 */

public class JustinTestScript : MonoBehaviour
{
    public bool checkItems = false;

    public bool giveSlime = false;
    public bool giveMimic = false;
    public bool giveHoly = false;

    public bool useSlime = false;
    public bool useMimic = false;
    public bool useHoly = false;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if (checkItems)
        {
            checkItems = false;
            Debug.Log("Does player have a slime ball: " + PowerItemData.Instance.hasSlime);
            Debug.Log("Does player have a mimic tounge: " + PowerItemData.Instance.hasMimic);
            Debug.Log("Does player have a holy aura: " + PowerItemData.Instance.hasHoly);
        }

        if (giveSlime)
        {
            giveSlime = false;
            PowerItemData.Instance.GainPowerItem(PowerItemEnum.SlimeBall);
        }
        if (giveMimic)
        {
            giveMimic = false;
            PowerItemData.Instance.GainPowerItem(PowerItemEnum.MimicTongue);
        }
        if (giveHoly)
        {
            giveHoly = false;
            PowerItemData.Instance.GainPowerItem(PowerItemEnum.HolyAura);
        }

        if (useSlime)
        {
            useSlime = false;
            PowerItemData.Instance.UsePowerItem(PowerItemEnum.SlimeBall);
        }
        if (useMimic)
        {
            useMimic = false;
            PowerItemData.Instance.UsePowerItem(PowerItemEnum.MimicTongue);
        }
        if (useHoly)
        {
            useHoly = false;
            PowerItemData.Instance.UsePowerItem(PowerItemEnum.HolyAura);
        }
    }
}
