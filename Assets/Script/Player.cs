using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		float xAxis = Input.GetAxis("Horizontal");
		float zAxis = Input.GetAxis("Vertical");

		Vector3 position = transform.position;
		Vector3 newPosition = new Vector3(position.x + xAxis, position.y, position.z + zAxis);

		transform.position = newPosition;
	}
}
