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
    private bool isJumping;

    private Vector3 moveVec;
    private Animator anim;
    private Rigidbody rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
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

    private void HandleWalk()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * Time.deltaTime;
        transform.LookAt(transform.position + moveVec);
        
        anim.SetBool("isWalking", moveVec != Vector3.zero);
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
