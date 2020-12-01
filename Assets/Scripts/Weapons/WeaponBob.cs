using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponBob : MonoBehaviour
{
    [SerializeField] bool m_canFire = true;
    [SerializeField] bool m_infiniteAmmo = false;

    [Header("Appearance Properties")]
    [SerializeField] AnimationCurve m_appearCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));
    [SerializeField] float m_appearanceSpeed = 1f;
    Vector3 m_originalPosition = Vector3.zero;

    [Header("Effect Properties")]
    [SerializeField] ParticleSystem m_muzzleFlash = null;
    [SerializeField] ParticleSystem m_impact = null;

    [Header("Audio Properties")]
    [SerializeField] AudioClip m_gunShot = null;

    [Header("Weapon Bob Properties")]
    [SerializeField] CurvedObjectBob m_idleBob = new CurvedObjectBob();
    [SerializeField] CurvedObjectBob m_firedBob = new CurvedObjectBob();
    [SerializeField] float m_fireSpeed = 20f;
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
    bool m_appearing = true;

    private void OnEnable()
    {
        PlayerInput.FIRE += PlayerInput_FIRE;
        PlayerInput.CONTROL += PlayerInput_CONTROL;
        StartCoroutine(nameof(OnAppear));
    }

    private void OnDisable()
    {
        PlayerInput.FIRE -= PlayerInput_FIRE;
        PlayerInput.CONTROL += PlayerInput_CONTROL;
    }

    private void PlayerInput_FIRE()
    {
        if (!m_canFire) return;

        if (!m_firing && !m_appearing)
        {
            if (m_infiniteAmmo)
            {
                StartCoroutine(nameof(FiredBob));
            }
            else
            {
                try
                {
                    if (SuppliesManager.current.Ammo() > 0) StartCoroutine(nameof(FiredBob));
                }
                catch
                {
                    Debug.LogWarning("GameData object does not exist. Player will be unable to shoot.");
                }
            }
        }
    }

    private void PlayerInput_CONTROL(bool obj)
    {
        m_canFire = !obj;
    }

    private void Awake()
    {
        m_originalPosition = transform.localPosition;
    }

    private void Start()
    {
        m_fpc = FindObjectOfType<FirstPersonController>();
        m_panim = FindObjectOfType<PlayerAnimator>();
        m_idleBob.Setup(transform, 5f);
        m_firedBob.Setup(transform, 5f);
    }

    private void Update()
    {
        if (m_appearing) return;

        if (!m_firing) IdleBob();
        else m_idleBob.ResetBob();
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

    IEnumerator FiredBob()
    {
        m_firing = true;
        m_muzzleFlash.Play();

        if (!m_infiniteAmmo) SuppliesManager.current.UseAmmo();
        PlayerInput.current.audios.PlayOneShot(m_gunShot, 0.3f);

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
            transform.localPosition = m_firedBob.DoHeadBob(m_fireSpeed);
            if (m_firedBob.FinishedCycle())
            {
                m_firing = false;
                m_firedBob.ResetBob();
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator OnAppear()
    {
        m_appearing = true;

        yield return new WaitUntil(() => m_originalPosition != Vector3.zero);

        var cyclePos = 0f;
        var time = m_appearCurve[m_appearCurve.length - 1].time;

        while (cyclePos < time)
        {
            var yPos = m_originalPosition.y + m_appearCurve.Evaluate(cyclePos);
            transform.localPosition = new Vector3(transform.localPosition.x, yPos, transform.localPosition.z);
            cyclePos += Time.deltaTime * m_appearanceSpeed;
            yield return null;
        }
        transform.localPosition = m_originalPosition;

        yield return new WaitForSeconds(.3f);
        m_appearing = false;
        yield break;
    }
}
