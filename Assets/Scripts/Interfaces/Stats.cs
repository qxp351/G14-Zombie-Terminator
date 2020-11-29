using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stats : MonoBehaviour
{
    [Header("Status Properties")]
    [SerializeField] protected Vector2Int health = new Vector2Int(20, 20);
    bool isDying = false;

    public virtual void Damage(int amount)
    {
        health.x = health.x - amount < 0 ? 0 : health.x - amount;

        if (health.x == 0 && !isDying)
        {
            isDying = true;
            StartCoroutine(nameof(Die));
        }
    }

    protected abstract IEnumerator Die();
}
