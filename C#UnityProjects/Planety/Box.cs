using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public OreType type;
    public int quantity = GameManager.boxQuantity;
    public SpriteRenderer boxMaterial;

    private void Start()
    {
        switch (type)
        {
            case OreType.fuel:
                boxMaterial.color = GameManager.fuelColor;
                break;

            case OreType.metal:
                boxMaterial.color = GameManager.metalBoxColor;
                break;

            case OreType.water:
                boxMaterial.color = GameManager.waterColor;
                break;
        }
    }
}
