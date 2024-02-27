using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class GameData : MonoBehaviour
{
    public bool hasDoneTutorial;
    public int hasDoneTutorialInt;
    public int highScore;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Load();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath +
            "/gameData.dat", FileMode.Create);
        GameDataContainer gameData = new GameDataContainer();
        gameData.hasDoneTutorialInt = hasDoneTutorial ? 1 : 0;
        gameData.highScore = highScore;
        bf.Serialize(file, gameData);
        file.Close();
    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath +
            "/gameData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath +
                "/gameData.dat", FileMode.Open);
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
        }
    }
}

[Serializable]
public class GameDataContainer
{
    public int hasDoneTutorialInt;
    public int highScore;
}
