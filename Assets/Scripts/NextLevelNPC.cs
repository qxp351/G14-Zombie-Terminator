using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.WSA.Input;

public class NextLevelNPC : MonoBehaviour
{
    private bool triggering;
    public GameObject npcText;
    public GameObject newLevelText;
    private bool interacting = false;

    private void Update()
    {
        if (triggering)
        {
            npcText.SetActive(true);
        }
        else
        {
            npcText.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.F) && triggering)
        {
            PlayerInput.current.TogglePlayerControl(true);
            interacting = true;
            newLevelText.SetActive(true);
        }
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Cancel();
        //}
    }
    public void Dusk()
    {
        try
        {
            LevelConditions.current.timeOfDay = LevelConditions.Time.dusk;
        }
        catch
        {
            Debug.LogWarning("GameData does not exist. Time of day will remain its default value.");
        }
    }
    public void Day()
    {
        try
        {
            LevelConditions.current.timeOfDay = LevelConditions.Time.day;
        }
        catch
        {
            Debug.LogWarning("GameData does not exist. Time of day will remain its default value.");
        }
    }
    public void Night()
    {
        try
        {
            LevelConditions.current.timeOfDay = LevelConditions.Time.night;
        }
        catch
        {
            Debug.LogWarning("GameData does not exist. Time of day will remain its default value.");
        }
    }
    //public void Docile()
    //{
    //    LevelConditions.current.zombieAggression = LevelConditions.Difficulty.docile;

    //}
    //public void Agitated()
    //{
    //    LevelConditions.current.zombieAggression = LevelConditions.Difficulty.agitated;

    //}
    //public void Crazed()
    //{
    //    LevelConditions.current.zombieAggression = LevelConditions.Difficulty.crazed;

    //}

    public void Go()
    {
        var buildIndex = SceneIndices.DayScene;

        try
        {
            if (LevelConditions.current.timeOfDay == LevelConditions.Time.dusk)
            {
                buildIndex = SceneIndices.DuskScene;
            }
            else if (LevelConditions.current.timeOfDay == LevelConditions.Time.night)
            {
                buildIndex = SceneIndices.NightScene;
            }
        }
        catch
        {
            Debug.LogWarning("GameData does not exist. Sending player to DayScene.");
        }
        SceneManager.LoadScene(buildIndex);
    }
    public void Cancel()
    {
        PlayerInput.current.TogglePlayerControl(false);
        newLevelText.SetActive(false);
        triggering = false;
        interacting = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggering = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggering = false;
        }
    }
}
