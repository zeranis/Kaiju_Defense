using UnityEngine;
using System.Collections;

public class BulletLifetime : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, 4.0f);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
