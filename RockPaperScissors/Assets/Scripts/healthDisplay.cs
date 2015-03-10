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
	private Vector3 P1TopLeftPoint, P2TopLeftPoint; // The top left locations of each player on the screen (in pixels)
	private Vector3 P1BottomPoint; // The bottom-centre of each character (in pixels). Used to calculate the relative height
	private int CharacterScreenHeight; // The height of the player (in pixels) on the camera.
    public Texture P1Tag, P2Tag; // Floats above the players' heads
    private Vector2 P1Offset, P2Offset; // How much the tags will be offset from the players' positions. Public so we can easily modify it.
    private int n;

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
        n=0;
    }

    // Update is called once per frame
    void Update () {
    	// Get each player's top left screen position
		P1TopLeftPoint = Camera.main.WorldToScreenPoint(p1.transform.position);
		P2TopLeftPoint = Camera.main.WorldToScreenPoint(p2.transform.position);
		
		// Get each player's bottom-centre screen position
		Vector3 extents = p1.renderer.bounds.extents;
		CharacterScreenHeight = (int) Camera.main.WorldToScreenPoint(new Vector3(0f, extents.y*2, 0f)).y;
		print ("Scale: " + ((float)CharacterScreenHeight/275f).ToString());
		
		// Adjust the offset based on the character's screen height
		P1Offset = P2Offset = new Vector2((int) (-17f * ((float)CharacterScreenHeight/275f)), (int) ((float)(0 - P1Tag.height/4) - (40f * ((float)CharacterScreenHeight)/275f))); // divides the character's current height by their original height
		
    }

    void OnGUI() {
		GUI.Label (new Rect(P1TopLeftPoint[0]+P1Offset[0], Screen.height-P1TopLeftPoint[1]+P1Offset[1]-40, 300, 300), p1.playerHealth.getHealth().ToString(), labelFontSize);
        GUI.DrawTexture (new Rect(P1TopLeftPoint[0]+P1Offset[0], Screen.height-P1TopLeftPoint[1]+P1Offset[1], P1Tag.width/4, P1Tag.height/4), P1Tag);
		GUI.Label (new Rect(P2TopLeftPoint[0]+P2Offset[0], Screen.height-P2TopLeftPoint[1]+P2Offset[1]-40, 300, 300), p2.playerHealth.getHealth().ToString(), labelFontSize);
        GUI.DrawTexture (new Rect(P2TopLeftPoint[0]+P2Offset[0], Screen.height-P2TopLeftPoint[1]+P2Offset[1], P2Tag.width/4, P2Tag.height/4), P2Tag);
    }
}
