using UnityEngine;
using System.Collections;

public class playerSpawner : MonoBehaviour {

	public InitializeStorage gameSettings;

	public int playerNum; // Player this Spawner is responsibile for. Player 1 = 1, Player 2 = 2, etc.

	private Player player;
	private GameObject newPlayer;

	// Use this for initialization
	void Start () {
		//Find the options.
		gameSettings = FindObjectOfType<InitializeStorage>();
		Spawn ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Spawn() {
		if (playerNum == 1 && gameSettings.P1Controller != 0) {
			//Assign Character
			switch((int)gameSettings.P1Character) {
				case 1: //Violet
					newPlayer = Resources.Load ("Violet") as GameObject;
					break;
				case 2: //Zakir
					newPlayer = Resources.Load ("Zakir") as GameObject;
					break;
				case 3: //Noir
					newPlayer = Resources.Load ("Noir") as GameObject;
					break;
				default:
					print("Error: Character not selected.");
					break;
			}

			//Assign Controller
			switch((int)gameSettings.P1Controller) {
				case 1:
					newPlayer.GetComponent<Player>().layout = 1;
					break;
				case 2:
					newPlayer.GetComponent<Player>().layout = 2;
					break;
				case 3:
					newPlayer.GetComponent<Player>().layout = 3;
					break;
				case 4:
					newPlayer.GetComponent<Player>().layout = 4;
					break;
				case 5:
					newPlayer.GetComponent<Player>().layout = 5;
					break;
				case 6:
					newPlayer.GetComponent<Player>().layout = 6;
					break;
				default:
					print ("Error: Controller not assigned.");
					break;
			}

			//Spawn the player.
			Instantiate(newPlayer, transform.position, transform.rotation);
		}

		else if (playerNum == 2  && gameSettings.P2Controller != 0) {
			//Assign Character
			switch((int)gameSettings.P2Character) {
			case 1: //Violet
				newPlayer = Resources.Load ("Violet") as GameObject;
				break;
			case 2: //Zakir
				newPlayer = Resources.Load ("Zakir") as GameObject;
				break;
			case 3: //Noir
				newPlayer = Resources.Load ("Noir") as GameObject;
				break;
			default:
				print("Error: Character not selected.");
				break;
			}
			
			//Assign Controller
			switch((int)gameSettings.P2Controller) {
			case 1:
				newPlayer.GetComponent<Player>().layout = 1;
				break;
			case 2:
				newPlayer.GetComponent<Player>().layout = 2;
				break;
			case 3:
				newPlayer.GetComponent<Player>().layout = 3;
				break;
			case 4:
				newPlayer.GetComponent<Player>().layout = 4;
				break;
			case 5:
				newPlayer.GetComponent<Player>().layout = 5;
				break;
			case 6:
				newPlayer.GetComponent<Player>().layout = 6;
				break;
			default:
				print ("Error: Controller not assigned.");
				break;
			}
			
			//Spawn the player.
			Instantiate(newPlayer, transform.position, transform.rotation);
		}

		else if (playerNum == 3  && gameSettings.P3Controller != 0) {
			//Assign Character
			switch((int)gameSettings.P3Character) {
			case 1: //Violet
				newPlayer = Resources.Load ("Violet") as GameObject;
				break;
			case 2: //Zakir
				newPlayer = Resources.Load ("Zakir") as GameObject;
				break;
			case 3: //Noir
				newPlayer = Resources.Load ("Noir") as GameObject;
				break;
			default:
				print("Error: Character not selected.");
				break;
			}
			
			//Assign Controller
			switch((int)gameSettings.P3Controller) {
			case 1:
				newPlayer.GetComponent<Player>().layout = 1;
				break;
			case 2:
				newPlayer.GetComponent<Player>().layout = 2;
				break;
			case 3:
				newPlayer.GetComponent<Player>().layout = 3;
				break;
			case 4:
				newPlayer.GetComponent<Player>().layout = 4;
				break;
			case 5:
				newPlayer.GetComponent<Player>().layout = 5;
				break;
			case 6:
				newPlayer.GetComponent<Player>().layout = 6;
				break;
			default:
				print ("Error: Controller not assigned.");
				break;
			}
			
			//Spawn the player.
			Instantiate(newPlayer, transform.position, transform.rotation);
		}

		else if(playerNum == 4  && gameSettings.P4Controller != 0) {
			//Assign Character
			switch((int)gameSettings.P4Character) {
			case 1: //Violet
				newPlayer = Resources.Load ("Violet") as GameObject;
				break;
			case 2: //Zakir
				newPlayer = Resources.Load ("Zakir") as GameObject;
				break;
			case 3: //Noir
				newPlayer = Resources.Load ("Noir") as GameObject;
				break;
			default:
				print("Error: Character not selected.");
				break;
			}
			
			//Assign Controller
			switch((int)gameSettings.P4Controller) {
			case 1:
				newPlayer.GetComponent<Player>().layout = 1;
				break;
			case 2:
				newPlayer.GetComponent<Player>().layout = 2;
				break;
			case 3:
				newPlayer.GetComponent<Player>().layout = 3;
				break;
			case 4:
				newPlayer.GetComponent<Player>().layout = 4;
				break;
			case 5:
				newPlayer.GetComponent<Player>().layout = 5;
				break;
			case 6:
				newPlayer.GetComponent<Player>().layout = 6;
				break;
			default:
				print ("Error: Controller not assigned.");
				break;
			}
			
			//Spawn the player.
			Instantiate(newPlayer, transform.position, transform.rotation);
		}
	}
}
