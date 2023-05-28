using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerStatsManager {
    //this class is used as a shell for the json encoding/decoding container PlayerStats. it provides constructor methods that would otherwise be in the PlayerStats class.
    public playerStats playerCurrentStats;

    public PlayerStatsManager(playerStats from){
        playerCurrentStats = new playerStats();

        playerCurrentStats.healthLevel = from.healthLevel;
        playerCurrentStats.damageLevel = from.damageLevel;
        playerCurrentStats.dropLevel = from.dropLevel;
        playerCurrentStats.collectedSouls = from.collectedSouls;
        playerCurrentStats.maxHealth = from.maxHealth;
        playerCurrentStats.dropRate = from.dropRate;
        playerCurrentStats.speed = from.speed;
        playerCurrentStats.morningStarDamage = from.morningStarDamage;
        playerCurrentStats.rangedDamage = from.rangedDamage;

    }

    public PlayerStatsManager(){
        playerCurrentStats = new playerStats();
    } 
}

[System.Serializable]
public class playerStats 
{
    //this class serves as a container for json encoding/decoding
        public int healthLevel = 1;
        public int damageLevel = 2;
        public int dropLevel = 3;
        public int collectedSouls = 1000;
        public int maxHealth = 100;
        public int dropRate = 1;
        public int health = 100;
        public int speed = 10;
        public int morningStarDamage = 10;
        public int rangedDamage = 5;
}



