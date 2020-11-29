using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    public enum ReticleType { shoot, grab }
    public static ReticleType Is;

    [SerializeField] List<GameObject> reticleType = new List<GameObject>();
    [SerializeField] float m_grabDistance = 2f;

    private void Update()
    {
        if (ReticleManager.Mask == LayerMask.GetMask("Collectable"))
        {
            if (ReticleManager.Distance <= m_grabDistance) ReticleMask(1);
        }
        else ReticleMask(0);
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
