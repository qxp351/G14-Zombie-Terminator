using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats
{
    public static event Action HEAL;
    public static event Action DAMAGE;
    public static event Action<int> HEALTH;

    public static PlayerStats current;
    private void Awake() => current = this;

    public override void Heal(int amount)
    {
        base.Heal(amount);
        HEAL?.Invoke();
        HEALTH?.Invoke(health.x);
    }
    public override void Damage(int amount)
    {
        base.Damage(amount);
        DAMAGE?.Invoke();
        HEALTH?.Invoke(health.x);
    }

    protected override IEnumerator Die()
    {
        throw new NotImplementedException();
    }
}
