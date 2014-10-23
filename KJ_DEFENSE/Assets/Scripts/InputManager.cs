using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	//Declaring local variables
	
	//--Private Members
	private Vector3 mousePosition;
	private bool panningActive;
	private CameraManager cameraManager;
	private delegate void HandleAllInputs();
	private HandleAllInputs handleAllInputs;

	// Use this for initialization
	void Start () 
	{	
		panningActive = false;
		cameraManager = Camera.main.GetComponent<CameraManager> ();
		handleAllInputs += HandleUnitCameraFocus;
		handleAllInputs += HandleCameraPanning;
	}

	// Update is called once per frame
	void Update () 
	{
		handleAllInputs();
	}
	
	// Called after Update
	void LateUpdate()
	{
		
	}

	void HandleUnitCameraFocus()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{

		}
	}

	void HandleCameraPanning()
	{
		//Disables the mouse panning
		if (!Input.GetMouseButton(0)) 
		{
			panningActive = false;
		}
		//User is panning the camera
		else if(Input.GetMouseButtonDown(0))
		{
			//Get the origin of the mouse's position
			mousePosition = Input.mousePosition;
			panningActive = true;
		}
		
		//If the user wants to pan, camera will call CameraPanning, taking the position of the mouse
		//as an argument
		if(panningActive == true)
		{
			cameraManager.CameraPanning(mousePosition);
		}
		
		
	}
}
