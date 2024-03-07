using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Playables;


public class GameData : MonoBehaviour
{
    public bool hasDoneTutorial;
    public int hasDoneTutorialInt;
    public int highScore;
    public List<GameDataContainer> highScoreTable;

    private void Awake()
    {
        if(File.Exists(Application.persistentDataPath + "/gameData.dat") == false)
        {
            highScoreTable = new List<GameDataContainer>()
            { 
                new GameDataContainer{highScore = 300},
                new GameDataContainer{highScore = 250},
                new GameDataContainer{highScore = 200},
                new GameDataContainer{highScore = 150},
                new GameDataContainer{highScore = 100}
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
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gameData.dat", FileMode.Create);
        GameDataContainer gameData = new GameDataContainer();
        gameData.hasDoneTutorialInt = hasDoneTutorial ? 1 : 0;
        gameData.highScore = highScore;
        gameData.firstScore = highScoreTable[0].highScore;
        gameData.secondScore = highScoreTable[1].highScore;
        gameData.thirdScore = highScoreTable[2].highScore;
        gameData.fourthScore = highScoreTable[3].highScore;
        gameData.fifthScore = highScoreTable[4].highScore;
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
                new GameDataContainer{highScore = gameData.firstScore},
                new GameDataContainer{highScore = gameData.secondScore},
                new GameDataContainer{highScore = gameData.thirdScore},
                new GameDataContainer{highScore = gameData.fourthScore},
                new GameDataContainer{highScore = gameData.fifthScore}
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
    public string firstName;
    public string secondName;
    public string thirdName;
    public string fourthName;
    public string fifthName;
}
