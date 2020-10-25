using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] float speed;
    Animator anim;
    NavMeshAgent agent;
    Vector3 previousPosition;

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
        anim.SetTrigger("hit");
        agent.isStopped = true;
        yield return new WaitForSeconds(1f);
        agent.isStopped = false;
        yield break;
    }

    public void Headshot()
    {
        anim.SetTrigger("headshot");
        agent.isStopped = true;
        anim.SetBool("dead", true);
    }

    void Update()
    {
        speed = Vector3.Distance(previousPosition, transform.position) / Time.deltaTime;
        anim.SetFloat("speed", speed);
        previousPosition = transform.position;
    }
}
