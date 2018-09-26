using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{


	public MazeGenerator mazeGenerator;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("was here");
		if (other.CompareTag("FinalWallTrigger"))
		{
			
			other.gameObject.SetActive(false);
			mazeGenerator.setFinalWall();
			Destroy(this.gameObject);
		}
		else if (other.CompareTag("Wall"))
		{
			Destroy(this.gameObject);
		}
		else if (other.CompareTag("Destructable Wall"))
		{
			Destroy(this.gameObject);
			GameObject text = GameObject.FindGameObjectWithTag("Message");
			text.GetComponent<Text>().text = "You shoot a destructable wall";
			//Player.displayMessage("You shoot a destructable wall");
			other.GetComponent<WallBreak>().beingShoot();
		}
	}
}
