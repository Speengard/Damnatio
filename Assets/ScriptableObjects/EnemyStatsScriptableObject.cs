using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats")]
public class EnemyStatsScriptableObject : ScriptableObject
{
    public string enemyName;
    public int health;
    public int damage;
    public float dropChance; // chance for enemies to dropQuantity objects
    public float dropQuantity; // number of dropped objects
}
