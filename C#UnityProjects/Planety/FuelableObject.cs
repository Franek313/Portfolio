using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelableObject : InteractableObject
{
    public int fuelQuantity = GameManager.maxFuel;
    public Transform fuelBar;
    public Transform fuelBarExternal;

    public virtual void LoadFuel(int quantity)
    {
        if(fuelQuantity < GameManager.maxFuel)
        {
            Transform fuelBox = player.GetComponent<PlayerHolder>().heldItem;
            if (fuelBox.GetComponent<Box>().type == OreType.fuel)
            {
                fuelQuantity += quantity;

                if(fuelQuantity > GameManager.maxFuel)
                    fuelQuantity = GameManager.maxFuel;

                Destroy(fuelBox.gameObject);
            }
        }
    }

    public override void Interact()
    {
        base.Interact();
        if(fuelQuantity > 0)
        {
            fuelBar.localScale = new Vector3((float)fuelQuantity / GameManager.maxFuel, 1, 1);
            fuelBarExternal.localScale = new Vector3((float)fuelQuantity / GameManager.maxFuel, 1, 1);
        }   
    }

    public virtual void UseFuel(int quantity)
    {
        fuelQuantity -= quantity;
    }
}
