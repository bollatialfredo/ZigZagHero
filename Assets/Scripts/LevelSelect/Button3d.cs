using UnityEngine;
using System.Collections;
using System;

public class Button3d : MonoBehaviour {

	public int buttonNumber;
	public bool interactable;
	public bool mastered;
	public Color col;

	private Vector3 origin;
	private Color c;
	private Transform myTransform;
	private bool pressed;

	void Start(){
		SetOriginScale ();
		pressed = false;
		c = GetComponent<Renderer>().material.color;
	}	

	public void SetButtonNumber(int n){
		buttonNumber = n;
		TextMesh text = GetComponentInChildren<TextMesh> ();
		text.text = n.ToString ();
	}

	void SetOriginScale (){
		myTransform = GetComponent<Transform> ();
		origin = new Vector3 (myTransform.localScale.x, myTransform.localScale.y, myTransform.localScale.z);
		
	}

	void OnTouchDown(){
		if (interactable) {
			pressed = true;
			GetComponent<Renderer> ().material.color = col;
			Vector3 target = new Vector3 (myTransform.localScale.x + 0.07f, myTransform.localScale.y + 0.07f, myTransform.localScale.z + 0.07f);
			myTransform.localScale = target;
		}
	}
		
	void OnTouchUp(){
		if (interactable && pressed) {
			GetComponent<Renderer> ().material.color = c;
			myTransform.localScale = origin;
			PlayerPrefs.SetInt ("ChosenLevelToStart", buttonNumber);
			LoadTargetScene lts = GetComponentInParent <LoadTargetScene> ();
			lts.LoadSceneNumWithWaitingScreen (1);
		} 
		pressed = false;
	}

	void OnTouchExit(){
		pressed = false;
		myTransform.localScale = origin;
		GetComponent<Renderer> ().material.color = c;
	}
}
