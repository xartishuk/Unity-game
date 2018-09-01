using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UFOMovement : MonoBehaviour {

    //const float DIAGONAL_MOVEMENT_MOD = 0.71f;

    public float maxSpeed;
    public float speedGain;
    public float speedLossOnStop;
    public float speedLossOnChangeDirection;
    public float speedScaling = 1f;
    public float tiltScaling = 1f;

    private Rigidbody rb;
    //private bool isMoving;
    //private bool isMovingForward;
    //private bool isMovingBackwards;
    //private bool isMovingLeft;
    //private bool isMovingRight;
    //private bool isMovingUp;
    //private bool isMovingDown;
    private Vector3 currentSpeed;

    private float verticalInput;
    private float horizontalInput;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        //isMoving = false;
        //isMovingForward = false;
        //isMovingBackwards = false;
        //isMovingLeft = false;
        //isMovingRight = false;
        //isMovingUp = false;
        //isMovingDown = false;
        currentSpeed = new Vector3();

        verticalInput = 0f;
        horizontalInput = 0f;

}

    void FixedUpdate()
    {
        ReadInput();
        Move();
        Tilt();
    }

    private void ReadInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        Debug.Log("verticalInput = " + verticalInput);
        Debug.Log("horizontalInput = " + horizontalInput);
        // UP & DOWN BUTTONS
    }

    private void Move()
    {
        if (verticalInput != 0) // Moving
        {
            if ((currentSpeed.z < 0 && verticalInput > 0) || (currentSpeed.z > 0 && verticalInput < 0)) // Change directions
            {
                currentSpeed.z += verticalInput * speedLossOnChangeDirection * Time.fixedDeltaTime * speedScaling;
            }
            else // Same direction
            {
                currentSpeed.z += verticalInput * speedGain * Time.fixedDeltaTime * speedScaling;
            }

            /*
             *  Using Mathf.Clamp() for MaxSpeed would be wasteful
             */

            // Max speed check
            float maxSpeedVertical = maxSpeed * verticalInput;
            if (Mathf.Abs(currentSpeed.z) > Mathf.Abs(maxSpeedVertical))
            {
                currentSpeed.z = maxSpeedVertical;
            }
        }
        else // Stopping
        {
            float deltaSpeedLossOnStop = speedLossOnStop * Time.fixedDeltaTime * speedScaling;
            if (Mathf.Abs(currentSpeed.z) > deltaSpeedLossOnStop)
            {
                currentSpeed.z -= Mathf.Sign(currentSpeed.z) * deltaSpeedLossOnStop;
            }
            else
            {
                currentSpeed.z = 0;
            }
        }
        if (horizontalInput != 0) // Moving
        {
            if ((currentSpeed.x < 0 && horizontalInput > 0) || (currentSpeed.x > 0 && horizontalInput < 0)) // Change directions
            {
                currentSpeed.x += horizontalInput * speedLossOnChangeDirection * Time.fixedDeltaTime * speedScaling;
            }
            else // Same direction
            {
                currentSpeed.x += horizontalInput * speedGain * Time.fixedDeltaTime * speedScaling;
            }
            // Max speed check
            float maxSpeedHorizontal = maxSpeed * horizontalInput;
            if (Mathf.Abs(currentSpeed.x) > Mathf.Abs(maxSpeedHorizontal))
            {
                currentSpeed.x = maxSpeedHorizontal;
            }
        }
        else // Stopping
        {
            float deltaSpeedLossOnStop = speedLossOnStop * Time.fixedDeltaTime * speedScaling;
            if (Mathf.Abs(currentSpeed.x) > deltaSpeedLossOnStop)
            {
                currentSpeed.x -= Mathf.Sign(currentSpeed.x) * deltaSpeedLossOnStop;
            }
            else
            {
                currentSpeed.x = 0;
            }
        }

        rb.velocity = currentSpeed;
    }

    private void Tilt()
    {
        transform.rotation = Quaternion.Euler(currentSpeed.z * tiltScaling, transform.rotation.eulerAngles.y, currentSpeed.x * tiltScaling * -1f);
    }

    // Update is called once per frame
    //	void FixedUpdate () {

    //        CheckButtons();
    //        ChangeSpeed();
    //        Move();
    //        Tilt();
    //    }


    //    private void CheckButtons()
    //    {
    //        if (Input.GetKey(KeyCode.W))
    //        {
    //            isMovingForward = true;
    //        }
    //        else
    //        {
    //            isMovingForward = false;
    //        }
    //        if (Input.GetKey(KeyCode.S))
    //        {
    //            isMovingBackwards = true;
    //        }
    //        else
    //        {
    //            isMovingBackwards = false;
    //        }
    //        if (Input.GetKey(KeyCode.A))
    //        {
    //            isMovingLeft = true;
    //        }
    //        else
    //        {
    //            isMovingLeft = false;
    //        }
    //        if (Input.GetKey(KeyCode.D))
    //        {
    //            isMovingRight = true;
    //        }
    //        else
    //        {
    //            isMovingRight = false;
    //        }
    //        if (Input.GetKey(KeyCode.Space))
    //        {
    //            isMovingUp = true;
    //        }
    //        else
    //        {
    //            isMovingUp = false;
    //        }
    //        if (Input.GetKey(KeyCode.LeftShift))
    //        {
    //            isMovingDown = true;
    //        }
    //        else
    //        {
    //            isMovingDown = false;
    //        }
    //    }

    //    private void ChangeSpeed()
    //    {   
    //        // Zeroing contradicting input
    //        if (isMovingForward || isMovingBackwards || isMovingLeft || isMovingRight)
    //        {
    //            isMoving = true;
    //            if (isMovingForward && isMovingBackwards)
    //            {
    //                isMovingForward = false;
    //                isMovingBackwards = false;
    //            }
    //            if (isMovingLeft && isMovingRight)
    //            {
    //                isMovingLeft = false;
    //                isMovingRight = false;
    //            }
    //            if (isMovingUp && isMovingDown)
    //            {
    //                isMovingUp = false;
    //                isMovingDown = false;
    //            }
    //        }
    //        else
    //        {
    //            isMoving = false;
    //        }

    //        // Applying speed changes according to directions on XZ plane

    //        if (isMovingForward && isMovingLeft)
    //        {
    //            // Forward
    //            if(currentSpeed.z < 0) // Changing directions
    //            {
    //                currentSpeed.z += speedLossOnChangeDirection * Time.fixedDeltaTime * speedScaling * DIAGONAL_MOVEMENT_MOD;
    //            }
    //            else // Same direction
    //            {
    //                currentSpeed.z += speedGain * Time.fixedDeltaTime * speedScaling * DIAGONAL_MOVEMENT_MOD;
    //            }
    //            // Left
    //            if (currentSpeed.x > 0) // Changing directions
    //            {
    //                currentSpeed.x -= speedLossOnChangeDirection * Time.fixedDeltaTime * speedScaling * DIAGONAL_MOVEMENT_MOD;
    //            }
    //            else // Same direction
    //            {
    //                currentSpeed.x -= speedGain * Time.fixedDeltaTime * speedScaling * DIAGONAL_MOVEMENT_MOD;
    //            }
    //        }
    //        else if (isMovingForward && isMovingRight)
    //        {
    //            // Forward
    //            if (currentSpeed.z < 0) // Changing directions
    //            {
    //                currentSpeed.z += speedLossOnChangeDirection * Time.fixedDeltaTime * speedScaling * DIAGONAL_MOVEMENT_MOD;
    //            }
    //            else // Same direction
    //            {
    //                currentSpeed.z += speedGain * Time.fixedDeltaTime * speedScaling * DIAGONAL_MOVEMENT_MOD;
    //            }
    //            // Right
    //            if (currentSpeed.x < 0) // Changing directions
    //            {
    //                currentSpeed.x += speedLossOnChangeDirection * Time.fixedDeltaTime * speedScaling * DIAGONAL_MOVEMENT_MOD;
    //            }
    //            else // Same direction
    //            {
    //                currentSpeed.x += speedGain * Time.fixedDeltaTime * speedScaling * DIAGONAL_MOVEMENT_MOD;
    //            }
    //        }
    //        else if (isMovingBackwards && isMovingRight)
    //        {
    //            // Backwards
    //            if (currentSpeed.z > 0) // Changing directions
    //            {
    //                currentSpeed.z -= speedLossOnChangeDirection * Time.fixedDeltaTime * speedScaling * DIAGONAL_MOVEMENT_MOD;
    //            }
    //            else // Same direction
    //            {
    //                currentSpeed.z -= speedGain * Time.fixedDeltaTime * speedScaling * DIAGONAL_MOVEMENT_MOD;
    //            }
    //            // Right
    //            if (currentSpeed.x < 0) // Changing directions
    //            {
    //                currentSpeed.x += speedLossOnChangeDirection * Time.fixedDeltaTime * speedScaling * DIAGONAL_MOVEMENT_MOD;
    //            }
    //            else // Same direction
    //            {
    //                currentSpeed.x += speedGain * Time.fixedDeltaTime * speedScaling * DIAGONAL_MOVEMENT_MOD;
    //            }
    //        }
    //        else if (isMovingBackwards && isMovingLeft)
    //        {
    //            // Backwards
    //            if (currentSpeed.z > 0) // Changing directions
    //            {
    //                currentSpeed.z -= speedLossOnChangeDirection * Time.fixedDeltaTime * speedScaling * DIAGONAL_MOVEMENT_MOD;
    //            }
    //            else // Same direction
    //            {
    //                currentSpeed.z -= speedGain * Time.fixedDeltaTime * speedScaling * DIAGONAL_MOVEMENT_MOD;
    //            }
    //            // Left
    //            if (currentSpeed.x > 0) // Changing directions
    //            {
    //                currentSpeed.x -= speedLossOnChangeDirection * Time.fixedDeltaTime * speedScaling * DIAGONAL_MOVEMENT_MOD;
    //            }
    //            else // Same direction
    //            {
    //                currentSpeed.x -= speedGain * Time.fixedDeltaTime * speedScaling * DIAGONAL_MOVEMENT_MOD;
    //            }
    //        }
    //        else if (isMovingForward)
    //        {
    //            // Forward
    //            if (currentSpeed.z < 0) // Changing directions
    //            {
    //                currentSpeed.z += speedLossOnChangeDirection * Time.fixedDeltaTime * speedScaling;
    //            }
    //            else // Same direction
    //            {
    //                currentSpeed.z += speedGain * Time.fixedDeltaTime * speedScaling;
    //            }
    //        }
    //        else if (isMovingBackwards)
    //        {
    //            // Backwards
    //            if (currentSpeed.z > 0) // Changing directions
    //            {
    //                currentSpeed.z -= speedLossOnChangeDirection * Time.fixedDeltaTime * speedScaling;
    //            }
    //            else // Same direction
    //            {
    //                currentSpeed.z -= speedGain * Time.fixedDeltaTime * speedScaling;
    //            }
    //        }
    //        else if (isMovingLeft)
    //        {
    //            // Left
    //            if (currentSpeed.x > 0) // Changing directions
    //            {
    //                currentSpeed.x -= speedLossOnChangeDirection * Time.fixedDeltaTime * speedScaling;
    //            }
    //            else // Same direction
    //            {
    //                currentSpeed.x -= speedGain * Time.fixedDeltaTime * speedScaling;
    //            }
    //        }
    //        else if (isMovingRight)
    //        {
    //            // Right
    //            if (currentSpeed.x < 0) // Changing directions
    //            {
    //                currentSpeed.x += speedLossOnChangeDirection * Time.fixedDeltaTime * speedScaling;
    //            }
    //            else // Same direction
    //            {
    //                currentSpeed.x += speedGain * Time.fixedDeltaTime * speedScaling;
    //            }
    //        }

    //        // Applying speed changes according to directions on Y axis
    //        if (isMovingUp)
    //        {
    //            // Up
    //            if (currentSpeed.y < 0) // Changing directions
    //            {
    //                currentSpeed.y += speedLossOnChangeDirection * Time.fixedDeltaTime * speedScaling;
    //            }
    //            else // Same direction
    //            {
    //                currentSpeed.y += speedGain * Time.fixedDeltaTime * speedScaling;
    //            }
    //        }
    //        else if (isMovingDown)
    //        {
    //            // Down
    //            if (currentSpeed.y > 0) // Changing directions
    //            {
    //                currentSpeed.y -= speedLossOnChangeDirection * Time.fixedDeltaTime * speedScaling;
    //            }
    //            else // Same direction
    //            {
    //                currentSpeed.y -= speedGain * Time.fixedDeltaTime * speedScaling;
    //            }
    //        }

    //        //Stopping
    //        {
    //            // Z-axis
    //            if (!isMovingForward && !isMovingBackwards && currentSpeed.z != 0)
    //            {
    //                if (Mathf.Abs(currentSpeed.z) < speedLossOnStop * Time.fixedDeltaTime) // Almost Zero
    //                {
    //                    currentSpeed.z = 0;
    //                }
    //                else if (currentSpeed.z > 0) // Forward
    //                {
    //                    currentSpeed.z -= speedLossOnStop * Time.fixedDeltaTime * speedScaling;
    //                }
    //                else // Backwards
    //                {
    //                    currentSpeed.z += speedLossOnStop * Time.fixedDeltaTime * speedScaling;
    //                }
    //            }
    //            // X-axis
    //            if (!isMovingLeft && !isMovingRight && currentSpeed.x != 0)
    //            {
    //                if (Mathf.Abs(currentSpeed.x) < speedLossOnStop * Time.fixedDeltaTime) // Almost Zero
    //                {
    //                    currentSpeed.x = 0;
    //                }
    //                else if (currentSpeed.x > 0) // Right
    //                {
    //                    currentSpeed.x -= speedLossOnStop * Time.fixedDeltaTime * speedScaling;
    //                }
    //                else // Left
    //                {
    //                    currentSpeed.x += speedLossOnStop * Time.fixedDeltaTime * speedScaling;
    //                }
    //            }
    //            // Y-axis
    //            if (!isMovingUp && !isMovingDown && currentSpeed.y != 0)
    //            {
    //                if (Mathf.Abs(currentSpeed.y) < speedLossOnStop * Time.fixedDeltaTime) // Almost Zero
    //                {
    //                    currentSpeed.y = 0;
    //                }
    //                else if (currentSpeed.y > 0) // Forward
    //                {
    //                    currentSpeed.y -= speedLossOnStop * Time.fixedDeltaTime * speedScaling;
    //                }
    //                else // Backwards
    //                {
    //                    currentSpeed.y += speedLossOnStop * Time.fixedDeltaTime * speedScaling;
    //                }
    //            }
    //        }

    //    }

    //    private void Move()
    //    {
    //        rb.velocity = currentSpeed;
    //        Debug.Log(currentSpeed);
    //    }

    //    private void Tilt()
    //    {
    //        transform.rotation = Quaternion.Euler(currentSpeed);
    //    }


}


