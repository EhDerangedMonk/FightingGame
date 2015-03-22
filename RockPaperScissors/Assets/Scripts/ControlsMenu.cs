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
	public Texture GreyOutTexture;
	
	private enum ControllerSelectionState {None=0, KB1, KB2, C1, C2, C3, C4}; // Store where the controllers are
	private ControllerSelectionState Slot1Selection;
	private ControllerSelectionState Slot2Selection;
	private ControllerSelectionState Slot3Selection;
	private ControllerSelectionState Slot4Selection;
	
	private enum SelectingState {None=0, P1, P2, P3, P4}; // Keeps track of which slot the player is choosing for
	private SelectingState CurrentlySelecting;
	
	
	
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
		
		
		// Initialize the slot states by getting the storage object
		InitializeStorage storage = (InitializeStorage) GameObject.Find("VariableStorage").GetComponent(typeof(InitializeStorage));
		InitializeStorage.ControllerSelection storedP1Controller = storage.P1Controller;
		InitializeStorage.ControllerSelection storedP2Controller = storage.P2Controller;
		InitializeStorage.ControllerSelection storedP3Controller = storage.P3Controller;
		InitializeStorage.ControllerSelection storedP4Controller = storage.P4Controller;
		
		Slot1Selection = (ControllerSelectionState)((int)storedP1Controller);
		Slot2Selection = (ControllerSelectionState)((int)storedP2Controller);
		Slot3Selection = (ControllerSelectionState)((int)storedP3Controller);
		Slot4Selection = (ControllerSelectionState)((int)storedP4Controller);
		
		// Initialize the what the player is currently selecting
		CurrentlySelecting = SelectingState.None;
	}
	
	
	
	/********\
	* UPDATE *
	\********/
	void Update () {
		/*if(CurrentlySelecting == SelectingState.None)
			Debug.Log("NONE");
		if(CurrentlySelecting == SelectingState.P1)
			Debug.Log("P1");
		if(CurrentlySelecting == SelectingState.P2)
			Debug.Log("P2");
		if(CurrentlySelecting == SelectingState.P3)
			Debug.Log("P3");
		if(CurrentlySelecting == SelectingState.P4)
			Debug.Log("P4");*/
	}
	
	
	
	/*******\
	* ONGUI *
	\*******/
	void OnGUI()
	{
		// Check to see if a button has been pressed
		if (GUI.Button (P1SlotRect, ControllerSlotTexture, ""))
		{
			// Toggle selecting for the slot
			if (CurrentlySelecting == SelectingState.None)
			{
				if (Slot1Selection == ControllerSelectionState.None)
					CurrentlySelecting = SelectingState.P1; // toggle
				else
					Slot1Selection = ControllerSelectionState.None; // reset slot
			}				
			else if (CurrentlySelecting == SelectingState.P1)
				CurrentlySelecting = SelectingState.None;
		}
		if (GUI.Button (P2SlotRect, ControllerSlotTexture, ""))
		{
			// Toggle selecting for the slot
			if (CurrentlySelecting == SelectingState.None)
			{
				if (Slot2Selection == ControllerSelectionState.None)
					CurrentlySelecting = SelectingState.P2; // toggle
				else
					Slot2Selection = ControllerSelectionState.None; // reset slot
			}	
			else if (CurrentlySelecting == SelectingState.P2)
				CurrentlySelecting = SelectingState.None;
		}
		GUI.depth = 0;
		if (GUI.Button (P3SlotRect, ControllerSlotTexture, ""))
		{
			// Toggle selecting for the slot
			if (CurrentlySelecting == SelectingState.None)
			{
				if (Slot3Selection == ControllerSelectionState.None)
					CurrentlySelecting = SelectingState.P3; // toggle
				else
					Slot3Selection = ControllerSelectionState.None; // reset slot
			}	
			else if (CurrentlySelecting == SelectingState.P3)
				CurrentlySelecting = SelectingState.None;
		}
		GUI.depth = 10;
		if (GUI.Button (P4SlotRect, ControllerSlotTexture, ""))
		{
			// Toggle selecting for the slot
			if (CurrentlySelecting == SelectingState.None)
			{
				if (Slot4Selection == ControllerSelectionState.None)
					CurrentlySelecting = SelectingState.P4; // toggle
				else
					Slot4Selection = ControllerSelectionState.None; // reset slot
			}	
			else if (CurrentlySelecting == SelectingState.P4)
				CurrentlySelecting = SelectingState.None;
			
		}
		
	
	
		// Draw the textures
		GUI.DrawTexture(P1LabelRect, P1LabelTexture);
		GUI.DrawTexture(P2LabelRect, P2LabelTexture);
		GUI.DrawTexture(P3LabelRect, P3LabelTexture);
		GUI.DrawTexture(P4LabelRect, P4LabelTexture);
		
		GUI.DrawTexture(SaveButtonRect, SaveButtonTexture);
		GUI.DrawTexture(CancelButtonRect, CancelButtonTexture);
		
		
		
		// Draw the controller images
		if (Slot1Selection == ControllerSelectionState.KB1)      // Draw it in the correct Player slot
			GUI.DrawTexture(P1SlotRect, KB1Texture);
		else if (Slot2Selection == ControllerSelectionState.KB1)
			GUI.DrawTexture(P2SlotRect, KB1Texture);
		else if (Slot3Selection == ControllerSelectionState.KB1)
			GUI.DrawTexture(P3SlotRect, KB1Texture);
		else if (Slot4Selection == ControllerSelectionState.KB1)
			GUI.DrawTexture(P4SlotRect, KB1Texture);
		else // Draw it as a button
		{
			if (GUI.Button(KB1SlotRect, KB1Texture, ""))
			{
				if (CurrentlySelecting == SelectingState.P1)
					Slot1Selection = ControllerSelectionState.KB1;
				else if (CurrentlySelecting == SelectingState.P2)
					Slot2Selection = ControllerSelectionState.KB1;
				else if (CurrentlySelecting == SelectingState.P3)
					Slot3Selection = ControllerSelectionState.KB1;
				else if (CurrentlySelecting == SelectingState.P4)
					Slot4Selection = ControllerSelectionState.KB1;
				
				if (CurrentlySelecting != SelectingState.None)
					CurrentlySelecting = SelectingState.None;
			}
		}
			
		if (Slot1Selection == ControllerSelectionState.KB2)
			GUI.DrawTexture(P1SlotRect, KB2Texture);
		else if (Slot2Selection == ControllerSelectionState.KB2)
			GUI.DrawTexture(P2SlotRect, KB2Texture);
		else if (Slot3Selection == ControllerSelectionState.KB2)
			GUI.DrawTexture(P3SlotRect, KB2Texture);
		else if (Slot4Selection == ControllerSelectionState.KB2)
			GUI.DrawTexture(P4SlotRect, KB2Texture);
		else // Draw it as a button
		{
			if (GUI.Button(KB2SlotRect, KB2Texture, ""))
			{
				if (CurrentlySelecting == SelectingState.P1)
					Slot1Selection = ControllerSelectionState.KB2;
				else if (CurrentlySelecting == SelectingState.P2)
					Slot2Selection = ControllerSelectionState.KB2;
				else if (CurrentlySelecting == SelectingState.P3)
					Slot3Selection = ControllerSelectionState.KB2;
				else if (CurrentlySelecting == SelectingState.P4)
					Slot4Selection = ControllerSelectionState.KB2;
				
				if (CurrentlySelecting != SelectingState.None)
					CurrentlySelecting = SelectingState.None;
			}
		}
			
		if (Slot1Selection == ControllerSelectionState.C1)
			GUI.DrawTexture(P1SlotRect, C1Texture);
		else if (Slot2Selection == ControllerSelectionState.C1)
			GUI.DrawTexture(P2SlotRect, C1Texture);
		else if (Slot3Selection == ControllerSelectionState.C1)
			GUI.DrawTexture(P3SlotRect, C1Texture);
		else if (Slot4Selection == ControllerSelectionState.C1)
			GUI.DrawTexture(P4SlotRect, C1Texture);
		else // Draw it as a button
		{
			if (GUI.Button(C1SlotRect, C1Texture, ""))
			{
				if (CurrentlySelecting == SelectingState.P1)
					Slot1Selection = ControllerSelectionState.C1;
				else if (CurrentlySelecting == SelectingState.P2)
					Slot2Selection = ControllerSelectionState.C1;
				else if (CurrentlySelecting == SelectingState.P3)
					Slot3Selection = ControllerSelectionState.C1;
				else if (CurrentlySelecting == SelectingState.P4)
					Slot4Selection = ControllerSelectionState.C1;
				
				if (CurrentlySelecting != SelectingState.None)
					CurrentlySelecting = SelectingState.None;
			}
		}
			
		if (Slot1Selection == ControllerSelectionState.C2)
			GUI.DrawTexture(P1SlotRect, C2Texture);
		else if (Slot2Selection == ControllerSelectionState.C2)
			GUI.DrawTexture(P2SlotRect, C2Texture);
		else if (Slot3Selection == ControllerSelectionState.C2)
			GUI.DrawTexture(P3SlotRect, C2Texture);
		else if (Slot4Selection == ControllerSelectionState.C2)
			GUI.DrawTexture(P4SlotRect, C2Texture);
		else // Draw it as a button
		{
			if (GUI.Button(C2SlotRect, C2Texture, ""))
			{
				if (CurrentlySelecting == SelectingState.P1)
					Slot1Selection = ControllerSelectionState.C2;
				else if (CurrentlySelecting == SelectingState.P2)
					Slot2Selection = ControllerSelectionState.C2;
				else if (CurrentlySelecting == SelectingState.P3)
					Slot3Selection = ControllerSelectionState.C2;
				else if (CurrentlySelecting == SelectingState.P4)
					Slot4Selection = ControllerSelectionState.C2;
				
				if (CurrentlySelecting != SelectingState.None)
					CurrentlySelecting = SelectingState.None;
			}
		}
			
		if (Slot1Selection == ControllerSelectionState.C3)
			GUI.DrawTexture(P1SlotRect, C3Texture);
		else if (Slot2Selection == ControllerSelectionState.C3)
			GUI.DrawTexture(P2SlotRect, C3Texture);
		else if (Slot3Selection == ControllerSelectionState.C3)
			GUI.DrawTexture(P3SlotRect, C3Texture);
		else if (Slot4Selection == ControllerSelectionState.C3)
			GUI.DrawTexture(P4SlotRect, C3Texture);
		else // Draw it as a button
		{
			if (GUI.Button(C3SlotRect, C3Texture, ""))
			{
				if (CurrentlySelecting == SelectingState.P1)
					Slot1Selection = ControllerSelectionState.C3;
				else if (CurrentlySelecting == SelectingState.P2)
					Slot2Selection = ControllerSelectionState.C3;
				else if (CurrentlySelecting == SelectingState.P3)
					Slot3Selection = ControllerSelectionState.C3;
				else if (CurrentlySelecting == SelectingState.P4)
					Slot4Selection = ControllerSelectionState.C3;
				
				if (CurrentlySelecting != SelectingState.None)
					CurrentlySelecting = SelectingState.None;
			}
		}
			
		if (Slot1Selection == ControllerSelectionState.C4)
			GUI.DrawTexture(P1SlotRect, C4Texture);
		else if (Slot2Selection == ControllerSelectionState.C4)
			GUI.DrawTexture(P2SlotRect, C4Texture);
		else if (Slot3Selection == ControllerSelectionState.C4)
			GUI.DrawTexture(P3SlotRect, C4Texture);
		else if (Slot4Selection == ControllerSelectionState.C4)
			GUI.DrawTexture(P4SlotRect, C4Texture);
		else // Draw it as a button
		{
			if (GUI.Button(C4SlotRect, C4Texture, ""))
			{
				if (CurrentlySelecting == SelectingState.P1)
					Slot1Selection = ControllerSelectionState.C4;
				else if (CurrentlySelecting == SelectingState.P2)
					Slot2Selection = ControllerSelectionState.C4;
				else if (CurrentlySelecting == SelectingState.P3)
					Slot3Selection = ControllerSelectionState.C4;
				else if (CurrentlySelecting == SelectingState.P4)
					Slot4Selection = ControllerSelectionState.C4;
				
				if (CurrentlySelecting != SelectingState.None)
					CurrentlySelecting = SelectingState.None;
			}
		}
		
		
		
		
		// Draw the grey-out texture
		/*GUI.depth = 5; // Place it in between the greyed-out textures and the full-colour textures
		GUI.color = new Color(1.0f, 1.0f, 1.0f, 0.2f); // Make it translucent
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), GreyOutTexture);
		GUI.color = new Color(1.0f, 1.0f, 1.0f, 1.0f); // Reset the colour*/
	}
}
