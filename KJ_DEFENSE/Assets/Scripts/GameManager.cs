using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	//Declaring local variables

	//--Private Members
	public GameObject focusedUnit;
	// Use this for initialization
	void Start () 
	{	

	}

	// Update is called once per frame
	void Update () 
	{

	}

	public GameObject GetFocusedUnit()
	{
		return focusedUnit;
	}

	public void SetFocusedUnit(GameObject newFocusedUnit)
	{
		focusedUnit = newFocusedUnit;
	}

}
