using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingManager : MonoBehaviour
{

    private void OnEnable()
    {
        PlayerInput.TALK += PlayerInput_TALK;
    }

    private void OnDisable()
    {
        PlayerInput.TALK -= PlayerInput_TALK;
    }

    private void PlayerInput_TALK()
    {
        try
        {
            ReticleManager.Object.GetComponent<ITalkable>().SpeakTo();
        }
        catch
        {
            Debug.LogWarning("There is no one to talk to.");
        }
    }
}
