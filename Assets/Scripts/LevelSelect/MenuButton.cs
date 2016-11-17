using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {

	public bool toMainMenu;
	public bool toLevelSelect;
	public bool toSettings;

	public Color col;
	public MenuMovement mm;

	private Vector3 origin;
	private Color c;
	private Transform myTransform;
	private bool pressed;

	void Start(){
		SetOriginScale ();
		pressed = false;
		c = GetComponent<Renderer>().material.color;
	}	

	void SetOriginScale (){
		myTransform = GetComponent<Transform> ();
		origin = new Vector3 (myTransform.localScale.x, myTransform.localScale.y, myTransform.localScale.z);

	}

	void OnTouchDown(){
			pressed = true;
			GetComponent<Renderer> ().material.color = col;
			Vector3 target = new Vector3 (myTransform.localScale.x * 1.5f, myTransform.localScale.y * 1.5f, myTransform.localScale.z * 1.5f);
			myTransform.localScale = target;
	}

	void OnTouchUp(){
		if (pressed) {
			GetComponent<Renderer> ().material.color = c;
			myTransform.localScale = origin;
			if (toLevelSelect)
				mm.MoveDown ();
			else if (toMainMenu)
				mm.MoveUp ();
			
		} 
		pressed = false;
	}

	void OnTouchExit(){
		pressed = false;
		myTransform.localScale = origin;
		GetComponent<Renderer> ().material.color = c;
	}

}
//poner flechas para indicar el giro
// intento arregla que no gire el cubo negro o que no se vea
// queda arriba boton play boton y settings
// cuando toca play va a level select
// si apreta settigs va abajo del cubo y muestro sonido, lenguaje, musica
// a settings se va solo de main menu
// level select tiene q tener play level # y un boton menu que va al menu principal
// que aparezca mastered si junto todos los coins