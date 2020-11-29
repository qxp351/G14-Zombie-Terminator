using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : Consumable
{
    public Ammo()
    {
        name = "Ammo";
        amount = Random.Range(1, 41);
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
