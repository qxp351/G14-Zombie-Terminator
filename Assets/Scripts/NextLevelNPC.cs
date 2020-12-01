using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevelNPC : MonoBehaviour, ITalkable
{
    //private bool triggering;
    public Text npcText;
    public GameObject newLevelText;
    public GameObject mustRestText;
    public GameObject lastShotText;
    //private bool interacting = false;

    //private void Update()
    //{
    //    if (triggering)
    //    {
    //        npcText.SetActive(true);
    //    }
    //    else
    //    {
    //        npcText.SetActive(false);
    //    }
    //    if(Input.GetKeyDown(KeyCode.F) && triggering)
    //    {
    //        PlayerInput.current.TogglePlayerControl(true);
    //        interacting = true;
    //        newLevelText.SetActive(true);
    //    }
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        Cancel();
    //    }
    //}

    LevelConditions.Difficulty m_newDayDifficulty;
    bool m_canLeave = false;

    private void Start()
    {
        m_newDayDifficulty = (LevelConditions.Difficulty)Random.Range(0, 3);

        switch (m_newDayDifficulty)
        {
            case LevelConditions.Difficulty.docile: npcText.text = $"Zombie Activity: Docile"; break;
            case LevelConditions.Difficulty.agitated: npcText.text = $"Zombie Activity: Agitated"; break;
            case LevelConditions.Difficulty.crazed: npcText.text = $"Zombie Activity: Crazed"; break;

        }

        m_canLeave = false;
    }

    private void OnEnable()
    {
        NPC_HP.REST += NPC_HP_REST;
    }

    private void OnDisable()
    {
        NPC_HP.REST -= NPC_HP_REST;
    }

    private void NPC_HP_REST()
    {
        m_newDayDifficulty = (LevelConditions.Difficulty)Random.Range(0, 3);

        switch (m_newDayDifficulty)
        {
            case LevelConditions.Difficulty.docile: npcText.text = $"Zombie Activity: Docile"; break;
            case LevelConditions.Difficulty.agitated: npcText.text = $"Zombie Activity: Agitated"; break;
            case LevelConditions.Difficulty.crazed: npcText.text = $"Zombie Activity: Crazed"; break;

        }
        m_canLeave = true;
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
        PlayerInput.current.ToggleInventory(true);
        newLevelText.SetActive(false);
        mustRestText.SetActive(false);
        lastShotText.SetActive(false);
        //triggering = false;
        //interacting = false;
    }
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        triggering = true;
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        triggering = false;
    //    }
    //}

    public void SpeakTo()
    {
        PlayerInput.current.TogglePlayerControl(true);
        PlayerInput.current.ToggleInventory(false);
        //interacting = true;
        int food = int.MaxValue;
        try
        {
            food = SuppliesManager.current.Food();
        }
        catch
        {
            Debug.LogWarning("GameData does not exist.");
        }

        if (m_canLeave)
        {
            if (food == 0) lastShotText.SetActive(true);
            else newLevelText.SetActive(true);
        }
        else mustRestText.SetActive(true);
    }

    public void LastShotGo() => SceneManager.LoadScene(SceneIndices.NightScene);
}
