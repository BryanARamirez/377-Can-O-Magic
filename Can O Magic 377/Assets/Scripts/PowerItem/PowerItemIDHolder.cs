using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItemIDHolder : MonoBehaviour
{
    [SerializeField] private PowerItemSO _itemData;
    public PowerItemEnum powerItemName
    {
        get { return _itemData.powerItemName; }
    }
}
