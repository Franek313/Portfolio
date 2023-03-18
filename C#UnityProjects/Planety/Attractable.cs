using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractable : MonoBehaviour
{
    [SerializeField] private bool rotateToCenter = true;
    public Attractor currentAttractor;

    Transform m_transform;
    Collider2D m_collider;
    Rigidbody2D m_rigidbody;

    void Awake()
    {
        m_transform = GetComponent<Transform>();
        m_collider = GetComponent<Collider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(currentAttractor != null)
        {
            if(!currentAttractor.attractedObjects.Contains(m_collider)) currentAttractor = null;
            if(rotateToCenter) RotateToCenter();
        }
    }

    Vector2 toDisplay = new Vector2();
    public void Attract(Attractor artificialGravity)
    {
        Vector2 attractionDir = (Vector2)artificialGravity.planetTransform.position - m_rigidbody.position;
        m_rigidbody.AddForce(attractionDir.normalized * -artificialGravity.gravity * 100 * Time.fixedDeltaTime);
        toDisplay = attractionDir.normalized * -artificialGravity.gravity * 100 * Time.fixedDeltaTime;
        if(currentAttractor == null)
        {
            currentAttractor = artificialGravity;
        }
    }
    
    void RotateToCenter()
    {
        Vector2 distanceVector = (Vector2)currentAttractor.planetTransform.position - (Vector2)m_transform.position;
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        m_transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

}
