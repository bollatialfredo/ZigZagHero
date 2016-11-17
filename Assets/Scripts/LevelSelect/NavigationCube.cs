using UnityEngine;
using System.Collections;

public class NavigationCube : MonoBehaviour {

	public bool dirLeft;
	public bool dirRight;
	public bool dirUp;
	public bool dirDown;
	public MenuMovement menuMovement;

	private bool pressed;

	void Start(){
		pressed = false;
	}

	void OnTouchDown()	{
		pressed = true;
	}

	void OnTouchUp(){
		if (pressed) {
			if (dirRight) {	
				menuMovement.TurnRight ();
			} else if (dirLeft) {
				menuMovement.TurnLeft ();
			} else if (dirUp) {
				menuMovement.MoveUp ();
			} else if (dirDown){
				menuMovement.MoveDown ();
			}
			pressed = false;
		}
	}

	void OnTouchExit(){
		pressed = false;
	}

}
