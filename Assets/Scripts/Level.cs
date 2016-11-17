using UnityEngine;
using System.Collections;

public class Level{

	public int id;
	public GameObject level;

	private Transform arrows;
	private Transform currentArrow;
	private int index;

	public Level(int id, GameObject level)
	{
		this.level = level;
		level.transform.position = new Vector3 (level.transform.position.x, 0, level.transform.position.z);
		arrows = null;
		currentArrow = null;
		index = 0;
		this.id = id;
	}

	public void LoadArrows()
	{
		foreach (Transform t in level.transform) 
		{
			if (t.name == "Arrows") 
			{
				arrows = t;
			}
		}
	}

	public Transform GetNextArrow()
	{
		if (index < arrows.childCount) 
		{
			currentArrow = arrows.GetChild(index);
			index++;
		}

		return currentArrow;
	}

	public Vector3 GetFinishPosition()
	{
		Vector3 posFinish;

		foreach (Transform t in level.transform) 
		{
			if (t.name == "FinishMaze(Clone)") 
			{
				posFinish = new Vector3 (t.transform.position.x, 0, t.transform.position.z + 2);
				return posFinish;
			}
		}

		return Vector3.zero;
	}

	public Transform GetCurrentArrow()
	{
		return currentArrow;
	}
}
