using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    public enum ReticleType { shoot, grab }
    public static ReticleType Is;

    [SerializeField] List<GameObject> reticleType = new List<GameObject>();
    [SerializeField] float m_grabDistance = 2f;
    bool isInMenu = false;

    private void OnEnable()
    {
        PlayerInput.INVENTORY += PlayerInput_INVENTORY;
    }

    private void OnDisable()
    {
        PlayerInput.INVENTORY -= PlayerInput_INVENTORY;
    }

    private void PlayerInput_INVENTORY()
    {
        isInMenu = !isInMenu;
    }

    private void Update()
    {
        if (isInMenu)
        {
            ReticleOff();
            return;
        }

        if (ReticleManager.Mask == LayerMask.GetMask("Collectable"))
        {
            if (ReticleManager.Distance <= m_grabDistance) ReticleMask(1);
        }
        else ReticleMask(0);
    }

    void ReticleOff()
    {
        foreach (GameObject obj in reticleType)
        {
            obj.SetActive(false);
        }
    }

    void ReticleMask(int type)
    {
        Is = (ReticleType)type;

        for (int i = 0; i < reticleType.Count; i++)
        {
            if (i == type) reticleType[i].SetActive(true);
            else reticleType[i].SetActive(false);
        }
    }
}
