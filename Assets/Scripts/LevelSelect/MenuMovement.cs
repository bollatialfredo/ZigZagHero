using UnityEngine;
using System.Collections;

public class MenuMovement : MonoBehaviour {

	public float turnSpeed;
	public GameObject cube;
	public GameObject navCubeRight;
	public GameObject navCubeLeft;
	public GameObject mainButtonsPanel;
	public GameObject mainMenuButton;

	private float menuYRotation = 0;
	private float menuXRotation = -90;
	private float menuZRotation = 0;
	private Vector3 toMainMenuPos;
	private Vector3 targetPos;


	private float menuPanelYRotation = 0;
	private float menuPanelXRotation = -90;

	private LevelManager lm;
	private bool isUp;

	void Start (){
		targetPos = new Vector3 ();
		lm = GetComponent<LevelManager> ();
		navCubeLeft.gameObject.SetActive (false);
		navCubeRight.gameObject.SetActive (false);
		mainMenuButton.gameObject.SetActive (false);
		toMainMenuPos = new Vector3(mainMenuButton.transform.position.x,mainMenuButton.transform.position.y,mainMenuButton.transform.position.z);
		isUp = true;
	}

	void Update ()
	{
		Quaternion target = Quaternion.Euler (menuXRotation, menuYRotation, menuZRotation);
		cube.transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * turnSpeed);

		Quaternion target2 = Quaternion.Euler (menuPanelXRotation, menuPanelYRotation, 0);
		mainButtonsPanel.transform.rotation = Quaternion.Slerp (mainButtonsPanel.transform.rotation, target2, Time.deltaTime * (turnSpeed));

		mainMenuButton.transform.position = Vector3.MoveTowards (mainMenuButton.transform.position, toMainMenuPos, Time.deltaTime * turnSpeed);
	}

	public void MoveDown(){
		isUp = false;
		mainButtonsPanel.gameObject.SetActive (false);
		mainMenuButton.gameObject.SetActive (true);
		navCubeRight.gameObject.SetActive (true);
		navCubeLeft.gameObject.SetActive (true);
		switch (lm.getCubePosition()) {
		case 1:
			menuXRotation = 0;
			menuPanelYRotation = 0;
			menuPanelXRotation = 0;
			break;
		case 2:
			menuZRotation = 0;
			menuPanelYRotation = -90;
			menuPanelXRotation = 0;
			break;
		case 3:
			menuXRotation = 0;
			menuPanelYRotation = -90;
			menuPanelXRotation = 0;
			break;
		case 4:
			menuZRotation = 0;
			menuPanelYRotation = -90;
			menuPanelXRotation = 0;
			break;
		}
	}

	public void MoveUp(){
		isUp = true;
		mainMenuButton.gameObject.SetActive (false);
		mainButtonsPanel.gameObject.SetActive (true);
		navCubeLeft.gameObject.SetActive (false);
		navCubeRight.gameObject.SetActive (false);
		switch (lm.getCubePosition()) {
		case 1:
			menuXRotation = -90;
			menuPanelYRotation = 0;
			menuPanelXRotation = -90;
			break;
		case 2:
			menuZRotation = -90;
			menuPanelYRotation = 0;
			menuPanelXRotation = -90;
			break;
		case 3:
			menuXRotation = 90;
			menuPanelYRotation = 0;
			menuPanelXRotation = 270;
			break;
		case 4:
			menuZRotation = 90;
			menuPanelYRotation = 0;
			menuPanelXRotation = -90;
			break;
		}
	}

	public void TurnRight()
	{
		if(lm.IsMovingRight () && !isUp){
			menuYRotation += 90;
			lm.MoveRight ();
		}
	}

	public void TurnLeft()
	{
		if (lm.IsMovingLeft () && !isUp) {
			menuYRotation -= 90;
			lm.MoveLeft ();
		}
	}
}
