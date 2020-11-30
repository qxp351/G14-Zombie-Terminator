using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static event System.Action<List<Item>> ITEMS;
    public static List<Item> m_items = new List<Item>();

    public static Inventory current;
    private void Awake() => current = this;

    private void Start()
    {
        AddItem(new Pistol());
    }

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
            try
            {
                AddItem(collectable.ItemData);
            }
            catch
            {
                Debug.LogWarning("Object does not have any item data.");
                return;
            }
            Destroy(ReticleManager.Object);
        }
    }

    public void AddItem(Item item)
    {
        if (item is CannedFood || item is Ammo)
        {
            item.Use();
            return;
        }

        if (m_items.Contains(item))
        {
            if (item is Consumable) (m_items[m_items.IndexOf(item)] as Consumable).amount += (item as Consumable).amount;
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
        if (m_items.Contains(item)) item.Use();
        ITEMS?.Invoke(m_items);
    }

    public void DropItem(Consumable item, int amount)
    {
        var consumable = (m_items[m_items.IndexOf(item)] as Consumable);

        consumable.amount -= amount;
        if (consumable.amount <= 0) m_items.Remove(consumable);
        else m_items[m_items.IndexOf(consumable)] = consumable;
        ITEMS?.Invoke(m_items);
    }
}
