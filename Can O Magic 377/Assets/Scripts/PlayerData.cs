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
    private int tempScore;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text firstPlaceScoreText;
    [SerializeField] private TMP_Text secondPlaceScoreText;
    [SerializeField] private TMP_Text thirdPlaceScoreText;
    [SerializeField] private TMP_Text fourthPlaceScoreText;
    [SerializeField] private TMP_Text fifthPlaceScoreText;
    private GameData gameData;
    private GameObject[] itemsInCan;
    public bool inTutorial;

    private void Awake()
    {
        NextImage = FindAnyObjectByType<Image>();
        gameData = FindAnyObjectByType<GameData>();
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
    private void OnApplicationQuit()
    {
        if(currentScore > gameData.highScoreTable[0].highScore)
        {
            gameData.highScoreTable[0].highScore = currentScore;
            gameData.RankScores();
        }
        gameData.Save();
    }

    public void DisplayNextItem(Sprite nextItemSprite)
    {
        NextImage.sprite = nextItemSprite;
    }

    private void Update()
    {
        UpdateScore();
        scoreText.text = "Score: " + currentScore.ToString();
        UpdateLeaderboard();
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
    public void UpdateLeaderboard()
    {
        firstPlaceScoreText.text = "1st " + gameData.highScoreTable[4].highScore;
        secondPlaceScoreText.text = "2nd " + gameData.highScoreTable[3].highScore;
        thirdPlaceScoreText.text = "3rd " + gameData.highScoreTable[2].highScore;
        fourthPlaceScoreText.text = "4th " + gameData.highScoreTable[1].highScore;
        fifthPlaceScoreText.text = "5th " + gameData.highScoreTable[0].highScore;
    }
}
