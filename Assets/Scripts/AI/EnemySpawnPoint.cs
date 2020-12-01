using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public static HashSet<GameObject> spawnedEnemies = new HashSet<GameObject>();
    public static  Vector3Int m_maxEnemies = new Vector3Int(10, 30, 50);
    public static Vector3 m_spawnSpeeds = new Vector3(8f, 5f, 2f);

    [SerializeField] GameObject m_enemyPrefab = null;

    private void OnDisable()
    {
        if (spawnedEnemies.Count > 0) spawnedEnemies.Clear();
    }

    private void Start()
    {
        if (LevelConditions.current)
        {
            if (LevelConditions.current.zombieAggression == LevelConditions.Difficulty.docile)
            {
                StartCoroutine(Spawn(m_maxEnemies.x, m_spawnSpeeds.x));
            }
            else if (LevelConditions.current.zombieAggression == LevelConditions.Difficulty.agitated)
            {
                StartCoroutine(Spawn(m_maxEnemies.y, m_spawnSpeeds.y));
            }
            else if (LevelConditions.current.zombieAggression == LevelConditions.Difficulty.crazed)
            {
                StartCoroutine(Spawn(m_maxEnemies.z, m_spawnSpeeds.z));
            }
        }
    }

    IEnumerator Spawn(int maxEnemies, float spawnSpeed)
    {
        yield return new WaitForSeconds(Random.Range(0f, 3f));

        while (Application.isPlaying)
        {
            if (spawnedEnemies.Count >= maxEnemies)
            {
                yield return new WaitUntil(() => spawnedEnemies.Count < maxEnemies);
            }
            yield return new WaitForSeconds(spawnSpeed);

            var obj = Instantiate(m_enemyPrefab, transform.position, m_enemyPrefab.transform.rotation);
            spawnedEnemies.Add(obj);
            Debug.Log($"spawned enemies: {spawnedEnemies.Count}");
            yield return null;
        }
        yield break;
    }
}
