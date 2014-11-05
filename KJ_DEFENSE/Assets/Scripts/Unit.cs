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
	//public int tankHealth = 10; //Starting armor
	public float objSpeed;
	private Vector3 position;
	
	void Start()
	{
		//isFocused = true;
		anim = GetComponent<Animator>();
		position = this.transform.position;
	}

	public void MoveFocusedUnit(float moveDirection)
	{
		position.x += moveDirection * objSpeed * Time.deltaTime;
		this.transform.position = position;
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
