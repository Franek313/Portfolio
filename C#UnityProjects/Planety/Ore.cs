using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OreType
{
    fuel, metal, water
}

public class Ore : MonoBehaviour
{
    public OreType oreType;
    public int size, scaleSize;
    public Transform fluid;
    public Transform signalEnd;
    public SpriteRenderer oreMaterial;
    public GameObject pipeEnd;

    void Start()
    {
        int random = Random.Range(0, 3);
        switch(random)
        {
            case 0: oreType = OreType.fuel;
                    oreMaterial.color = GameManager.fuelColor;
            break;

            case 1: oreType = OreType.metal;
                    oreMaterial.color = GameManager.metalColor;
            break;

            case 2: oreType = OreType.water;
                    oreMaterial.color = GameManager.waterColor;
            break;
        }

        scaleSize = size;
    }

    public void ShowPipe()
    {
        pipeEnd.SetActive(true);
    }
    public void HidePipe()
    {
        pipeEnd.SetActive(false);
    }
    public void SubstractFromOre(int quantity)
    {
        size += quantity; //increase because LoadFuel is using -1 parameter
        fluid.localScale -= new Vector3(0, (float)1/scaleSize, 0);
    }

}
