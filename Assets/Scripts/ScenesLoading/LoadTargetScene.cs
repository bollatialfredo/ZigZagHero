using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Globalization;


public class LoadTargetScene : MonoBehaviour {


	public void LoadGameFromMainMenu()
	{
		PlayerPrefs.SetInt ("ChosenLevelToStart", 1);
		LoadSceneNumWithWaitingScreen(1);
	}

	public void LoadSceneNumWithWaitingScreen(int num)
	{
		correctSceneCheck (num);
		StartCoroutine (LoadSceneWithWait (num));
	}


	public void LoadSceneNum(int num)
	{
		correctSceneCheck (num);
		StartCoroutine (LoadScene (num));
	}


	IEnumerator LoadSceneWithWait(int num)
	{
		yield return new WaitForSeconds (0.4f);
		LoadingScreenManager.LoadScene(num);
	}
	IEnumerator LoadScene(int num)
	{
		yield return new WaitForSeconds (0.17f);
		SceneManager.LoadScene(num);
	}

	public void correctSceneCheck(int num)
	{
		if (num < 0 || num >= SceneManager.sceneCountInBuildSettings) {
			Debug.LogWarning ("Wrong scene number");
			return;
		}
	}
}
