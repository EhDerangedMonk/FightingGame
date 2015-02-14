using UnityEngine;
using System.Collections;

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
        GUI.Label (new Rect(10, 10, 300, 300), p1.playerHealth.getHealth().ToString(), labelFontSize);
        GUI.Label (new Rect(1200, 10, 300, 300), p2.playerHealth.getHealth().ToString(), labelFontSize);
    }
}
