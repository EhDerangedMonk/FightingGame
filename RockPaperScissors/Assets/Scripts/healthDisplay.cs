/*****************************************************************************\
*	Author:		Nigel Martinez/Jerrit Anderson/Braden Gibson				  *
*	Purpose:	Attaches to main camera and displays the health of P1 and P2. *
\*****************************************************************************/
using UnityEngine;
using System.Collections;

// TODO: make the health display above the players' heads

public class healthDisplay : MonoBehaviour {
    Player p1, p2;
    GUIStyle labelFontSize;

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
    }

    // Update is called once per frame
    void Update () {

    }

    void OnGUI() {
		Vector3 P1ScreenPoint = Camera.main.WorldToScreenPoint(p1.transform.position);
		Vector3 P2ScreenPoint = Camera.main.WorldToScreenPoint(p2.transform.position);
		
        GUI.Label (new Rect(10, 10, 300, 300), p1.playerHealth.getHealth().ToString(), labelFontSize);
        GUI.Label (new Rect(P1ScreenPoint[0], Screen.height-P1ScreenPoint[1], 300, 300), P1ScreenPoint.ToString(), labelFontSize);
        GUI.Label (new Rect(1200, 10, 300, 300), p2.playerHealth.getHealth().ToString(), labelFontSize);
        GUI.Label (new Rect(P2ScreenPoint[0], Screen.height-P2ScreenPoint[1], 300, 300), P2ScreenPoint.ToString(), labelFontSize);
    }
}
