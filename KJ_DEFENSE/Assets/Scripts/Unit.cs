using UnityEngine;
using UnityEngine.UI;
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
	public  int currentHealth;
	private int CurrentHealth 
	{
		get{return currentHealth;}
		set{currentHealth = value;
			}
		}
	public float coolDown;
	private bool onCD;
	IEnumerator coolDownDmg(){
		onCD = true;
		yield return new WaitForSeconds(coolDown);
		onCD = false;
	}
	//Public members:
	//public Texture playerHealthTexture; //Player Life
	//public float screenPosX; //Controls screen position of texture
	//public float screenPosY; //...
	//public int iconSizeX = 10; //Controls icon size on screen
	//public int iconSizeY = 10; //...
	
	public float objSpeed;
	private Vector3 position;
	private Quaternion rotation;
	public float timeBetweenAttack = 0.5f;
	public float timer;
	//=====================================================================
	void Start()
	{
		//isFocused = true;
		anim = GetComponent<Animator>();
		position = this.transform.position;
		
		onCD  = false;
		

//		rotation = this.transform.GetChild (0).transform.rotation;
	}

	//=====================================================================
	// Update every frame, make sure destroy tank if health below 0
	void Update()
	{
		timer += Time.deltaTime; // record time since the last trigger

		if (currentHealth <= 0) 
		{
			Destroy (this.gameObject, 0.05f);
		}

	}

	//=====================================================================
	//change the x cordinate of the unit with float value
	//parameter moveDirection<float>
	public void MoveFocusedUnit(float moveDirection)
	{
		position.x += moveDirection * objSpeed * Time.deltaTime;
		this.transform.position = position;
	}

	//=====================================================================
	// change the angle of cannon with parameter
	//parameter angel<float>
	public void ChangeCannonAngle(float angle)
	{
		this.transform.GetChild(0).transform.Rotate(Vector3.forward * angle);
	}

	//=====================================================================
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
	
	//=====================================================================
	// collider, check if it hit the damage 
	void OnTriggerStay2D(Collider2D other )
	{
		if(other.CompareTag("Danger")  )
		{
			if (!onCD && currentHealth>0) {
				StartCoroutine(coolDownDmg());
				CurrentHealth--;
			}
		}
	}

}
