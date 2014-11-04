using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	//Declaring local variables

	public GameObject focusedUnit;
	private GameObject[] listOfUnits;
	private int unitFocused;

	void Start()
	{
		listOfUnits = GetUnitList();
		unitFocused = 0;
	}

	void Update()
	{
		listOfUnits = GetUnitList();
	}

	private GameObject[] GetUnitList()
	{
		//Declaring local variables
		List<GameObject> units = new List<GameObject>();

		foreach (Unit unit in GameObject.FindObjectsOfType(typeof(Unit))) 
		{
			units.Add(unit.gameObject);
		}

		Debug.Log("unitFocused value is: " + unitFocused);
		Debug.Log (units.Count);

		return units.ToArray();
	}

	public void CycleUnits()
	{
		if(unitFocused >= listOfUnits.Length - 1)
		{
			unitFocused = 0;
		}
		else
		{
			unitFocused++;
		}
	}

	public GameObject GetFocusedUnit()
	{
		return focusedUnit;
	}

	public void SetFocusedUnit()
	{
		focusedUnit = listOfUnits[unitFocused];
		Debug.Log("Test");
	}

}