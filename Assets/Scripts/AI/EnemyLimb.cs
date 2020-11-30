using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLimb : MonoBehaviour, IHitable
{
    [SerializeField] GameObject m_impactParticles = null;

    public enum Limb { head, body, forearm, arm, hand, thigh, calf, foot }
    [SerializeField] Limb limb = Limb.head;

    public UnityEngine.Events.UnityEvent<int> onHit;

    [Header("Damage Properties")]
    [SerializeField] List<float> m_values = new List<float>(8);
    [SerializeField] List<float> m_weaponValues = new List<float>(2);
    [SerializeField] List<Vector3> m_multipliers = new List<Vector3>(8);

    public void OnHit(Vector3 hitPoint, Vector3 hitNormal)
    {
        var i = (int)limb;

        var w = (int)WeaponManager.CurrentWeapon;
        w = w < m_weaponValues.Count && w >= 0 ? w : 0;

        var m = 1f;
        if (LevelConditions.current)
        {
            if (LevelConditions.current.zombieAggression == LevelConditions.Difficulty.docile) m = m_multipliers[i].x;
            if (LevelConditions.current.zombieAggression == LevelConditions.Difficulty.agitated) m = m_multipliers[i].y;
            if (LevelConditions.current.zombieAggression == LevelConditions.Difficulty.crazed) m = m_multipliers[i].z;
        }
        var damageValue = m_weaponValues[w] + (m_values[i] * m);

        Instantiate(m_impactParticles, hitPoint, Quaternion.LookRotation(hitNormal, Vector3.up));
        onHit?.Invoke((int)damageValue);
    }
}
