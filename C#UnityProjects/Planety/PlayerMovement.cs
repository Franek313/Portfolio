using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    Rigidbody2D rb;
    public Animator animator;
    public Transform planet;
    float speed = 2f; 
    public float walkSpeed = 2f;
    public float runSpeed = 3f;
    public float jumpHeight= 5f;
    public float jumpDelay = 0.05f;
    [SerializeField] bool IsGrounded = false, isMoving = false;
    public LayerMask groundLayer;
    Attractable attractable;
    public float checkRadius;
    public Transform groundChecker;

    [Header("Details")]
    public List<SpriteRenderer> bodyParts = new List<SpriteRenderer>();
    public Color playerColor;

    void Start()
    {
        ChangeColor(playerColor);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attractable = GetComponent<Attractable>();
    }

    void Update()
    {
        if(attractable.currentAttractor)
        planet = attractable.currentAttractor.transform;

        if(attractable.currentAttractor)
        {
            IsGrounded = Physics2D.OverlapCircle(groundChecker.position, checkRadius, groundLayer);
            
            if(Input.GetKey(GameManager.run))
            {
                speed = runSpeed;
                animator.speed = 1.25f;
            }
            else 
            {
                speed = walkSpeed;
                animator.speed = 1f;
            }

            if(Input.GetKey(GameManager.moveRight))
            {
                transform.localScale = new Vector3(1,1,1);
                transform.Translate(Vector2.right * (Time.deltaTime * speed), Space.Self);
                animator.SetBool("Run", true);
            }
            else if(Input.GetKey(GameManager.moveLeft))
            {
                transform.Translate(Vector2.left * (Time.deltaTime * speed), Space.Self);
                animator.SetBool("Run", true);
                transform.localScale = new Vector3(-1,1,1);
            }
            else
            {
                transform.Translate(Vector2.zero, Space.Self);
                animator.SetBool("Run", false);
            }

            if(Input.GetKeyDown(GameManager.jump) && IsGrounded)
            {   
                rb.AddRelativeForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            }
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    public void ChangeColor(Color color)
    {
        foreach (SpriteRenderer sr in bodyParts)
        {
            sr.color = color;
        }
    }

    public void EnterVehicle(Transform seat)
    {
        foreach (SpriteRenderer sr in bodyParts)
        {
            transform.SetParent(seat, false);
            GetComponent<Collider2D>().isTrigger = true;
            transform.localPosition = Vector3.zero;
            sr.sortingLayerName = "Rockets";
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;

        }
    }
    public void ExitVehicle()
    {
        foreach (SpriteRenderer sr in bodyParts)
        {
            transform.SetParent(null);
            GetComponent<Collider2D>().isTrigger = false;
            transform.localScale = Vector3.one;
            sr.sortingLayerName = "MovableObjects";
            rb.isKinematic = false;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundChecker.position, checkRadius);
    }

}
