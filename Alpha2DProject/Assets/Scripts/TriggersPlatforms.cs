using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersPlatforms : MonoBehaviour {

    //Separa siempre privates de publics

    private EnemySkeletonController controladorEsqueleto;
    private int contador = 1;

    public GameObject EsqueletoAsociado;
    public bool EsEnemigo;
    
    private void Start()
    {
       controladorEsqueleto = EsqueletoAsociado.GetComponent<EnemySkeletonController>();
    }

    /// <summary>
    /// EXPLICAR CÓDIGO
    /// </summary>
    /// <param name="Colision"></param>
    private void OnTriggerEnter2D(Collider2D Colision)
    {
        if (!EsEnemigo)
        {
            if (Colision.tag == "Player")
            {
                controladorEsqueleto.JugadorEnPlataforma = true;
                controladorEsqueleto.posicionJugador.x = Colision.transform.position.x;
                controladorEsqueleto.posicionJugador.y = Colision.transform.position.y;
            }
        }
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
    /// EXPLICAR CODIGO
    /// </summary>
    /// <param name="Colision"></param>
    private void OnTriggerExit2D(Collider2D Colision)
    {
        if (!EsEnemigo)
        {
            if (Colision.tag == "Player")
            {
                controladorEsqueleto.JugadorEnPlataforma = false;
                controladorEsqueleto.posicionJugador.x = 0;
                controladorEsqueleto.posicionJugador.y = 0;
                controladorEsqueleto.VoltearCaminar = true;
            }
        }
        else
        {
            contador = 1;
        }
    }

    /// <summary>
    /// EXPLICAR CODIGO
    /// </summary>
    /// <param name="Colision"></param>
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
