using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeshiftRifle : Tool
{
    public MakeshiftRifle()
    {
        name = "Makeshift Rifle";
    }

    public override void Use()
    {
        WeaponManager.CurrentWeapon = WeaponManager.Weapon.rifle;
    }
}
