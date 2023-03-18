using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public Transform buildingHolder;
    public GameObject building;
    public GameObject buildingPanel;

    private void Update()
    {
        if (Input.GetKeyDown(GameManager.switchLeft))
        {
            if (buildingPanel.activeSelf)
            {
                buildingPanel.SetActive(false);
                GameManager.virtualCamera.Follow = transform;
                GameManager.virtualCamera.m_Lens.OrthographicSize = 2.5f;
            }
            else
            {
                buildingPanel.SetActive(true);
                GameManager.virtualCamera.Follow = GameManager.player.GetComponent<PlayerMovement>().planet;
                GameManager.virtualCamera.m_Lens.OrthographicSize = 5f;
            }
        }

        if (Input.GetKeyDown(GameManager.pickUp))
        {
            if(building)
            {
                building.GetComponent<Building>().Build();
                building = null;
                GameManager.virtualCamera.Follow = transform;
                GameManager.virtualCamera.m_Lens.OrthographicSize = 2.5f;
            }
            
        }
    }

    public void Spawn(GameObject prefab)
    {
        building  = Instantiate(prefab, buildingHolder);
        building.transform.localPosition = Vector2.zero + prefab.GetComponent<Building>().offset;
        building.transform.localRotation = Quaternion.identity;
        buildingPanel.SetActive(false);
    }
}
