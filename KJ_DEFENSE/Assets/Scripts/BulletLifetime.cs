using UnityEngine;
using System.Collections;

public class BulletLifetime : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		Destroy (this.gameObject, 4.0f);
	}
	public void destroy()
	{
		Destroy (this.gameObject, 0.0f);
	}
	void OnTriggerEnter2D(Collider2D bullet) 
	{
		if(bullet.name == "Kaiju_Total")
		{
			
			bullet.GetComponent<Kaiju_Health>().KaijuGetDmg();
			Instantiate(Resources.Load("Explosion"),this.transform.position,this.transform.rotation)	;
			this.destroy();

		}
	}

}
