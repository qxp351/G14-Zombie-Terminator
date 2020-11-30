using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 topview;

    void Update()
    {
        Vector3 targetPosition = target.TransformPoint(topview);
        transform.position = targetPosition;
    }
}
