using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFollow : MonoBehaviour
{
    private Transform target;
    [SerializeField] private Vector3 topview = new Vector3(0f, 20f);

    private void Start()
    {
        try
        {
            target = FindObjectOfType<PlayerStats>().transform;
        }
        catch
        {
            throw new System.MissingMemberException("Could not find player in scene.");
        }
    }

    void Update()
    {
        Vector3 targetPosition = target.TransformPoint(topview);
        transform.position = targetPosition;
    }
}
