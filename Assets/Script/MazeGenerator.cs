using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Random = System.Random;

public class MazeGenerator : MonoBehaviour
{

	public GameObject mazePlane;
	public GameObject initialMazePlane;

	private float Position;
	private int checker;

	private Cell[] currentRow = new Cell[8];
	// Use this for initialization
	void Start ()
	{
		Position = 0.0f;
		checker = 0;
		for (int i = 0; i < 8; i++)
		{
			currentRow[i] = new Cell();
			currentRow[i].ID = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void generateRow()
	{
		Position = Position + 2.5f;
		GameObject newMazePlane = Instantiate(mazePlane, transform);
		Vector3 position = initialMazePlane.transform.position;
		position.z = position.z + Position;
		newMazePlane.transform.position = position;
		givesID();
	}

	private void givesID()
	{
		int previousNumber = checker * 8;
		checker++;
		for(int i = 0;i<8;i++){
			if(currentRow[i].ID== 0){
				currentRow[i].ID = ++previousNumber;
			}
		}
	}
	
	
	private void givesRightWall()
	{
		for (int i = 0; i < 7; i++)
		{
			double random = UnityEngine.Random.Range(0, 1);
			if (currentRow[i].ID == currentRow[i + 1].id)
			{
				currentRow[i].hasRightWall = true;
			}
			else if (random >= 0.5)
			{
				currentRow[i].hasRightWall = true;
			}
			else
			{
				currentRow[i + 1].ID = currentRow[i].ID;
			}
		}
	}
	
	
}
