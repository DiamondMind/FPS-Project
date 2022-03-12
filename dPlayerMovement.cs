using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DiamondMind.Prototypes.FPSPlayer
{
    public class dPlayerMovement : MonoBehaviour
    {   [Header("Movement")]
        public float playerHeight = 2f;
        public bool isWalking;
        public float walkSpeed = 5f;
        public float crouchSpeed = 2f;
        public float sprintSpeed = 10f;
        public float jumpHeight = 3f;
        public bool isCrouching = false;
        public float crouchHeight = 1f; 
        [Range(0f, 3f)]public float crouchTimer = 2f;
        [Range(0f, 5f)]public float jumpTimer = 3f;
        [Header("Gravity")]
        public bool isGrounded;
        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundLayer;
        public float gravity = -9.81f;

        float currentSpeed;
        Vector3 velocity;
        float jumpDelay;
        float crouchDelay;
        dPlayerInput playerInput;
        CharacterController controller;
        
        private void Start()
        {
            playerInput = GetComponent<dPlayerInput>();
            controller = GetComponent<CharacterController>();
            // set height
            controller.height = playerHeight;
        }
        // Update is called once per frame
        void Update()
        {
            CheckGrounded();
            Gravity();
            Movement();
            Jump();
            Crouch();
        }
 
        void Movement()
        {
            // gather keyboard/joystick input and store in float variables
            float x = playerInput.horizontalInput;
            float z = playerInput.verticalInput;
            // check isWalking bool
            if (x != 0 || z != 0)
            {
                isWalking = true;
            }
            else
            {
                isWalking = false;
            }
            // movement
            Vector3 move = transform.right * x + transform.forward * z; // get the local direction of the player and store in a vector3 variable
            if (isWalking && !isCrouching && !playerInput.sprintInput)  // walk
            {
                currentSpeed = walkSpeed;
                controller.Move(move * currentSpeed * Time.deltaTime);
            }
            else if (playerInput.sprintInput && isWalking)    // sprint
            {
                currentSpeed = sprintSpeed;
                controller.Move(move * currentSpeed * Time.deltaTime);
                if(isCrouching) // stand up while sprinting
                {
                    controller.height = playerHeight;
                    isCrouching = false;
                }
                
            } 
            if (isWalking && isCrouching && !playerInput.sprintInput)    // crouchwalk
            {
                currentSpeed = crouchSpeed;
                controller.Move(move * currentSpeed * Time.deltaTime);
            }
    
        }
        void Jump()
        {
            // jump 
            if (playerInput.jumpImput && isGrounded && Time.time > jumpDelay)
            {
                if (isCrouching)
                {
                    controller.height = playerHeight;
                    //transform.position = Mathf.Lerp(crouchHeight, playerHeight, crouchSmooth);
                    //transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                    isCrouching = false;
                }
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);    // based on formula
                jumpDelay = Time.time + jumpTimer;  // prevent jump until delay is completed
            }
        }
        void Crouch()
        {
            // crouch
            if (!isCrouching && playerInput.crouchInput && isGrounded && Time.time > crouchDelay)
            {
                controller.height = crouchHeight;   // set characterController height to crouch height
                //transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                isCrouching = true;
                crouchDelay = Time.time + crouchTimer;  // prevent crouch until delay is completed 
            }
            else if (isCrouching && playerInput.crouchInput && isGrounded && Time.time > crouchDelay)
            {
                controller.height = playerHeight;
                //transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
                isCrouching = false;
                crouchDelay = Time.time + crouchTimer;
            }
        }

        void Gravity()
        {
            // gravity
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
        void CheckGrounded()
        {
            // set isGrounded bool
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
            // reset velocity at the right conditions
            if (isGrounded && velocity.y < 0f)
            {
                velocity.y = -2f;
            }
        }
    }
}
