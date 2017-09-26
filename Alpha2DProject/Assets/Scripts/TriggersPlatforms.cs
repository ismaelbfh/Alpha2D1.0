using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersPlatforms : MonoBehaviour {

    private EnemySkeletonController controladorEsqueleto;
    private int contador = 1;

    public GameObject EsqueletoAsociado;
    public bool EsEnemigo;
    
    private void Start()
    {
       controladorEsqueleto = EsqueletoAsociado.GetComponent<EnemySkeletonController>();
    }

    /// <summary>
    /// Revisa las colisiones que haga la plataforma para de esta manera verificar si esta entrando el jugador y activar la funciòn atacar o si es el esqueleto para
	/// activar la función de voltearse.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D Colision)
    {
		//Si el collider esta configurado para verificar si el player esta en la plataforma y el player entra en el collider activa la función de ataque del enemigo y 
		//le da la posición del player cuando entró al collider.
        if (!EsEnemigo)
        {
            if (Colision.tag == "Player")
            {
                controladorEsqueleto.JugadorEnPlataforma = true;

                //NO PODEMOS ESTABLECER LA X e Y DE UN VALOR CONCRETO, SOLO ESTABLECER LA CONFIGURACION DE ESA VARIABLE,
                //la variable PosicionJugador necesita que establezcan un Vector2 como minimo, para ello lo resumimos en:
                controladorEsqueleto.PosicionJugador = Colision.transform.position;
                //en vez de
                //controladorEsqueleto.PosicionJugador.x = Colision.transform.position.x;
                //controladorEsqueleto.PosicionJugador.y = Colision.transform.position.y;
            }
		}//Si el collider esta activado como verificador de limite de caminar del enemigo y si es el enemigo que entra en el trigger del collider activa la función 
		//de voltearse y le dice la posición del jugador cuando entro al collider.
        else if (EsEnemigo)
        {

            if (Colision.name == EsqueletoAsociado.name)
            {
                controladorEsqueleto.Voltear = true;
                contador = 0;
            }

        }
    }

    /// <summary>
    /// Verifica cuando el jugador sale del collider para poder desactivar la función de ataque del jugador, además le quita la posición dicha anteriormente y
	/// le activa la función de voltearse.
    /// </summary>
    private void OnTriggerExit2D(Collider2D Colision)
    {
        if (!EsEnemigo)
        {
            if (Colision.tag == "Player")
            {
                controladorEsqueleto.JugadorEnPlataforma = false;
                controladorEsqueleto.PosicionJugador = new Vector2(0, 0); //Establece un unico valor, no podemos sacar la x o la y, establece su tipo(en este caso Vector2)
                controladorEsqueleto.VoltearCaminar = true;
            }
        }
        else
        {
            contador = 1;
        }
    }

    /// <summary>
    /// Activa la función de volteo del enemigo por si se queda trabado dentro del collider.
    /// </summary>
    private void OnTriggerStay2D(Collider2D Colision)
    {
        if (EsEnemigo)
        {
            if (Colision.name == EsqueletoAsociado.name && contador == 1)
            {
                controladorEsqueleto.Voltear = true;
                contador = 0;
            }

        }
    }

}
