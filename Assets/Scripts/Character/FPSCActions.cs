using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FPSCActions : MonoBehaviour
{
    public static event Action SHOOT;
    public static event Action<RaycastHit> GUN_HIT;

    Transform mainCam;

    void Start()
    {
        mainCam = Camera.main.transform;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SHOOT?.Invoke();
            if (Physics.Raycast(mainCam.position, mainCam.forward, out RaycastHit hitInfo, 10000f))
            {
                GUN_HIT?.Invoke(hitInfo);
            }
        }

        // restart scene after falling off terrain
        if (transform.position.y < -50f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
