using UnityEngine;
using System.Collections;

public class Kaiju_Health : MonoBehaviour 
{
	//CircleCollider2D headCollider;
	// Use this for initialization
	//BulletLifetime bullet1;
	GameObject bullet;

	void Start () 
	{
		//headCollider = GetComponent <CircleCollider2D> ();

		bullet = GameObject.FindGameObjectWithTag ("Bullet");
	}
	
	// Update is called once per frame
	//void Update () {

	//}
	void OnTriggerEnter2D(Collider2D bullet) 
	{
		if(bullet != null)
		{
			Debug.Log("Hit");
			bullet.GetComponent<BulletLifetime>().destroy();
			//Destroy(bullet.gameObject);
		}
	}
}
