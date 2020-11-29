using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CurvedObjectBob
{
    public Vector3 BobRange = new Vector3(0.33f, 0.33f, 0.33f);

    [Header("Animation Curves")]
    public AnimationCurve HorizontalCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f),
                                                        new Keyframe(1f, 0f), new Keyframe(1.5f, -1f),
                                                        new Keyframe(2f, 0f)); // sin curve for head bob
    public AnimationCurve VerticalCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f),
                                                    new Keyframe(1f, 0f), new Keyframe(1.5f, -1f),
                                                    new Keyframe(2f, 0f)); // sin curve for head bob
    public AnimationCurve DistalCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f),
                                                    new Keyframe(1f, 0f), new Keyframe(1.5f, -1f),
                                                    new Keyframe(2f, 0f)); // sin curve for head bob

    [Space(20)]
    public Vector3 speed = Vector3.one;

    private Vector3 m_CyclePosition;
    private float m_BobBaseInterval;
    private Vector3 m_OriginalObjectPosition;
    private Vector3 m_Time;

    bool finishedCycle = false;


    public void Setup(Transform obj, float bobBaseInterval)
    {
        m_BobBaseInterval = bobBaseInterval;
        m_OriginalObjectPosition = obj.localPosition;

        // get the length of the curve in time
        m_Time.x = HorizontalCurve[HorizontalCurve.length - 1].time;
        m_Time.y = VerticalCurve[VerticalCurve.length - 1].time;
        m_Time.z = DistalCurve[DistalCurve.length - 1].time;
    }


    public Vector3 DoHeadBob(float speed)
    {
        float xPos = m_OriginalObjectPosition.x + (HorizontalCurve.Evaluate(m_CyclePosition.x) * BobRange.x);
        float yPos = m_OriginalObjectPosition.y + (VerticalCurve.Evaluate(m_CyclePosition.y) * BobRange.y);
        float zPos = m_OriginalObjectPosition.z + (DistalCurve.Evaluate(m_CyclePosition.z) * BobRange.z);

        m_CyclePosition.x += ((speed * Time.deltaTime) / m_BobBaseInterval) * this.speed.x;
        m_CyclePosition.y += ((speed * Time.deltaTime) / m_BobBaseInterval) * this.speed.y;
        m_CyclePosition.z += ((speed * Time.deltaTime) / m_BobBaseInterval) * this.speed.z;

        if (m_CyclePosition.x > m_Time.x)
        {
            m_CyclePosition.x -= m_Time.x;
            finishedCycle = true;
        }
        else finishedCycle = false;

        if (m_CyclePosition.y > m_Time.y)
        {
            m_CyclePosition.y -= m_Time.y;
            finishedCycle = true;
        }
        else finishedCycle = false;

        if (m_CyclePosition.z > m_Time.z)
        {
            m_CyclePosition.z -= m_Time.z;
            finishedCycle = true;
        }
        else finishedCycle = false;

        return new Vector3(xPos, yPos, zPos);
    }

    public void ResetBob()
    {
        m_CyclePosition = Vector3.zero;
    }

    public bool FinishedCycle() => finishedCycle;
}
