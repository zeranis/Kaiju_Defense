using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Kaiju_Health : MonoBehaviour 
{
	// Action
	public Vector3 KaijuPosition;
	public float kaijuSpeed;
	// health
	public int maxHealth;   
	public  int currentHealth;

	// audio
	public List<AudioClip> listAudioClip ;

	Animator anim;   

	// this group of value use for cool down
	public float coolDownBetweenHit;
	public float dieCD;
	private bool onCD;
	IEnumerator coolDownDmg(){
		onCD = true;
		yield return new WaitForSeconds(coolDownBetweenHit);
		onCD = false;
	}

	//=====================================================================
	void Start () 
	{


		onCD  = false;
		anim = GetComponent <Animator> ();
		currentHealth = maxHealth;

		KaijuPosition = this.transform.position;
	}
	//=================Update every frame===========
	// Check if kaiju is half health or 0 health to play sound. and play animation.
	void Update(){


		KaijuPosition.x += -1.0f * kaijuSpeed * Time.deltaTime;;
		this.transform.position = KaijuPosition;
		//KaijuPosition = new Vector3 (speed.x * direction.x, speed.y * direction.y);
		
		if (currentHealth == (maxHealth/2)+1)//i dont know why i must be +1 to give the right value.
			playSound(0);
		else if (currentHealth == 0){
			anim.SetTrigger ("Die");
			playSound(1);
		}
		if (currentHealth == 0) {
			dieCD+= Time.deltaTime;
		//	Destroy (this.gameObject, 2.0f);
		//	Debug.Log("destroy");
				}
		if (currentHealth == 0 && dieCD >= 1f) {
			anim.SetTrigger ("New Bool");
			Destroy (this.gameObject, 4.0f);

				}

	}



	//=====================================================================
	// Calculate healthbar position
	private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
	{
		return (x-inMin)*(outMax-outMin)/ (inMax -inMin) + outMin;
	}
	//=====================================================================
	public void KaijuGetDmg(){
	
		if (!onCD && currentHealth>0) {
			StartCoroutine(coolDownDmg());
			currentHealth--;
	
		}
	}
	//=====================================================================
	// Play sound
	private void playSound(int clip)
	{
		audio.clip = listAudioClip[clip];
		audio.Play();
	}
	//=====================================================================
	// kaiju moving
	private void moving(float moveDirection)
	{
		KaijuPosition.x +=  moveDirection * kaijuSpeed * Time.deltaTime;;
		this.transform.position = KaijuPosition;
		Debug.Log("Kaiju pos" + KaijuPosition + this.transform.position);
//		this.gameObject.GetComponent <Transform> ().position
	}
}
