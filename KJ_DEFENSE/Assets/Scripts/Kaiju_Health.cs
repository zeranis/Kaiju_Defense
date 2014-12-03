using UnityEngine;
using System.Collections;

public class Kaiju_Health : MonoBehaviour 
{


	void Start () 
	{

	}
	

	void OnTriggerEnter2D(Collider2D bullet) 
	{
		if(bullet != null)
		{
			//Debug.Log("Hit");
			try 
			{
				bullet.GetComponent<BulletLifetime>().destroy();
				Instantiate(Resources.Load("Explosion"),bullet.transform.position,bullet.transform.rotation)	;
			} 
			catch (System.Exception ex) 
			{

			}
		}
	}
}
