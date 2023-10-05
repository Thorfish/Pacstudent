using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public List<Transform> stops;
    [SerializeField]
    public float duration;

    private float speed;
    private Vector2 velocity;

    private int target = 0;
    private new Transform transform;
    private Animator animator;

    void Start()
    {
        transform = GetComponent<Transform>(); 
        animator = GetComponent<Animator>();
        float distanceSum = 0;
        for (int i = 1; i < stops.Count; i++)
        {
            distanceSum += Vector2.Distance(stops[i].position, stops[i - 1].position);
        }
        speed = distanceSum / duration;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = (stops[target].position - transform.position).normalized * speed;
        if (Vector2.Distance(transform.position, stops[target].position) > speed)
        {
            transform.position = new Vector2(transform.position.x + velocity.x, transform.position.y + velocity.y);
        }
        else
        {
            transform.position = stops[target].position;
            if (target == stops.Count - 1) { target = 0; }
            else { target++; }
        }

        animator.SetFloat("Face_x", velocity.x);
        animator.SetFloat("Face_y", velocity.y);
        animator.SetFloat("Speed", speed);
    }
}
