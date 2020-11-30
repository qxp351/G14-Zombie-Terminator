using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPoint : MonoBehaviour
{
    [SerializeField] List<GameObject> m_itemPrefabs = new List<GameObject>();
    [SerializeField] Vector3 m_spawnRates = new Vector3(0.8f, 0.5f, 0.4f);

    private void Start()
    {
        float odds = 1f;
        if (LevelConditions.current)
        {
            if (LevelConditions.current.zombieAggression == LevelConditions.Difficulty.docile)
                odds = m_spawnRates.x;
            else if (LevelConditions.current.zombieAggression == LevelConditions.Difficulty.agitated)
                odds = m_spawnRates.y;
            else if (LevelConditions.current.zombieAggression == LevelConditions.Difficulty.crazed)
                odds = m_spawnRates.z;
        }

        if (Random.Range(0f, 1.01f) < odds)
        {
            var prefab = m_itemPrefabs[Random.Range(0, m_itemPrefabs.Count)];
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
