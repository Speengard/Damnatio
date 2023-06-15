using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;



public class PlayerStatsManager {

    public int[] healthIncrease = {0, 5, 15, 30, 45, 55 };
    public int[] dropIncrease = {0, 2, 2, 2, 4, 10 };
    public int[] damageIncrease = { 0, 1, 1, 2, 2, 3 };

    //this class is used as a shell for the json encoding/decoding container PlayerStats. it provides constructor methods that would otherwise be in the PlayerStats class.
    public playerStats playerCurrentStats;

    //these two constructors serve as a way to create a copy given a single PlayerStats instance and to create a blank playerstats instance using the default values the class provides
    public PlayerStatsManager(playerStats from){
        playerCurrentStats = new playerStats();

        playerCurrentStats.healthLevel = from.healthLevel;
        playerCurrentStats.meleeLevel = from.meleeLevel;
        playerCurrentStats.rangedLevel = from.rangedLevel;
        playerCurrentStats.dropLevel = from.dropLevel;
        playerCurrentStats.collectedSouls = from.collectedSouls;
        playerCurrentStats.maxHealth = from.maxHealth;
        playerCurrentStats.dropRate = from.dropRate;
        playerCurrentStats.morningStarDamage = from.morningStarDamage;
        playerCurrentStats.rangedDamage = from.rangedDamage;

    }

    public PlayerStatsManager(){
        playerCurrentStats = new playerStats();
    } 

    public void BuyPowerUp(string type){

        switch(type){
            case "health":
                playerCurrentStats.maxHealth += healthIncrease[playerCurrentStats.healthLevel];
                playerCurrentStats.health = playerCurrentStats.maxHealth; 
            break;

            case "melee":
                playerCurrentStats.rangedDamage += damageIncrease[playerCurrentStats.meleeLevel];
            break;

            case "ranged":
                playerCurrentStats.rangedDamage += damageIncrease[playerCurrentStats.rangedLevel];
                break;

            case "drop":
                playerCurrentStats.dropRate += dropIncrease[playerCurrentStats.dropLevel];
            break;

            default:
            throw new System.Exception();
        }

    }
}

[System.Serializable]
public class playerStats 
{
    //this class serves as a container for json encoding/decoding
        public int healthLevel = 0;
        public int meleeLevel = 0;
        public int rangedLevel = 0;
        public int dropLevel = 0;
        public int collectedSouls = 0;
        public int maxHealth = 100;
        public int dropRate = 1;
        public int health = 100;
        public int morningStarDamage = 10;
        public int rangedDamage = 5;
}



