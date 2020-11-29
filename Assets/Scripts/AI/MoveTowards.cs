using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    [Header("Target Properties")]
    [SerializeField] Transform m_target = null;
    [SerializeField] float m_speed = 5f;
    [SerializeField] float m_turnRadius = 0.05f;
    [SerializeField] float m_stoppingDistance = 1f;

    [Header("Option")]
    [SerializeField] bool m_useRootMotion = true;
    [SerializeField] bool m_useGroundedMotion = true;

    Quaternion m_lookAt;
    Animator m_anim;
    Vector3 m_transformPos;
    Vector3 m_targetPos;

    private void Start()
    {
        m_anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (m_target && !m_anim.GetBool("isDead")) MoveToTarget();
    }

    void MoveToTarget()
    {
        // grab positions from the current frame, with or with out the y axis
        m_transformPos = new Vector3(transform.position.x, m_useGroundedMotion ? 0f : transform.position.y, transform.position.z);
        m_targetPos = new Vector3(m_target.position.x, m_useGroundedMotion ? 0f : m_target.position.y, m_target.position.z);

        // slerp rotate towards the target
        m_lookAt = Quaternion.LookRotation(m_targetPos - m_transformPos, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, m_lookAt, m_turnRadius);

        // move towards target if there is no root motion
        if (!m_useRootMotion) transform.position += transform.forward * m_speed * Time.deltaTime;


        // determine animation states depending on distance to target
        if (Vector3.Distance(m_transformPos, m_targetPos) > m_stoppingDistance)
        {
            if (!m_anim.GetBool("isWalking")) m_anim.SetBool("isWalking", true);
        }
        else
        {
            if (m_anim.GetBool("isWalking")) m_anim.SetBool("isWalking", false);
        }
    }

    public Transform Target() => m_target;
}
