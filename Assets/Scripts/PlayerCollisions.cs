using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour {

	public AudioClip[] coinSound;

	private GameManager gm;
	private PlayerMovement pm;

	void Start()
	{
		pm = GetComponent<PlayerMovement> ();
		gm = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Ground") 
		{									
			pm.SetOnGorund (true);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name == "Coin") 
		{
			GetComponent<AudioSource> ().PlayOneShot (coinSound [Random.Range (0, 7)]);
			Destroy (other.gameObject);
		} 
		else if (other.gameObject.name == "Finish") 
		{
			if (gm.currentLevel <= gm.GetCountLevels()) 
			{
				gm.LoadNextLevel ();
			} 

			if (gm.lastLevelPlayed < gm.GetCountLevels ()) 
			{
				gm.lastLevelPlayed++;
				PlayerPrefs.SetInt ("Level"+ gm.lastLevelPlayed.ToString (), 1);
				gm.SetNextLevel ();
				pm.SetAlignPosition (gm.GetCurrentLevel ());
				gm.LoadArrows ();
				gm.DeleteLastLevel ();
			} else {
				pm.speed = 0;
				Debug.Log ("YOU HAVE FINISHED ALL LEVELS");
			}
		}
	}
}
