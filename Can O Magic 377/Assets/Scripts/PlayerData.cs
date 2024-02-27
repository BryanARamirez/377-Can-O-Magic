using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private Image NextImage;
    [SerializeField] private int imageIndex;
    [SerializeField] private TutorialScript tutorialScript;
    [SerializeField] private int currentScore;
    [SerializeField] private TMP_Text scoreText;
    private GameObject[] itemsInCan;
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

    private void Update()
    {
        UpdateScore();
        scoreText.text = "Score: " + currentScore.ToString();
    }

    public void UpdateScore()
    {
        itemsInCan = GameObject.FindGameObjectsWithTag("MagicItem");
        currentScore = 0;
        for (int i = 0; i < itemsInCan.Length; i++)
        {
            currentScore += itemsInCan[i].GetComponent<MagicalItemScript>().GetPoints();
        }
    }
}
