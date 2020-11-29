using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoodDisplay : MonoBehaviour
{
    UnityEngine.UI.Text m_display;

    private void OnEnable()
    {
        SuppliesManager.FOOD += SuppliesManager_FOOD;
    }

    private void OnDisable()
    {
        SuppliesManager.FOOD -= SuppliesManager_FOOD;
    }

    private void SuppliesManager_FOOD(int obj)
    {
        if (!m_display) m_display = GetComponentInChildren<UnityEngine.UI.Text>();

        m_display.text = $"{obj}";
    }
}
