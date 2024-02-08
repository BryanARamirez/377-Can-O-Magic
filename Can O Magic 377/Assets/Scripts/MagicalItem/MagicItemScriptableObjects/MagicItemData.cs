using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MagicItemData", menuName = "Magic Item Data", order = 51)]
public class MagicItemData : ScriptableObject
{
    [SerializeField] private MagicItemEnum _magicItemName;
    [SerializeField] private int _points;

    public MagicItemEnum magicItemName
    {
        get { return _magicItemName; }
    }

    public int points
    {
        get { return points; }
    }
}
