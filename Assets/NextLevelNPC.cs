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
    private LevelConditions levelConditions;

    void Start()
    {
        levelConditions = GetComponent<LevelConditions>();
    }
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
        levelConditions.timeOfDay = LevelConditions.Time.dusk;

    }
    public void Day()
    {
        levelConditions.timeOfDay = LevelConditions.Time.day;

    }
    public void Night()
    {
        levelConditions.timeOfDay = LevelConditions.Time.night;
    }
    public void Docile()
    {
        levelConditions.zombieAggression = LevelConditions.Difficulty.docile;

    }
    public void Agitated()
    {
        levelConditions.zombieAggression = LevelConditions.Difficulty.agitated;

    }
    public void Crazed()
    {
        levelConditions.zombieAggression = LevelConditions.Difficulty.crazed;

    }

    public void Go()
    {
        //SceneManager.SetActiveScene("INSERT SCENE HERE");
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
        if (other.tag == "Player")
        {
            triggering = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            triggering = false;
        }
    }
}
