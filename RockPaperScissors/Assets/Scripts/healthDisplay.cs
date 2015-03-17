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
    public float xOffset, yOffset;

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
        
        xOffset = -0.62f;
        yOffset = 2.8f; // These are just where it looks good, they can be modified
        
        n=0;
    }

    // Update is called once per frame
    void Update () {
    	// Get each player's top left screen position
    	Vector3 P1Location = p1.transform.position;
    	Vector3 P2Location = p2.transform.position;
    	
		P1TopLeftPoint = Camera.main.WorldToScreenPoint(new Vector3(P1Location.x+xOffset, P1Location.y+yOffset, P1Location.z));
		P2TopLeftPoint = Camera.main.WorldToScreenPoint(new Vector3(P2Location.x+xOffset, P2Location.y+yOffset, P2Location.z));
    }

    void OnGUI() {
		//GUI.Label (new Rect(P1TopLeftPoint[0]+P1Offset[0], Screen.height-P1TopLeftPoint[1]+P1Offset[1]-40, 300, 300), p1.playerHealth.getHealth().ToString(), labelFontSize);
        GUI.DrawTexture (new Rect(P1TopLeftPoint.x, Screen.height-P1TopLeftPoint.y, P1Tag.width/4, P1Tag.height/4), P1Tag);
		//GUI.Label (new Rect(P2TopLeftPoint[0]+P2Offset[0], Screen.height-P2TopLeftPoint[1]+P2Offset[1]-40, 300, 300), p2.playerHealth.getHealth().ToString(), labelFontSize);
        GUI.DrawTexture (new Rect(P2TopLeftPoint.x, Screen.height-P2TopLeftPoint.y, P2Tag.width/4, P2Tag.height/4), P2Tag);
    }
}
