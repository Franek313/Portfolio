using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreSpawner : MonoBehaviour
{
    public GameObject orePrefab;
    public Planet planet;

    private void Start()
    {
        Transform planetTransform = transform.parent;
        int random = Random.Range(GameManager.minOreCount, GameManager.maxOreCount + 1);

        for (int i = 0; i < random; i++)
        {
            List<Transform> slots = new List<Transform>();
            foreach(Transform t in transform)
            {
                if (t.childCount == 0)
                {
                    slots.Add(t);
                }
            }
            
            GameObject newOre = Instantiate(orePrefab, slots[Random.Range(0,slots.Count)]);
            planet.ores.Add(newOre.transform);
            newOre.transform.up = -planetTransform.position + newOre.transform.position;
            int size = Random.Range(GameManager.oreMinSize, GameManager.oreMaxSize + 1);
            newOre.transform.localScale = new Vector3((float)size/10, (float)size /10, (float)size /10);
            newOre.GetComponent<Ore>().size = size;
        }

    }
}
