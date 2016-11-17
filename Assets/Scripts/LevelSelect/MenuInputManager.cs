using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuInputManager : MonoBehaviour {

	public LayerMask touchInputMask;
	public Camera camera;

	private RaycastHit hit;
	private List<GameObject> touchList = new List<GameObject> ();
	private GameObject[] touchesOld;


	void Update () {

#if UNITY_EDITOR

		if(Input.GetMouseButton (0) || Input.GetMouseButtonDown (0) || Input.GetMouseButtonUp (0)){


			Ray ray = camera.ScreenPointToRay (Input.mousePosition);

				if(Physics.Raycast (ray,out hit,touchInputMask))
				{
					GameObject recipient = hit.transform.gameObject;
					touchList.Add (recipient);

				if (Input.GetMouseButtonDown (0)) {
						recipient.SendMessage ("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					if (Input.GetMouseButtonUp (0)) {
						recipient.SendMessage ("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					if (Input.GetMouseButton (0)) {
						recipient.SendMessage ("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					
				}

			foreach(GameObject g in touchesOld){
				if(!touchList.Contains (g))
					g.SendMessage ("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
			}
		}

#endif
		touchesOld = new GameObject[touchList.Count];
		touchList.CopyTo (touchesOld);
		touchList.Clear ();

		if(Input.touchCount > 0){

			foreach (Touch t in Input.touches) {
				Ray ray = camera.ScreenPointToRay (t.position);

				if(Physics.Raycast (ray,out hit,touchInputMask)){
					GameObject recipient = hit.transform.gameObject;
					touchList.Add (recipient);

					if (t.phase == TouchPhase.Began) {
						recipient.SendMessage ("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					if (t.phase == TouchPhase.Ended) {
						recipient.SendMessage ("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					if (t.phase == TouchPhase.Stationary) {
						recipient.SendMessage ("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					if (t.phase == TouchPhase.Canceled) {
						recipient.SendMessage ("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
					}
				}
			}
			foreach(GameObject g in touchesOld){
				if(!touchList.Contains (g))
					g.SendMessage ("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}