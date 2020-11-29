using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : Consumable
{
    public Medkit()
    {
        name = "Medkit";
        amount = 1;
    }

    public override void Use()
    {
        PlayerStats.current.Heal(3);
        Inventory.current.DropItem(this, 1);
    }
}
