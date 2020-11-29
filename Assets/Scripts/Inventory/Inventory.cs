using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static event System.Action<List<Item>> ITEMS;
    [SerializeField] List<Item> m_items = new List<Item>();

    public static Inventory current;
    private void Awake() => current = this;

    private void OnEnable()
    {
        PlayerInput.GRAB += PlayerInput_GRAB;
    }

    private void OnDisable()
    {
        PlayerInput.GRAB -= PlayerInput_GRAB;
    }

    private void PlayerInput_GRAB()
    {
        if (ReticleManager.Object.TryGetComponent(out ICollectable collectable))
        {
            AddItem(collectable.ItemData);
            Destroy(ReticleManager.Object);
        }
    }

    public void AddItem(Item item)
    {
        if (m_items.Contains(item))
        {
            if (item is Consumable) (m_items[m_items.IndexOf(item)] as Consumable).amount += 1;
            else return;
        }
        else
        {
            m_items.Add(item);
        }
        ITEMS?.Invoke(m_items);
    }

    public void UseItem(Item item)
    {
        item.Use();
        ITEMS?.Invoke(m_items);
    }

    public void DropItem(Consumable item, int amount)
    {
        (m_items[m_items.IndexOf(item)] as Consumable).amount -= amount;
        ITEMS?.Invoke(m_items);
    }
}
