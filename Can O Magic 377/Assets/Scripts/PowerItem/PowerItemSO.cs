using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New PowerItemData", menuName = "Power Item Data", order = 0)]

public class PowerItemSO : ScriptableObject
{
    [SerializeField] private PowerItemEnum _powerItemName;

    public PowerItemEnum powerItemName
    {
        get { return _powerItemName; }
    }
}
