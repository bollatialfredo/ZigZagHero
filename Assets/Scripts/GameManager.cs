using UnityEngine;
using System.Collections;
using System.IO;

public class GameManager : MonoBehaviour 
{
	public int countTotalLevels;
	public int currentLevel;
	public GameObject player;
	public GameObject levels;
	public float fadeSpeed;

	private int countLevels;

	private int levelIndex;
	private Level[] arrayLevels;
	private Vector3 posNextLevel;
	public int lastLevelPlayed;

	private PlayerMovement pm;

	private Transform currentArrow;

	void Start () 
	{
		pm = player.GetComponent<PlayerMovement> ();

		// Es util tener una variable para la cantidad total de niveles porque hay que consultarla despues de cada restart de escena.
		countTotalLevels = GetCountLevels();

		// lastLevelPlayed aumenta solo cuando el jugador pasa la linea de llegada. Asi en esta variable queda guardado el ultimo nivel alcanzado.
		lastLevelPlayed = PlayerPrefs.GetInt ("ChosenLevelToStart");


		// Se inicia la escena.
		ResetScene ();
	}

	public void ResetScene()
	{
		// Se actualiza la cantidad de niveles que va a tener el arreglo.
		countLevels = countTotalLevels - lastLevelPlayed;

		if (countLevels == 1) { // Cuando perdemos en el anteultimo nivel necesitamos que el arreglo sea de dos.
			countLevels = 2;
		}
		else if (countLevels > 3) { // La mayoria de las veces nos dara mayor que 3 pero solo queremos 3 niveles cargados a la vez.
			countLevels = 3;
		} else if (countLevels == 0) // Si nos da 0 es porque estamos en el ultimo nivel asi que el arreglo seria de 1.
			countLevels = 1;

		// Indice de la posicion actual del arreglo de niveles arrayLevels[].
		levelIndex = 0;

		// Se actualiza currentLevel al ultimo nivel alcanzado.
		currentLevel = lastLevelPlayed;
		//currentLevel = PlayerPrefs.GetInt ("ChosenLevelToStart");

		// Se borran todos los niveles 
		DeleteAllLevels ();

		// Se cargan los niveles.
		for (int i = 0; i < countLevels; i++) 
		{
			LoadNextLevel ();
			SetNextLevel ();
		}

		// Se actualiza la posicion del Player sobre el comienzo del ultimo nivel alcanzado.
		SetPlayerPosition ();

		// Se carga el arreglo de flechas correspondientes al siguiente nivel a jugar.
		levelIndex = 0;
		LoadArrows ();

	}

	private void SetPlayerPosition()
	{
		foreach (Transform t in arrayLevels[0].level.transform) 
		{
			if (t.name == "StartMaze(Clone)") 
			{
				Vector3 posPlayer = new Vector3 (t.transform.position.x, t.transform.position.y + .1f, t.transform.position.z);
				pm.transform.position = posPlayer;
				break;
			}
		}
	}

	public void Tap()
	{
		if (currentArrow.name == "Left(Clone)")
			pm.TurnLeft ();
		else if (currentArrow.name == "Right(Clone)")
			pm.TurnRight ();
		else if (currentArrow.name == "Jump(Clone)") {
			pm.Jump ();
		}
			
		currentArrow.GetChild (1).gameObject.SetActive (false);
		NextArrow ();
	}
		
	public void LoadNextLevel()
	{
		Object obj = Resources.Load<GameObject>("Level" + currentLevel.ToString());
		GameObject level = Instantiate(obj,new Vector3 (posNextLevel.x, posNextLevel.y, posNextLevel.z), Quaternion.identity) as GameObject;
		level.transform.parent = levels.transform;

		arrayLevels [levelIndex] = new Level (currentLevel,level);
		posNextLevel = arrayLevels [levelIndex].GetFinishPosition ();
		currentLevel++;
	}

	public void SetNextLevel()
	{
		levelIndex++;
		if (levelIndex == countLevels)
			levelIndex = 0;
	}

	public void LoadArrows()
	{
		arrayLevels [levelIndex].LoadArrows ();
		NextArrow ();
	}

	public void NextArrow()
	{
		currentArrow = arrayLevels [levelIndex].GetNextArrow ();
		StartCoroutine (FadeIn ());
		currentArrow.GetChild (0).gameObject.SetActive (false);
	}

	public void DeleteLastLevel()
	{
		int index = lastLevelPlayed - 2;

		GameObject go = GameObject.Find ("Level" + index.ToString () + "(Clone)");
		if (go != null)
			Destroy (go.gameObject);
	}

	public void DeleteAllLevels()
	{
		foreach (Transform t in levels.transform) 
		{
			Destroy (t.gameObject);
		}

		// Se instancia el arreglo donde se almacenan los 3 niveles cargados.
		arrayLevels = new Level[countLevels];
		posNextLevel = new Vector3 (0, 0, 0);
	}

	public GameObject GetCurrentLevel()
	{
		return arrayLevels [levelIndex].level;
	}

	public int GetCountLevels()
	{
		return countTotalLevels;
	}

	IEnumerator FadeIn()
	{
		Color c = currentArrow.GetChild(1).GetComponent<SpriteRenderer>().color;

		while (c.a < 255f) 
		{
			c.a = c.a + fadeSpeed;
			currentArrow.GetChild (1).GetComponent<SpriteRenderer> ().color = c;
			yield return null;
		}
	}
		
}
