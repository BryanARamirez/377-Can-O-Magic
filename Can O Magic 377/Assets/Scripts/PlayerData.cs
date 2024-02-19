using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private Image NextImage;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private List<Sprite> itemImages = new List<Sprite>();
    [SerializeField] private int imageIndex;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        imageIndex = playerController.randomNextIndex;
        NextImage.sprite = itemImages[imageIndex];
    }
}
