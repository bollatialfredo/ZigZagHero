using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	private int levelButtonCount = 1;
	private int LEVELS_PER_PAGE = 12;
	private int cubePosition;
	private TextMesh buttonNumberText;
	private List<Button3d> levelListFront;
	private List<Button3d> levelListBack;
	private List<Button3d> levelListLeft;
	private List<Button3d> levelListRight;

	public GameObject conteinerFront;
	public GameObject conteinerBack;
	public GameObject conteinerLeft;
	public GameObject conteinerRight;
	public int MaxLevels;
	
	void Start () {
		//PlayerPrefs.DeleteAll ();PlayerPrefs.SetInt ("Level1", 1);
		ListsInit ();
		IterateList (levelListFront);
		cubePosition = 1;
	}

	void UpdateMenuButtons(){
		CheckCurrentCubePosition ();
		switch (cubePosition) {
		case 1:
			IterateList (levelListFront);
			break;
		case 2:
			IterateList (levelListLeft);
			break;
		case 3:
			IterateList (levelListBack);
			break;
		case 4:
			IterateList (levelListRight);
			break;
		}
	}

	void CheckCurrentCubePosition(){
		if (cubePosition == 5)
			cubePosition = 1;
		if (cubePosition == 0)
			cubePosition = 4;
	}

	void IterateList(List<Button3d> li){		
		foreach (Button3d btn in li) {
			ModifyButton (btn);
		}
	}

	void ModifyButton(Button3d btn){
		btn.SetButtonNumber (levelButtonCount);
		levelButtonCount++;
		CheckPlayerPrefs (btn);
		if (btn.buttonNumber > MaxLevels) {
			btn.gameObject.SetActive (false);
		} else {
			btn.gameObject.SetActive (true);
		}		
	}

	void CheckPlayerPrefs(Button3d btn){
		buttonNumberText = btn.GetComponentInChildren<TextMesh> ();
		if (PlayerPrefs.GetInt ("Level" + btn.buttonNumber.ToString ()) >= 1){			 
			if (PlayerPrefs.GetInt ("Level" + btn.buttonNumber.ToString ()) > 1) {
				btn.interactable = true;
				btn.mastered = true;
			} else {
				btn.interactable = true;
				buttonNumberText.fontSize = 30;
				buttonNumberText.text = btn.buttonNumber.ToString ();
			}
		} else {
			btn.interactable = false;
			buttonNumberText.fontSize = 10;
			buttonNumberText.text = btn.buttonNumber.ToString ()+"-Locked";
		}
	}

	public void MoveRight(){
		cubePosition++;
		UpdateMenuButtons ();
	}
	public void MoveLeft(){
		cubePosition--;
		levelButtonCount -= LEVELS_PER_PAGE*2;
		UpdateMenuButtons ();
	}
	public bool IsMovingRight(){
		return levelButtonCount < MaxLevels;
	}
	public bool IsMovingLeft(){
		return levelButtonCount > LEVELS_PER_PAGE + 1;
	}

	public int getCubePosition(){
		return cubePosition;
	}
	
	void ListsInit(){
		levelListFront = new List<Button3d> ();
		levelListBack = new List<Button3d> ();
		levelListLeft = new List<Button3d> ();
		levelListRight = new List<Button3d> ();
		
		foreach (Transform child in conteinerFront.transform) {
			levelListFront.Add (child.GetComponent<Button3d> ());
		}
		foreach (Transform child in conteinerBack.transform) {
			levelListBack.Add (child.GetComponent<Button3d> ());
		}
		foreach (Transform child in conteinerLeft.transform) {
			levelListLeft.Add (child.GetComponent<Button3d> ());
		}
		foreach (Transform child in conteinerRight.transform) {
			levelListRight.Add (child.GetComponent<Button3d> ());
		}
	}
}
