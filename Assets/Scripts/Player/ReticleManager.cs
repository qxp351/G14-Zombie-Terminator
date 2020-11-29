using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleManager : MonoBehaviour
{
    Ray ray;
    LayerMask rayMask;
    public static bool hit;
    public static RaycastHit hitInfo;

    private void Start()
    {
        rayMask = LayerMask.GetMask(new string[] { "Hitable", "Collectable" });
    }

    private void Update()
    {
        ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hitInfo, 1000f, rayMask)) hit = true;
        else hit = false;
    }
}
