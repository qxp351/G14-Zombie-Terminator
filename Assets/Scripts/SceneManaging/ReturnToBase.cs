using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToBase : MonoBehaviour, ITalkable
{
    public static event System.Action RETURNTOBASE;

    public void SpeakTo()
    {
        RETURNTOBASE?.Invoke();
        PlayerInput.current.TogglePlayerControl(true);
        PlayerInput.current.ToggleInventory(false);
    }
}
