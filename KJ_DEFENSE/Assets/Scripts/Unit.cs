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
	
	//Public members:
	public Texture playerHealthTexture; //Player Life
	public float screenPosX; //Controls screen position of texture
	public float screenPosY; //...
	public int iconSizeX = 10; //Controls icon size on screen
	public int iconSizeY = 10; //...
	public int tankHealth = 10; //Starting armor
	public float objSpeed;

	void FixedUpdate()
	{
		//Declaring local variables
		float move = 0;
					
		if(Input.GetKey("a")/*Down(KeyCode.A)*/)
		{	
			move = -1.0f;
			rigidbody2D.velocity = new Vector2(move * objSpeed, rigidbody2D.velocity.y);
		}
		
		if(Input.GetKey("d")/*Down(KeyCode.D)*/)
		{
			move = 1.0f;
			rigidbody2D.velocity = new Vector2(move * objSpeed, rigidbody2D.velocity.y);
			
		}
	}
}
