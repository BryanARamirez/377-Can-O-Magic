using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
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
    public SoundSettings soundSettings;
    public List<GameDataContainer> highScoreTable;

    public List<int> prefabIDS = new List<int>();
    public List<GameObject> prefabGO = new List<GameObject>();
    private List<int> gameObjectIDs = new List<int>();
    public List<GameObject> gameObjectsInScene = new List<GameObject>();

    private List<float> gameObjectsLocationsX = new List<float>();
    private List<float> gameObjectsLocationsY = new List<float>();
    private List<float> gameObjectsLocationsZ = new List<float>();
    private List<int> gameObjectsHaveReacted = new List<int>();
    private List<int> conductingWaterOrb = new List<int>();
    private int steamOn;
    private int steamCount;
    public GameObject steam;
    public bool spawningStart;
    public int screenOrientaionID = 0;



    public override void Awake()
    {
        playerData = FindAnyObjectByType<PlayerData>();
        soundSettings = FindAnyObjectByType<SoundSettings>();
        steam = FindAnyObjectByType<SteamScript>().gameObject;
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
        //For some reason Load(); needs to be active when testing on PC but not when on the actual phone.
        Load();
        RankScores();
    }
    private void OnApplicationQuit()
    {
        Save();
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
        switch (Screen.orientation)
        {
            case ScreenOrientation.Portrait:
            case ScreenOrientation.PortraitUpsideDown:
                gameData.musicVolume = soundSettings.musicSliderV.value;
                break;
            case ScreenOrientation.LandscapeLeft:
            case ScreenOrientation.LandscapeRight:
                gameData.musicVolume = soundSettings.musicSliderH.value;
                break;
            default:
                break;
        }
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
        gameData.screenOrientaionID = screenOrientaionID;

        bf.Serialize(file, gameData);
        file.Close();

        file = File.Open(Application.persistentDataPath + "/sceneData.dat", FileMode.Create);
        GameScene sceneData = new GameScene();
        gameObjectsInScene = new List<GameObject>();
        gameObjectIDs = new List<int>();
        gameObjectsLocationsX = new List<float>();
        gameObjectsLocationsY = new List<float>();
        gameObjectsLocationsZ = new List<float>();
        gameObjectsHaveReacted = new List<int>();
        conductingWaterOrb = new List<int>();
        steamOn = 0;
        steamCount = 0;
        if (steam.GetComponent<SteamScript>()._currentDropCount > 0)
        {
            steamOn = 1;
            steamCount = steam.GetComponent<SteamScript>()._currentDropCount;
        }
        sceneData.steamOn = steamOn;
        sceneData.steamCount = steamCount;
        gameObjectsInScene.AddRange(GameObject.FindGameObjectsWithTag("MagicItem"));
        gameObjectsInScene.AddRange(GameObject.FindGameObjectsWithTag("PowerItem"));
        sceneData.currentOrb = (int)playerData.gameObject.GetComponent<PlayerController>().currentObj.GetComponent<MagicalItemScript>().magicItemName;
        Debug.Log("Current Orb ID: " + sceneData.currentOrb);
        sceneData.nextOrbIndex = playerData.gameObject.GetComponent<PlayerController>().randomNextIndex;
        for (int i = 0; i < gameObjectsInScene.Count; i++)
        {
            if (gameObjectsInScene[i].gameObject.tag == "MagicItem")
            {
                if (gameObjectsInScene[i].GetComponent<MagicalItemScript>().hasDropped)
                {
                    //Debug.Log("Working");
                    gameObjectIDs.Add((int)gameObjectsInScene[i].GetComponent<MagicalItemScript>().magicItemName);
                    gameObjectsLocationsX.Add(gameObjectsInScene[i].transform.position.x);
                    gameObjectsLocationsY.Add(gameObjectsInScene[i].transform.position.y);
                    gameObjectsLocationsZ.Add(gameObjectsInScene[i].transform.position.z);
                    if (gameObjectsInScene[i].GetComponent<MagicalItemScript>().hasReacted)
                    {
                        gameObjectsHaveReacted.Add(1);
                    }
                    else
                    {
                        gameObjectsHaveReacted.Add(0);
                    }
                }
                if (gameObjectsInScene[i].GetComponent<MagicalItemScript>().magicItemName == MagicItemEnum.WaterOrb)
                {
                    if(gameObjectsInScene[i].GetComponent<WaterPlasmaReaction>()._isConducting)
                    {
                        conductingWaterOrb.Add(1);
                    }
                }
                else
                {
                    conductingWaterOrb.Add(0);
                }
            }
            if (gameObjectsInScene[i].gameObject.tag == "PowerItem")
            {
                //Debug.Log("Power ItemSaved");
                gameObjectIDs.Add((int)gameObjectsInScene[i].GetComponent<PowerItemIDHolder>().powerItemName + 11);
                gameObjectsLocationsX.Add(gameObjectsInScene[i].transform.position.x);
                gameObjectsLocationsY.Add(gameObjectsInScene[i].transform.position.y);
                gameObjectsLocationsZ.Add(gameObjectsInScene[i].transform.position.z);
            }
        }

        if(PowerItemData.Instance.checkAvailable(PowerItemEnum.MimicTongue))
        {
            sceneData.hasMimicTongue = 1;
        }
        else
        {
            sceneData.hasMimicTongue = 0;
        }
        if (PowerItemData.Instance.checkAvailable(PowerItemEnum.SlimeBall))
        {
            sceneData.hasSlimeBall = 1;
        }
        else
        {
            sceneData.hasSlimeBall = 0;
        }
        if (PowerItemData.Instance.checkAvailable(PowerItemEnum.HolyAura))
        {
            sceneData.hasHolyAura = 1;
        }
        else
        {
            sceneData.hasHolyAura = 0;
        }
        if (PowerItemData.Instance.checkAvailable(PowerItemEnum.Bomb))
        {
            sceneData.hasBomb = 1;
        }
        else
        {
            sceneData.hasBomb = 0;
        }

        sceneData.gameObjectsIDS = gameObjectIDs.ToArray();
        sceneData.gameObjectLocationX = gameObjectsLocationsX.ToArray();
        sceneData.gameObjectLocationY = gameObjectsLocationsY.ToArray();
        sceneData.gameObjectLocationZ = gameObjectsLocationsZ.ToArray();
        sceneData.gameObjectsHaveReacted = gameObjectsHaveReacted.ToArray();
        sceneData.conductingWaterOrb = conductingWaterOrb.ToArray();


        bf.Serialize(file, sceneData);
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
            screenOrientaionID = gameData.screenOrientaionID;
            soundSettings.musicSliderH.value = gameData.musicVolume;
            soundSettings.musicSliderV.value = gameData.musicVolume;
            soundSettings.SetMusicVolume();
        }

        if(File.Exists(Application.persistentDataPath + "/sceneData.dat"))
        {
            spawningStart = true;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/sceneData.dat", FileMode.Open);
            GameScene sceneData = (GameScene)bf.Deserialize(file);
            file.Close();
            Time.timeScale = 0;

            for (int i = 0; i < sceneData.gameObjectsIDS.Length; i++)
            {
                float tempLocationX = sceneData.gameObjectLocationX[i];
                float tempLocationY = sceneData.gameObjectLocationY[i];
                float tempLocationZ = sceneData.gameObjectLocationZ[i];
                Vector3 tempLocation = new Vector3((float)tempLocationX, (float)tempLocationY, (float)tempLocationZ);
                Debug.Log(sceneData.gameObjectsIDS[i]);
                GameObject NewItem = Instantiate(prefabGO[sceneData.gameObjectsIDS[i]], tempLocation, Quaternion.identity);
                if(NewItem.tag == "MagicItem")
                {
                    NewItem.GetComponent<MagicalItemScript>().SetDrop();
                    if (sceneData.gameObjectsHaveReacted[i] == 1)
                    {
                        NewItem.GetComponent<MagicalItemScript>().Reacted();
                    }
                    if(NewItem.GetComponent<MagicalItemScript>().magicItemName == MagicItemEnum.WaterOrb)
                    {
                        if (sceneData.conductingWaterOrb[i] == 1)
                        {
                            NewItem.GetComponent<OnLoadConduction>().canConduct = true;
                        }
                    }
                }
            }
            if (sceneData.steamOn == 1)
            {
                steam.GetComponent<SteamScript>().ActivateSteam();
                steam.GetComponent<SteamScript>()._currentDropCount = sceneData.steamCount;
            }
            if(sceneData.hasSlimeBall == 1)
                PowerItemData.Instance.GainPowerItem(PowerItemEnum.SlimeBall);
            if (sceneData.hasHolyAura == 1)
                PowerItemData.Instance.GainPowerItem(PowerItemEnum.HolyAura);
            if (sceneData.hasBomb == 1)
                PowerItemData.Instance.GainPowerItem(PowerItemEnum.Bomb);
            if (sceneData.hasMimicTongue == 1)
                PowerItemData.Instance.GainPowerItem(PowerItemEnum.MimicTongue);
            playerData.gameObject.GetComponent<PlayerController>().ReplaceCurrentItem(prefabGO[sceneData.currentOrb]);
            playerData.gameObject.GetComponent<PlayerController>().randomNextIndex = sceneData.nextOrbIndex;
            Time.timeScale = 1;
        }
        Time.timeScale = 1;
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
    public float musicVolume;
    public int screenOrientaionID;
}

[Serializable]
public class GameScene
{
    public int[] gameObjectsIDS;
    public float[] gameObjectLocationX;
    public float[] gameObjectLocationY;
    public float[] gameObjectLocationZ;
    public int[] gameObjectsHaveReacted;
    public int[] conductingWaterOrb;
    public int hasHolyAura;
    public int hasBomb;
    public int hasSlimeBall;
    public int hasMimicTongue;
    public int steamOn;
    public int steamCount;
    public int currentOrb;
    public int nextOrbIndex;
}