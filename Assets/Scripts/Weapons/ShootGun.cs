using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour
{
    ParticleSystem muzzleFlash; // particles that appear when the gun is shot
    [SerializeField] GameObject impactPrefab = null; // particles that appear when the "bullet" hits an object

    void Start()
    {
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        FPSCActions.SHOOT += FPSCActions_SHOOT; // subscribes to the player's shoot action
        FPSCActions.GUN_HIT += FPSCActions_GUN_HIT; // subscribes to the player's gun's impact action
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
        // creates the impact particle system at the point the raycast hit
        var obj = Instantiate(impactPrefab, hit.point, impactPrefab.transform.rotation);

        var ps = obj.GetComponent<ParticleSystem>(); // gets the particle system from the game object
        // simulates a bullet having to physically travel a distance
        yield return new WaitForSeconds(hit.distance / 500f);
        ps.Play();
        yield break;
    }
}
