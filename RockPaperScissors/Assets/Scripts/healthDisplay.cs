/*****************************************************************************\
*	Author:		Nigel Martinez/Jerrit Anderson/Braden Gibson				  *
*	Purpose:	Attaches to main camera and displays the health of P1 and P2. *
\*****************************************************************************/
using UnityEngine;
using System.Collections;

public class healthDisplay : MonoBehaviour {

	private InitializeStorage gameSettings;
    private Player p1, p2, p3, p4;
	private GUIStyle labelFontSize; // Formats the labels
	private Vector3 P1TagPoint, P2TagPoint, P3TagPoint, P4TagPoint; // The top left locations of each player on the screen (in pixels)
	private Vector3 P1HealthPoint, P2HealthPoint, P3HealthPoint, P4HealthPoint; // Where to draw the health indicators.
    public Texture P1Tag, P2Tag, P3Tag, P4Tag; // Floats above the players' heads
    public Texture Health_100to81, Health_80to61, Health_60to41, Health_40to21, Health_20to1; // Indicate the players' health levels
	private float xTagOffset, yTagOffset; // How much the tags will be offset from the players' positions. 
	public float xHealthOffset, yHealthOffset; // How much the health indicators will be offset from the players' positions.
	private bool P3Playing, P4Playing; // Determine if these players are playing
	
    // Use this for initialization
    void Start () {
		gameSettings = FindObjectOfType<InitializeStorage> ();
        
        labelFontSize = new GUIStyle();
        labelFontSize.fontSize = 30;
        labelFontSize.normal.textColor = Color.white;
        
        xHealthOffset = -0.62f;
        yHealthOffset = 2.14f;
        xTagOffset = -0.75f;
        yTagOffset = 3.3f; // These are just where it looks good, they can be modified
        
        // Determine if P3 and P4 are playing
		InitializeStorage settings = (InitializeStorage) GameObject.Find("VariableStorage").GetComponent(typeof(InitializeStorage));
		if ((int)settings.P3Controller == 0)
			P3Playing = false;
		else
			P3Playing = true;
		if ((int)settings.P4Controller == 0)
			P4Playing = false;
		else
			P4Playing = true;
    }

    // Update is called once per frame
    void Update () {
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		
		//Find players.
		if (players != null) {
			foreach (GameObject player in players) {
				Player subject = (Player)player.gameObject.GetComponent (typeof(Player));
				
				if(subject.layout == (int)gameSettings.P1Controller)
					p1 = subject;
				else if(subject.layout == (int)gameSettings.P2Controller)
					p2 = subject;
				else if (subject.layout == (int)gameSettings.P3Controller)
					p3 = subject;
				else if (subject.layout == (int)gameSettings.P4Controller)
					p4 = subject;
			}
		}

    	// Get each player's top left screen position
    	Vector3 P1Location = p1.transform.position;
    	Vector3 P2Location = p2.transform.position;
    	Vector3 P3Location, P4Location = new Vector3(0f, 0f, 0f);
    	if (P3Playing)
    	{
			P3Location = p3.transform.position;
			P3TagPoint = Camera.main.WorldToScreenPoint(new Vector3(P3Location.x+xTagOffset, P3Location.y+yTagOffset, P3Location.z));
			P3HealthPoint = Camera.main.WorldToScreenPoint(new Vector3(P3Location.x+xHealthOffset, P3Location.y+yHealthOffset, P3Location.z));
    	}
    	if (P4Playing)
    	{
			P4Location = p4.transform.position;
			P4TagPoint = Camera.main.WorldToScreenPoint(new Vector3(P4Location.x+xTagOffset, P4Location.y+yTagOffset, P4Location.z));
			P4HealthPoint = Camera.main.WorldToScreenPoint(new Vector3(P4Location.x+xHealthOffset, P4Location.y+yHealthOffset, P4Location.z));
		}
    	
		P1TagPoint = Camera.main.WorldToScreenPoint(new Vector3(P1Location.x+xTagOffset, P1Location.y+yTagOffset, P1Location.z));
		P2TagPoint = Camera.main.WorldToScreenPoint(new Vector3(P2Location.x+xTagOffset, P2Location.y+yTagOffset, P2Location.z));
		
		P1HealthPoint = Camera.main.WorldToScreenPoint(new Vector3(P1Location.x+xHealthOffset, P1Location.y+yHealthOffset, P1Location.z));
		P2HealthPoint = Camera.main.WorldToScreenPoint(new Vector3(P2Location.x+xHealthOffset, P2Location.y+yHealthOffset, P2Location.z));
    }

    void OnGUI() {
		//GUI.Label (new Rect(P1TagPoint.x, Screen.height-P1TagPoint.y-40, 300, 300), p1.playerHealth.getHealth().ToString(), labelFontSize);
        GUI.DrawTexture (new Rect(P1TagPoint.x, Screen.height-P1TagPoint.y, P1Tag.width/4, P1Tag.height/4), P1Tag);GUI.DrawTexture (new Rect(P1TagPoint.x, Screen.height-P1TagPoint.y, P1Tag.width/4, P1Tag.height/4), P1Tag);
        // Determine which health indicator texture to draw based on the player's health
		if (801 <= p1.playerHealth.getHealth() && p1.playerHealth.getHealth() <= 1000)
			GUI.DrawTexture (new Rect(P1HealthPoint.x, Screen.height-P1HealthPoint.y, Health_100to81.width/3, Health_100to81.height/3), Health_100to81);
		else if (601 <= p1.playerHealth.getHealth() && p1.playerHealth.getHealth() <=800)
			GUI.DrawTexture (new Rect(P1HealthPoint.x, Screen.height-P1HealthPoint.y, Health_100to81.width/3, Health_100to81.height/3), Health_80to61);
		else if (401 <= p1.playerHealth.getHealth() && p1.playerHealth.getHealth() <=600)
			GUI.DrawTexture (new Rect(P1HealthPoint.x, Screen.height-P1HealthPoint.y, Health_100to81.width/3, Health_100to81.height/3), Health_60to41);
		else if (201 <= p1.playerHealth.getHealth() && p1.playerHealth.getHealth() <=400)
			GUI.DrawTexture (new Rect(P1HealthPoint.x, Screen.height-P1HealthPoint.y, Health_100to81.width/3, Health_100to81.height/3), Health_40to21);
		else if (1 <= p1.playerHealth.getHealth() && p1.playerHealth.getHealth() <=200)
			GUI.DrawTexture (new Rect(P1HealthPoint.x, Screen.height-P1HealthPoint.y, Health_100to81.width/3, Health_100to81.height/3), Health_20to1);	
		
		//GUI.Label (new Rect(P2TagPoint.x, Screen.height-P2TagPoint.y-40, 300, 300), p2.playerHealth.getHealth().ToString(), labelFontSize);
        GUI.DrawTexture (new Rect(P2TagPoint.x, Screen.height-P2TagPoint.y, P2Tag.width/4, P2Tag.height/4), P2Tag);
		// Determine which health indicator texture to draw based on the player's health
		if (801 <= p2.playerHealth.getHealth() && p2.playerHealth.getHealth() <= 1000)
			GUI.DrawTexture (new Rect(P2HealthPoint.x, Screen.height-P2HealthPoint.y, Health_100to81.width/3, Health_100to81.height/3), Health_100to81);
		else if (601 <= p2.playerHealth.getHealth() && p2.playerHealth.getHealth() <=800)
			GUI.DrawTexture (new Rect(P2HealthPoint.x, Screen.height-P2HealthPoint.y, Health_100to81.width/3, Health_100to81.height/3), Health_80to61);
		else if (401 <= p2.playerHealth.getHealth() && p2.playerHealth.getHealth() <=600)
			GUI.DrawTexture (new Rect(P2HealthPoint.x, Screen.height-P2HealthPoint.y, Health_100to81.width/3, Health_100to81.height/3), Health_60to41);
		else if (201 <= p2.playerHealth.getHealth() && p2.playerHealth.getHealth() <=400)
			GUI.DrawTexture (new Rect(P2HealthPoint.x, Screen.height-P2HealthPoint.y, Health_100to81.width/3, Health_100to81.height/3), Health_40to21);
		else if (1 <= p2.playerHealth.getHealth() && p2.playerHealth.getHealth() <=200)
			GUI.DrawTexture (new Rect(P2HealthPoint.x, Screen.height-P2HealthPoint.y, Health_100to81.width/3, Health_100to81.height/3), Health_20to1);
		
		if (P3Playing)
		{
			//GUI.Label (new Rect(P3TagPoint.x, Screen.height-P3TagPoint.y-40, 300, 300), p3.playerHealth.getHealth().ToString(), labelFontSize);
			GUI.DrawTexture (new Rect(P3TagPoint.x, Screen.height-P3TagPoint.y, P3Tag.width/4, P3Tag.height/4), P3Tag);
			// Determine which health indicator texture to draw based on the player's health
			if (801 <= p3.playerHealth.getHealth() && p3.playerHealth.getHealth() <= 1000)
				GUI.DrawTexture (new Rect(P3HealthPoint.x, Screen.height-P3HealthPoint.y, Health_100to81.width/3, Health_100to81.height/3), Health_100to81);
			else if (601 <= p3.playerHealth.getHealth() && p3.playerHealth.getHealth() <=800)
				GUI.DrawTexture (new Rect(P3HealthPoint.x, Screen.height-P3HealthPoint.y, Health_100to81.width/3, Health_100to81.height/3), Health_80to61);
			else if (401 <= p3.playerHealth.getHealth() && p3.playerHealth.getHealth() <=600)
				GUI.DrawTexture (new Rect(P3HealthPoint.x, Screen.height-P3HealthPoint.y, Health_100to81.width/3, Health_100to81.height/3), Health_60to41);
			else if (201 <= p3.playerHealth.getHealth() && p3.playerHealth.getHealth() <=400)
				GUI.DrawTexture (new Rect(P3HealthPoint.x, Screen.height-P3HealthPoint.y, Health_100to81.width/3, Health_100to81.height/3), Health_40to21);
			else if (1 <= p3.playerHealth.getHealth() && p3.playerHealth.getHealth() <=200)
				GUI.DrawTexture (new Rect(P3HealthPoint.x, Screen.height-P3HealthPoint.y, Health_100to81.width/3, Health_100to81.height/3), Health_20to1);
		}
		
		if (P4Playing)
		{
			//GUI.Label (new Rect(P4TagPoint.x, Screen.height-P4TagPoint.y-40, 300, 300), p4.playerHealth.getHealth().ToString(), labelFontSize);
			GUI.DrawTexture (new Rect(P4TagPoint.x, Screen.height-P4TagPoint.y, P4Tag.width/4, P4Tag.height/4), P4Tag);
			// Determine which health indicator texture to draw based on the player's health
			if (801 <= p4.playerHealth.getHealth() && p4.playerHealth.getHealth() <= 1000)
				GUI.DrawTexture (new Rect(P4HealthPoint.x, Screen.height-P4HealthPoint.y, Health_100to81.width/3, Health_100to81.height/3), Health_100to81);
			else if (601 <= p4.playerHealth.getHealth() && p4.playerHealth.getHealth() <=800)
				GUI.DrawTexture (new Rect(P4HealthPoint.x, Screen.height-P4HealthPoint.y, Health_100to81.width/3, Health_100to81.height/3), Health_80to61);
			else if (401 <= p4.playerHealth.getHealth() && p4.playerHealth.getHealth() <=600)
				GUI.DrawTexture (new Rect(P4HealthPoint.x, Screen.height-P4HealthPoint.y, Health_100to81.width/3, Health_100to81.height/3), Health_60to41);
			else if (201 <= p4.playerHealth.getHealth() && p4.playerHealth.getHealth() <=400)
				GUI.DrawTexture (new Rect(P4HealthPoint.x, Screen.height-P4HealthPoint.y, Health_100to81.width/3, Health_100to81.height/3), Health_40to21);
			else if (1 <= p4.playerHealth.getHealth() && p4.playerHealth.getHealth() <=200)
				GUI.DrawTexture (new Rect(P4HealthPoint.x, Screen.height-P4HealthPoint.y, Health_100to81.width/3, Health_100to81.height/3), Health_20to1);
		}
	}
}
