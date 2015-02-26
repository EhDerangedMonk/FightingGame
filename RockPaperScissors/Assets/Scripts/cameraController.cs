// cameraController.cs
// Nigel Martinez
// GMMA Studios
//February 20th, 2015

using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {
	//Elements of code inspired by a tutorial by Ryan Nielson.
	//http://nielson.io/2014/03/making-a-target-tracking-orthographic-camera-in-unity/

	private float boundingBoxPadding = 2f;
	private float minimumOrthographicSize = 5f;
	private float zoomSpeed = 20f;
	private Camera cam;

	//Boundaries and default camera location may differ between maps.
	//[SerializeField] 
	public Transform[] targets;
	//public GameObject cameraBounds;
	public GameObject defaultCam;
	public GameObject topBounds;
	public GameObject leftBounds;
	public GameObject rightBounds;
	public GameObject bottomBounds;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();
		cam.orthographic = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate()
	{
		Rect boundingBox = CalculateTargetsBoundingBox();
		transform.position = CalculateCameraPosition(boundingBox);
		cam.orthographicSize = CalculateOrthographicSize(boundingBox);
	}

	// Calculates a bounding box that contains all the targets, returning a Rect containing all targets.
	Rect CalculateTargetsBoundingBox()
	{
		float minX = Mathf.Infinity;
		float maxX = Mathf.NegativeInfinity;
		float minY = Mathf.Infinity;
		float maxY = Mathf.NegativeInfinity;

		//Find the dimensions of a box that will contain all the targets, a.k.a. players.
		//DefaultCam is among the targets to avoid any 'accessing empty object' errors.
		foreach (Transform target in targets) {
			Vector3 position = target.position;

			if(target.position.y > topBounds.transform.position.y)
				maxY = topBounds.transform.position.y;
			else
				maxY = Mathf.Max(maxY, position.y);

			if(target.position.y < bottomBounds.transform.position.y)
				minY = bottomBounds.transform.position.y;
			else
				minY = Mathf.Min(minY, position.y);

			if(target.position.x < leftBounds.transform.position.x)
				minX = leftBounds.transform.position.x;
			else
				minX = Mathf.Min(minX, position.x);

			if(target.position.x > rightBounds.transform.position.x)
				maxX = rightBounds.transform.position.x;
			else
				maxX = Mathf.Max(maxX, position.x);

			/*
			minX = Mathf.Min(minX, position.x);
			minY = Mathf.Min(minY, position.y);
			maxX = Mathf.Max(maxX, position.x);
			maxY = Mathf.Max(maxY, position.y);
			*/
		}
		
		return Rect.MinMaxRect(minX - boundingBoxPadding, maxY + boundingBoxPadding, maxX + boundingBoxPadding, minY - boundingBoxPadding);
	}

	// Calculates a camera position given the a bounding box containing all the targets.
	Vector3 CalculateCameraPosition(Rect boundingBox)
	{
		Vector2 boundingBoxCenter = boundingBox.center;
		
		return new Vector3(boundingBoxCenter.x, boundingBoxCenter.y, cam.transform.position.z);
	}

	// Calculates a new orthographic size for the camera based on the target bounding box.
	float CalculateOrthographicSize(Rect boundingBox)
	{
		float orthographicSize = cam.orthographicSize;
		Vector3 topRight = new Vector3(boundingBox.x + boundingBox.width, boundingBox.y, 0f);
		Vector3 topRightAsViewport = cam.WorldToViewportPoint(topRight);
		
		if (topRightAsViewport.x >= topRightAsViewport.y)
			orthographicSize = Mathf.Abs(boundingBox.width) / cam.aspect / 2f;
		else
			orthographicSize = Mathf.Abs(boundingBox.height) / 2f;
		
		return Mathf.Clamp(Mathf.Lerp(cam.orthographicSize, orthographicSize, Time.deltaTime * zoomSpeed), minimumOrthographicSize, Mathf.Infinity);
	}

}
