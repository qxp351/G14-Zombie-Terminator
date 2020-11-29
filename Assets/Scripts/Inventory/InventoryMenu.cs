using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class InventoryMenu : MonoBehaviour
{
    public static bool inMenu = false;
    CanvasGroup m_cg = null;
    FirstPersonController fpc = null;

    private void OnEnable()
    {
        PlayerInput.INVENTORY += PlayerInput_INVENTORY;
    }

    private void OnDisable()
    {
        PlayerInput.INVENTORY -= PlayerInput_INVENTORY;
    }

    private void PlayerInput_INVENTORY()
    {
        if (!fpc) fpc = FindObjectOfType<FirstPersonController>();
        if (!m_cg) m_cg = GetComponent<CanvasGroup>();
        if (m_cg.alpha == 1f)
        {
            inMenu = false;
            m_cg.alpha = 0f;
            m_cg.interactable = false;
            m_cg.blocksRaycasts = false;
            fpc.ToggleCursor(true);
            WeaponManager.current.SwapWeapon(WeaponManager.Weapon.pistol);
        }
        else if (m_cg.alpha == 0f)
        {
            inMenu = true;
            m_cg.alpha = 1f;
            m_cg.interactable = true;
            m_cg.blocksRaycasts = true;
            fpc.ToggleCursor(false);
            WeaponManager.current.SwapWeapon(WeaponManager.Weapon.bag);
        }
    }
}
