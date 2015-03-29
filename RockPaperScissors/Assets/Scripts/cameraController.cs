/*
	Authored By: Nigel Martinez
	Purpose: Controls the movement and zoom of the camera.
*/

using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {
	/*
		Elements of code dervied from a tutorial by Ryan Nielson.
		http://nielson.io/2014/03/making-a-target-tracking-orthographic-camera-in-unity/
	*/
	
	private float boundingBoxPadding = 2f;
	private float minimumOrthographicSize = 5f;
	private float zoomSpeed = 20f;
	private Camera cam;
	private Transform[] targets;
	private int targetSize;
	
	//Boundaries and default camera location may differ between maps.
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
		findTargets ();
		Rect boundingBox = CalculateTargetsBoundingBox();
		transform.position = CalculateCameraPosition(boundingBox);
		cam.orthographicSize = CalculateOrthographicSize(boundingBox);
	}
	
	void findTargets() {
		Player subject;
		GameObject[] players;

		targets = new Transform[5];
		targets [0] = defaultCam.transform;
		targetSize = 0;
		
		players = GameObject.FindGameObjectsWithTag ("Player");
		
		foreach (GameObject player in players) {
			subject = (Player)player.gameObject.GetComponent(typeof(Player));

			if(subject.playerHealth != null) {
				if(!subject.playerHealth.isDead()) {
					targetSize++;
					targets[targetSize] = player.transform;
				}
			}
		}
	}
	
	/*
     * DESCR: Calculates an rectangle that contains the players.
     * PRE: NONE
     * POST: A rectangle with an area containing the players.
     */
	Rect CalculateTargetsBoundingBox()
	{
		float minX = Mathf.Infinity;
		float maxX = Mathf.NegativeInfinity;
		float minY = Mathf.Infinity;
		float maxY = Mathf.NegativeInfinity;
		
		//Find the dimensions of a box that will contain all the targets, a.k.a. players.
		//DefaultCam is among the targets to avoid any 'accessing empty object' errors.
		for (int i = 0; i <= targetSize; i++) {
			Vector3 position = targets[i].position;
			
			//If the player goes too high, adjust the value to the top boundary.
			if(position.y > topBounds.transform.position.y)
				maxY = topBounds.transform.position.y;
			else
				maxY = Mathf.Max(maxY, position.y);
			
			//Likewise for a player going too low.
			if(position.y < bottomBounds.transform.position.y)
				minY = bottomBounds.transform.position.y;
			else
				minY = Mathf.Min(minY, position.y);
			
			//Likewise for a player going too far to the left.
			if(position.x < leftBounds.transform.position.x)
				minX = leftBounds.transform.position.x;
			else
				minX = Mathf.Min(minX, position.x);
			
			//Liksewise for a player going too far to the right.
			if(position.x > rightBounds.transform.position.x)
				maxX = rightBounds.transform.position.x;
			else
				maxX = Mathf.Max(maxX, position.x);
			
			/* Nielson's orginal calculations, which did not account for boundaries.
			minX = Mathf.Min(minX, position.x);
			minY = Mathf.Min(minY, position.y);
			maxX = Mathf.Max(maxX, position.x);
			maxY = Mathf.Max(maxY, position.y);
			*/
		}
		
		return Rect.MinMaxRect(minX - boundingBoxPadding, maxY + boundingBoxPadding, maxX + boundingBoxPadding, minY - boundingBoxPadding);
	}
	
	/*
     * DESCR: Determines the location for the camera.
     * PRE: A rectangle with an area containing the players.
     * POST: A 3D vector for location of the camera.
     */
	Vector3 CalculateCameraPosition(Rect boundingBox)
	{
		Vector2 boundingBoxCenter = boundingBox.center;
		
		return new Vector3(boundingBoxCenter.x, boundingBoxCenter.y, cam.transform.position.z);
	}
	
	/*
     * DESCR: Determines a size for the camera.
     * PRE: A rectangle with an area containing the players.
     * POST: A float for the size of the camera.
     */
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
