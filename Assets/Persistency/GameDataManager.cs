using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class GameDataManager : MonoBehaviour
{

    //this class is used to access IO for storing different files. right now since we just have to store the player data, just the playerfile string containing the path to the json. in the future, this implementation would have to be changed in order to choose which path to access.
    string playerFile;
    void Awake()
    {
        playerFile = Application.persistentDataPath + "/PlayerCurrentStats.json";

        Debug.Log("Playerfile" + playerFile);
    }

    public PlayerStatsManager readPlayerFile()
    {
        print(Application.persistentDataPath);

        if (File.Exists(playerFile))
        {
            Debug.Log("File exists");
            string fileContents = System.IO.File.ReadAllText(playerFile);

            playerStats playerCurrentStats = JsonConvert.DeserializeObject<playerStats>(fileContents);  

            Debug.Log("Health" + playerCurrentStats.healthLevel);
            return new PlayerStatsManager(playerCurrentStats);

        }

        else return null;
    }

    public void writePlayerData(PlayerStatsManager playerCurrentStats)
    {
        print("before json");
        string json = JsonConvert.SerializeObject(playerCurrentStats.playerCurrentStats);

        Debug.Log("json:" + json);
        JsonConvert.SerializeObject(json);
        print("reading back json:" + readPlayerFile());
    }
}
