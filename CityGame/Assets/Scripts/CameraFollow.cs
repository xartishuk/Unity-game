using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform followedObject;
    public Vector3 offset;
    public float lagBehindModifier = 25f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Vector3 desiredPosition = followedObject.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, lagBehindModifier * Time.fixedDeltaTime);

	}
}
