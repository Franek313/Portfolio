using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Transform buildSign;
    public Vector2 offset;

    public void Build()
    {
        transform.parent = null;
        GetComponent<Collider2D>().enabled = true;
        if(buildSign)
        Destroy(buildSign.gameObject);
    }
}
