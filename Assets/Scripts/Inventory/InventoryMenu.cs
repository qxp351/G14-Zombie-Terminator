using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class InventoryMenu : MonoBehaviour
{
    public static bool inMenu = false;
    CanvasGroup m_cg = null;

    private void OnEnable()
    {
        PlayerInput.INVENTORY += PlayerInput_INVENTORY;
    }

    private void OnDisable()
    {
        PlayerInput.INVENTORY -= PlayerInput_INVENTORY;
    }

    private void PlayerInput_INVENTORY(bool obj)
    {
        if (!m_cg) m_cg = GetComponent<CanvasGroup>();
        if (!obj)
        {
            inMenu = false;
            m_cg.alpha = 0f;
            m_cg.interactable = false;
            m_cg.blocksRaycasts = false;
            WeaponManager.current.CloseInventory();
        }
        else
        {
            inMenu = true;
            m_cg.alpha = 1f;
            m_cg.interactable = true;
            m_cg.blocksRaycasts = true;
            WeaponManager.current.OpenInventory();
        }
    }
}
