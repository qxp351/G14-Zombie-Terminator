using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : Tool
{
    public Flashlight()
    {
        name = "Flashlight";
    }

    public override void Use()
    {
        WeaponManager.current.EquipFlashlight();
    }
}
