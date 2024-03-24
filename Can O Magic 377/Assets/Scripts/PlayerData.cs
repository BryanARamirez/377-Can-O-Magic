using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private Image NextImageV;
    [SerializeField] private Image NextImageH;
    [SerializeField] private int imageIndex;
    [SerializeField] private TutorialScript tutorialScript;
    public int currentScore;
    private int tempScore;

    [SerializeField] private TMP_Text scoreTextV;
    [SerializeField] private TMP_Text firstPlaceScoreTextV;
    [SerializeField] private TMP_Text secondPlaceScoreTextV;
    [SerializeField] private TMP_Text thirdPlaceScoreTextV;
    [SerializeField] private TMP_Text fourthPlaceScoreTextV;
    [SerializeField] private TMP_Text fifthPlaceScoreTextV;
    [SerializeField] private TMP_Text firstNameTextV;
    [SerializeField] private TMP_Text secondNameTextV;
    [SerializeField] private TMP_Text thirdNameTextV;
    [SerializeField] private TMP_Text fourthNameTextV;
    [SerializeField] private TMP_Text fifthNameTextV;

    [SerializeField] private TMP_Text scoreTextH;
    [SerializeField] private TMP_Text firstPlaceScoreTextH;
    [SerializeField] private TMP_Text secondPlaceScoreTextH;
    [SerializeField] private TMP_Text thirdPlaceScoreTextH;
    [SerializeField] private TMP_Text fourthPlaceScoreTextH;
    [SerializeField] private TMP_Text fifthPlaceScoreTextH;
    [SerializeField] private TMP_Text firstNameTextH;
    [SerializeField] private TMP_Text secondNameTextH;
    [SerializeField] private TMP_Text thirdNameTextH;
    [SerializeField] private TMP_Text fourthNameTextH;
    [SerializeField] private TMP_Text fifthNameTextH;

    public GameObject vLeaderboardData;
    public GameObject verticalUI;
    public GameObject horizontalUI;
    private GameObject[] itemsInCan;
    public bool inTutorial;

    private void Awake()
    {
        NextImageV = GameObject.FindGameObjectWithTag("NextItemV").GetComponent<Image>();
        NextImageH = GameObject.FindGameObjectWithTag("NextItemH").GetComponent<Image>();
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
        NextImageV.sprite = nextItemSprite;
        NextImageH.sprite = nextItemSprite;
    }

    private void Update()
    {
        UpdateScore();
        scoreTextV.text = "Score: " + currentScore.ToString();
        scoreTextH.text = "Score: " + currentScore.ToString();
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
        firstPlaceScoreTextV.text = "1st " + GameData.Instance.highScoreTable[4].highScore;
        secondPlaceScoreTextV.text = "2nd " + GameData.Instance.highScoreTable[3].highScore;
        thirdPlaceScoreTextV.text = "3rd " + GameData.Instance.highScoreTable[2].highScore;
        fourthPlaceScoreTextV.text = "4th " + GameData.Instance.highScoreTable[1].highScore;
        fifthPlaceScoreTextV.text = "5th " + GameData.Instance.highScoreTable[0].highScore;
        firstNameTextV.text = GameData.Instance.highScoreTable[4].playerName;
        secondNameTextV.text = GameData.Instance.highScoreTable[3].playerName;
        thirdNameTextV.text = GameData.Instance.highScoreTable[2].playerName;
        fourthNameTextV.text = GameData.Instance.highScoreTable[1].playerName;
        fifthNameTextV.text = GameData.Instance.highScoreTable[0].playerName;

        firstPlaceScoreTextH.text = "1st " + GameData.Instance.highScoreTable[4].highScore;
        secondPlaceScoreTextH.text = "2nd " + GameData.Instance.highScoreTable[3].highScore;
        thirdPlaceScoreTextH.text = "3rd " + GameData.Instance.highScoreTable[2].highScore;
        fourthPlaceScoreTextH.text = "4th " + GameData.Instance.highScoreTable[1].highScore;
        fifthPlaceScoreTextH.text = "5th " + GameData.Instance.highScoreTable[0].highScore;
        firstNameTextH.text = GameData.Instance.highScoreTable[4].playerName;
        secondNameTextH.text = GameData.Instance.highScoreTable[3].playerName;
        thirdNameTextH.text = GameData.Instance.highScoreTable[2].playerName;
        fourthNameTextH.text = GameData.Instance.highScoreTable[1].playerName;
        fifthNameTextH.text = GameData.Instance.highScoreTable[0].playerName;
    }
}
