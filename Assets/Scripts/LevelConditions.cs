using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConditions : MonoBehaviour
{
    public enum Time { dusk, day, night }
    public enum Difficulty { docile, agitated, crazed }

    public Time timeOfDay;
    public Difficulty zombieAggression;

    public static LevelConditions current;

    private void Awake()
    {
        current = this;
        DontDestroyOnLoad(this);
    }
}
