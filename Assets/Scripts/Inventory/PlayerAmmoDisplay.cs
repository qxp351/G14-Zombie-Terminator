using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmoDisplay : MonoBehaviour
{
    UnityEngine.UI.Text m_display;

    private void OnEnable()
    {
        SuppliesManager.AMMO += SuppliesManager_AMMO;
    }

    private void OnDisable()
    {
        SuppliesManager.AMMO -= SuppliesManager_AMMO;
    }

    private void SuppliesManager_AMMO(int obj)
    {
        if (!m_display) m_display = GetComponentInChildren<UnityEngine.UI.Text>();

        m_display.text = $"{obj}";
    }
}
