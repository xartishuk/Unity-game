using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody rb;
    
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W) && rb.velocity.y < 50)
        {
            rb.AddRelativeForce(10, 0, 0, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.A) && rb.velocity.z > -50)
        {
            rb.AddRelativeForce(0, -10, 0, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.S) && rb.velocity.y > -50)
        {
            rb.AddRelativeForce(-10, 0, 0, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.D) && rb.velocity.z < 50)
        {
            rb.AddRelativeForce(0, 10, 0, ForceMode.Acceleration);
        }
    }
}
