using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleManager : MonoBehaviour
{
    Ray m_ray;
    LayerMask m_mask;
    RaycastHit m_hit;

    public static LayerMask Mask;
    public static GameObject Object;

    [SerializeField] float m_rayDistance = 3f;

    LayerMask m_talkMask;
    float m_talkRayDistance = 6f;

    [Header("Debug Properties")]
    [SerializeField] LayerMask m_debugMask;
    [SerializeField] GameObject m_object;

    private void Start()
    {
        m_mask = LayerMask.GetMask(new string[] { "Hitable", "Collectable" });
        m_talkMask = LayerMask.GetMask("Talkable");
    }

    private void Update()
    {
        m_ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(m_ray, out m_hit, m_rayDistance, m_mask))
        {
            Mask = LayerMask.GetMask(LayerMask.LayerToName(m_hit.transform.gameObject.layer));
            Object = m_hit.transform.gameObject;
        }
        else if (Physics.Raycast(m_ray, out m_hit, m_talkRayDistance, m_talkMask))
        {
            Mask = LayerMask.GetMask(LayerMask.LayerToName(m_hit.transform.gameObject.layer));
            Object = m_hit.transform.gameObject;
        }
        else
        {
            Mask = LayerMask.GetMask();
            Object = null;
        }
    }

    private void LateUpdate()
    {
        m_debugMask = Mask;
        m_object = Object;
    }
}
