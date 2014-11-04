using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour 
{
	//Declaring local variables
	
	//--Private Members
	private delegate void HandleAllInputs();
	private CameraManager cameraManager;
	private GameManager gameManager;
	private HandleAllInputs handleAllInputs;
	private Unit unit;
	private float back = -1.0f;
	private float forward = 1.0f;
	private int unitFocused;
	private Vector3 lastMousePosition;

	// Use this for initialization
	void Start () 
	{	
		cameraManager = Camera.main.GetComponent<CameraManager>();
		gameManager = gameObject.GetComponent<GameManager>();
		unit = gameManager.GetFocusedUnit().GetComponent<Unit>();
		//unit = gameObject.GetComponent<GameManager>().GetFocusedUnit().GetComponent<Unit>();
		//nitList = gameObject.GetComponent<Unit>().GetComponent<GameManager>().GetUnitFocused();
	
		handleAllInputs += HandleUnitCameraFocus;
		handleAllInputs += HandleCameraPanning;
		handleAllInputs += HandleUnitMovement;
	}

	// Update is called once per frame
	void Update () 
	{
		handleAllInputs();
	}

	void HandleUnitCameraFocus()
	{
		if(Input.GetKeyDown(KeyCode.Tab))
		{
			gameManager.CycleUnits();
			gameManager.SetFocusedUnit();
			unit = gameManager.GetFocusedUnit().GetComponent<Unit>();
			cameraManager.UnitFocus(unit);
		}
	}

	void HandleUnitMovement()
	{
		if(Input.GetKey("a")/*Down(KeyCode.A)*/)
		{	
			unit.MoveFocusedUnit(back);
		}
		
		if(Input.GetKey("d")/*Down(KeyCode.D)*/)
		{
			unit.MoveFocusedUnit(forward);
		}
	}

	void HandleCameraPanning()
	{
		if (Input.GetMouseButtonDown(0))
		{
			//Get the origin of the mouse's position
			lastMousePosition = Input.mousePosition;
		}

		if (Input.GetMouseButton(0))
		{
			cameraManager.CameraPanning(lastMousePosition);
		}
	}
}
