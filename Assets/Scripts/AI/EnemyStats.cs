using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats
{
    [Header("Custom Properties")]
    [SerializeField] Material m_deathMat = null;

    [Header("Custom Animation Properties")]
    [Range(0f, 1f)]
    [SerializeField] float m_animationSpeed = .2f;
    [Range(0f, 1f)]
    [SerializeField] float m_dissolveSpeed = .3f;
    [SerializeField] Gradient m_animationGradient = new Gradient();
    [SerializeField] AnimationCurve m_cutoffCurve = new AnimationCurve();

    protected override IEnumerator Die()
    {
        try
        {
            EnemySpawnPoint.spawnedEnemies.Remove(gameObject);
            Debug.Log($"spawned enemies: {EnemySpawnPoint.spawnedEnemies.Count}");
        }
        catch
        {
            Debug.LogWarning("Could not remove this enemy from the spawned enemies list. continuing script...");
        }

        var anim = GetComponentInChildren<Animator>();
        if (anim)
        {
            if (anim.GetBool("isWalking")) anim.SetBool("isWalking", false);
            if (!anim.GetBool("isDead"))
            {
                anim.SetTrigger("die");
                anim.SetBool("isDead", true);
            }
        }

        // begin death material animation
        transform.GetChild(0).GetChild(0).TryGetComponent(out SkinnedMeshRenderer mesh);
        if (mesh)
        {
            var mats = mesh.materials;
            float cyclePosition = 0f;
            var curveTime = m_cutoffCurve[m_cutoffCurve.length - 1].time;
            var gradientTime = m_animationGradient.colorKeys[m_animationGradient.colorKeys.Length - 1].time;

            // step 1: turn the material black and smoothness to 0
            while (cyclePosition < gradientTime)
            {
                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i].color = m_animationGradient.Evaluate(cyclePosition);
                }
                cyclePosition += Time.deltaTime * m_animationSpeed;

                mesh.materials = mats;
                yield return null;
            }
            cyclePosition = 0f;

            // step 2: swap to the death material
            for (int i = 0; i < mats.Length; i++) mats[i] = m_deathMat;
            mesh.materials = mats;

            // step 3: "dissolve" the mesh
            while (cyclePosition < curveTime)
            {
                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i].SetFloat("_Cutoff", m_cutoffCurve.Evaluate(cyclePosition));
                }
                cyclePosition += Time.deltaTime * m_dissolveSpeed;

                mesh.materials = mats;
                yield return null;
            }
            Destroy(gameObject);
        }
        yield break;
    }
}
