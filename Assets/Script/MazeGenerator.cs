using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Random = System.Random;

public class MazeGenerator : MonoBehaviour
{

	public GameObject mazePlane;
	public GameObject rightWall;
	public GameObject bottomWall;
	public GameObject pickUp;
	public GameObject pickUps;
	
	private Vector3 initialPickUpPosition;
	private GameObject currentMazePlane;
	private int checker;
	private Vector3 initialPosition;
	private Vector3 initialRightWallPosition;
	private Vector3 initialBottomWallPosition;
	private bool isFinalRow;
	private Cell[] currentRow = new Cell[8];
	// Use this for initialization
	void Start ()
	{
		checker = 0;
		initialPosition = new Vector3(0f, 0f, 11f);
		initialRightWallPosition = new Vector3(3.75f, 0.25f, 0f);
		initialBottomWallPosition = new Vector3(4.375f, 0.25f, 5f);
		initialPickUpPosition = new Vector3(-8.75f,0.5f,11f);
		for (int i = 0; i < 8; i++)
		{
			currentRow[i] = new Cell();
			currentRow[i].ID = 0;
		}

		isFinalRow = false;
		generateRow();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void generateRow()
	{
		GameObject newMazePlane = Instantiate(mazePlane, transform);
		if (isFinalRow)
		{
			newMazePlane.transform.GetChild(0).gameObject.SetActive(false);
		}
		Vector3 position  = initialPosition;
		//Debug.Log("Initial Position" +  position.x + " " + position.y + "  "+ position.z);
		newMazePlane.transform.position = position;
		initialPosition.z = initialPosition.z + 2.5f;
		currentMazePlane = newMazePlane;
		givesID();
		givesRightWall();
		givesBottomWall();
		if (!isFinalRow)
		{
			drawWalls();
			setNewRow();
		}
		else
		{
			setLastWall();
			drawWalls();
		}
	}

	private void givesID()
	{
		int previousNumber = checker * 8;
		checker++;
		for(int i = 0;i<8;i++){
			if(currentRow[i].ID == 0){
				currentRow[i].ID = ++previousNumber;
				//Debug.Log(currentRow[i].ID);
			}
		}
	}
	
	
	private void givesRightWall()
	{
		for (int i = 0; i < 7; i++)
		{
			int random = UnityEngine.Random.Range(0, 10);
			if (currentRow[i].ID == currentRow[i + 1].ID)
			{
				currentRow[i].hasRightWall = true;
			}
			else if (random >=5)
			{
				//Debug.Log("wasHere");
				currentRow[i].hasRightWall = true;
			}
			else
			{
				int idToChange = currentRow[i+1].ID;
				for(int j = 0;j<8;j++) {
					if(currentRow[j].ID ==idToChange) {
						currentRow[j].ID = currentRow[i].ID;
					}
				}
			}
		}
	}

	private void givesBottomWall()
	{
		List<Cell> sets = new List<Cell>();
		for (int i = 0; i < 8; i++)
		{
			int random = UnityEngine.Random.Range(0, 10);
			if (random >= 5)
			{
				currentRow[i].hasBottomWall = true;
			}

			sets.Add(currentRow[i]);
			if (i == 7|| currentRow[i].ID != currentRow[i + 1].ID){
				if (!hasDownPassage(sets))
				{
					getaDownPassage(sets);
				}

				sets.Clear();
			}
		}
	}

	private void drawWalls()
	{
		foreach (Cell cell in currentRow)
		{
			if (cell.hasRightWall)
			{
				GameObject newRightWall = Instantiate(rightWall, currentMazePlane.transform);
				//Debug.Log("newPosition.x before" + initialRightWallPosition.x);	
				
				Vector3 newPosition = initialRightWallPosition;
				Vector3 newScale = new Vector3(0.25f, 0.5f, 10f);
				newRightWall.transform.localScale = newScale;				
				//Debug.Log("newPosition.x after" + newPosition.x);
				newRightWall.transform.localPosition = newPosition;
			}

			int random = UnityEngine.Random.Range(0, 10);
			if (random == 4)
			{
				GameObject newPickUp = Instantiate(pickUp, pickUps.transform);
				Vector3 newPosition = initialPickUpPosition;
				newPickUp.transform.localPosition = newPosition;
			}
			
			initialPickUpPosition.x = initialPickUpPosition.x + 2.5f;
			initialRightWallPosition.x = initialRightWallPosition.x - 1.25f;
			if (cell.hasBottomWall)
			{
				GameObject newBottomWall = Instantiate(bottomWall, currentMazePlane.transform);
				Vector3 newPosition = initialBottomWallPosition;
				Vector3 newScale = new Vector3(1.5f, 0.5f, 2f);
				newBottomWall.transform.localScale = newScale;
				newBottomWall.transform.localPosition = newPosition;
			}

			initialBottomWallPosition.x = initialBottomWallPosition.x - 1.25f;
		}
		
		
	}

	private bool hasDownPassage(List<Cell> sets)
	{
		if (sets.Count == 0)
		{
			return true;
		}

		foreach (Cell cell in sets)
		{
			if (!cell.hasBottomWall)
			{
				return true;
			}
		}

		return false;
	}

	private void getaDownPassage(List<Cell> sets)
	{
		int size = sets.Count;
		int random = UnityEngine.Random.Range(0, size);
		sets[random].hasBottomWall = false;
	}

	private void setNewRow()
	{
		for(int i = 0;i<8;i++){
			if(currentRow[i].hasBottomWall){
				currentRow[i].ID = 0;
			}

			currentRow[i].hasBottomWall = false;
			currentRow[i].hasRightWall = false;
			//Debug.Log(currentRow[i].ID);
		}
		initialRightWallPosition = new Vector3(3.75f, 0.25f, 0f);
		initialBottomWallPosition = new Vector3(4.375f, 0.25f, 5f);
		initialPickUpPosition.x = -8.75f;
		initialPickUpPosition.z = initialPickUpPosition.z + 2.5f;
	}

	public void setFinalWall()
	{
		isFinalRow = true;
		generateRow();
	}

	private void setLastWall()
	{
		for(int i = 0;i<8;i++)
		{
			currentRow[i].hasBottomWall = true;
			if (i < 7)
			{
				if(currentRow[i].ID != currentRow[i+1].ID&&(currentRow[i].hasRightWall)){
					currentRow[i].hasRightWall = false;
					int idToChange = currentRow[i+1].ID;
					for(int j = 0;j<8;j++) {
						if(currentRow[j].ID ==idToChange) {
							currentRow[j].ID = currentRow[i].ID;
						}
					}
				}
			}	
		}
	}
}
