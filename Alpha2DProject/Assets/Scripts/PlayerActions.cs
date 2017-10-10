using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {

	//Script Controlador del Player y HUD:
	PlayerAndHUDController ControladorJugador;

	void Start(){
		ControladorJugador = GameObject.Find ("HUD").GetComponent<PlayerAndHUDController>();
	}

	public void ActivarAtaquePLayer(){
		ControladorJugador.AtacarPlayer (0, true);
	}
}
