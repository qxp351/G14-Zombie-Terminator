using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleManager : MonoBehaviour
{
    Ray m_ray;
    LayerMask m_mask;
    RaycastHit m_hit;

    public static LayerMask Mask;
    public static float Distance;
    public static GameObject Object;

    private void Start()
    {
        m_mask = LayerMask.GetMask(new string[] { "Hitable", "Collectable" });
    }

    private void Update()
    {
        m_ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(m_ray, out m_hit, 1000f, m_mask))
        {
            Mask = LayerMask.GetMask(LayerMask.LayerToName(m_hit.transform.gameObject.layer));
            Distance = m_hit.distance;
            Object = m_hit.transform.gameObject;
        }
    }
}
