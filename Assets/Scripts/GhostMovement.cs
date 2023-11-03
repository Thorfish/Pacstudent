using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    private Animator animator;
    private int x = 0;
    private int y = -1;

    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Face_x", x);
        animator.SetFloat("Face_y", y);
    }
}
