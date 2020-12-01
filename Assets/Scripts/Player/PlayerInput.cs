using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public static event Action FIRE;
    public static event Action GRAB;
    public static event Action TALK;
    public static event Action<bool> INVENTORY;
    public static event Action<bool> CONTROL;

    FirstPersonController m_fpc;
    bool m_inInventory = false;
    public static bool disabler = false;

    public static PlayerInput current;
    private void Awake() => current = this;

    private void Start()
    {
        m_fpc = GetComponent<FirstPersonController>();
        disabler = false;
        StartCoroutine(nameof(StepUpdate));
        TogglePlayerControl(false);
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
            if (CrossPlatformInputManager.GetButtonDown("Fire1") && Reticle.Is == Reticle.ReticleType.talk)
            {
                TALK?.Invoke();
                yield return new WaitForSeconds(0.2f);
            }
            if (CrossPlatformInputManager.GetButton("Fire1") && Reticle.Is == Reticle.ReticleType.shoot) FIRE?.Invoke();
            if (CrossPlatformInputManager.GetButtonDown("Inventory") && m_fpc.IsGrounded() && disabler == false)
            {
                m_inInventory = !m_inInventory;
                TogglePlayerControl(m_inInventory);
                INVENTORY?.Invoke(m_inInventory);
            }
            if (CrossPlatformInputManager.GetButtonDown("Flashlight") && WeaponManager.HasFlashlight)
            {
                WeaponManager.current.ToggleFlashlight();
            }
            yield return null;
        }
    }

    public void TogglePlayerControl(bool obj)
    {
        Cursor.lockState = obj ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = obj;
        m_fpc.enabled = !obj;
        CONTROL?.Invoke(obj);
    }

    public static void InvokeDeath()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneIndices.GameOver);
    }

    public static void DisableInventory()
    {
        disabler = true;
    }
}
