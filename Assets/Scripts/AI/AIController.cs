using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 previousPosition; // used to find speed

    Animator anim;
    NavMeshAgent agent;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        previousPosition = transform.position;
    }

    public bool IsDead() => anim.GetBool("dead");

    public void BodyHit()
    {
        StartCoroutine(React());
    }

    IEnumerator React()
    {
        anim.SetTrigger("hit"); // triggers the hit animation
        agent.isStopped = true; // prevents the nav agent from continuing to move forward
        yield return new WaitForSeconds(1f);
        agent.isStopped = false; // allows the nav agent to move again
        yield break;
    }

    public void Headshot()
    {
        anim.SetTrigger("headshot"); // triggers headshot death animation
        agent.isStopped = true; // indefinitely pauses nav agent
        anim.SetBool("dead", true); // declares the entity as dead
    }

    void Update()
    {
        // calculating the speed to use for the animator's blend tree
        speed = Vector3.Distance(previousPosition, transform.position) / Time.deltaTime;
        anim.SetFloat("speed", speed);
        previousPosition = transform.position;
    }
}
