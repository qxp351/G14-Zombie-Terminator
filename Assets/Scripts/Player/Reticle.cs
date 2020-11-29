using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    public enum ReticleType { shoot, grab }
    public static ReticleType Is;

    [SerializeField] List<GameObject> reticleType = new List<GameObject>();
    [SerializeField] float m_grabDistance = 2f;
    LayerMask layer;

    private void Update()
    {
        if (ReticleManager.hit)
        {
            layer = LayerMask.GetMask(LayerMask.LayerToName(ReticleManager.hitInfo.transform.gameObject.layer));
            if (layer == LayerMask.GetMask("Collectable"))
            {
                if (ReticleManager.hitInfo.distance <= m_grabDistance) ReticleMask(1);
            }
            else ReticleMask(0);
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
