using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : Tool
{
    public Launcher()
    {
        name = "Grenade Launcher";
    }

    public override void Use()
    {
        WeaponManager.current.SwapWeapon(WeaponManager.Weapon.launcher);
    }
}
