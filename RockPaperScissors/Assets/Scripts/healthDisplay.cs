﻿/*****************************************************************************\
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
    public Texture Health_100to81, Health_80to61, Health_60to41, Health_40to21, Health_20to1, Health_Blank; // Indicate the players' health levels
	public float xRightTagOffset, xLeftTagOffset, yTagOffset; // How much the tags will be offset from the players' positions. 
	public float xRightHealthOffset, xLeftHealthOffset, yHealthOffset; // How much the health indicators will be offset from the players' positions.
	private bool P3Playing, P4Playing; // Determine if these players are playing
	
    // Use this for initialization
    void Start () {
		gameSettings = FindObjectOfType<InitializeStorage> ();
        
        labelFontSize = new GUIStyle();
        labelFontSize.fontSize = 30;
        labelFontSize.normal.textColor = Color.white;
        
        xRightHealthOffset = -0.62f;
        xLeftHealthOffset = -0.32f;
        yHealthOffset = 2.14f;
        xRightTagOffset = -0.75f;
        xLeftTagOffset = -0.43f;
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
    	Vector3 P1Scale = p1.transform.localScale;
    	Vector3 P2Location = p2.transform.position;
    	Vector3 P2Scale = p2.transform.localScale;
    	float P1xTagOffset, P2xTagOffset, P3xTagOffset, P4xTagOffset, P1xHealthOffset, P2xHealthOffset, P3xHealthOffset, P4xHealthOffset;
    	Vector3 P3Location, P3Scale, P4Location, P4Scale = new Vector3(0f, 0f, 0f);   	
    	if (P3Playing)
    	{
			P3Location = p3.transform.position;
			P3Scale = p3.transform.localScale;
			
			if (P3Scale.x > 0)
			{
				P3xTagOffset = xLeftTagOffset;
				P3xHealthOffset = xLeftHealthOffset;
			}
			else
			{
				P3xTagOffset = xRightTagOffset;
				P3xHealthOffset = xRightHealthOffset;
			}
			
			P3TagPoint = Camera.main.WorldToScreenPoint(new Vector3(P3Location.x+P3xTagOffset, P3Location.y+yTagOffset, P3Location.z));
			P3HealthPoint = Camera.main.WorldToScreenPoint(new Vector3(P3Location.x+P3xHealthOffset, P3Location.y+yHealthOffset, P3Location.z));
    	}
    	if (P4Playing)
    	{
			P4Location = p4.transform.position;
			P4Scale = p4.transform.localScale;
			
			if (P4Scale.x > 0)
			{
				P4xTagOffset = xLeftTagOffset;
				P4xHealthOffset = xLeftHealthOffset;
			}
			else
			{
				P4xTagOffset = xRightTagOffset;
				P4xHealthOffset = xRightHealthOffset;
			}
			
			P4TagPoint = Camera.main.WorldToScreenPoint(new Vector3(P4Location.x+P4xTagOffset, P4Location.y+yTagOffset, P4Location.z));
			P4HealthPoint = Camera.main.WorldToScreenPoint(new Vector3(P4Location.x+P4xHealthOffset, P4Location.y+yHealthOffset, P4Location.z));
		}
		
		if (P1Scale.x > 0)
		{
			P1xTagOffset = xLeftTagOffset;
			P1xHealthOffset = xLeftHealthOffset;
		}
		else
		{
			P1xTagOffset = xRightTagOffset;
			P1xHealthOffset = xRightHealthOffset;
		}
		if (P2Scale.x > 0)
		{
			P2xTagOffset = xLeftTagOffset;
			P2xHealthOffset = xLeftHealthOffset;
		}
		else
		{
			P2xTagOffset = xRightTagOffset;
			P2xHealthOffset = xRightHealthOffset;
		}
		
		P1TagPoint = Camera.main.WorldToScreenPoint(new Vector3(P1Location.x+P1xTagOffset, P1Location.y+yTagOffset, P1Location.z));
		P2TagPoint = Camera.main.WorldToScreenPoint(new Vector3(P2Location.x+P2xTagOffset, P2Location.y+yTagOffset, P2Location.z));
		
		P1HealthPoint = Camera.main.WorldToScreenPoint(new Vector3(P1Location.x+P1xHealthOffset, P1Location.y+yHealthOffset, P1Location.z));
		P2HealthPoint = Camera.main.WorldToScreenPoint(new Vector3(P2Location.x+P2xHealthOffset, P2Location.y+yHealthOffset, P2Location.z));
    }

    void OnGUI() {
		float green;
		float red;
		Color original = new Color (GUI.color.r, GUI.color.g, GUI.color.b);
		
		//GUI.Label (new Rect(P1TagPoint.x, Screen.height-P1TagPoint.y-40, 300, 300), p1.playerHealth.getHealth().ToString(), labelFontSize);
        if (p1.playerHealth.getHealth() > 0)
        {
	        GUI.DrawTexture (new Rect(P1TagPoint.x, Screen.height-P1TagPoint.y, P1Tag.width/4, P1Tag.height/4), P1Tag);GUI.DrawTexture (new Rect(P1TagPoint.x, Screen.height-P1TagPoint.y, P1Tag.width/4, P1Tag.height/4), P1Tag);
			if (p1.playerHealth.getHealth() >= 500)
			{
				green = 1.0f;
				red = (500f -((float)p1.playerHealth.getHealth() - 500f)) / 500f;
			}
			else
			{
				red = 1.0f;
				green = ((float)p1.playerHealth.getHealth()) / 500f;
			}
			GUI.color = new Color(red, green, 0f);
			GUI.DrawTexture (new Rect(P1HealthPoint.x, Screen.height-P1HealthPoint.y, (int)(Health_Blank.width/2.5), (int)(Health_Blank.height/2.5)), Health_Blank);
			GUI.color = original;
		}
		
		
		//GUI.Label (new Rect(P2TagPoint.x, Screen.height-P2TagPoint.y-40, 300, 300), p2.playerHealth.getHealth().ToString(), labelFontSize);
        if (p2.playerHealth.getHealth() > 0)
        {
	        GUI.DrawTexture (new Rect(P2TagPoint.x, Screen.height-P2TagPoint.y, P2Tag.width/4, P2Tag.height/4), P2Tag);
			if (p2.playerHealth.getHealth() >= 500)
			{
				green = 1.0f;
				red = (500f -((float)p2.playerHealth.getHealth() - 500f)) / 500f;
			}
			else
			{
				red = 1.0f;
				green = ((float)p2.playerHealth.getHealth()) / 500f;
			}
			GUI.color = new Color(red, green, 0f);
			GUI.DrawTexture (new Rect(P2HealthPoint.x, Screen.height-P2HealthPoint.y, (int)(Health_Blank.width/2.5), (int)(Health_Blank.height/2.5)), Health_Blank);
			GUI.color = original;
		}
		
		if (P3Playing && p3.playerHealth.getHealth() > 0)
		{
			//GUI.Label (new Rect(P3TagPoint.x, Screen.height-P3TagPoint.y-40, 300, 300), p3.playerHealth.getHealth().ToString(), labelFontSize);
			GUI.DrawTexture (new Rect(P3TagPoint.x, Screen.height-P3TagPoint.y, P3Tag.width/4, P3Tag.height/4), P3Tag);
			if (p3.playerHealth.getHealth() >= 500)
			{
				green = 1.0f;
				red = (500f -((float)p3.playerHealth.getHealth() - 500f)) / 500f;
			}
			else
			{
				red = 1.0f;
				green = ((float)p3.playerHealth.getHealth()) / 500f;
			}
			GUI.color = new Color(red, green, 0f);
			GUI.DrawTexture (new Rect(P3HealthPoint.x, Screen.height-P3HealthPoint.y, (int)(Health_Blank.width/2.5), (int)(Health_Blank.height/2.5)), Health_Blank);
			GUI.color = original;
		}
		
		
		if (P4Playing && p4.playerHealth.getHealth() > 0)
		{
			//GUI.Label (new Rect(P4TagPoint.x, Screen.height-P4TagPoint.y-40, 300, 300), p4.playerHealth.getHealth().ToString(), labelFontSize);
			GUI.DrawTexture (new Rect(P4TagPoint.x, Screen.height-P4TagPoint.y, P4Tag.width/4, P4Tag.height/4), P4Tag);
			if (p4.playerHealth.getHealth() >= 500)
			{
				green = 1.0f;
				red = (500f -((float)p4.playerHealth.getHealth() - 500f)) / 500f;
			}
			else
			{
				red = 1.0f;
				green = ((float)p4.playerHealth.getHealth()) / 500f;
			}
			GUI.color = new Color(red, green, 0f);
			GUI.DrawTexture (new Rect(P4HealthPoint.x, Screen.height-P4HealthPoint.y, (int)(Health_Blank.width/2.5), (int)(Health_Blank.height/2.5)), Health_Blank);
			GUI.color = original;
		}
	}
}
