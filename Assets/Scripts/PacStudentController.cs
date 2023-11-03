using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PacStudentController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Tilemap walls;
    [SerializeField]
    private AudioClip walkSfx;
    [SerializeField]
    private AudioClip bumpSfx;
    [SerializeField]
    private ParticleSystem particleSystem;

    private Vector3Int gridPosition;
    private Vector3 velocity, target;
    private KeyCode lastInput, currentInput;
    private Vector3Int lastInputDir, currentInputDir;

    private bool isMoving = false, canBump = true;

    private new Transform transform;
    private Animator animator;
    private AudioSource audioPlayer;

    void Start()
    {
        transform = GetComponent<Transform>(); 
        animator = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
        velocity = Vector3Int.zero;
        target = transform.position;
    }

    void FixedUpdate()
    {
        //Check for Input
        if (Input.GetKey(KeyCode.W))
        {
            lastInput = KeyCode.W;
            lastInputDir = new Vector3Int(0, 1, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            lastInput = KeyCode.S;
            lastInputDir = new Vector3Int(0, -1, 0);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            lastInput = KeyCode.A;
            lastInputDir = new Vector3Int(-1, 0, 0);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            lastInput = KeyCode.D;
            lastInputDir = new Vector3Int(1, 0, 0);
        }

        //Move PacStudent
        if (Vector2.Distance(transform.position, target) > speed)
        {
            //Lerping
            transform.position = new Vector3(transform.position.x + velocity.x, transform.position.y + velocity.y, transform.position.z);
            isMoving = true;
            canBump = true;
        }
        else
        {
            transform.position = target;
            gridPosition = walls.layoutGrid.WorldToCell(transform.position);
            if (!walls.HasTile(gridPosition + lastInputDir))
            {
                currentInput = lastInput;
                currentInputDir = lastInputDir;
                particleSystem.transform.rotation = Quaternion.Euler(90*lastInputDir.y, -90*lastInputDir.x, 0);
                target += lastInputDir;
                velocity = new Vector3(lastInputDir.x * speed, lastInputDir.y * speed, 0);
            }
            else if (!walls.HasTile(gridPosition + currentInputDir))
            {
                target += currentInputDir;
                velocity = new Vector3(currentInputDir.x * speed, currentInputDir.y * speed, 0);
            }
            else
            {
                isMoving = false;
            }
        }

        if (isMoving)
        {
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
            }

            if (!audioPlayer.isPlaying)
            {
                audioPlayer.clip = walkSfx;
                audioPlayer.Play();
                audioPlayer.loop = true;
            }
        }
        else
        {
            if (particleSystem.isPlaying)
            {
                particleSystem.Stop();
            }

            if (canBump)
            {
                canBump = false;
                audioPlayer.clip = bumpSfx;
                audioPlayer.loop = false;
                audioPlayer.Play();
            }
        }


        //Set Animation Variables
        animator.SetFloat("Face_x", velocity.x);
        animator.SetFloat("Face_y", velocity.y);
        animator.SetBool("IsMoving", isMoving);
    }
}
