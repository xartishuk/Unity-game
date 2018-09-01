using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UFOMovement : MonoBehaviour {

    public float maxSpeed;
    public float speedGain;
    public float speedLossOnStop;
    public float speedLossOnChangeDirection;
    public float speedScaling = 1f;
    public float tiltScaling = 1f;

    private Rigidbody rb;
    private Vector3 currentSpeed;

    private float verticalInput;
    private float horizontalInput;
    private float jumpInput;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        currentSpeed = new Vector3();

        verticalInput = 0f;
        horizontalInput = 0f;
        jumpInput = 0f;

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
        jumpInput = Input.GetAxis("Jump");

        Debug.Log("verticalInput = " + verticalInput);
        Debug.Log("horizontalInput = " + horizontalInput);
        Debug.Log("jumpInput = " + jumpInput);

    }

    private void Move()
    {
        // Frontal movement block
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

        // Horizontal movement block
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

        // Vertical movement block
        if (jumpInput != 0) // Moving
        {
            if ((currentSpeed.y < 0 && jumpInput > 0) || (currentSpeed.y > 0 && jumpInput < 0)) // Change directions
            {
                currentSpeed.y += jumpInput * speedLossOnChangeDirection * Time.fixedDeltaTime * speedScaling;
            }
            else // Same direction
            {
                currentSpeed.y += jumpInput * speedGain * Time.fixedDeltaTime * speedScaling;
            }
            // Max speed check
            float maxSpeedHorizontal = maxSpeed * jumpInput;
            if (Mathf.Abs(currentSpeed.y) > Mathf.Abs(maxSpeedHorizontal))
            {
                currentSpeed.y = maxSpeedHorizontal;
            }
        }
        else // Stopping
        {
            float deltaSpeedLossOnStop = speedLossOnStop * Time.fixedDeltaTime * speedScaling;
            if (Mathf.Abs(currentSpeed.y) > deltaSpeedLossOnStop)
            {
                currentSpeed.y -= Mathf.Sign(currentSpeed.y) * deltaSpeedLossOnStop;
            }
            else
            {
                currentSpeed.y = 0;
            }
        }

        rb.velocity = currentSpeed;
    }

    private void Tilt()
    {
        transform.rotation = Quaternion.Euler(currentSpeed.z * tiltScaling, transform.rotation.eulerAngles.y, currentSpeed.x * tiltScaling * -1f);
    }

}


