using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHolder : MonoBehaviour
{
    public Transform heldItem = null, currentHoldable = null, currentFullable = null;
    public Transform holder = null;
    public float detectionRadius = 1f;
    public LayerMask layerMask;
    
    void Update()
    {
        if(Input.GetKeyDown(GameManager.useItem))
        {
            if(currentFullable)
            {
                if(heldItem && heldItem.GetComponent<Box>())
                {
                    if(heldItem.GetComponent<Box>().type == OreType.fuel)
                    {
                        int boxQuantity = heldItem.GetComponent<Box>().quantity;
                        int fullableQuantity = currentFullable.GetComponent<Mine>().fuelQuantity;

                        if(fullableQuantity + boxQuantity <= 10)
                        {
                            if(currentFullable.GetComponent<Mine>().fuelQuantity == 0)
                            {
                                StartCoroutine(currentFullable.GetComponent<Mine>().SubstractFuel());
                            }
                            
                            currentFullable.GetComponent<Mine>().LoadFuel(boxQuantity);
                            Destroy(heldItem.gameObject);
                            GetComponent<PlayerMovement>().animator.SetBool("Hold", false);
                        }
                    }
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(holder.childCount != 0)
            {
                Transform childToRelease = holder.GetChild(0);
                childToRelease.SetParent(null);
                heldItem = null;
                childToRelease.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 3;
                childToRelease.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 4;
                Rigidbody2D childRb2D = childToRelease.GetComponent<Rigidbody2D>();
                childRb2D.isKinematic = false;
                childRb2D.constraints = RigidbodyConstraints2D.None;
                childToRelease.GetComponent<Collider2D>().enabled = true;
                GetComponent<PlayerMovement>().animator.SetBool("Hold", false);
            }
            
            if(currentHoldable)
            {
                currentHoldable.SetParent(holder);
                heldItem = currentHoldable;
                Rigidbody2D boxRb2D = currentHoldable.GetComponent<Rigidbody2D>();
                boxRb2D.isKinematic = true;
                boxRb2D.constraints = RigidbodyConstraints2D.FreezeAll;
                currentHoldable.GetComponent<Collider2D>().enabled = false;
                currentHoldable.localPosition = Vector3.zero;
                currentHoldable.localRotation = Quaternion.identity;
                currentHoldable.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 5;
                currentHoldable.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 6;
                GetComponent<PlayerMovement>().animator.SetBool("Hold", true);
            }
        }
    }

    void FixedUpdate()
    {
        currentHoldable = null;
        currentFullable = null; 
        Collider2D collider = Physics2D.OverlapCircle(holder.position, 
                              detectionRadius, layerMask);

        if(collider) 
        {    
            if(collider.tag == "Holdable")
            {
                currentHoldable = collider.transform;
            }
            else if(collider.tag == "Fullable")
            {
                currentFullable = collider.transform;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(holder.position, detectionRadius);
    }

}
