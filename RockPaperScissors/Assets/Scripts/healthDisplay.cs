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
	private Vector3 P1ScreenPoint, P2ScreenPoint; // The locations of each player on the screen (in pixels)
    public Texture P1Tag, P2Tag; // Floats above the players' heads
    private Vector2 P1Offset, P2Offset; // How much the tags will be offset from the players' positions. Public so we can easily modify it.
    

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
        
        P1Offset = P2Offset = new Vector2(-17, -90);
    }

    // Update is called once per frame
    void Update () {
    	// Get each player's screen position
		P1ScreenPoint = Camera.main.WorldToScreenPoint(p1.transform.position);
		P2ScreenPoint = Camera.main.WorldToScreenPoint(p2.transform.position);
    }

    void OnGUI() {
		GUI.Label (new Rect(10, 10, 300, 300), p1.playerHealth.getHealth().ToString(), labelFontSize);
        GUI.DrawTexture (new Rect(P1ScreenPoint[0]+P1Offset[0], Screen.height-P1ScreenPoint[1]+P1Offset[1], P1Tag.width/4, P1Tag.height/4), P1Tag);
        GUI.Label (new Rect(1200, 10, 300, 300), p2.playerHealth.getHealth().ToString(), labelFontSize);
        print ("P2 Health: " + p2.playerHealth.getHealth().ToString());
        GUI.DrawTexture (new Rect(P2ScreenPoint[0]+P2Offset[0], Screen.height-P2ScreenPoint[1]+P2Offset[1], P2Tag.width/4, P2Tag.height/4), P2Tag);
    }
}
