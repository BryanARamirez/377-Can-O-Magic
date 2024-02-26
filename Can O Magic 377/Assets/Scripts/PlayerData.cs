using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private Image NextImage;
    [SerializeField] private int imageIndex;

    public void DisplayNextItem(Sprite nextItemSprite)
    {
        NextImage.sprite = nextItemSprite;
    }
}
