using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [RequireComponent(typeof(BoxCollider2D))]

public class QuitarVidaArma : MonoBehaviour
{
    //Variables
    private float contadorEliminar = 1.5f;

    public float DañoACausar;
	private PlayerAndHUDController ScriptPlayer;
     
	/// <summary>
	/// Busca el objeto HUD y el script que maneja la HUD y al player.
	/// </summary>
     void Awake()
     {
         ScriptPlayer = GameObject.Find("HUD").GetComponent<PlayerAndHUDController>();
     }

	/// <summary>
	/// Cuando su trigger se encuentre con el jugador activará la función de bajar vida del mismo y destruirá el objeto en cuestión.
	/// </summary>
     void OnTriggerEnter2D(Collider2D colision)
     {
		if (colision.tag == "Player")
         {
             ScriptPlayer.BajarVida(DañoACausar);
             Destroy(this.gameObject);
         }
     }

	/// <summary>
	/// Contador que elimina el objeto despúes de un tiempo definido anteriormente.
	/// </summary>
     void Update()
     {
		if (contadorEliminar <= 0)
         {
             Destroy(this.gameObject);
         }
          contadorEliminar -= Time.deltaTime;
     }


}
