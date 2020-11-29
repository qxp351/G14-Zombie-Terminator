using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float speed = 0f;
    FirstPersonController fpc;
    Vector3 previousPosition;

    private void Start()
    {
        fpc = transform.parent.parent.GetComponent<FirstPersonController>();
        if (!anim) anim = GetComponent<Animator>();
        StartCoroutine(nameof(CalculateSpeed));
    }

    IEnumerator CalculateSpeed()
    {
        while (Application.isPlaying)
        {
            previousPosition = transform.parent.parent.position;
            yield return new WaitForFixedUpdate();
            speed = Mathf.RoundToInt(Vector3.Distance(transform.parent.parent.position, previousPosition) / Time.fixedDeltaTime);

            if (speed == fpc.WalkSpeed())
            {
                if (anim.GetBool("isRunning")) anim.SetBool("isRunning", false);
                if (!anim.GetBool("isWalking")) anim.SetBool("isWalking", true);
            }
            else if (speed == fpc.RunSpeed())
            {
                if (anim.GetBool("isWalking")) anim.SetBool("isWalking", false);
                if (!anim.GetBool("isRunning")) anim.SetBool("isRunning", true);
            }
            else
            {
                if (anim.GetBool("isRunning")) anim.SetBool("isRunning", false);
                if (anim.GetBool("isWalking")) anim.SetBool("isWalking", false);
            }
        }
    }

    public float Speed() => speed;
}
