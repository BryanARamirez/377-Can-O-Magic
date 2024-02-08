using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalItemScript : MonoBehaviour
{
    [SerializeField] private MagicItemData _itemData;

    public MagicItemEnum magicItemName
    {
        get { return _itemData.magicItemName; }
    }
}
