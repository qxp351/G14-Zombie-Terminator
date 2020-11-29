using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponBob : MonoBehaviour
{
    [SerializeField] bool canFire = true;

    [Header("Effect Properties")]
    [SerializeField] ParticleSystem m_muzzleFlash = null;
    [SerializeField] ParticleSystem m_impact = null;

    [Header("Weapon Bob Properties")]
    [SerializeField] CurvedObjectBob m_idleBob = new CurvedObjectBob();
    [SerializeField] CurvedObjectBob m_firedBob = new CurvedObjectBob();
    [SerializeField] CurvedObjectBob m_swapBob = new CurvedObjectBob();
    [SerializeField] float fireSpeed = 20f;
    FirstPersonController m_fpc;
    PlayerAnimator m_panim;

    [Header("Idle Bob Ranges")]
    [SerializeField] Vector3 m_whileIdle = new Vector3(0.000015f, 0.000015f, 0.000015f);
    [SerializeField] Vector3 m_whileWalking = new Vector3(0.000025f, 0.000025f, 0.000025f);
    [SerializeField] Vector3 m_whileRunning = new Vector3(0.00005f, 0.00005f, 0.00005f);

    [Header("Idle Bob Speeds")]
    [SerializeField] Vector3 m_speedWhileIdle = new Vector3(1.6f, 1.4f, 1.5f);
    [SerializeField] Vector3 m_speedWhileMoving = new Vector3(1.6f, 1.5f, 1.4f);

    bool m_firing = false;
    bool m_swaping = false;

    private void OnEnable()
    {
        if (canFire) PlayerInput.FIRE += PlayerInput_FIRE;
    }

    private void OnDisable()
    {
        if (canFire) PlayerInput.FIRE -= PlayerInput_FIRE;
    }

    private void PlayerInput_FIRE()
    {
        if (!m_firing) StartCoroutine(nameof(FiredBob));
    }

    private void Start()
    {
        m_fpc = FindObjectOfType<FirstPersonController>();
        m_panim = FindObjectOfType<PlayerAnimator>();
        m_idleBob.Setup(transform, 5f);
        m_firedBob.Setup(transform, 5f);
        m_swapBob.Setup(transform, 5f);
    }

    private void Update()
    {
        if (!m_firing) IdleBob();
        else if (m_swaping) SwapBob();
        else m_idleBob.ResetBob();
    }

    IEnumerator FiredBob()
    {
        m_firing = true;
        m_muzzleFlash.Play();

        var mask = LayerMask.GetMask("Hitable");
        var cam = Camera.main.transform;

        if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, 1000f, mask))
        {
            hitInfo.collider.TryGetComponent(out IHitable hit);
            if (hit != null) hit.OnHit(hitInfo.point, hitInfo.normal);
            else
            {
                Instantiate(m_impact, hitInfo.point, Quaternion.LookRotation(hitInfo.normal, Vector3.up));
            }
        }

        while (m_firing)
        {
            transform.localPosition = m_firedBob.DoHeadBob(fireSpeed);
            if (m_firedBob.FinishedCycle())
            {
                m_firing = false;
                m_firedBob.ResetBob();
                yield break;
            }
            yield return null;
        }
    }

    void IdleBob()
    {
        if (m_panim.Speed() == 0 || m_fpc.IsJumping())
        {
            m_idleBob.BobRange = m_whileIdle;

            m_idleBob.speed = m_speedWhileIdle;
        }
        else
        {
            m_idleBob.speed = m_speedWhileMoving;

            if (m_fpc.IsWalking())
            {
                m_idleBob.BobRange = m_whileWalking;
            }
            else
            {
                m_idleBob.BobRange = m_whileRunning;
                m_idleBob.BobRange.x = m_whileRunning.x * 2f;
            }
        }
        transform.localPosition = m_idleBob.DoHeadBob((m_panim.Speed() == 0 ? 1f : m_panim.Speed()) * 2f);
    }

    void SwapBob()
    {
        transform.localPosition = m_swapBob.DoHeadBob(1f);
    }
}
