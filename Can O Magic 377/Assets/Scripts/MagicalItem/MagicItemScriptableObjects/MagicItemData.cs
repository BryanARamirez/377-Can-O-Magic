using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Author: [Lam, Justin]
 * Last Updated: [02/08/2024]
 * [Scriptable object of data for magical item]
 */

[CreateAssetMenu(fileName = "New MagicItemData", menuName = "Magic Item Data", order = 0)]
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
        get { return _points; }
    }
}
