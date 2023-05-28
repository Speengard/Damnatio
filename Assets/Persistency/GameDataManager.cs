using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameDataManager : MonoBehaviour
{
    string playerFile;
    // Start is called before the first frame update
    void Awake()
    {
        playerFile = Application.persistentDataPath + "/PlayerCurrentStats.json";
    }

    public PlayerStatsManager readPlayerFile(){
        print(Application.persistentDataPath);
        if(File.Exists(playerFile)){
            
            string fileContents = System.IO.File.ReadAllText(playerFile);

            playerStats playerCurrentStats = JsonUtility.FromJson<playerStats>(fileContents);

            return new PlayerStatsManager(playerCurrentStats);

        }
        
        else return null;
    }

    public void writePlayerData(PlayerStatsManager playerCurrentStats){
        
            string json = JsonUtility.ToJson(playerCurrentStats.playerCurrentStats);
            System.IO.File.WriteAllText(playerFile, json);
        
    }
}
