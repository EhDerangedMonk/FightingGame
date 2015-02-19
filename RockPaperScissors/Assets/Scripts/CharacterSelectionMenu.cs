/// <summary>
/// Character Selection Menu.
/// Attaches to main camera.
/// </summary>
using UnityEngine;
using System.Collections;

public class CharacterSelectionMenu : MonoBehaviour {
	
	public Texture NoirSelectionButton;
	public Texture ZakirSelectionButton;
	public Texture VioletSelectionButton;
	public Texture CharacterSelectionTitle;
	
	private Rect NoirRect;
	private Rect ZakirRect;
	private Rect VioletRect;
	private Rect CharacterSelectionRect;
	
	
	/*******\
	* START *
	\*******/
	void Start()
	{
		int buttonWidth = Screen.width/3;
		int buttonHeight = (int) (Screen.width/3 * 0.78947368); // The height is multiplied by 0.78947368 to match the aspect ratio of the button to 16:9
		int vertSpacing = (Screen.height - 2*buttonHeight)/3;
	
		// Initialize the buttons' rectangles
		CharacterSelectionRect = new Rect(Screen.width/9, vertSpacing, buttonWidth, buttonHeight);
		VioletRect = new Rect(Screen.width/9, Screen.width/3 + 2*vertSpacing, buttonWidth, buttonHeight);
		ZakirRect = new Rect(Screen.width*5/9, vertSpacing, buttonWidth, buttonHeight);
		NoirRect = new Rect(Screen.width*5/9, Screen.width/3 + 2*vertSpacing, buttonWidth, buttonHeight);
		
	} 
	
	/*******\
	* ONGUI *
	\*******/
	void OnGUI()
	{
		// Draw the buttons, adjusted for screen width
		GUI.DrawTexture(CharacterSelectionRect, CharacterSelectionTitle, ScaleMode.ScaleToFit, true);
		GUI.DrawTexture(VioletRect, VioletSelectionButton, ScaleMode.ScaleToFit, true);
		GUI.DrawTexture(ZakirRect, ZakirSelectionButton, ScaleMode.ScaleToFit, true);
		GUI.DrawTexture(NoirRect, NoirSelectionButton, ScaleMode.ScaleToFit, true);
	}
}
