using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitable : MonoBehaviour, IHitable
{
    [SerializeField] GameObject m_impactParticles = null;

    public void OnHit(Vector3 hitPoint, Vector3 hitNormal)
    {
        Instantiate(m_impactParticles, hitPoint, Quaternion.LookRotation(hitNormal, Vector3.up));
        onHit?.Invoke();
    }

    public UnityEngine.Events.UnityEvent onHit;
}
