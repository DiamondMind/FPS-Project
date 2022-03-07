using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DiamondMind.Prototypes.FPSPlayer
{
    public class dMouseLook : MonoBehaviour
    {
        public dPlayerInput playerInput;
        public Transform player;
        public float mouseSensitivity = 100f;
        public float maxYRotation = 90f;
        float xRotation = 0f;
        public bool lockCursor = true;

        // Start is called before the first frame update
        void Start()
        {
             //lock the cursor
            if(lockCursor == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            
        }

        // Update is called once per frame
        void Update()
        {
            // Gather mouse imput and store in a float variable
            float mouseX = playerInput.mouseInputX * mouseSensitivity * Time.deltaTime;
            float mouseY = playerInput.mouseInputY * mouseSensitivity * Time.deltaTime;
           
            // Rotate player along the x axis
            player.transform.Rotate(Vector3.up * mouseX);

            xRotation -= mouseY;    // decrease x rotation by mouseY every frame
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);    // this is done instead of transform.rotate so we can clamp the rotation
            xRotation = Mathf.Clamp(xRotation, -maxYRotation, maxYRotation);    // clamp xRotation to -90, 90

        }
    }
}
