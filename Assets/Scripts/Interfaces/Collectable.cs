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
                case Item.Type.Weapon: return new Weapon(m_itemName);
                case Item.Type.Tool: return new Tool(m_itemName);
                case Item.Type.Consumable:
                    if (m_randomAmount) return new Consumable(m_itemName, Random.Range(1, 41));
                    else return new Consumable(m_itemName, m_itemAmount);
                default: return new Consumable("Null", -1);
            }
        }
    }

    [SerializeField] Item.Type itemType = Item.Type.Weapon;
    [SerializeField] string m_itemName = "";
    [SerializeField] int m_itemAmount = 1;
    [SerializeField] bool m_randomAmount = false;
}
