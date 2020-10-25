using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    ParticleSystem muzzleFlash;
    [SerializeField] GameObject impactPrefab = null;

    void Start()
    {
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        FPSCActions.SHOOT += FPSCActions_SHOOT;
        FPSCActions.GUN_HIT += FPSCActions_GUN_HIT;
    }

    void FPSCActions_SHOOT()
    {
        muzzleFlash.Play();
    }

    private void FPSCActions_GUN_HIT(RaycastHit obj)
    {
        StartCoroutine(Impact(obj));
    }

    IEnumerator Impact(RaycastHit hit)
    {
        var obj = Instantiate(impactPrefab, hit.point, impactPrefab.transform.rotation);

        var ps = obj.GetComponent<ParticleSystem>();
        yield return new WaitForSeconds(hit.distance / 500f);
        ps.Play();
        yield break;
    }
}
