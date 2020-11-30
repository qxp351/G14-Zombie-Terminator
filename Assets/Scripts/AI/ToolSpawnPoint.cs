using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSpawnPoint : MonoBehaviour
{
    [SerializeField] List<GameObject> m_toolPrefabs = new List<GameObject>();
    [SerializeField] int m_chosenTool = 0;

    private void Start()
    {
        var prefab = m_toolPrefabs[m_chosenTool];
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
