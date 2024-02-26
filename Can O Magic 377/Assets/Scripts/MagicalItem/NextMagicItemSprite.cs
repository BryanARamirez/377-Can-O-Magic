using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMagicItemSprite : MonoBehaviour
{
    [SerializeField] private Sprite _itemSprite;

    public Sprite itemSprite
    {
        get { return _itemSprite; }
    }
}
