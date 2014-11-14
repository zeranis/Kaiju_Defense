using UnityEngine;
using System.Collections;

public enum Unit_Type
{
	TANK,
	ARTILLERY,
	INFANTRY
}

public class Unit : MonoBehaviour 
{
	//Declaring local variables:
	public Unit_Type type;
	Animator anim;

	
	//Public members:
	//public Texture playerHealthTexture; //Player Life
	//public float screenPosX; //Controls screen position of texture
	//public float screenPosY; //...
	//public int iconSizeX = 10; //Controls icon size on screen
	//public int iconSizeY = 10; //...
	public int tankHealth = 10; //Starting armor
	public float objSpeed;
	private Vector3 position;
	private Quaternion rotation;
	
	void Start()
	{
		//isFocused = true;
		anim = GetComponent<Animator>();
		position = this.transform.position;
//		rotation = this.transform.GetChild (0).transform.rotation;
	}
	void Update()
	{
		if (tankHealth <= 0) {
			Destroy(this,0.75f);
				}
		}
	public void MoveFocusedUnit(float moveDirection)
	{
		position.x += moveDirection * objSpeed * Time.deltaTime;
		this.transform.position = position;
	}

	public void ChangeCannonAngle(float angle)
	{
		//Vector3 world_pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		//this.transform.GetChild (0).LookAt (world_pos);
		//rotation.z +=  1.0f * Time.deltaTime;
		this.transform.GetChild(0).transform.Rotate(Vector3.forward * angle);
		//Debug.Log (this.transform.GetChild (0).transform.rotation);
	}
	public void setAnimation(bool move)
	{
		if(move == true)
		{
			anim.SetFloat("Speed", Mathf.Abs(objSpeed));
		}
		else
		{
			anim.SetFloat("Speed", Mathf.Abs(0.0f));
		}
	}
}
