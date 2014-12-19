using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour 
{
	//Declaring local variables

	//--Private Members
	public delegate void HandleAllInputs();
	public delegate void HandleUnitInputs();

	private CameraManager cameraManager;
	private GameManager gameManager;
	private HandleAllInputs handleAllInputs;
	private HandleUnitInputs handleUnitInputs;
	public Unit unit;
	private float back = -1.0f;
	private float forward = 1.0f;
	private int unitFocused;
	private Vector3 lastMousePosition;
	private RaycastHit2D cameraRay;
	public float timeBetweenShot = 1.5f;
	float timerShot;

	private bool isNotHit;

	// Use this for initialization
	void Start () 
	{	
		isNotHit = true;
		cameraManager = Camera.main.GetComponent<CameraManager>();
		gameManager = gameObject.GetComponent<GameManager>();
		unit = gameManager.GetFocusedUnit().GetComponent<Unit>();

		handleAllInputs += HandleAddUnit;
		handleAllInputs += HandleCameraPanning;
		handleAllInputs += HandleCameraZooming;

		handleUnitInputs += HandleUnitCameraFocus;
		handleUnitInputs += HandleUnitMovement;
		handleUnitInputs += HandleUnitShoot;
		handleUnitInputs += HandleCannonAngle;
		cameraManager.UnitFocus(unit);
	}

	// Update is called once per frame
	void Update () 
	{
		handleAllInputs();
		if (gameManager.focusedUnit != null && unit != null && unit == gameManager.focusedUnit.GetComponent<Unit>())
		{
			handleUnitInputs();
		}
		else if (gameManager.focusedUnit != null)
		{
			unit = gameManager.focusedUnit.GetComponent<Unit>();
		}
		timerShot += Time.deltaTime;
		//else
			//HandleUnitCameraFocus();
	}

	//press tab to switch between unit.
	void HandleUnitCameraFocus()
	{
		if(Input.GetKeyDown(KeyCode.Tab))
		{

			isNotHit = true;
			unit.setAnimation(false);
			gameManager.CycleUnits();
			gameManager.SetFocusedUnit();
			unit = gameManager.GetFocusedUnit().GetComponent<Unit>();
			cameraManager.UnitFocus(unit);
		}

		if(isNotHit == false)
		{

			if (cameraRay.collider.gameObject.CompareTag("Tank"))
			{
				unit.setAnimation(false);
				unit = cameraRay.collider.gameObject.GetComponent<Unit>();
			}
			cameraManager.UnitFocus(unit);
		}
	}

	 //press a or d to move unit being controled
	void HandleUnitMovement()
	{
		if(Input.GetKey(KeyCode.A))
		{	
			unit.setAnimation(true);
			unit.MoveFocusedUnit(back);
		//	Debug.Log("here");
		}
		
		else if(Input.GetKey(KeyCode.D)/*Down(KeyCode.D)*/)
		{
			unit.setAnimation(true);
			unit.MoveFocusedUnit(forward);

		}
		else
		{
			unit.setAnimation(false);
		}

	}

	void HandleCannonAngle()
	{
		if(Input.GetKey("w"))
		{	
			unit.setAnimation(true);
			unit.ChangeCannonAngle(forward);
		}
		
		else if(Input.GetKey("s")/*Down(KeyCode.D)*/)
		{
			unit.setAnimation(true);
			unit.ChangeCannonAngle(back);
		}
		else
		{
			unit.setAnimation(false);
		}
		
	}

	// move mouse while holding left click to pan
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

	// zoom in and out with mouse wheel
	void HandleCameraZooming()
	{
		float newValue = Input.GetAxis("Mouse ScrollWheel");
		cameraManager.CameraZooming(newValue);

	}

	// Adding new unit
	void HandleAddUnit()
	{
		if (Input.GetButtonDown ("SpawnTank")) {
			GameObject newUnit = Instantiate(Resources.Load("TankObj-A"),gameManager.SpawnLocation.position,gameManager.SpawnLocation.rotation) as GameObject;
			gameManager.AppendUnitToList (newUnit);
			gameManager.CycleUnits();
			gameManager.SetFocusedUnit();
			cameraManager.UnitFocus(newUnit.GetComponent<Unit>());
		}
	}

	void HandleUnitShoot()
	{
		if (Input.GetButtonDown ("Shoot") && timerShot >= timeBetweenShot) {
			GameObject newUnit = Instantiate(Resources.Load("Bullet"),unit.transform.GetChild(0).transform.position,unit.transform.GetChild(0).transform.rotation) as GameObject;
			newUnit.rigidbody2D.AddForce(900.0f * unit.transform.GetChild(0).transform.right);
			unit.GetComponent<Unit>().ShootSound();
			timerShot = 0;
		}
		
	}
}
