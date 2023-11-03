using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed;
    private Vector2 velocity;

    private new Transform transform;
    private Animator animator;

    void Start()
    {
        transform = GetComponent<Transform>(); 
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        animator.SetFloat("Face_x", velocity.x);
        animator.SetFloat("Face_y", velocity.y);
        animator.SetFloat("Speed", speed);
    }
}
