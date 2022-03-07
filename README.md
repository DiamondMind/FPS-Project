# FPS-Project
This is a simple first person controller in Unity with the following features;
* Walk
* Run
* Jump
* Crouch
* Gravity 
* Ground check 
* Mouse look
* Movement speeds

The code has been separated into three scripts. Each handles a specific attribute of the player;
* Mouse look- For the mouse movement with clamped vertical rotation to avoid flipping the camera past -90 and +90. The mouselook speed is dependent on a movement speed variable.
* Player input- This handles all the input from the keyboard 
* Player movement- This handles all the player's abilities. Each of them has been separated into different methods which makes it easier to understand and update the code.
