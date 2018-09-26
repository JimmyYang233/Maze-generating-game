using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngineInternal.Input;

public class Player : MonoBehaviour
{

	[SerializeField]private float speed;
	[SerializeField]private float mouseSensitivity;
	[SerializeField]private MazeGenerator mazeGenerator;
	[SerializeField]private AnimationCurve jumpFallOff;
	[SerializeField]private float jumpMultiplier;
	
	private GameObject camera;
	private float xAxisClamp;
	private bool isJumping;
	
	private CharacterController charController;
	// Use this for initialization
	void Start ()
	{
		camera = transform.GetChild(0).gameObject;
		charController = gameObject.GetComponent<CharacterController>();
		xAxisClamp = 0;
		isJumping = false;

	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		CameraMovement();
		CameraRotation();
		if (Input.GetKeyDown(KeyCode.Space))
		{
			jump();
		}
	}

	private void CameraRotation()
	{
		float mouseX = Input.GetAxis("Mouse X")*mouseSensitivity;
		float mouseY = Input.GetAxis("Mouse Y")*mouseSensitivity;

		xAxisClamp += mouseY;

		if (xAxisClamp > 90.0f)
		{
			xAxisClamp = 90.0f;
			mouseY = 0.0f;
			ClampXAxisRotationToValue(270.0f);
		}
		
		else if (xAxisClamp < -90.0f)
		{
			xAxisClamp = -90.0f;
			mouseY = 0.0f;
			ClampXAxisRotationToValue(90.0f);
		}
		
		camera.transform.Rotate(Vector3.left*mouseY);
		transform.Rotate(Vector3.up*mouseX);
		
		//Debug.Log(mouseX + ", " + mouseY);
	}

	private void CameraMovement()
	{
		float xAxis = Input.GetAxis("Horizontal");
		float zAxis = Input.GetAxis("Vertical");

		Vector3 newPosition = transform.forward * zAxis * speed+ transform.right*xAxis*speed;

		charController.SimpleMove(newPosition);
	}

	private void ClampXAxisRotationToValue(float value)
	{
		Vector3 eulerRotation = camera.transform.eulerAngles;
		eulerRotation.x = value;
		camera.transform.eulerAngles = eulerRotation;
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("RowTrigger"))
		{
			mazeGenerator.generateRow();
			other.gameObject.SetActive(false);
		}
	}

	private void jump()
	{
		if (!isJumping)
		{
			isJumping = true;
			StartCoroutine(jumpEvent());
		}
		
	}

	private IEnumerator jumpEvent()
	{

		float timeInAir = 0.0f;
		do
		{
			float jumpForce = jumpFallOff.Evaluate(timeInAir);
			charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
			timeInAir += Time.deltaTime;
			yield return null;
		} while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);
		isJumping = false;
	}
}
