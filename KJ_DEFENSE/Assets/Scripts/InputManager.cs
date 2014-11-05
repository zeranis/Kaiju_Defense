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
	private RaycastHit2D cameraRay;
	private bool isNotHit;

	// Use this for initialization
	void Start () 
	{	
		isNotHit = true;
		cameraManager = Camera.main.GetComponent<CameraManager>();
		gameManager = gameObject.GetComponent<GameManager>();
		unit = gameManager.GetFocusedUnit().GetComponent<Unit>();
			
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
			isNotHit = true;
			gameManager.CycleUnits();
			gameManager.SetFocusedUnit();
			unit = gameManager.GetFocusedUnit().GetComponent<Unit>();
			cameraManager.UnitFocus(unit);
		}

		if(isNotHit == false)
		{
			unit = cameraRay.collider.gameObject.GetComponent<Unit>();
			cameraManager.UnitFocus(unit);
		}
	}

	void HandleUnitMovement()
	{
		if(Input.GetKey("a"))
		{	
			unit.setAnimation(true);
			unit.MoveFocusedUnit(back);
		}
		
		else if(Input.GetKey("d")/*Down(KeyCode.D)*/)
		{
			unit.setAnimation(true);
			unit.MoveFocusedUnit(forward);
		}
		else
		{
			unit.setAnimation(false);
		}

	}

	void HandleCameraPanning()
	{
		//Declaring local variables

		if (Input.GetMouseButtonDown(0))
		{
			cameraRay = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

			if(cameraRay.collider == null)
			{
				//Get the origin of the mouse's position
				lastMousePosition = Input.mousePosition;
				isNotHit = true;
			}
			else
			{
				isNotHit = false;
				Debug.Log("Mouse clicked an object: " + isNotHit);
			}
		}

		if (Input.GetMouseButton(0) && isNotHit == true)
		{
			cameraManager.CameraPanning(lastMousePosition);
		}
	}
}
