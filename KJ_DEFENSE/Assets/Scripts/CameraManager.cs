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
	//private float centerX;
	//private float centerY;
	//private float offsetX;
	//private float offsetY;
	//private float aspectRatio;
	private bool isUnitFocused;
	private float panningSpeed = 2.0f;
	private bool stopPanning = false;
	private bool colliding = false;
	private Vector3 temp;
	private Unit focusedUnit;


	//Temporary variables -- some of these might stay
	private float rightBound, leftBound, topBound, bottomBound;
	private Vector3 pos;


	// Use this for super initialization
	void Awake()
	{
		//Initializes the position of the camera to the coordinate: 
		//(x,y,z) = (0, 20, camera's z-coordinates)
		//HACK
		if (centerBg == null || centerBg == null)
		{
			centerBg = GameObject.Find ("BG2");
		}

		//gameObject.transform.position = new Vector3(0, 3.5f, gameObject.transform.position.z);
		//cameraBounds = GameObject.Find("CameraBounds").GetComponent<BoxCollider>();
		//cameraColl = GameObject.Find("CameraBounds").GetComponent<Collision>();
	}
			
	// Use this for initialization
	void Start () 
	{
		//Setting the camera into ortographic mode
		Camera.main.orthographic = true;

		//Changing the camera's half-vertical size
		//Camera.main.orthographicSize = 4.0f; 

		//Initializing unitFocused to zero
		//unitFocused = 0;

		//Initializing isUnitFocused to false
		isUnitFocused = false;

		//float vertExtent = Camera.main.camera.orthographicSize;
		//float horzExtend = vertExtent * Screen.width/Screen.height;
		//spriteBounds = GameObject.Find("BG1").GetComponentInChildren<SpriteRenderer>();
		//leftBound = (float)(horzExtend - spriteBounds.sprite.bounds.size.x/2.0f);
		//rightBound = (float)(spriteBounds.sprite.bounds.size.x/2.0f - horzExtend);
		//bottomBound = (float)(vertExtent - spriteBounds.sprite.bounds.size.y/2.0f);
		//topBound = (float)(spriteBounds.sprite.bounds.size.y/2.0f - vertExtent);

		//Debug.Log(spriteBounds.bounds.size.x);
		//Debug.Log(spriteBounds.bounds.size.y);

		//Debug.Log(leftBound);
		//Debug.Log (rightBound);
		//Debug.Log (topBound);
		//Debug.Log(bottomBound);
		float widthBG = 38.4f;
		Debug.Log ("width: " + widthBG);
		float heightBG = 10.24f;
		Debug.Log ("height: " + heightBG);
		float vertExtent = camera.orthographicSize; 
		Debug.Log ("vertEx: " + vertExtent);
		float horzExtent = camera.orthographicSize * Screen.width / Screen.height;
		Debug.Log ("horzEx: " + horzExtent);
		float minX = horzExtent - (widthBG / 2.0f);
		float maxX = (widthBG / 2.0f) - horzExtent;
		Debug.Log ("MinX: " + minX);
		Debug.Log("MaxX: " + maxX);
		float minY = vertExtent - (heightBG / 2.0f);
		float maxY = (heightBG / 2.0f) - vertExtent;
		Debug.Log ("MinY" + minY);
		Debug.Log ("MaxY" + maxY);
		leftBound = minX;
		Debug.Log ("leftBound: " + leftBound);
		rightBound = maxX;
		Debug.Log ("rightBound: " + rightBound);
		bottomBound = minY;
		Debug.Log ("bottomBound: " + bottomBound);
		topBound = maxY;
		Debug.Log ("topBound: " + topBound);
		//leftBound = leftmostBg.transform.position.x + (2 * (camera.orthographicSize - 4.5f));
		//rightBound = rightmostBg.transform.position.x + (2 * (4.5f - camera.orthographicSize));
		//topBound = leftmostBg.transform.position.y + (heightBG - camera.orthographicSize);
		//bottomBound = leftmostBg.transform.position.y - (heightBG - camera.orthographicSize);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Making the camera move to the focused GameObject
		if(isUnitFocused == true)
		{
			//Lerp the camera to new position. 
			Debug.Log("Lerping");
			CameraFollowUnit();
			//Camera.main.transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * camSpeed);
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

		Debug.Log(mouseDestination);
		temp = gameObject.transform.position + mouseDestination;
		temp.x = Mathf.Clamp(temp.x, leftBound, rightBound);
		temp.y = Mathf.Clamp(temp.y, bottomBound, topBound);

		//This will Translate, or move, the camera's position to the mouse's position
		//Camera.main.transform.Translate(mouseDestination, Space.Self);
		gameObject.transform.position = temp;


	}

	private void CameraFollowUnit()
	{
		newPosition = new Vector3 (focusedUnit.transform.position.x - 2.0f, focusedUnit.transform.position.y + 2.0f, -10.0f);
		newPosition.x = Mathf.Clamp (newPosition.x, leftBound, rightBound);
		newPosition.y = Mathf.Clamp (newPosition.y, bottomBound, rightBound);
		Camera.main.transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * camSpeed);
		//gameObject.transform.position = newPosition;
	}
//	void OnTriggerExit(Collider cameraBounds)
//	{
//		Debug.Log ("OnTriggerExit");
//		//panningSpeed *= -1.0f;
//		stopPanning = true;
//	
//	}
//
//	void OnTriggerEnter(Collider cameraBounds)
//	{
//		Debug.Log ("OnTriggerEnter");
//		stopPanning = false;
//	}

	public void BoundCamera(float width, float height)
	{
		
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
