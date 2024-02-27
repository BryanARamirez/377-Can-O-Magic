using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private Image NextImage;
    [SerializeField] private int imageIndex;
    [SerializeField] private TutorialScript tutorialScript;
    [SerializeField] private int currentScore;
    public bool inTutorial;

    private void Awake()
    {
        NextImage = FindAnyObjectByType<Image>();
        tutorialScript = FindAnyObjectByType<TutorialScript>();
        if (tutorialScript != null )
        {
            inTutorial = true;
        }
        else
        {
            inTutorial = false;
        }
    }

    public void DisplayNextItem(Sprite nextItemSprite)
    {
        NextImage.sprite = nextItemSprite;
    }
}
