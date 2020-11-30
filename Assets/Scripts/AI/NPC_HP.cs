using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_HP : MonoBehaviour
{
    public GameObject npcText;
    public GameObject tradeText;
    private GameObject triggeringNpc;
    private bool triggering, interacting;
    private SuppliesManager supplies;
    private PlayerStats player;

    private void Start()
    {
        supplies = GetComponent<SuppliesManager>();
        player = GetComponent<PlayerStats>();
    }
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
            supplies.UseFood();
            player.Heal(5);
        }
        else if (Input.GetKeyDown(KeyCode.N) && interacting)
        {
            interacting = false;
            tradeText.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            triggering = true;
            triggeringNpc = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            triggering = false;
            triggeringNpc = null;
            interacting = false;
        }
    }
}
