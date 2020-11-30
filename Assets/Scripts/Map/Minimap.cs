using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    CanvasGroup m_cg;

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

        m_cg.alpha = obj ? 0f : 1f;
    }
}
