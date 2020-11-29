using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, ICollectable
{
    public Item ItemData
    {
        get
        {
            switch(itemType)
            {
                case Item.Type.Flashlight: return new Flashlight();
                case Item.Type.Pistol: return new Pistol();
                case Item.Type.MakeshiftRifle: return new MakeshiftRifle();
                case Item.Type.Launcher: return new Launcher();
                case Item.Type.Medkit: return new Medkit();
                case Item.Type.Ammo: return new Ammo();
                case Item.Type.CannedFood: return new CannedFood();
                default: throw new System.NotSupportedException();
            }
        }
    }

    [SerializeField] Item.Type itemType = Item.Type.Pistol;
}
