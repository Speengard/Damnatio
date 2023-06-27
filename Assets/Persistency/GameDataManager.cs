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
    }

    public PlayerStatsManager readPlayerFile()
    {
        print(Application.persistentDataPath);

        if (File.Exists(playerFile))
        {

            string fileContents = System.IO.File.ReadAllText(playerFile);

            playerStats playerCurrentStats = JsonConvert.DeserializeObject<playerStats>(fileContents);  

            return new PlayerStatsManager(playerCurrentStats);

        }

        else return null;
    }

    public void writePlayerData(PlayerStatsManager playerCurrentStats)
    {
#if UNITY_EDITOR
        Debug.Log("in unity editor");
        string json = JsonUtility.ToJson(playerCurrentStats.playerCurrentStats);
        File.WriteAllText(playerFile,json);

#else
        Debug.Log("in other editor");
        string json = JsonConvert.SerializeObject(playerCurrentStats.playerCurrentStats);

        JsonConvert.SerializeObject(json);
#endif
    }

}
