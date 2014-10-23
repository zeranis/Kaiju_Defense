using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour 
{
	//Declaring local variables:

	//--Public members
	public float camSpeed;

	//--Private members
	private int unitFocused;
	private Vector3 newPosition;
	//private float centerX;
	//private float centerY;
	//private float offsetX;
	//private float offsetY;
	//private float aspectRatio;
	private bool isUnitFocused;
	private float panningSpeed = 2.0f;

	// Use this for super initialization
	void Awake()
	{
		//Initializes the position of the camera to the coordinate: 
		//(x,y,z) = (0, 20, camera's z-coordinates)
		gameObject.transform.position = new Vector3(0, 3.5f, gameObject.transform.position.z);
	}
			
	// Use this for initialization
	void Start () 
	{
		//Setting the camera into ortographic mode
		Camera.main.orthographic = true;

		//Changing the camera's half-vertical size
		Camera.main.orthographicSize = 4.5f; 

		//Initializing unitFocused to zero
		unitFocused = 0;

		//Initializing isUnitFocused to false
		isUnitFocused = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Making the camera move to the focused GameObject
		if(isUnitFocused == true)
		{
			//Lerp the camera to new position. 
			Camera.main.transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * camSpeed);
		}
	}

	//Method -- CameraPanning
	//Parameters -- mousePosOrigin
	//Purpose -- This will make the camera pan around the world screen.
	public void CameraPanning(Vector3 mousePosOrigin)
	{
		//Transforms the mouse position from screen space to viewport space
		Vector3 mousePosNormalized = Camera.main.ScreenToViewportPoint(Input.mousePosition - mousePosOrigin);
		
		//This will be the new position of the mouse, thus will also be the camera's new position
		Vector3 mouseDestination = new Vector3(mousePosNormalized.x * panningSpeed, mousePosNormalized.y * panningSpeed, 0);
		//This will Translate, or move, the camera's position to the mouse's position
		Camera.main.transform.Translate(mouseDestination, Space.Self);
	}

	//Method -- UnitFocus
	//Parameters -- GameObject unitObj
	//Purpose -- UnitFocus will take in an argument of GameObject unitObj, this will tell the camera to then focus on that
	//GameObject. Essentially, the camera's transform will be changed to that of the transform of the GameObject.
	public void UnitFocus(GameObject unitObj)
	{
		//Assigns the transform the unitObj to the camera
		newPosition = new Vector3 (unitObj.transform.position.x - 2.0f, unitObj.transform.position.y + 2.0f, 0.0f);
	}
}
