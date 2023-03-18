using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public LayerMask attractionLayer;
    public float gravity = -10;
    [SerializeField] private float effectionRadius = 10;
    public List<Collider2D> attractedObjects = new List<Collider2D>();
    [HideInInspector] public Transform planetTransform;

    void Awake()
    {
        planetTransform = GetComponent<Transform>();
    }

    void Update()
    {
        SetAttractedObjects();
    }

    void FixedUpdate()
    {
        AttractObjects();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, effectionRadius);
    }

    void SetAttractedObjects()
    {
        attractedObjects = Physics2D.OverlapCircleAll(planetTransform.position, effectionRadius, attractionLayer).ToList();
    }

    void AttractObjects()
    {
        for(int i = 0; i < attractedObjects.Count; i++)
        {
            attractedObjects[i].GetComponent<Attractable>().Attract(this);
        }
    }
}
