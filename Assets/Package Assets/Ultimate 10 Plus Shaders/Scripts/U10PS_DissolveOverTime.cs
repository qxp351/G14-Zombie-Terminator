using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U10PS_DissolveOverTime : MonoBehaviour
{
    SkinnedMeshRenderer skinnedMeshRenderer;

    public float speed = .5f;

    private void Start(){
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    private float t = 0.0f;
    private void Update(){
        Material[] mats = skinnedMeshRenderer.materials;

        foreach (Material mat in mats)
        {
            if (mat.GetFloat("_Cutoff") < 1f) mat.SetFloat("_Cutoff", Mathf.Sin(t * speed));
        }
        t += Time.deltaTime;
        
        // Unity does not allow meshRenderer.materials[0]...
        skinnedMeshRenderer.materials = mats;
    }
}
