/********************************************************************************\
*	Author:		Braden Gibson													 *
*	Purpose:	Attaches to main camera. Allows selection of controllers through *
*				use of the mouse.										 		 *
\********************************************************************************/
using UnityEngine;
using System.Collections;

public class ControlsMenu : MonoBehaviour {

	/***********\
	* VARIABLES *
	\***********/
	
	private Rect P1LabelRect; // Where to draw the labels
	private Rect P2LabelRect;
	private Rect P3LabelRect;
	private Rect P4LabelRect;	
	
	private Rect P1SlotRect; // Where the controllers are dropped
	private Rect P2SlotRect;
	private Rect P3SlotRect;
	private Rect P4SlotRect;
	
	private Rect KB1SlotRect; // Where the controllers are stored
	private Rect KB2SlotRect;
	private Rect C1SlotRect;
	private Rect C2SlotRect;
	private Rect C3SlotRect;
	private Rect C4SlotRect;
	
	private Rect SaveButtonRect; // Used to draw and detect button clicks
	private Rect CancelButtonRect;
	
	public Texture P1LabelTexture; // Textures for drawing all the rects
	public Texture P2LabelTexture;
	public Texture P3LabelTexture;
	public Texture P4LabelTexture;
	public Texture ControllerSlotTexture;
	public Texture KB1Texture;
	public Texture KB2Texture;
	public Texture C1Texture;
	public Texture C2Texture;
	public Texture C3Texture;
	public Texture C4Texture;
	public Texture SaveButtonTexture;
	public Texture CancelButtonTexture;
	
	
	
	/*******\
	* START *
	\*******/
	void Start () {
		int buttonHeight = (int)(Screen.height * 0.15);
		int buttonWidth = (int)((float)ControllerSlotTexture.width * ((float)buttonHeight / (float)ControllerSlotTexture.height)); // Set the width based on the height ratio
		int vertSpacing = (int)(Screen.height * 0.08);
		int horzSpacing = (int)((Screen.width - (buttonWidth*4)) / 6);
		
		//Initialize the rect positions
		P1LabelRect = new Rect(horzSpacing, vertSpacing, buttonWidth, buttonHeight);
		P2LabelRect = new Rect(horzSpacing, vertSpacing*2 + buttonHeight, buttonWidth, buttonHeight);
		P3LabelRect = new Rect(horzSpacing, vertSpacing*3 + buttonHeight*2, buttonWidth, buttonHeight);
		P4LabelRect = new Rect(horzSpacing, vertSpacing*4 + buttonHeight*3, buttonWidth, buttonHeight);
		
		P1SlotRect = new Rect(horzSpacing*2 + buttonWidth, vertSpacing, buttonWidth, buttonHeight);
		P2SlotRect = new Rect(horzSpacing*2 + buttonWidth, vertSpacing*2 + buttonHeight, buttonWidth, buttonHeight);
		P3SlotRect = new Rect(horzSpacing*2 + buttonWidth, vertSpacing*3 + buttonHeight*2, buttonWidth, buttonHeight);
		P4SlotRect = new Rect(horzSpacing*2 + buttonWidth, vertSpacing*4 + buttonHeight*3, buttonWidth, buttonHeight);
		
		KB1SlotRect = new Rect(horzSpacing*4 + buttonWidth*2, vertSpacing, buttonWidth, buttonHeight);
		C1SlotRect = new Rect(horzSpacing*4 + buttonWidth*2, vertSpacing*2 + buttonHeight, buttonWidth, buttonHeight);
		C3SlotRect = new Rect(horzSpacing*4 + buttonWidth*2, vertSpacing*3 + buttonHeight*2, buttonWidth, buttonHeight);
		SaveButtonRect = new Rect(horzSpacing*4 + buttonWidth*2, vertSpacing*4 + buttonHeight*3, buttonWidth, buttonHeight);
		
		KB2SlotRect = new Rect(horzSpacing*5 + buttonWidth*3, vertSpacing, buttonWidth, buttonHeight);
		C2SlotRect = new Rect(horzSpacing*5 + buttonWidth*3, vertSpacing*2 + buttonHeight, buttonWidth, buttonHeight);
		C4SlotRect = new Rect(horzSpacing*5 + buttonWidth*3, vertSpacing*3 + buttonHeight*2, buttonWidth, buttonHeight);
		CancelButtonRect = new Rect(horzSpacing*5 + buttonWidth*3, vertSpacing*4 + buttonHeight*3, buttonWidth, buttonHeight);
	}
	
	
	
	/********\
	* UPDATE *
	\********/
	void Update () {
	
	}
	
	
	
	/*******\
	* ONGUI *
	\*******/
	void OnGUI()
	{
		// Draw the textures
		GUI.DrawTexture(P1LabelRect, P1LabelTexture);
		GUI.DrawTexture(P2LabelRect, P2LabelTexture);
		GUI.DrawTexture(P3LabelRect, P3LabelTexture);
		GUI.DrawTexture(P4LabelRect, P4LabelTexture);
		
		GUI.DrawTexture(P1SlotRect, ControllerSlotTexture);
		GUI.DrawTexture(P2SlotRect, ControllerSlotTexture);
		GUI.DrawTexture(P3SlotRect, ControllerSlotTexture);
		GUI.DrawTexture(P4SlotRect, ControllerSlotTexture);
		
		GUI.DrawTexture(KB1SlotRect, KB1Texture);
		GUI.DrawTexture(C1SlotRect, C1Texture);
		GUI.DrawTexture(C3SlotRect, C3Texture);
		GUI.DrawTexture(SaveButtonRect, SaveButtonTexture);
		
		GUI.DrawTexture(KB2SlotRect, KB2Texture);
		GUI.DrawTexture(C2SlotRect, C2Texture);
		GUI.DrawTexture(C4SlotRect, C4Texture);
		GUI.DrawTexture(CancelButtonRect, CancelButtonTexture);
	}
}
