using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	//Declaring local variables

	public GameObject focusedUnit;
	public Transform SpawnLocation;
	public List<GameObject> listOfUnits = new List<GameObject>();
	private int unitFocused;

	void Awake()
	{
		listOfUnits.Add(focusedUnit);
	}
	void Start()
	{
		//listOfUnits = GetUnitList();
		unitFocused = 0;
	}

	public void AppendUnitToList(GameObject newUnit)
	{
		listOfUnits.Add(newUnit);
	}

	private GameObject[] GetUnitList()
	{
		return listOfUnits.ToArray();
	}

	public void CycleUnits()
	{
		if(unitFocused >= GetUnitList().Length - 1)
		{
			unitFocused = 0;
		}
		else
		{
			unitFocused++;
		}
	}
	public void getAllUnit()
	{
		foreach (Unit unit in GameObject.FindObjectsOfType(typeof(Unit))) 
		{
			listOfUnits.Add(unit.gameObject);
		}
	}
	public GameObject GetFocusedUnit()
	{
		return focusedUnit;
	}

	public void SetFocusedUnit()
	{
		focusedUnit = listOfUnits[unitFocused];
	
	}
	//Reference
	//Declaring local variables
	//List<GameObject> units = new List<GameObject>();	
	//foreach (Unit unit in GameObject.FindObjectsOfType(typeof(Unit))) 
	//{
	//units.Add(unit.gameObject);
	//}
	//Debug.Log("unitFocused value is: " + unitFocused);
	//Debug.Log (units.Count);
}