// ledgeBehaviour.cs
// Nigel Martinez
// GMMA Studios
//February 18th, 2015

using UnityEngine;
using System.Collections;

public class ledgeBehaviour : MonoBehaviour {

	public Transform hangCheck;
	public LayerMask players;
	private bool hang = false;
	private float hangRadius = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		hang = Physics2D.OverlapCircle (hangCheck.position, hangRadius, players);

		// if (hang)
		// 	Debug.Log ("Player should be hanging!");
		// else
		// 	Debug.Log ("Player isn't hanging!");
	}
}
