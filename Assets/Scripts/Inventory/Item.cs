using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Item
{
    public enum Type { Weapon, Tool, Consumable }
    public string named;
    public int amount;
    public abstract void Use();
}
