using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour 
{
	//Declaring local variables:

	//--Public members
	public float camSpeed;
	public GameObject centerBg;
	//--Private members
	private Vector3 newPosition;
	private bool isUnitFocused;
	private float panningSpeed = 2.0f;
	private Vector3 temp;
	private Unit focusedUnit;


	//Temporary variables -- some of these might stay
	private float rightBound, leftBound, topBound, bottomBound;
	private Vector3 pos;


	// Use this for super initialization
	void Awake()
	{
		//HACK
		if (centerBg == null || centerBg == null)
		{
			centerBg = GameObject.Find ("BG2");
		}
	}
			
	// Use this for initialization
	void Start () 
	{
		//Setting the camera into ortographic mode
		Camera.main.orthographic = true;

		//Initializing isUnitFocused to false
		isUnitFocused = false;
		float widthBG = 38.4f;
		float heightBG = 10.24f;
		float vertExtent = camera.orthographicSize; 
		float horzExtent = camera.orthographicSize * Screen.width / Screen.height;
		leftBound = horzExtent - (widthBG / 2.0f);
		rightBound = (widthBG / 2.0f) - horzExtent;
		bottomBound = vertExtent - (heightBG / 2.0f);
		topBound = (heightBG / 2.0f) - vertExtent;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Making the camera move to the focused GameObject
		if(isUnitFocused == true)
		{
			//Lerp the camera to new position. 
			CameraFollowUnit();
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

		isUnitFocused = false;

		temp = gameObject.transform.position + mouseDestination;
		temp.x = Mathf.Clamp(temp.x, leftBound, rightBound);
		temp.y = Mathf.Clamp(temp.y, bottomBound, topBound);

		//This will Translate, or move, the camera's position to the mouse's position
		gameObject.transform.position = temp;


	}

	private void CameraFollowUnit()
	{
		newPosition = new Vector3 (focusedUnit.transform.position.x - 2.0f, focusedUnit.transform.position.y + 2.0f, -10.0f);
		newPosition.x = Mathf.Clamp (newPosition.x, leftBound, rightBound);
		newPosition.y = Mathf.Clamp (newPosition.y, bottomBound, rightBound);
		Camera.main.transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * camSpeed);
	}

	//Method -- UnitFocus
	//Parameters -- GameObject unitObj
	//Purpose -- UnitFocus will take in an argument of GameObject unitObj, this will tell the camera to then focus on that
	//GameObject. Essentially, the camera's transform will be changed to that of the transform of the GameObject.

	public void UnitFocus(Unit unitObj)
	{
		//Assigns the transform the unitObj to the camera
		newPosition = new Vector3 (unitObj.transform.position.x - 2.0f, unitObj.transform.position.y + 2.0f, -10.0f);
		newPosition.x = Mathf.Clamp (newPosition.x, leftBound, rightBound);
		newPosition.y = Mathf.Clamp (newPosition.y, bottomBound, rightBound);
		Camera.main.transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * camSpeed);
		isUnitFocused = true;
		focusedUnit = unitObj;
	}
}
