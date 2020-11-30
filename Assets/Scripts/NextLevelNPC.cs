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
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            interacting = true;
            newLevelText.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cancel();
        }
    }
    public void Dusk()
    {
        LevelConditions.current.timeOfDay = LevelConditions.Time.dusk;

    }
    public void Day()
    {
        LevelConditions.current.timeOfDay = LevelConditions.Time.day;

    }
    public void Night()
    {
        LevelConditions.current.timeOfDay = LevelConditions.Time.night;
    }
    public void Docile()
    {
        LevelConditions.current.zombieAggression = LevelConditions.Difficulty.docile;

    }
    public void Agitated()
    {
        LevelConditions.current.zombieAggression = LevelConditions.Difficulty.agitated;

    }
    public void Crazed()
    {
        LevelConditions.current.zombieAggression = LevelConditions.Difficulty.crazed;

    }

    public void Go()
    {
        var buildIndex = SceneIndices.DayScene;
        if (LevelConditions.current.timeOfDay == LevelConditions.Time.dusk)
        {
            buildIndex = SceneIndices.DuskScene;
        }
        else if (LevelConditions.current.timeOfDay == LevelConditions.Time.night)
        {
            buildIndex = SceneIndices.NightScene;
        }
        SceneManager.LoadScene(buildIndex);
    }
    public void Cancel()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        newLevelText.SetActive(false);
        triggering = false;
        interacting = false;
        Time.timeScale = 1f;
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
