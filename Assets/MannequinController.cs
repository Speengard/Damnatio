using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannequinController : EnemyHealthController
{
    [SerializeField] private BoxCollider2D boxCollider;

    private void OnCollisionEnter2D(Collision2D other) {
        TakeDamage(0);
    }

    public override void TakeDamage(int damage)
    {
        StartCoroutine(base.GiveRedTint());
    }
}
