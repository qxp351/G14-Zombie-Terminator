using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Button button;
    Text display;

    public void Setup(Item item)
    {
        if (!display) display = GetComponentInChildren<Text>();
        if (!button) button = GetComponent<Button>();

        var str = item is Consumable ? $" [{(item as Consumable).amount}]" : "";
        display.text = $"{item.name}";

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => Button_OnClick(item));
    }

    public void Button_OnClick(Item item)
    {
        Inventory.current.UseItem(item);
    }
}
