using UnityEngine;
using System.Collections;

public class DeathDetection : MonoBehaviour {

	public Canvas afterDeadMenu;

	private PlayerMovement pm;
	private GameManager gm;
	//private Animator anim;

	void Start()
	{
		//anim = GetComponent <Animator> ();
		pm = GetComponent<PlayerMovement> ();
		gm = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}

	void Update () 
	{
		if (transform.position.y < 0) 
		{
			afterDeadMenu.gameObject.SetActive (true);
			pm.speed = 0;
		}
	}

	void RestartAfterDeath()
	{
		afterDeadMenu.gameObject.SetActive (false);
		pm.speed = 5;
		pm.ResetPlayer ();
		gm.ResetScene ();
	}
}
