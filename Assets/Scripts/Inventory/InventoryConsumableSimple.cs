using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryConsumableSimple : MonoBehaviour
{
    [SerializeField] GameObject m_slotPrefab = null;

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
        GameObject prefab;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out InventorySlot slot))
            {
                Destroy(slot.gameObject);
            }
        }
        foreach (Item item in obj)
        {
            if (!(item is Consumable)) continue;
            prefab = Instantiate(m_slotPrefab, transform);
            if (prefab.TryGetComponent(out InventorySlot slot))
            {
                slot.Setup(item);
            }
        }
    }
}
