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
    public int currentScore;
    private int tempScore;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text firstPlaceScoreText;
    [SerializeField] private TMP_Text secondPlaceScoreText;
    [SerializeField] private TMP_Text thirdPlaceScoreText;
    [SerializeField] private TMP_Text fourthPlaceScoreText;
    [SerializeField] private TMP_Text fifthPlaceScoreText;
    [SerializeField] private TMP_Text firstNameText;
    [SerializeField] private TMP_Text secondNameText;
    [SerializeField] private TMP_Text thirdNameText;
    [SerializeField] private TMP_Text fourthNameText;
    [SerializeField] private TMP_Text fifthNameText;
    [SerializeField] private TMP_Text test;
    public GameObject verticalLeaderboardData;
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
    private void OnApplicationQuit()
    {
        if(currentScore > GameData.Instance.highScoreTable[0].highScore)
        {
            GameData.Instance.highScoreTable[0].highScore = currentScore;
            GameData.Instance.RankScores();
        }
        GameData.Instance.Save();
    }

    public void DisplayNextItem(Sprite nextItemSprite)
    {
        NextImage.sprite = nextItemSprite;
    }

    private void Update()
    {
        UpdateScore();
        scoreText.text = "Score: " + currentScore.ToString();
        test.text = GameData.Instance.playerName;
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
        firstPlaceScoreText.text = "1st " + GameData.Instance.highScoreTable[4].highScore;
        secondPlaceScoreText.text = "2nd " + GameData.Instance.highScoreTable[3].highScore;
        thirdPlaceScoreText.text = "3rd " + GameData.Instance.highScoreTable[2].highScore;
        fourthPlaceScoreText.text = "4th " + GameData.Instance.highScoreTable[1].highScore;
        fifthPlaceScoreText.text = "5th " + GameData.Instance.highScoreTable[0].highScore;
        firstNameText.text = GameData.Instance.highScoreTable[4].playerName;
        secondNameText.text = GameData.Instance.highScoreTable[3].playerName;
        thirdNameText.text = GameData.Instance.highScoreTable[2].playerName;
        fourthNameText.text = GameData.Instance.highScoreTable[1].playerName;
        fifthNameText.text = GameData.Instance.highScoreTable[0].playerName;
    }
}
