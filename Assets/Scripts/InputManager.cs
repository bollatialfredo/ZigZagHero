using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	private GameManager gm;

	void Start ()
    {
		gm = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}

	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Space))
        {
			gm.Tap ();
        }

		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) 
		{
			gm.Tap ();
		}

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Application.Quit ();
		}
    }
}
