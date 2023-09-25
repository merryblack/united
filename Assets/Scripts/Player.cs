using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private float hAxis;
    private float vAxis;
    private float jumpPower;
    private bool isJumpBtnDown;
    private bool isJumping = false;
    private bool isWalking = false;

    private Vector3 moveVec;
    private Animator anim;
    private Rigidbody rigid;

    private float scoreNow = 0;
    private float scoreAll;

    public AudioClip foodGetSound;
    private AudioSource audioDefaultWalkSound;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        audioDefaultWalkSound = GetComponent<AudioSource>();
    }

    void Start()
    {
        speed = 7;
        jumpPower = 4;
    }

    void Update()
    {
        HandleWalk();
        HandleJump();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isJumping = false;
            anim.SetBool("isJumping", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            audioDefaultWalkSound.PlayOneShot(foodGetSound);
            scoreNow += 1;
            other.gameObject.SetActive(false);
        }
    }

    private void HandleWalk()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * Time.deltaTime;
        transform.LookAt(transform.position + moveVec);
        
        anim.SetBool("isWalking", moveVec != Vector3.zero);

        if (moveVec != Vector3.zero && !isWalking)
        {
            isWalking = true;
            anim.SetBool("isWalking", true);
            audioDefaultWalkSound.Play();
        }
        else if (moveVec == Vector3.zero && isWalking)
        {
            isWalking = false;
            anim.SetBool("isWalking", false);
            audioDefaultWalkSound.Stop();
        }
    }

    private void HandleJump()
    {
        isJumpBtnDown = Input.GetButtonDown("Jump");

        if (isJumpBtnDown && !isJumping)
        {
            isJumping = true;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            anim.SetBool("isJumping", true);
        }
    }
}
