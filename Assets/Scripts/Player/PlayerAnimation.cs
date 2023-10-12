using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    public CharacterController2D controller;

    public void Animation()
    {
        animator = GetComponent<Animator>();

        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("Ismoving", true);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Ismoving", true);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            animator.SetBool("Ismoving", false);
        }


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("isCrouching", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("isCrouching", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isJumping", true);

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("isJumping", false);
        }
    }
    public void Update()
    {
        Animation();
    }
}
