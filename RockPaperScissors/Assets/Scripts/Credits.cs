using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	public Texture mainLogo;
	private float mainLogoScale;
	private Rect mainLogoRect;
	private int mainLogoY;
	
	public GUIStyle creditFont;
	private int fontSize;
	
	private int xOffset; // X distance from the left side of the screen
	private int xOffset2;
	private int nameTitle_Spacing; // Y distance between title and name
	private int nameName_Spacing; // Y distance between each name/title combo
	
	private int title1y;
	private int name1y;
	private int title2y;
	private int name2y;
	private int title3y;
	private int name3y;
	private int title4y;
	private int name4y;
	
	
	
	/*******\
	* START *
	\*******/
	void Start ()
	{
		fontSize = 40;
		xOffset = 10;
		xOffset2 = 40;
		nameTitle_Spacing = (int) (fontSize * 0.5);
		nameName_Spacing = (int) (fontSize * 2.0);
	
		// Set up the font
		creditFont = new GUIStyle();
		creditFont.fontSize = fontSize;
		creditFont.normal.textColor = Color.black;
		creditFont.font = (Font)Resources.Load("go3v2", typeof(Font));
	
		// Set the initial positions for the credits
		mainLogoY = Screen.height;
		mainLogoScale = 0.5f;
		
		title1y = mainLogoY + (int)(mainLogo.height*mainLogoScale) + nameName_Spacing;
		name1y = title1y + fontSize + nameTitle_Spacing;
		
		title2y = name1y + fontSize + nameName_Spacing;
		name2y = title2y + fontSize + nameTitle_Spacing;
		
		title3y = name2y + fontSize + nameName_Spacing;
		name3y = title3y + fontSize + nameTitle_Spacing;
		
		title4y = name3y + fontSize + nameName_Spacing;
		name4y = title4y + fontSize + nameTitle_Spacing;
	}
	
	
	
	/*******\
	* ONGUI *
	\*******/
	void OnGUI()
	{
		// Draw all the labels and textures
		GUI.DrawTexture(new Rect((int)(xOffset-(100*mainLogoScale)), mainLogoY, mainLogo.width*mainLogoScale, mainLogo.height*mainLogoScale), mainLogo, ScaleMode.ScaleToFit, true);
		GUI.Label (new Rect(xOffset, title1y, fontSize, Screen.width), "Character Interactions", creditFont);
		GUI.Label (new Rect(xOffset2, name1y, fontSize, Screen.width), "Jerrit Anderson", creditFont);
		GUI.Label (new Rect(xOffset, title2y, fontSize, Screen.width), "User Interface", creditFont);
		GUI.Label (new Rect(xOffset2, name2y, fontSize, Screen.width), "Braden Gibson", creditFont);
		GUI.Label (new Rect(xOffset, title3y, fontSize, Screen.width), "Art & Map Creation", creditFont);
		GUI.Label (new Rect(xOffset2, name3y, fontSize, Screen.width), "Nigel Martinez", creditFont);
		GUI.Label (new Rect(xOffset, title4y, fontSize, Screen.width), "Character Control", creditFont);
		GUI.Label (new Rect(xOffset2, name4y, fontSize, Screen.width), "Jo Menezes", creditFont);
	}
	
	
	
	/**************\
	* FIXED UPDATE *
	\**************/
	void FixedUpdate () {
		int scrollRate = 1;
		// Scroll the credits up each frame
		mainLogoY -= scrollRate;
		title1y -= scrollRate;
		title2y -= scrollRate;
		title3y -= scrollRate;
		title4y -= scrollRate;
		name1y -= scrollRate;
		name2y -= scrollRate;
		name3y -= scrollRate;
		name4y -= scrollRate;
	}
	
	
	
	/********\
	* UPDATE *
	\********/
	void Update()
	{
		// Go back to main menu if the credits are done rolling, or if the user hits "Escape"
		if (((name4y + fontSize) < 0) || (Input.GetKeyDown(KeyCode.Escape)))
		{
			Application.LoadLevel ("mainmenu");
		}
	}
}
