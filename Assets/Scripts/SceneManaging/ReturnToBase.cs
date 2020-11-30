using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToBase : MonoBehaviour, ICollectable
{
    public Item ItemData => throw new System.NotImplementedException();

    public static event System.Action RETURNTOBASE;

    private void OnEnable()
    {
        PlayerInput.GRAB += PlayerInput_GRAB;
    }

    private void OnDisable()
    {
        PlayerInput.GRAB -= PlayerInput_GRAB;
    }

    private void PlayerInput_GRAB()
    {
        RETURNTOBASE?.Invoke();
        PlayerInput.current.TogglePlayerControl(true);
    }
}
