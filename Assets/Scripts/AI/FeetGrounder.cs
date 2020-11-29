using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class FeetGrounder : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] Animator m_anim = null;
    string m_leftFootCurve = "leftFootCurve";
    string m_rightFootCurve = "rightFootCurve";

    [Header("Rig Constraints")]
    [SerializeField] TwoBoneIKConstraint m_leftIK = null;
    [SerializeField] TwoBoneIKConstraint m_rightIK = null;

    [Header("IK Targets")]
    [SerializeField] Transform m_leftTarget = null;
    [SerializeField] Transform m_rightTarget = null;

    [Header("Debug")]
    public float leftCurve;
    public float rightCurve;

    private void Start()
    {
        if (!m_leftIK || !m_rightIK || !m_leftTarget || !m_rightTarget) throw new System.Exception("Missing Components!");

        //StartCoroutine(nameof(LeftFoot));
        //StartCoroutine(nameof(RightFoot));
    }

    private void FixedUpdate()
    {
        if (!m_leftIK || !m_rightIK || !m_leftTarget || !m_rightTarget) throw new System.Exception("Missing Components!");

        leftCurve = m_anim.GetFloat(m_leftFootCurve);
        rightCurve = m_anim.GetFloat(m_rightFootCurve);

        m_leftIK.weight = leftCurve;
        m_rightIK.weight = rightCurve;
    }

    IEnumerator LeftFoot()
    {
        while (Application.isPlaying)
        {
            yield return new WaitUntil(() => m_leftIK.weight == 1f);
            if (Physics.Raycast(m_leftTarget.position, Vector3.down, out RaycastHit hitInfo))
            {
                m_leftTarget.parent = null;
                m_leftTarget.position = hitInfo.point;
            }
            

            yield return new WaitUntil(() => m_leftIK.weight == 0f);
            m_leftTarget.parent = m_leftIK.transform;
        }
    }

    IEnumerator RightFoot()
    {
        var data = m_rightIK.data;
        GameObject newTarget = null;
        while (Application.isPlaying)
        {
            yield return new WaitUntil(() => m_rightIK.weight == 1f);
            if (Physics.Raycast(m_rightTarget.position, Vector3.down, out RaycastHit hitInfo))
            {
                newTarget = Instantiate(new GameObject(), hitInfo.point, Quaternion.identity);
                data.target = newTarget.transform;
            }

            yield return new WaitUntil(() => m_rightIK.weight == 0f);
            data.target = m_rightTarget;
            if (newTarget) Destroy(newTarget);
        }
    }
}
