using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannedFood : Consumable
{
    public CannedFood()
    {
        name = "Canned Food";
        amount = 1;
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
