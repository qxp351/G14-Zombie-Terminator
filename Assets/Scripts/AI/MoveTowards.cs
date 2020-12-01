using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    [Header("Target Properties")]
    [SerializeField] Transform m_target = null;
    [SerializeField] Vector3 m_speeds = new Vector3(3f, 6f, 9f);
    [SerializeField] float m_turnRadius = 0.05f;
    [SerializeField] float m_stoppingDistance = 1f;

    [Header("Obstacle Avoidance Properties")]
    [SerializeField] float m_rayDistance = 1f;
    [SerializeField] float m_avoidanceMultiplier = 15f;

    [Header("Audio Properties")]
    [SerializeField] List<AudioClip> m_noises = new List<AudioClip>();
    AudioSource m_audios;

    [Header("Options")]
    [SerializeField] bool m_useRootMotion = true;

    Quaternion m_lookAt;
    Animator m_anim;
    Vector3 m_transformPos;
    Vector3 m_targetPos;
    Vector3 m_offset;

    LevelConditions.Difficulty m_difficulty;
    readonly List<Vector2Int> m_docileRanges = new List<Vector2Int>
    {
        new Vector2Int(0, 39),
        new Vector2Int(0, 19),
        new Vector2Int(0, 9)
    };
    readonly List<Vector2Int> m_agitatedRanges = new List<Vector2Int>
    {
        new Vector2Int(40, 69),
        new Vector2Int(20, 69),
        new Vector2Int(10, 29)
    };
    readonly List<Vector2Int> m_crazedRanges = new List<Vector2Int>
    {
        new Vector2Int(70, 99),
        new Vector2Int(70, 99),
        new Vector2Int(30, 99)
    };

    private void Start()
    {
        m_anim = GetComponent<Animator>();
        m_target = FindObjectOfType<PlayerStats>().transform;
        m_audios = GetComponent<AudioSource>();

        m_difficulty = WeightedDifficulty();
        StartCoroutine(nameof(MakeNoises));
    }

    private void Update()
    {
        if (m_target && !m_anim.GetBool("isDead"))
        {
            GetPositions();
            switch (m_difficulty)
            {
                case LevelConditions.Difficulty.docile: MoveTo_Docile(); break;
                case LevelConditions.Difficulty.agitated: MoveTo_Agitated(); break;
                case LevelConditions.Difficulty.crazed: MoveTo_Crazed(); break;
            }
        }
    }

    LevelConditions.Difficulty WeightedDifficulty()
    {
        var value = Random.Range(0, 100);

        try
        {
            switch (LevelConditions.current.zombieAggression)
            {
                case LevelConditions.Difficulty.docile:
                    if (value > m_docileRanges[0].x && value <= m_docileRanges[0].y) return LevelConditions.Difficulty.docile;
                    else if (value > m_agitatedRanges[0].x && value <= m_agitatedRanges[0].y) return LevelConditions.Difficulty.agitated;
                    else if (value > m_crazedRanges[0].x && value <= m_crazedRanges[0].y) return LevelConditions.Difficulty.crazed;
                    else return LevelConditions.Difficulty.docile;
                case LevelConditions.Difficulty.agitated:
                    if (value > m_docileRanges[1].x && value <= m_docileRanges[1].y) return LevelConditions.Difficulty.docile;
                    else if (value > m_agitatedRanges[1].x && value <= m_agitatedRanges[1].y) return LevelConditions.Difficulty.agitated;
                    else if (value > m_crazedRanges[1].x && value <= m_crazedRanges[1].y) return LevelConditions.Difficulty.crazed;
                    else return LevelConditions.Difficulty.docile;
                case LevelConditions.Difficulty.crazed:
                    if (value > m_docileRanges[2].x && value <= m_docileRanges[2].y) return LevelConditions.Difficulty.docile;
                    else if (value > m_agitatedRanges[2].x && value <= m_agitatedRanges[2].y) return LevelConditions.Difficulty.agitated;
                    else if (value > m_crazedRanges[2].x && value <= m_crazedRanges[2].y) return LevelConditions.Difficulty.crazed;
                    else return LevelConditions.Difficulty.docile;
            }
        }
        catch
        {
            Debug.LogWarning("GameData does not exist. Agitated difficulty will be chosen.");
        }

        return LevelConditions.Difficulty.agitated;
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
        transform.rotation = Quaternion.Slerp(transform.rotation, m_lookAt, m_turnRadius);

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
        transform.rotation = Quaternion.Slerp(transform.rotation, m_lookAt, m_turnRadius);

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
        transform.rotation = Quaternion.Slerp(transform.rotation, m_lookAt, m_turnRadius);

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

    IEnumerator MakeNoises()
    {
        while (Application.isPlaying)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            try
            {
                m_audios.PlayOneShot(m_noises[Random.Range(0, m_noises.Count)]);
            }
            catch
            {
                Debug.LogWarning("m_noises contains no audio clips.");
            }
        }
    }
}
