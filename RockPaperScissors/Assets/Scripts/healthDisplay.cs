using UnityEngine;
using System.Collections;

public class healthDisplay : MonoBehaviour {
	Player p1, p2;

	// Use this for initialization
	void Start () {
		GameObject tmpGameObject;
		tmpGameObject = GameObject.Find ("Player1");
		p1 = (Player)tmpGameObject.GetComponent(typeof(Player));
		tmpGameObject = GameObject.Find ("Player2");
		p2 = (Player)tmpGameObject.GetComponent(typeof(Player));
	}

	// Update is called once per frame
	void Update () {

	}

	void OnGUI() {
		GUI.Label (new Rect(10, 10, 300, 300), p1.playerHealth.health.ToString());
		GUI.Label (new Rect(1200, 10, 300, 300), p2.playerHealth.health.ToString());
	}
}
