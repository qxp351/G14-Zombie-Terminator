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
    [SerializeField] List<float> m_values = new List<float> { 10f, 1f, 2f, 3f, 4f, 2f, 3f, 4f };
    [SerializeField]
    List<Vector3> m_multipliers = new List<Vector3>
    {
        new Vector3(2f, 1f, 0.5f),
        new Vector3(1.5f, 1f, 0.5f),
        new Vector3(1.5f, 1f, 0.5f),
        new Vector3(1.5f, 1f, 0.5f),
        new Vector3(1.5f, 1f, 0.5f),
        new Vector3(1.5f, 1f, 0.5f),
        new Vector3(1.5f, 1f, 0.5f),
        new Vector3(1.5f, 1f, 0.5f)
    };
    [SerializeField]
    List<Vector3> m_weaponMultipliers = new List<Vector3>
    {
        new Vector3(1f, 0.9f, 0.5f),
        new Vector3(1.2f, 1f, 0.7f)
    };

    public void OnHit(Vector3 hitPoint, Vector3 hitNormal)
    {
        var i = (int)limb;

        var w = (int)WeaponManager.CurrentWeapon;
        w = w < m_weaponMultipliers.Count && w >= 0 ? w : 0;

        float m = 1f, wm = 1f;
        if (LevelConditions.current)
        {
            if (LevelConditions.current.zombieAggression == LevelConditions.Difficulty.docile)
            {
                m = m_multipliers[i].x;
                wm = m_weaponMultipliers[w].x;
            }
            if (LevelConditions.current.zombieAggression == LevelConditions.Difficulty.agitated)
            {
                m = m_multipliers[i].y;
                wm = m_weaponMultipliers[w].y;
            }
            if (LevelConditions.current.zombieAggression == LevelConditions.Difficulty.crazed)
            {
                m = m_multipliers[i].z;
                wm = m_weaponMultipliers[w].z;
            }
        }
        var damageValue = wm * (m_values[i] * m);

        Instantiate(m_impactParticles, hitPoint, Quaternion.LookRotation(hitNormal, Vector3.up));
        onHit?.Invoke((int)damageValue);
    }
}
