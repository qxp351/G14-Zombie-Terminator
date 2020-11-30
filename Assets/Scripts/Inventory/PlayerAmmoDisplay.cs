using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmoDisplay : MonoBehaviour
{
    UnityEngine.UI.Text m_display;

    private void Start()
    {
        m_display = GetComponentInChildren<UnityEngine.UI.Text>();
        try
        {
            m_display.text = $"{SuppliesManager.current.Ammo()}";
        }
        catch
        {
            throw new System.MissingMemberException("Game Data object cannot be found.");
        }
    }

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
        m_display.text = $"{obj}";
    }
}
