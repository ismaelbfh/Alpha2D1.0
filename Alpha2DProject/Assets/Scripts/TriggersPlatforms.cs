using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggersPlatforms : MonoBehaviour {

    public GameObject EsqueletoAsociado;
    EnemySkeletonController controladorEsqueleto;
    public bool EsEnemigo;
    int contador = 1;

    private void Start()
    {
       controladorEsqueleto = EsqueletoAsociado.GetComponent<EnemySkeletonController>();
    }

    void OnTriggerEnter2D(Collider2D Colision)
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
                controladorEsqueleto.voltear = true;
                contador = 0;
            }

        }
    }

    void OnTriggerExit2D(Collider2D Colision)
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

    void OnTriggerStay2D(Collider2D Colision)
    {
        if (EsEnemigo)
        {
            if (Colision.name == EsqueletoAsociado.name && contador == 1)
            {
                controladorEsqueleto.voltear = true;
                contador = 0;
            }

        }
    }

}
