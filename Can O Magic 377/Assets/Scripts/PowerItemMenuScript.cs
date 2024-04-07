using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItemMenuScript : MonoBehaviour
{
    public void HolyAuraSpawn()
    {
        PowerItemData.Instance.UsePowerItem(PowerItemEnum.HolyAura);
        GameManager.Instance.powerItemClose();
    }
    public void MimicToungeSpawn()
    {
        PowerItemData.Instance.UsePowerItem(PowerItemEnum.MimicTongue);
        GameManager.Instance.powerItemClose();
    }
    public void SlimeBallSpawn()
    {
        PowerItemData.Instance.UsePowerItem(PowerItemEnum.SlimeBall);
        GameManager.Instance.powerItemClose();
    }

    public void BombSpawn()
    {
        PowerItemData.Instance.UsePowerItem(PowerItemEnum.Bomb);
        GameManager.Instance.powerItemClose();
    }
}
