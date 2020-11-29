using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item
{
    public enum Type { Flashlight, Pistol, MakeshiftRifle, Launcher, Medkit, Ammo, CannedFood }
    public string name;
    public abstract void Use();
}
