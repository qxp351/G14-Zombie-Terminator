using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_HP : MonoBehaviour
{
    public GameObject npcText;
    public GameObject tradeText;
    private GameObject triggeringNpc;
    private bool triggering, interacting;

    void Update()
    {
        if(triggering)
        {
            npcText.SetActive(true);
        }
        else
        {
            npcText.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.F) && triggering)
        {
            triggering = false;
            tradeText.SetActive(true);
            interacting = true;
        }
        if (Input.GetKeyDown(KeyCode.Y) && interacting)
        {
            // initiate resting sequence
            try
            {
                SuppliesManager.current.UseAmmo();
            }
            catch
            {
                Debug.Log("GameData object does not exist. Halting rest cutscene.");
            }
        }
        else if (Input.GetKeyDown(KeyCode.N) && interacting)
        {
            interacting = false;
            tradeText.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggering = true;
            triggeringNpc = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggering = false;
            triggeringNpc = null;
            interacting = false;
        }
    }
}
