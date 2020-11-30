﻿using System;
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
    public static event Action INVENTORY;

    FirstPersonController m_fpc;
    bool m_inInventory = false;

    RawImage m_cam;

    private void Start()
    {
        m_fpc = GetComponent<FirstPersonController>();
        //m_cam = GameObject.FindGameObjectWithTag("MiniCam").GetComponent<RawImage>();
        StartCoroutine(nameof(StepUpdate));
        ToggleCursor(false);
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
            if (CrossPlatformInputManager.GetButtonDown("Inventory") && m_fpc.IsGrounded())
            {
                INVENTORY?.Invoke();
                m_inInventory = !m_inInventory;
                ToggleCursor(m_inInventory);
                m_fpc.enabled = !m_inInventory;
                //m_cam.enabled = !m_inInventory;
            } 
            if (CrossPlatformInputManager.GetButtonDown("Flashlight") && WeaponManager.HasFlashlight)
            {
                WeaponManager.current.ToggleFlashlight();
            }
            yield return null;
        }
    }

    void ToggleCursor(bool obj)
    {
        Cursor.lockState = obj ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = obj;
    }
}
