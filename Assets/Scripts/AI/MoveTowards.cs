using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    [Header("Target Properties")]
    [SerializeField] Transform m_target = null;
    [SerializeField] Vector3 m_speeds = new Vector3(3f, 6f, 9f);
    [SerializeField] Vector3 m_turnRadii = new Vector3(0.05f, 0.07f, 0.1f);
    [SerializeField] float m_stoppingDistance = 1f;

    [Header("Obstacle Avoidance Properties")]
    [SerializeField] float m_rayDistance = 1f;
    [SerializeField] float m_avoidanceMultiplier = 15f;

    [Header("Options")]
    [SerializeField] bool m_useRootMotion = true;

    Quaternion m_lookAt;
    Animator m_anim;
    Vector3 m_transformPos;
    Vector3 m_targetPos;
    Vector3 m_offset;

    private void Start()
    {
        m_anim = GetComponent<Animator>();
        m_target = FindObjectOfType<PlayerStats>().transform;
    }

    private void Update()
    {
        if (m_target && !m_anim.GetBool("isDead"))
        {
            GetPositions();
            if (LevelConditions.current)
            {
                if (LevelConditions.current.zombieAggression == LevelConditions.Difficulty.docile)
                {
                    MoveTo_Docile();
                }
                else if (LevelConditions.current.zombieAggression == LevelConditions.Difficulty.agitated)
                {
                    MoveTo_Agitated();
                }
                else if (LevelConditions.current.zombieAggression == LevelConditions.Difficulty.crazed)
                {
                    MoveTo_Crazed();
                }
            }
        }
    }

    void GetPositions()
    {
        // grab positions from the current frame that do not care about the y axis
        m_transformPos = new Vector3(transform.position.x, 0f, transform.position.z);
        m_targetPos = new Vector3(m_target.position.x, 0f, m_target.position.z);
    }

    void MoveTo_Docile()
    {
        // determine if there is an obstacle in between this and the player
        if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), transform.forward, m_rayDistance))
        {
            m_offset = Vector3.right * m_avoidanceMultiplier;
        }
        else m_offset = Vector3.zero;

        // slerp rotate towards the target
        m_lookAt = Quaternion.LookRotation(m_targetPos - (m_transformPos + m_offset), Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, m_lookAt, m_turnRadii.x);

        // move towards target if there is no root motion
        if (!m_useRootMotion) transform.position += transform.forward * m_speeds.x * Time.deltaTime;


        // determine animation states depending on distance to target
        if (Vector3.Distance(transform.position, m_target.position) > m_stoppingDistance)
        {
            if (!m_anim.GetBool("isWalking")) m_anim.SetBool("isWalking", true);
        }
        else
        {
            if (m_anim.GetBool("isWalking")) m_anim.SetBool("isWalking", false);
        }
    }

    void MoveTo_Agitated()
    {
        // determine if there is an obstacle in between this and the player
        if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), transform.forward, m_rayDistance))
        {
            m_offset = Vector3.right * m_avoidanceMultiplier;
        }
        else m_offset = Vector3.zero;

        // slerp rotate towards the target
        m_lookAt = Quaternion.LookRotation(m_targetPos - (m_transformPos + m_offset), Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, m_lookAt, m_turnRadii.y);

        // move towards target if there is no root motion
        if (!m_useRootMotion) transform.position += transform.forward * m_speeds.y * Time.deltaTime;


        // determine animation states depending on distance to target
        if (Vector3.Distance(transform.position, m_target.position) > m_stoppingDistance)
        {
            if (!m_anim.GetBool("isRunning")) m_anim.SetBool("isRunning", true);
        }
        else
        {
            if (m_anim.GetBool("isRunning")) m_anim.SetBool("isRunning", false);
        }
    }

    void MoveTo_Crazed()
    {
        // determine if there is an obstacle in between this and the player
        if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), transform.forward, m_rayDistance))
        {
            m_offset = Vector3.right * m_avoidanceMultiplier;
        }
        else m_offset = Vector3.zero;

        // slerp rotate towards the target
        m_lookAt = Quaternion.LookRotation(m_targetPos - (m_transformPos + m_offset), Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, m_lookAt, m_turnRadii.z);

        // move towards target if there is no root motion
        if (!m_useRootMotion) transform.position += transform.forward * m_speeds.z * Time.deltaTime;


        // determine animation states depending on distance to target
        if (Vector3.Distance(transform.position, m_target.position) > m_stoppingDistance)
        {
            if (!m_anim.GetBool("isSprinting")) m_anim.SetBool("isSprinting", true);
        }
        else
        {
            if (m_anim.GetBool("isSprinting")) m_anim.SetBool("isSprinting", false);
        }
    }

    public Transform Target() => m_target;
}
