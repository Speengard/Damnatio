using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStatsScriptableObject : ScriptableObject
{
    public int health = 50;
    public int rangedDamage = 2;
    public int morningStarDamage = 1;
    public int speed;
    public int dropChance; // chance for enemies to drop objects
}
