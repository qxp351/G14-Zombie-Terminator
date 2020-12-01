using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealDisplay : MonoBehaviour
{
    [Header("Animation Properties")]
    [SerializeField] AnimationCurve alphaCurve = new AnimationCurve();
    float m_CyclePosition, m_Time, m_OriginalAlphaValue;
    CanvasGroup m_cg;
    bool m_isThrobbing = false;

    private void Start()
    {
        m_cg = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        PlayerStats.HEAL += PlayerStats_DAMAGE;
    }

    private void OnDisable()
    {
        PlayerStats.HEAL -= PlayerStats_DAMAGE;
    }

    private void PlayerStats_DAMAGE()
    {
        if (!m_isThrobbing) StartCoroutine(nameof(ThrobEffect));
    }

    IEnumerator ThrobEffect()
    {
        // setup
        m_isThrobbing = true;
        m_OriginalAlphaValue = m_cg.alpha;
        m_Time = alphaCurve[alphaCurve.length - 1].time;

        while (m_CyclePosition < m_Time)
        {
            m_cg.alpha = m_OriginalAlphaValue + (alphaCurve.Evaluate(m_CyclePosition));
            m_CyclePosition += Time.deltaTime;
            yield return null;
        }
        m_CyclePosition -= m_Time;
        m_isThrobbing = false;
        yield break;
    }
}
