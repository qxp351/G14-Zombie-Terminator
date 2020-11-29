using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    UnityEngine.UI.Text display;

    private void Start()
    {
        display = GetComponentInChildren<UnityEngine.UI.Text>();
    }

    private void OnEnable()
    {
        PlayerStats.HEALTH += PlayerStats_HEALTH;
    }

    private void OnDisable()
    {
        PlayerStats.HEALTH -= PlayerStats_HEALTH;
    }

    private void PlayerStats_HEALTH(int obj)
    {
        if (display)
        {
            display.text = $"{obj}";
        }
    }
}
