using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplaySimple : MonoBehaviour
{
    UnityEngine.UI.Text display = null;

    private void OnEnable()
    {
        Inventory.ITEMS += Inventory_ITEMS;
    }

    private void OnDisable()
    {
        Inventory.ITEMS -= Inventory_ITEMS;
    }

    private void Inventory_ITEMS(List<Item> obj)
    {
        if (!display) display = GetComponent<UnityEngine.UI.Text>();

        display.text = "Inventory:\n";
        foreach (Item item in obj)
        {
            if (item is Weapon) display.text += "Weapon: ";
            if (item is Tool) display.text += "Tool: ";
            if (item is Consumable) display.text += "Consumable: ";

            display.text += $"{item.named} [{item.amount}]\n";
        }
    }
}
