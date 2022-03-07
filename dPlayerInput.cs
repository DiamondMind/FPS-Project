using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DiamondMind.Prototypes.FPSPlayer 
{
    public class dPlayerInput : MonoBehaviour
    {
        public float mouseInputX;
        public float mouseInputY;
        public float horizontalInput;
        public float verticalInput;
        public bool jumpImput;
        public bool crouchInput;
        public bool sprintInput;

        // Update is called once per frame
        public void Update()
        {
            // Gather mouse imput and store in a float variable
            mouseInputX = Input.GetAxis("Mouse X");
            mouseInputY = Input.GetAxis("Mouse Y");

            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            jumpImput = Input.GetButtonDown("Jump");
            crouchInput = Input.GetButton("Crouch");
            sprintInput = Input.GetButtonDown("Sprint");
            /*if(crouchInput)
             {
                 Debug.Log("Input");
             }*/
        }
    }

}
