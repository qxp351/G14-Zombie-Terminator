using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    public Consumable(string name, int amount)
    {
        named = name;
        this.amount = amount;
    }
    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
