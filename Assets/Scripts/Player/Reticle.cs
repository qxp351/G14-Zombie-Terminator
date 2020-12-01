using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    public enum ReticleType { shoot, grab, talk }
    public static ReticleType Is;

    [SerializeField] List<GameObject> reticleType = new List<GameObject>();
    bool isInMenu = false;

    private void OnEnable()
    {
        PlayerInput.INVENTORY += PlayerInput_INVENTORY;
        PlayerInput.CONTROL += PlayerInput_CONTROL;
    }

    private void OnDisable()
    {
        PlayerInput.INVENTORY -= PlayerInput_INVENTORY;
        PlayerInput.CONTROL -= PlayerInput_CONTROL;
    }

    private void PlayerInput_INVENTORY(bool obj)
    {
        isInMenu = obj;
    }

    private void PlayerInput_CONTROL(bool obj)
    {
        isInMenu = obj;
    }

    private void Update()
    {
        if (isInMenu)
        {
            ReticleOff();
            return;
        }

        if (ReticleManager.Mask == LayerMask.GetMask("Collectable")) ReticleMask(1);
        else if (ReticleManager.Mask == LayerMask.GetMask("Talkable")) ReticleMask(2);
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
