using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOfPlayerButton : MonoBehaviour {

	//Variables de Identificación de Ataques:
	public enum TipoDeAtaque {LluviaDeCentellas,MaldicionDelRayo};
	public TipoDeAtaque AtaqueARealizar = TipoDeAtaque.LluviaDeCentellas;
	//Variables comúnes:
	PlayerAndHUDController ControladorJugador;

	void Start(){
		ControladorJugador = GameObject.Find("HUD").GetComponent<PlayerAndHUDController>();
	}

	//Evento activado mediante un "EventTrigger" en el respectivo boton que activa la animación de recarga y hace la función de ataque.
	public void Pulsado(){
		//Activar Animación de Recarga.
		//Activar Método de Ataque del Player según cada ataque
		switch (AtaqueARealizar) {
		case TipoDeAtaque.LluviaDeCentellas:
			ControladorJugador.AtacarPlayer (1, false);
			break;
		default:
			Debug.Log ("No hay ataque seleccionado");
			break;
		}
	}

}
