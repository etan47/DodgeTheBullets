using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public PlayerHealth playerHealth;
    public Animator animator;
    public float runSpeed = 40f;
    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;
    private float minY = -8f;
    private Vector2 respawnPos = new Vector2(-2.81f, -3.16f);

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerHealth.getDeathStatus()){
            if (transform.position.y < minY){
                transform.position = respawnPos;
                playerHealth.takeDamage();
            }
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }
            else if (Input.GetButtonUp("Jump"))
            {
                jump = false;
            }

            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }
        }
        else {
            horizontalMove = 0f;
            jump = false;
            crouch = false;
            animator.SetBool("IsDead", true);
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsCrouching", false);
        }
    }

    public void OnLand() {
        jump = false;
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouch(bool crouchStatus){
        animator.SetBool("IsCrouching", crouchStatus);
    }
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
    }
}
