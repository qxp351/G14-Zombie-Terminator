using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    // enum describing which bodypart was hit by player
    public enum BodyPart { head, body }
    [SerializeField] BodyPart bodyPart = BodyPart.body;

    private void OnParticleCollision(GameObject other)
    {
        // if the zombie isn't dead, determine which bodypart was hit
        // and run the appropriate code
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
