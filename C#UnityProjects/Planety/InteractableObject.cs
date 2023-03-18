using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class InteractableObject : MonoBehaviour
{
    public Transform player; //stores player Transform (depends on if player is in range of b/m/t)
    public GameObject panel; //UI interface of building/machine/tool
    public Transform planet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.virtualCamera.Follow = player;
            panel.SetActive(false);
            player = null;
        }
    }

    public virtual void Interact()
    {
        if (player)
        {
            if (Input.GetKeyDown(GameManager.switchRight))
            {
                if (panel)
                {
                    if (panel.activeSelf)
                    {
                        panel.SetActive(false);
                    }
                    else
                    {
                        panel.SetActive(true);
                    }
                }
            }
        }
    }
    
    public void SetPlanet()
    {
        planet = GameManager.player.GetComponent<Attractable>().currentAttractor.transform;
    }
}
