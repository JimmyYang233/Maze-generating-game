using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	public float speed;
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
		Vector3 newPosition = new Vector3(position.x + xAxis*speed, position.y, position.z + zAxis*speed);

		transform.position = newPosition;
	}
}
