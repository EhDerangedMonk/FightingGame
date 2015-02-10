using UnityEngine;
using System.Collections;

public class rockBehaviour : MonoBehaviour {
	public float lifeTime = 8f; //TEMP: Amount of time before this object is destroyed.
	//TODO: Have rock destroyed when it hits either players or other rocks.


	// Use this for initialization
	void Start () {
		Invoke ("Destroy", lifeTime); //TEMP: Destroy this object after some time.
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Destroy () {
		Destroy (this.gameObject);
	}
}
