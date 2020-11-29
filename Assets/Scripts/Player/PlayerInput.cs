using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerInput : MonoBehaviour
{
    public static event Action FIRE;
    public static event Action GRAB;
    public static event Action INVENTORY;

    private void Start()
    {
        StartCoroutine(nameof(StepUpdate));
    }

    IEnumerator StepUpdate()
    {
        while (Application.isPlaying)
        {
            if (CrossPlatformInputManager.GetButtonDown("Fire1") && Reticle.Is == Reticle.ReticleType.grab)
            {
                GRAB?.Invoke();
                yield return new WaitForSeconds(0.2f);
            }
            if (CrossPlatformInputManager.GetButton("Fire1") && Reticle.Is == Reticle.ReticleType.shoot) FIRE?.Invoke();
            if (CrossPlatformInputManager.GetButtonDown("Inventory")) INVENTORY?.Invoke();
            yield return null;
        }
    }
}
