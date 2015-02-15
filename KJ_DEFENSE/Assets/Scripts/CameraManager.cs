using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour 
{
	//Declaring local variables:

	//--Public members
	public float camSpeed;
	public GameObject centerBg;
	public GameManager gameManager;
	//private GameManager gameManager;
	//--Private members
	private Vector3 newPosition;
	private float panningSpeed = 2.0f;
//	private float zoomingSpeed = 2.0f;
	private Vector3 temp;
	public Unit focusedUnit;

	public bool camMoved=false;// checking camera movement.


	//Temporary variables -- some of these might stay
	private float rightBound, leftBound, topBound, bottomBound;
	private Vector3 pos;
	//location of game platform
	//private Vector3 gameloc= new Vector3(5.7211f, 0f, -5f);//added to switch cam.
	//private Vector3 mapLoc= new Vector3(0.4f,-17.5f, -5f);//added to switch cam
	//8public Kaiju_Health Kh;//useless 
	//public bool MovetoMap=false; // move cam to world map?
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
		float widthBG = 38.4f;
		float heightBG = 10.24f;
		float vertExtent = camera.orthographicSize; 
		float horzExtent = camera.orthographicSize * Screen.width / Screen.height;
		leftBound = horzExtent - (widthBG / 2.0f);
		rightBound = (widthBG / 2.0f) - horzExtent;
		bottomBound = vertExtent - (heightBG / 2.0f);
		topBound = (heightBG / 2.0f) - vertExtent;
		//gameManager = gameObject.GetComponent<GameManager>();
		//focusedUnit = gameManager.GetFocusedUnit().GetComponent<Unit>();

	}
	
	// Update is called once per frame
	void Update () 
	{
			if (gameManager.listOfUnits.Count != 0) 
			{
			//Lerp the camera to new position. 
			CameraFollowUnit ();
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

		temp = gameObject.transform.position + mouseDestination;
		temp.x = Mathf.Clamp(temp.x, leftBound, rightBound);
		temp.y = Mathf.Clamp(temp.y, bottomBound, topBound);

		//This will Translate, or move, the camera's position to the mouse's position
		gameObject.transform.position = temp;


	}
	//Method -- CameraZooming
	//Parameters -- value
	//Purpose -- add new value to orthogragive size .
	public void CameraZooming(float value)
	{
		//Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mousePosOrigin);
		//Vector3 mov = pos.y * zoomingSpeed * transform.forward;
		//transform.Translate(mov, Space.World);
		if (camera.orthographicSize < 5 && value > 0)
		{
			camera.orthographicSize += value;
		}
		if (camera.orthographicSize > 2.4 && value < 0)
		{
			camera.orthographicSize += value;
		}
	}

	private void CameraFollowUnit()
	{
		newPosition = new Vector3 (focusedUnit.transform.position.x + 2.0f, focusedUnit.transform.position.y + 2.0f, -10.0f);
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
		newPosition = new Vector3 (unitObj.transform.position.x - 2.5f, unitObj.transform.position.y + 2.0f, -10.0f);
		newPosition.x = Mathf.Clamp (newPosition.x, leftBound, rightBound);
		newPosition.y = Mathf.Clamp (newPosition.y, bottomBound, rightBound);
		Camera.main.transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * camSpeed);
		focusedUnit = unitObj;
	}
}
