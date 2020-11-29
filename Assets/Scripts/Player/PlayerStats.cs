using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    public static event System.Action DAMAGE;
    public static event System.Action<int> HEALTH;

    public static PlayerStats current;
    private void Awake() => current = this;

    public override void Damage(int amount)
    {
        base.Damage(amount);
        DAMAGE?.Invoke();
        HEALTH?.Invoke(health.x);
    }

    protected override IEnumerator Die()
    {
        throw new System.NotImplementedException();
    }
}
