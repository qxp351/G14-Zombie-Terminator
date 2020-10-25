using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public enum BodyPart { head, body }
    [SerializeField] BodyPart bodyPart = BodyPart.body;

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.name);
        if (!GetComponentInParent<AIController>().IsDead())
        {
            if (bodyPart == BodyPart.body)
            {
                GetComponentInParent<AIController>().BodyHit();
            }
            else if (bodyPart == BodyPart.head)
            {
                GetComponentInParent<AIController>().Headshot();
            }
        }
    }
}
