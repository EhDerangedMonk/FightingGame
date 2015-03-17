/*****************************************************************************\
*	Author:		Nigel Martinez/Jerrit Anderson/Braden Gibson				  *
*	Purpose:	Attaches to main camera and displays the health of P1 and P2. *
\*****************************************************************************/
using UnityEngine;
using System.Collections;

// TODO: make the health display above the players' heads

public class healthDisplay : MonoBehaviour {
    private Player p1, p2;
	private GUIStyle labelFontSize; // Formats the labels
	private Vector3 P1TagPoint, P2TagPoint; // The top left locations of each player on the screen (in pixels)
	private Vector3 P1HealthPoint, P2HealthPoint; // Where to draw the health indicators.
    public Texture P1Tag, P2Tag; // Floats above the players' heads
    public Texture Health_100to81, Health_80to61, Health_60to41, Health_40to21, Health_20to1; // Indicate the players' health levels
    private int n;
	private float xTagOffset, yTagOffset; // How much the tags will be offset from the players' positions. 
	public float xHealthOffset, yHealthOffset; // How much the health indicators will be offset from the players' positions.
	
    // Use this for initialization
    void Start () {
        GameObject tmpGameObject;
        tmpGameObject = GameObject.Find ("Player1");
        p1 = (Player)tmpGameObject.GetComponent(typeof(Player));
        tmpGameObject = GameObject.Find ("Player2");
        p2 = (Player)tmpGameObject.GetComponent(typeof(Player));
        
        labelFontSize = new GUIStyle();
        labelFontSize.fontSize = 30;
        labelFontSize.normal.textColor = Color.white;
        
        xHealthOffset = -0.62f;
        yHealthOffset = 2.14f;
        xTagOffset = -0.75f;
        yTagOffset = 3.3f; // These are just where it looks good, they can be modified
        
        n=0;
    }

    // Update is called once per frame
    void Update () {
    	// Get each player's top left screen position
    	Vector3 P1Location = p1.transform.position;
    	Vector3 P2Location = p2.transform.position;
    	
		P1TagPoint = Camera.main.WorldToScreenPoint(new Vector3(P1Location.x+xTagOffset, P1Location.y+yTagOffset, P1Location.z));
		P2TagPoint = Camera.main.WorldToScreenPoint(new Vector3(P2Location.x+xTagOffset, P2Location.y+yTagOffset, P2Location.z));
		
		P1HealthPoint = Camera.main.WorldToScreenPoint(new Vector3(P1Location.x+xHealthOffset, P1Location.y+yHealthOffset, P1Location.z));
		P2HealthPoint = Camera.main.WorldToScreenPoint(new Vector3(P2Location.x+xHealthOffset, P2Location.y+yHealthOffset, P2Location.z));
    }

    void OnGUI() {
		GUI.Label (new Rect(P1TagPoint.x, Screen.height-P1TagPoint.y-40, 300, 300), p1.playerHealth.getHealth().ToString(), labelFontSize);
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
			
		
		
		
		GUI.Label (new Rect(P2TagPoint.x, Screen.height-P2TagPoint.y-40, 300, 300), p2.playerHealth.getHealth().ToString(), labelFontSize);
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
	}
}
