using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverConditions : MonoBehaviour
{
    Text m_display;

    private void Start()
    {
        m_display = GetComponent<Text>();

        try
        {
            if (SuppliesManager.current.Food() == 0) m_display.text = "YOU STARVED";
            else m_display.text = "YOU ARE DEAD";
        }
        catch
        {
            m_display.text = "YOU DIED";
        }
    }
}
