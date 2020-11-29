using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    MoveTowards m_moveto;
    Animator m_anim;

    [Header("Attack Properties")]
    [SerializeField] float m_attackDistance = 1.6f;

    [Header("Debug Properties")]
    [SerializeField] float m_distance = 0f;

    private void Start()
    {
        m_anim = GetComponent<Animator>();
        m_moveto = GetComponent<MoveTowards>();
    }

    private void Update()
    {
        if (m_moveto.Target()) m_distance = Vector3.Distance(transform.position, m_moveto.Target().position);
        if (m_distance < m_attackDistance)
        {
            if (!m_anim.GetBool("isAttacking")) m_anim.SetBool("isAttacking", true);
        }
        else
        {
            if (m_anim.GetBool("isAttacking")) m_anim.SetBool("isAttacking", false);
        }
    }

    public void Attack(int obj)
    {
        if (m_anim.GetBool("isAttacking"))
        {
            m_moveto.Target().TryGetComponent(out PlayerStats player);
            if (player) player.Damage(obj);
        }
    }
}
