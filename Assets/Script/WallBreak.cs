using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallBreak : MonoBehaviour
{

	private int numberOfShoots = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (numberOfShoots >= 3)
		{
			gameObject.SetActive(false);
			GameObject text = GameObject.FindGameObjectWithTag("Message");
			text.GetComponent<Text>().text = "You destroy a wall";
		}
		else if (numberOfShoots >= 2)
		{
			GetComponent<MeshRenderer>().material.color = new Color(1.0f,0.0f,200.0f/255.0f,1.0f);
		}
		
		else if (numberOfShoots >= 1)
		{
			Debug.Log("wasHere");
			GetComponent<MeshRenderer>().material.color = new Color(167.0f/255.0f,0.0f,1.0f,1.0f);
		}
	}

	public void beingShoot()
	{
		numberOfShoots++;
	}
}
