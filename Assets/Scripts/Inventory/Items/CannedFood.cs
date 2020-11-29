using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannedFood : Tool
{
    public CannedFood()
    {
        name = "Canned Food";
    }

    public override void Use()
    {
        SuppliesManager.current.UpdateFood(1);
    }
}
