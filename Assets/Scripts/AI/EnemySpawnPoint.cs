using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] GameObject m_enemyPrefab = null;
    [SerializeField] Vector3 m_startDelays = new Vector3(3f, 0f);
    [SerializeField] Vector3 m_spawnSpeeds = new Vector3(8f, 5f, 2f);

    private void Start()
    {
        if (LevelConditions.current)
        {
            if (LevelConditions.current.zombieAggression == LevelConditions.Difficulty.docile)
            {
                StartCoroutine(Spawn(m_startDelays.x, m_spawnSpeeds.x));
            }
            else if (LevelConditions.current.zombieAggression == LevelConditions.Difficulty.agitated)
            {
                StartCoroutine(Spawn(m_startDelays.y, m_spawnSpeeds.y));
            }
            else if (LevelConditions.current.zombieAggression == LevelConditions.Difficulty.crazed)
            {
                StartCoroutine(Spawn(m_startDelays.z, m_spawnSpeeds.z));
            }
        }
    }

    IEnumerator Spawn(float startDelay, float spawnSpeed)
    {
        yield return new WaitForSeconds(startDelay);

        while (Application.isPlaying)
        {
            yield return new WaitForSeconds(spawnSpeed);

            Instantiate(m_enemyPrefab, transform.position, m_enemyPrefab.transform.rotation);
            yield return null;
        }
        yield break;
    }
}
