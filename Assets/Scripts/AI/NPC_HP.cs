using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_HP : MonoBehaviour, ITalkable
{
    public static event System.Action REST;

    public GameObject optionCanvas;
    public Text npcText;
    //public GameObject npcText;
    //public GameObject tradeText;
    //private GameObject triggeringNpc;
    //private bool triggering, interacting;

    //void Update()
    //{
    //    if(triggering)
    //    {
    //        npcText.SetActive(true);
    //    }
    //    else
    //    {
    //        npcText.SetActive(false);
    //    }
    //    if (Input.GetKeyDown(KeyCode.F) && triggering)
    //    {
    //        triggering = false;
    //        tradeText.SetActive(true);
    //        interacting = true;
    //    }
    //    if (Input.GetKeyDown(KeyCode.Y) && interacting)
    //    {

    //    }
    //    else if (Input.GetKeyDown(KeyCode.N) && interacting)
    //    {
    //        interacting = false;
    //        tradeText.SetActive(false);
    //    }
    //}
    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        triggering = true;
    //        triggeringNpc = other.gameObject;
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        triggering = false;
    //        triggeringNpc = null;
    //        interacting = false;
    //    }
    //}

    public void Rest()
    {
        // initiate resting sequence
        try
        {
            SuppliesManager.current.UseFood();
            REST?.Invoke();
            Cancel();
        }
        catch
        {
            Debug.Log("GameData object does not exist. Halting rest cutscene.");
        }
    }

    public void Cancel()
    {
        PlayerInput.current.TogglePlayerControl(false);
        optionCanvas.SetActive(false);
    }

    public void SpeakTo()
    {
        try
        {
            if (SuppliesManager.current.Food() == 0)
            {
                npcText.text = "Would you like to rest? We will not survive the night.";
            }
            else npcText.text = "Rest for the day and feed the camp?";
        }
        catch
        {
            if (npcText) npcText.text = "Rest for the day and feed the camp?";
            Debug.LogWarning("GameData does not exist or the Text object does not exist.");
        }
        PlayerInput.current.TogglePlayerControl(true);
        optionCanvas.SetActive(true);
    }
}
