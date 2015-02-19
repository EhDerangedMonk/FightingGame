﻿using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {
	//Elements of code inspired by a tutorial by Ryan Nielson.
	//http://nielson.io/2014/03/making-a-target-tracking-orthographic-camera-in-unity/

	//In this case, the targets are players.
	[SerializeField] 
	Transform[] targets;
	
	[SerializeField] 
	float boundingBoxPadding = 2f;
	
	[SerializeField]
	float minimumOrthographicSize = 5f;
	
	[SerializeField]
	float zoomSpeed = 20f;
	
	Camera camera;

	public GameObject cameraBounds;
	public GameObject defaultCam;

	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera>();
		camera.orthographic = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate()
	{
		Rect boundingBox = CalculateTargetsBoundingBox();
		transform.position = CalculateCameraPosition(boundingBox);
		camera.orthographicSize = CalculateOrthographicSize(boundingBox);
	}

	// Calculates a bounding box that contains all the targets, returning a Rect containing all targets.
	Rect CalculateTargetsBoundingBox()
	{
		float minX = Mathf.Infinity;
		float maxX = Mathf.NegativeInfinity;
		float minY = Mathf.Infinity;
		float maxY = Mathf.NegativeInfinity;
		
		foreach (Transform target in targets) {
			Vector3 position = target.position;

			if(target.position.y > cameraBounds.transform.position.y) {
				minX = Mathf.Min(minX, position.x);
				minY = Mathf.Min(minY, position.y);
				maxX = Mathf.Max(maxX, position.x);
				maxY = Mathf.Max(maxY, position.y);
			}

			else {
				minX = Mathf.Min(minX, position.x);
				minY = defaultCam.transform.position.y;
				minX = Mathf.Max(maxX, position.x);
				maxY = Mathf.Max(maxY, position.y);
			}
		}
		
		return Rect.MinMaxRect(minX - boundingBoxPadding, maxY + boundingBoxPadding, maxX + boundingBoxPadding, minY - boundingBoxPadding);
	}

	// Calculates a camera position given the a bounding box containing all the targets.
	Vector3 CalculateCameraPosition(Rect boundingBox)
	{
		Vector2 boundingBoxCenter = boundingBox.center;
		
		return new Vector3(boundingBoxCenter.x, boundingBoxCenter.y, camera.transform.position.z);
	}

	// Calculates a new orthographic size for the camera based on the target bounding box.
	float CalculateOrthographicSize(Rect boundingBox)
	{
		float orthographicSize = camera.orthographicSize;
		Vector3 topRight = new Vector3(boundingBox.x + boundingBox.width, boundingBox.y, 0f);
		Vector3 topRightAsViewport = camera.WorldToViewportPoint(topRight);
		
		if (topRightAsViewport.x >= topRightAsViewport.y)
			orthographicSize = Mathf.Abs(boundingBox.width) / camera.aspect / 2f;
		else
			orthographicSize = Mathf.Abs(boundingBox.height) / 2f;
		
		return Mathf.Clamp(Mathf.Lerp(camera.orthographicSize, orthographicSize, Time.deltaTime * zoomSpeed), minimumOrthographicSize, Mathf.Infinity);
	}

}