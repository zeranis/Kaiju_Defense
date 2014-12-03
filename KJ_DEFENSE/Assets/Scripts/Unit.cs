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
	public float timeBetweenAttack = 0.5f;
	public float timer;
	void Start()
	{
		//isFocused = true;
		anim = GetComponent<Animator>();
		position = this.transform.position;
//		rotation = this.transform.GetChild (0).transform.rotation;
	}

	// Update every frame, make sure destroy tank if health below 0
	void Update()
	{
		timer += Time.deltaTime; // record time since the last trigger

		if (tankHealth <= 0) 
		{
			Destroy(this,0.75f);
		}
	}
	//change the x cordinate of the unit with float value
	//parameter moveDirection<float>
	public void MoveFocusedUnit(float moveDirection)
	{
		position.x += moveDirection * objSpeed * Time.deltaTime;
		this.transform.position = position;
	}
	// change the angle of cannon with parameter
	//parameter angel<float>
	public void ChangeCannonAngle(float angle)
	{
		this.transform.GetChild(0).transform.Rotate(Vector3.forward * angle);
	}
	// set the animation to moving mode or idle mode
	//parameter move<bool>
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
	// collider, check if it hit the damage 
	void OnTriggerStay2D(Collider2D danger) 
	{

			
			if(danger != null)
			{

				if (danger.CompareTag("Danger2"))
				{
					Debug.Log("Hit");
					tankHealth--;
				}
				timer = 0f;
			}


	}
}
