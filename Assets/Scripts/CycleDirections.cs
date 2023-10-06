using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleDirections : MonoBehaviour
{
    private Animator animator;
    private float timer = 0f;
    private int dir = 0;
    private int x = 0;
    private int y = -1;
    private float loopTime = 12f;

    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > loopTime)
        {
            dir = dir == 3 ? 0 : (dir + 1);

            switch (dir)
            {
                case 0:
                    x = 0;
                    y = -1;
                    break;
                case 1:
                    x = -1;
                    y = 0;
                    break;
                case 2:
                    x = 0;
                    y = 1;
                    break;
                case 3:
                    x = 1;
                    y = 0;
                    break;
                    
            }
            animator.SetFloat("Face_x", x);
            animator.SetFloat("Face_y", y);
            timer -= loopTime;
        }
    }
}
