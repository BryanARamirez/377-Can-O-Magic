using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Playables;


public class GameData : Singleton<GameData>
{
    public bool hasDoneTutorial;
    public bool nameEntered;
    public bool gameIsOver;
    public int hasDoneTutorialInt;
    public int highScore;
    public string playerName;
    public TouchScreenKeyboard keyboard;
    public PlayerData playerData;
    public List<GameDataContainer> highScoreTable;

    public override void Awake()
    {
        playerData = FindAnyObjectByType<PlayerData>();
        if (File.Exists(Application.persistentDataPath + "/gameData.dat") == false)
        {
            highScoreTable = new List<GameDataContainer>()
            { 
                new GameDataContainer{highScore = 300, playerName = "Pablo"},
                new GameDataContainer{highScore = 250, playerName = "Pablo"},
                new GameDataContainer{highScore = 200, playerName = "Emma"},
                new GameDataContainer{highScore = 150, playerName = "Rick"},
                new GameDataContainer{highScore = 100, playerName = "Jim"}
            };
            RankScores();
            Debug.Log("New Scores");
        }
        DontDestroyOnLoad(this.gameObject);
        Load();
        RankScores();
    }
    public void RankScores()
    {
        for (int i = 0; i < highScoreTable.Count; i++)
        {
            for (int j = i + 1; j < highScoreTable.Count; j++)
            {
                if (highScoreTable[j].highScore < highScoreTable[i].highScore)
                {
                    GameDataContainer tmp = highScoreTable[i];
                    highScoreTable[i] = highScoreTable[j];
                    highScoreTable[j] = tmp;
                }
            }
        }
    }
    public void UpdateScoreboard()
    {
        if (playerData.currentScore > GameData.Instance.highScoreTable[0].highScore)
        {
            highScoreTable[0].highScore = playerData.currentScore;
            highScoreTable[0].playerName = playerName;
            RankScores();
        }
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gameData.dat", FileMode.Create);
        GameDataContainer gameData = new GameDataContainer();
        gameData.hasDoneTutorialInt = hasDoneTutorial ? 1 : 0;
        gameData.highScore = highScore;
        gameData.playerName = playerName;
        gameData.firstScore = highScoreTable[0].highScore;
        gameData.secondScore = highScoreTable[1].highScore;
        gameData.thirdScore = highScoreTable[2].highScore;
        gameData.fourthScore = highScoreTable[3].highScore;
        gameData.fifthScore = highScoreTable[4].highScore;
        gameData.firstName = highScoreTable[0].playerName;
        gameData.secondName = highScoreTable[1].playerName;
        gameData.thirdName = highScoreTable[2].playerName;
        gameData.fourthName = highScoreTable[3].playerName;
        gameData.fifthName = highScoreTable[4].playerName;
        bf.Serialize(file, gameData);
        file.Close();
    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/gameData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameData.dat", FileMode.Open);
            GameDataContainer gameData = (GameDataContainer)bf.Deserialize(file);
            file.Close();
            hasDoneTutorialInt = gameData.hasDoneTutorialInt;
            if(hasDoneTutorialInt == 0)
            {
                hasDoneTutorial = false;
            }
            else if(hasDoneTutorialInt == 1)
            {
                hasDoneTutorial = true;
            }
            highScore = gameData.highScore;
            highScoreTable = new List<GameDataContainer>()
            {
                new GameDataContainer{highScore = gameData.firstScore, playerName = gameData.firstName},
                new GameDataContainer{highScore = gameData.secondScore, playerName = gameData.secondName},
                new GameDataContainer{highScore = gameData.thirdScore, playerName = gameData.thirdName},
                new GameDataContainer{highScore = gameData.fourthScore, playerName = gameData.fourthName},
                new GameDataContainer{highScore = gameData.fifthScore, playerName = gameData.fifthName}
            };
        }
    }
}

[Serializable]
public class GameDataContainer
{
    public int hasDoneTutorialInt;
    public int highScore;
    public int firstScore;
    public int secondScore;
    public int thirdScore;
    public int fourthScore;
    public int fifthScore;
    public string playerName;
    public string firstName;
    public string secondName;
    public string thirdName;
    public string fourthName;
    public string fifthName;
}
