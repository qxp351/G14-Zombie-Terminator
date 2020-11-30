using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuppliesManager : MonoBehaviour
{
    public static event Action<int> AMMO;
    public static event Action<int> FOOD;

    [SerializeField] int m_currentAmmo = 9;
    [SerializeField] int m_currentFood = 1;

    public static SuppliesManager current;
    private void Awake()
    {
        current = this;
        DontDestroyOnLoad(this);
    }

    public int Ammo() => m_currentAmmo;
    public void UpdateAmmo(int obj)
    {
        m_currentAmmo += obj;
        AMMO?.Invoke(m_currentAmmo);
    }
    public void UseAmmo()
    {
        m_currentAmmo = m_currentAmmo - 1 < 0 ? 0 : m_currentAmmo - 1;
        AMMO?.Invoke(m_currentAmmo);
    }

    public int Food() => m_currentFood;
    public void UpdateFood(int obj)
    {
        m_currentFood += obj;
        FOOD?.Invoke(m_currentFood);
    }
    public void UseFood()
    {
        m_currentFood = m_currentFood - 1 < 0 ? 0 : m_currentFood - 1;
        FOOD?.Invoke(m_currentFood);
    }
}
