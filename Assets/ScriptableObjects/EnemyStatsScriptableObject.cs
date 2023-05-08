using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats")]
public class EnemyStatsScriptableObject : ScriptableObject
{
    public string enemyName;
    public int health = 3;
    public int damage = 3;
    public int speed;
    public int dropChance; // chance for enemies to drop objects
}
