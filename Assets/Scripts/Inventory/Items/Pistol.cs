using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Tool
{
    public Pistol()
    {
        name = "Pistol";
    }

    public override void Use()
    {
        WeaponManager.current.SwapWeapon(WeaponManager.Weapon.pistol);
    }
}
