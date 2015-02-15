using UnityEngine;
using System.Collections;

public class ScencChangetest : MonoBehaviour {

	// Use this for initialization
	public void Start () {

	}
	
	// Update is called once per frame
	public void WorldMap () {
		Application.LoadLevel ("WorldMap");
	}
	public void Credit () {
		Application.LoadLevel ("Credits");
	}
	public void ToGame()
	{
		Application.LoadLevel ("temp");
	}
	public void MainMenu()
	{
		Application.LoadLevel ("Menu-Scene");
	}
}
